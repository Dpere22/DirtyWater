using System;

public static class PauseManager
{
    public static Action PauseGameAction;
    public static Action ResumeGameAction;
    
    public static bool gamePaused;

    public static void PauseGame()
    {
        gamePaused = true;
        PauseGameAction?.Invoke();
    }

    public static void ResumeGame()
    {
        gamePaused = false;
        ResumeGameAction?.Invoke();
    }
}
