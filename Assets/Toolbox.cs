using Events;
using Input;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    private bool _inRange;

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.OnSubmitPressed += CollectToolbox;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.OnSubmitPressed -= CollectToolbox;
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

    private void CollectToolbox(InputEventContext context)
    {
        if (!_inRange) return;
        GameEventsManager.Instance.QuestEvents.GetToolbox();
        GameEventsManager.Instance.DialogueEvents.EnterDialogue("player_collect_toolbox");
        Destroy(gameObject);
    }
}
