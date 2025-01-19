using UnityEngine;

public class EnterWater : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        playerMovement.StartSwimming();
    }
}
