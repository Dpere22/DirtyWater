using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        playerMovement.atWaterSurface = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        playerMovement.atWaterSurface = false;
    }
}
