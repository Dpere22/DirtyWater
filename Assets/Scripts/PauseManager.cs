using System;

public static class PauseManager
{
    public static bool GamePaused;

    public static void PauseGame()
    {
        GamePaused = true;
    }

    public static void ResumeGame()
    {
        GamePaused = false;
    }
}
