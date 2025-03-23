using System;

public class PauseEvents
{
    public event Action OnPause;
    public event Action OnResume;

    public bool IsPaused;

    public void Pause()
    {
        IsPaused = true;
        OnPause?.Invoke();
    }

    public void Resume()
    {
        IsPaused = false;
        OnResume?.Invoke();
    }
}
