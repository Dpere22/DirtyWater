using QuestSystem;
using UnityEngine;

namespace Resources.Quests.CollectPlasticQuest
{
    public class CollectPlasticQuestStep : QuestStep
    {
        private int _plasticCollected;

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
