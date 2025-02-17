using UnityEngine;

public class EnterWater : MonoBehaviour
{
    [SerializeField] private GameObject jumpUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        jumpUI.SetActive(true);
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        playerMovement.canJump = true;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        jumpUI.SetActive(false);
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        playerMovement.canJump = false;
    }
    
}
