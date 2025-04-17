using Events;
using UnityEngine;

namespace Resources.Quests.GetToolboxQuest
{
    public class GetToolboxQuestFinish : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GameEventsManager.Instance.ShopEvents.EnableShop();
            Destroy(gameObject);
        }
    }
}
