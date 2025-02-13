using UnityEngine;
using System;

public static class PauseManager
{
    public static Action PauseGameAction;

    public static void PauseGame()
    {
        PauseGameAction?.Invoke();
    }
}
