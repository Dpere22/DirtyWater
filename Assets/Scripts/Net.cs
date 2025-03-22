using Events;
using UnityEngine;

public class Net : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameEventsManager.Instance.PlayerManager.CurrentWeight = 0;
    }
}
