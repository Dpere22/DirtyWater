using Events;
using QuestSystem;

namespace Resources.Quests.ChestQuest
{
    public class ChestQuestStep : QuestStep
    {
        private void OnEnable()
        {
            GameEventsManager.Instance.QuestEvents.OnGetChest += HandleGetChest;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.QuestEvents.OnGetChest -= HandleGetChest;
        }

        private void Start()
        {
            GameEventsManager.Instance.QuestEvents.ActivateChest();
        }

        private void HandleGetChest()
        {
            FinishQuestStep();
        }
    }
}
