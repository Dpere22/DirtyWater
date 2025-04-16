using Events;
using Ink.Runtime;

namespace Dialog
{
    public class InkExternalFunctions
    {
        public void Bind(Story story)
        {
            story.BindExternalFunction("StartQuest", (string questId) => StartQuest(questId));
            story.BindExternalFunction("AdvanceQuest", (string questId) => AdvanceQuest(questId));
            story.BindExternalFunction("FinishQuest", (string questId) => FinishQuest(questId));
            story.BindExternalFunction("EnableQuest", (string questId) => EnableQuest(questId));
        }

        public void Unbind(Story story)
        {
            story.UnbindExternalFunction("StartQuest");
            story.UnbindExternalFunction("AdvanceQuest");
            story.UnbindExternalFunction("FinishQuest");
            story.UnbindExternalFunction("EnableQuest");
        }

        private void StartQuest(string questId) 
        {
            GameEventsManager.Instance.QuestEvents.StartQuest(questId);
        }

        private void AdvanceQuest(string questId) 
        {
            GameEventsManager.Instance.QuestEvents.AdvanceQuest(questId);
        }

        private void FinishQuest(string questId)
        {
            GameEventsManager.Instance.QuestEvents.FinishQuest(questId);
        }

        private void EnableQuest(string questId)
        {
            GameEventsManager.Instance.QuestEvents.EnableQuest(questId);
        }
    }
}