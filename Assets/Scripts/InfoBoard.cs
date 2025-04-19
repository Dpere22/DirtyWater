using Events;
using Input;
using UnityEngine;

public class InfoBoard : MonoBehaviour
{
    private bool _inRange;
    private bool _inInfoBoard;

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.OnSubmitPressed += EnableInfoBoard;
        GameEventsManager.Instance.InputEvents.OnCancelPressed += DisableInfoBoard;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.OnSubmitPressed -= EnableInfoBoard;
        GameEventsManager.Instance.InputEvents.OnCancelPressed -= DisableInfoBoard;
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

    private void EnableInfoBoard(InputEventContext context)
    {
        if (context != InputEventContext.Default || !_inRange || _inInfoBoard) return;
        _inInfoBoard = true;
        GameEventsManager.Instance.InputEvents.InfoBoardOpened();
        GameEventsManager.Instance.InputEvents.ChangeInputEventContext(InputEventContext.Shop);
        GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();
    }

    private void DisableInfoBoard()
    {
        if (!_inInfoBoard) return;
        GameEventsManager.Instance.InputEvents.InfoBoardClosed();
        GameEventsManager.Instance.InputEvents.ChangeInputEventContext(InputEventContext.Default);
        GameEventsManager.Instance.PlayerEvents.EnablePlayerMovement();
        _inInfoBoard = false;
    }
}
