using Events;
using QuestSystem;

namespace Resources.Quests.CollectPlasticQuest
{
    public class CollectPlasticQuestStep : QuestStep
    {
        private int _plasticCollected;

        private void Update()
        {
            _plasticCollected = GameEventsManager.Instance.PlayerManager.TotalPlastic;
            if (_plasticCollected >= 1)
            {
                FinishQuestStep();
            }
        }
    }
}
