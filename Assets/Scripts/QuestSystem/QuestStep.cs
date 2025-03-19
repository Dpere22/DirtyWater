using Events;
using UnityEngine;

namespace QuestSystem
{
    public abstract class QuestStep : MonoBehaviour
    {
        private bool _isFinished;
        private string _questId;
        private int _stepIndex;

        public void InitializeQuestStep(string questId, int stepIndex, string questStepState)
        {
            _questId = questId;
            _stepIndex = stepIndex;
            if (!string.IsNullOrEmpty(questStepState))
            {
                SetQuestStepState(questStepState);
            }
        }

        protected void FinishQuestStep()
        {
            if (!_isFinished)
            {
                _isFinished = true;
                GameEventsManager.Instance.QuestEvents.AdvanceQuest(_questId);
                Debug.Log("Quest Finished!");
                Destroy(gameObject);
            }
        }

        protected void ChangeState(string newState/*, string newStatus*/)
        {
            GameEventsManager.Instance.QuestEvents.QuestStepStateChange(
                _questId, 
                _stepIndex, 
                new QuestStepState(newState/*, newStatus*/)
            );
        }

        protected abstract void SetQuestStepState(string state);
    }
}