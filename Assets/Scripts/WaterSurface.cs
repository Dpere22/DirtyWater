using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    [SerializeField] private Water water;
    [SerializeField] private BoxCollider2D waterSurfaceCollider;

    private void Start()
    {
        water.PlayerEnteredWater += PlayerEnteredWater;
    }
    
    //Deprecated Trigger code, might use later
    
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (!other.CompareTag("Player")) return;
    //     
    //     PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
    //     playerMovement.atWaterSurface = true;
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (!other.CompareTag("Player")) return;
    //     
    //     PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
    //     playerMovement.atWaterSurface = false;
    // }

    private void PlayerEnteredWater()
    {
        waterSurfaceCollider.enabled = true;
    }
}
