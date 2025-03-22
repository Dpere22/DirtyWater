using UnityEngine;

public class EnterWater : MonoBehaviour
{
    [SerializeField] private GameObject interactIcon;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        interactIcon.SetActive(true);
        PlayerMovement playerMovement = other.gameObject.GetComponentInParent<PlayerMovement>();
        playerMovement.canJump = true;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        interactIcon.SetActive(false);
        PlayerMovement playerMovement = other.gameObject.GetComponentInParent<PlayerMovement>();
        playerMovement.canJump = false;
    }
    
}
