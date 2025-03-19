using System.Collections.Generic;
using Events;
using UnityEngine;

namespace QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private bool loadQuestState = true;

        private Dictionary<string, Quest> _questMap;

        // quest start requirements
        private int _currentPlayerLevel;

        private void Awake()
        {
            _questMap = CreateQuestMap();
        }

        private void OnEnable()
        {
            GameEventsManager.Instance.QuestEvents.OnStartQuest += StartQuest;
            GameEventsManager.Instance.QuestEvents.OnAdvanceQuest += AdvanceQuest;
            GameEventsManager.Instance.QuestEvents.OnFinishQuest += FinishQuest;

            GameEventsManager.Instance.QuestEvents.OnQuestStepStateChange += QuestStepStateChange;

            //GameEventsManager.instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.QuestEvents.OnStartQuest -= StartQuest;
            GameEventsManager.Instance.QuestEvents.OnAdvanceQuest -= AdvanceQuest;
            GameEventsManager.Instance.QuestEvents.OnFinishQuest -= FinishQuest;

            GameEventsManager.Instance.QuestEvents.OnQuestStepStateChange -= QuestStepStateChange;

            //GameEventsManager.instance.playerEvents.onPlayerLevelChange -= PlayerLevelChange;
        }

        private void Start()
        {
        
            foreach (Quest quest in _questMap.Values)
            {
                // initialize any loaded quest steps
                if (quest.State == QuestState.IN_PROGRESS)
                {
                    quest.InstantiateCurrentQuestStep(this.transform);
                }
                // broadcast the initial state of all quests on startup
                GameEventsManager.Instance.QuestEvents.QuestStateChange(quest);
            }
        }

        private void ChangeQuestState(string id, QuestState state)
        {
            Quest quest = GetQuestById(id);
            quest.State = state;
            GameEventsManager.Instance.QuestEvents.QuestStateChange(quest);
        }

        private void PlayerLevelChange(int level)
        {
            _currentPlayerLevel = level;
        }

        private bool CheckRequirementsMet(Quest quest)
        {
            // start true and prove to be false
            bool meetsRequirements = true;

            // check player level requirements
            if (_currentPlayerLevel < quest.Info.levelRequirement)
            {
                meetsRequirements = false;
            }

            // check quest prerequisites for completion
            foreach (QuestInfoSO prerequisiteQuestInfo in quest.Info.questPrerequisites)
            {
                if (GetQuestById(prerequisiteQuestInfo.ID).State != QuestState.FINISHED)
                {
                    meetsRequirements = false;
                }
            }

            return meetsRequirements;
        }

        private void Update()
        {
            // loop through ALL quests
            foreach (Quest quest in _questMap.Values)
            {
                // if we're now meeting the requirements, switch over to the CAN_START state
                if (quest.State == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
                {
                    ChangeQuestState(quest.Info.ID, QuestState.CAN_START);
                }
            }
        }

        private void StartQuest(string id) 
        {
            Quest quest = GetQuestById(id);
            quest.InstantiateCurrentQuestStep(this.transform);
            ChangeQuestState(quest.Info.ID, QuestState.IN_PROGRESS);
        }

        private void AdvanceQuest(string id)
        {
            Quest quest = GetQuestById(id);

            // move on to the next step
            quest.MoveToNextStep();

            // if there are more steps, instantiate the next one
            if (quest.CurrentStepExists())
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            // if there are no more steps, then we've finished all of them for this quest
            else
            {
                ChangeQuestState(quest.Info.ID, QuestState.CAN_FINISH);
            }
        }

        private void FinishQuest(string id)
        {
            Quest quest = GetQuestById(id);
            ClaimRewards(quest);
            ChangeQuestState(quest.Info.ID, QuestState.FINISHED);
        }

        private void ClaimRewards(Quest quest)
        {
            //GameEventsManager.instance.goldEvents.GoldGained(quest.info.goldReward);
            //GameEventsManager.instance.playerEvents.ExperienceGained(quest.info.experienceReward);
        }

        private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
        {
            Quest quest = GetQuestById(id);
            quest.StoreQuestStepState(questStepState, stepIndex);
            ChangeQuestState(id, quest.State);
        }

        private Dictionary<string, Quest> CreateQuestMap()
        {
            // loads all QuestInfoSO Scriptable Objects under the Assets/Resources/Quests folder
            QuestInfoSO[] allQuests = UnityEngine.Resources.LoadAll<QuestInfoSO>("Quests");
            // Create the quest map
            Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
            foreach (QuestInfoSO questInfo in allQuests)
            {
                if (idToQuestMap.ContainsKey(questInfo.ID))
                {
                    Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.ID);
                }
                idToQuestMap.Add(questInfo.ID, LoadQuest(questInfo));
            }
            return idToQuestMap;
        }

        private Quest GetQuestById(string id)
        {
            Quest quest = _questMap[id];
            if (quest == null)
            {
                Debug.LogError("ID not found in the Quest Map: " + id);
            }
            return quest;
        }

        private void OnApplicationQuit()
        {
            foreach (Quest quest in _questMap.Values)
            {
                SaveQuest(quest);
            }
        }

        private void SaveQuest(Quest quest)
        {
            try 
            {
                QuestData questData = quest.GetQuestData();
                // serialize using JsonUtility, but use whatever you want here (like JSON.NET)
                string serializedData = JsonUtility.ToJson(questData);
                // saving to PlayerPrefs is just a quick example for this tutorial video,
                // you probably don't want to save this info there long-term.
                // instead, use an actual Save & Load system and write to a file, the cloud, etc..
                PlayerPrefs.SetString(quest.Info.ID, serializedData);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to save quest with id " + quest.Info.ID + ": " + e);
            }
        }

        private Quest LoadQuest(QuestInfoSO questInfo)
        {
            Quest quest = null;
            try 
            {
                // load quest from saved data
                if (PlayerPrefs.HasKey(questInfo.ID) && loadQuestState)
                {
                    string serializedData = PlayerPrefs.GetString(questInfo.ID);
                    QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                    quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
                }
                // otherwise, initialize a new quest
                else 
                {
                    quest = new Quest(questInfo);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load quest with id " + quest?.Info.ID + ": " + e);
            }
            return quest;
        }
    }
}