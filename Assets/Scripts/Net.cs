using UnityEngine;

public class Net : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerManager.currentWeight = 0;
    }
}
