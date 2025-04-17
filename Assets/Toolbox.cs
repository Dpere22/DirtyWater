using Events;
using Input;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    private bool _inRange;
    private bool _questActive;

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.OnSubmitPressed += CollectToolbox;
        GameEventsManager.Instance.QuestEvents.OnActivateToolbox += ActivateToolbox;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.OnSubmitPressed -= CollectToolbox;
        GameEventsManager.Instance.QuestEvents.OnActivateToolbox -= ActivateToolbox;
    }

    private void ActivateToolbox()
    {
        _questActive = true;
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
        if (context != InputEventContext.Default) return;
        if (!_inRange) return;
        if (!_questActive)
        {
            GameEventsManager.Instance.DialogueEvents.EnterDialogue("player_cannot_collect_toolbox");
            return;
        }
        GameEventsManager.Instance.SoundEvents.PlaySoundEffect("bubble_pop");
        GameEventsManager.Instance.QuestEvents.GetToolbox();
        GameEventsManager.Instance.DialogueEvents.EnterDialogue("player_collect_toolbox");
        Destroy(gameObject);
    }
}
