using Events;
using Input;
using UnityEngine;

public class Net : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    private bool _inRange;

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.OnSubmitPressed += DropOffTrash;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.OnSubmitPressed -= DropOffTrash;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        icon.SetActive(true);
        _inRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        icon.SetActive(false);
        _inRange = false;
    }

    void DropOffTrash(InputEventContext context)
    {
        if (!_inRange) return;
        GameEventsManager.Instance.SoundEvents.PlaySoundEffect("drop_off");
        GameEventsManager.Instance.PlayerManager.DroppedOffTrash();
    }
}
