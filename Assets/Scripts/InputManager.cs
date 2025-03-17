using UnityEngine;
using Events;

public class InputManager : MonoBehaviour
{
    private void OnPause()
    {
        GameEventsManager.Instance.InputEvents.PausePressed();
    }
}
