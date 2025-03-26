using Events;
using Interactables;
using UnityEngine;

namespace Resources.Quests.CollectPlasticQuest
{
    public class CollectPlasticQuestFinish : MonoBehaviour
    {
        [SerializeField] private GarbageInfoSO info;
        public void Start()
        {
            GameEventsManager.Instance.ShopEvents.EnableShop();
            GameEventsManager.Instance.TrashManager.EnableCollect(info.garbageId);
            Destroy(gameObject);
        }
    
    }
}
