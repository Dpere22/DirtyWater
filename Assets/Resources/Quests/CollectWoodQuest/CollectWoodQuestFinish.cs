using Events;
using UnityEngine;

public class CollectWoodQuestFinish : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameEventsManager.Instance.QuestEvents.UpgradeShop();
        Destroy(gameObject);
    }
}
