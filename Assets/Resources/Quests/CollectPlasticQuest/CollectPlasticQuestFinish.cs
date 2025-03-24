using Events;
using UnityEngine;

namespace Resources.Quests.CollectPlasticQuest
{
    public class CollectPlasticQuestFinish : MonoBehaviour
    {
        public void Start()
        {
            GameEventsManager.Instance.ShopEvents.EnableShop();
            Destroy(gameObject);
        }
    
    }
}
