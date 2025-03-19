using QuestSystem;
using UnityEngine;

namespace Resources.Quests.CollectPlasticQuest
{
    public class CollectPlasticQuestStep : QuestStep
    {
        private int _plasticCollected;
        protected override void SetQuestStepState(string state)
        {
            Debug.Log("Setting the quest step state");
        }

        private void Update()
        {
            if (_plasticCollected >= PlayerManager.inventory["Plastic"])
                UpdateState();
            _plasticCollected = PlayerManager.inventory["Plastic"];
            if (_plasticCollected >= 1)
            {
                FinishQuestStep();
            }
        }

        private void UpdateState()
        {
            string state = PlayerManager.inventory["Plastic"].ToString();
            ChangeState(state);
        }
    }
}
