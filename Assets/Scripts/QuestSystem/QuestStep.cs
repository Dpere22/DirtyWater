using Events;
using UnityEngine;

namespace QuestSystem
{
    public abstract class QuestStep : MonoBehaviour
    {
        private bool _isFinished;
        private string _questId;
        private int _stepIndex;

        public void InitializeQuestStep(string questId)
        {
            _questId = questId;
        }

        protected void FinishQuestStep()
        {
            if (!_isFinished)
            {
                _isFinished = true;
                GameEventsManager.Instance.QuestEvents.AdvanceQuest(_questId);
                Destroy(gameObject);
            }
        }
    }
}