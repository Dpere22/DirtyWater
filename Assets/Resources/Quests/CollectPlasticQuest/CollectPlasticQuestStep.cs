using QuestSystem;

namespace Resources.Quests.CollectPlasticQuest
{
    public class CollectPlasticQuestStep : QuestStep
    {
        private int _plasticCollected;
        protected override void SetQuestStepState(string state)
        {
            throw new System.NotImplementedException();
        }

        private void Update()
        {
            _plasticCollected = PlayerManager.inventory["Plastic"];
            if (_plasticCollected >= 1)
            {
                FinishQuestStep();
            }
        }
    }
}
