using Events;
using UnityEngine;

public class Icon : MonoBehaviour
{
    // New script, so may not be used for every icon

    [SerializeField] private GameObject icon;
    [SerializeField] private Sprite controllerIcon;
    [SerializeField] private Sprite keyboardIcon;
    [SerializeField] private SpriteRenderer sr;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        sr.sprite = ControllerInfo.ControllerName.Equals("Keyboard&Mouse") ? keyboardIcon : controllerIcon;
        icon.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        icon.SetActive(false);
    }
}
