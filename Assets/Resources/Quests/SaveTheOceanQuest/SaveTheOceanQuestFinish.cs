using Events;
using UnityEngine;

namespace Resources.Quests.SaveTheOceanQuest
{
    public class SaveTheOceanQuestFinish : MonoBehaviour
    {
        public void Start()
        {
            GameEventsManager.Instance.QuestEvents.FinishGame();
            Destroy(gameObject);
        }
    }
}
