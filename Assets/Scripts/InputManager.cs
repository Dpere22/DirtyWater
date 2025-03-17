using UnityEngine;
using Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private void OnPause()
    {
        GameEventsManager.Instance.InputEvents.PausePressed();
    }
    public void OnMove(InputValue value)
    {
        GameEventsManager.Instance.InputEvents.MovePressed(value.Get<Vector2>());
    }
}
