using QuestSystem;

namespace Resources.Quests.SaveTheOceanQuest
{
    public class SaveTheOceanQuestStep : QuestStep
    {
        private void Update()
        {
            if (OceanHealth.GetCurrentHealth() >= 400)
            {
                FinishQuestStep();
            }
        }
    }
}
