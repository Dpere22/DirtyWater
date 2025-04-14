using Events;
using QuestSystem;

namespace Resources.Quests.GetToolboxQuest
{
    public class GetToolboxQuestStep : QuestStep
    {
        private void OnEnable()
        {
            GameEventsManager.Instance.QuestEvents.OnGetToolbox += FinishQuest;
        }
        
        private void OnDisable()
        {
            GameEventsManager.Instance.QuestEvents.OnGetToolbox -= FinishQuest;
        }
        private void FinishQuest()
        {
            FinishQuestStep();
        }
    }
}
