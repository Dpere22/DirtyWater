using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Events;

public class Input : MonoBehaviour
{
    private void OnPause()
    {
        GameEventsManager.Instance.InputEvents.PausePressed();
    }
}
