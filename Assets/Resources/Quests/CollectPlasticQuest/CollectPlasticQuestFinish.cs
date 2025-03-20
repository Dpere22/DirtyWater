using Events;
using UnityEngine;

namespace Resources.Quests.CollectPlasticQuest
{
    public class CollectPlasticQuestFinish : MonoBehaviour
    {
        public void Start()
        {
            Debug.Log("Finishing the Quest!");
            GameEventsManager.Instance.ShopEvents.EnableShop();
            Destroy(gameObject);
        }
    
    }
}
