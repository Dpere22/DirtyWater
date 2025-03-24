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
            Info = questInfo;
            State = QuestState.REQUIREMENTS_NOT_MET;
            _currentQuestStepIndex = 0;
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

        public void FinishQuest()
        {
            GameObject finishQuestPrefab = Info.rewardPrefab;
            Object.Instantiate(finishQuestPrefab);
        }
    }
}