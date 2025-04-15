using Events;
using QuestSystem;
using UnityEngine;

namespace Resources.Quests.CollectWoodQuest
{
    public class CollectWoodQuestStep : QuestStep
    {
        private int _woodCollected;

        private void Update()
        {
            _woodCollected = GameEventsManager.Instance.PlayerManager.TotalWood;
            if (_woodCollected >= 1)
            {
                FinishQuestStep();
            }
        }
    }
}
