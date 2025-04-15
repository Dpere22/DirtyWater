using Events;
using Input;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool _inRange;

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.OnSubmitPressed += HandleSubmit;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.OnSubmitPressed -= HandleSubmit;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        _inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        _inRange = false;
    }

    private void HandleSubmit(InputEventContext context)
    {
        if (!_inRange) return;
        GameEventsManager.Instance.SoundEvents.PlaySoundEffect("key");
        GameEventsManager.Instance.DialogueEvents.EnterDialogue("player_collect_chest");
        GameEventsManager.Instance.QuestEvents.GetChest();
        Destroy(gameObject);
    }
}
