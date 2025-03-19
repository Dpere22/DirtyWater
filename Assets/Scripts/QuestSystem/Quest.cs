using UnityEngine;

namespace QuestSystem
{
    public class Quest
    {
        // static info
        public QuestInfoSO Info;

        // state info
        public QuestState State;
        private int _currentQuestStepIndex;

        public Quest(QuestInfoSO questInfo)
        {
            this.Info = questInfo;
            this.State = QuestState.REQUIREMENTS_NOT_MET;
            this._currentQuestStepIndex = 0;
        }

        public void MoveToNextStep()
        {
            _currentQuestStepIndex++;
        }

        public bool CurrentStepExists()
        {
            return (_currentQuestStepIndex < Info.questStepPrefabs.Length);
        }

        public void InstantiateCurrentQuestStep(Transform parentTransform)
        {
            GameObject questStepPrefab = GetCurrentQuestStepPrefab();
            if (questStepPrefab != null)
            {
                QuestStep questStep = Object.Instantiate(questStepPrefab, parentTransform)
                    .GetComponent<QuestStep>();
                questStep.InitializeQuestStep(Info.ID);
            }
        }

        private GameObject GetCurrentQuestStepPrefab()
        {
            GameObject questStepPrefab = null;
            if (CurrentStepExists())
            {
                questStepPrefab = Info.questStepPrefabs[_currentQuestStepIndex];
            }
            else 
            {
                Debug.LogWarning("Tried to get quest step prefab, but stepIndex was out of range indicating that "
                                 + "there's no current step: QuestId=" + Info.ID + ", stepIndex=" + _currentQuestStepIndex);
            }
            return questStepPrefab;
        }
        

        // public string GetFullStatusText()
        // {
        //     string fullStatus = "";
        //
        //     if (State == QuestState.REQUIREMENTS_NOT_MET)
        //     {
        //         fullStatus = "Requirements are not yet met to start this quest.";
        //     }
        //     else if (State == QuestState.CAN_START)
        //     {
        //         fullStatus = "This quest can be started!";
        //     }
        //     else 
        //     {
        //         // display all previous quests with strikethroughs
        //         for (int i = 0; i < _currentQuestStepIndex; i++)
        //         {
        //             fullStatus += "<s>" + _questStepStates[i].status + "</s>\n";
        //         }
        //         // display the current step, if it exists
        //         if (CurrentStepExists())
        //         {
        //             fullStatus += _questStepStates[_currentQuestStepIndex].status;
        //         }
        //         // when the quest is completed or turned in
        //         if (State == QuestState.CAN_FINISH)
        //         {
        //             fullStatus += "The quest is ready to be turned in.";
        //         }
        //         else if (State == QuestState.FINISHED)
        //         {
        //             fullStatus += "The quest has been completed!";
        //         }
        //     }
        //
        //     return fullStatus;
        // }
    }
}