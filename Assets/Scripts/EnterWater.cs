using Events;
using UnityEngine;

public class EnterWater : MonoBehaviour
{
    [SerializeField] private GameObject interactIcon;
    private bool _inRange = false;

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.JumpAction += Dive;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.JumpAction -= Dive;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        interactIcon.SetActive(true);
        _inRange = true;

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        interactIcon.SetActive(false);
        _inRange = false;
    }

    private void Dive()
    {
        if (_inRange)
        {
            GameEventsManager.Instance.DayEvents.JumpIntoWater();
        }
    }
    
}
