using UnityEngine;

public class Icon : MonoBehaviour
{
    // New script, so may not be used for every icon

    [SerializeField] private GameObject icon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        icon.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        icon.SetActive(false);
    }
}
