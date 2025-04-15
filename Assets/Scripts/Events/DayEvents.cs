using System;

public class DayEvents
{
    public bool IsTransitioning {get; private set;}
    public event Action OnDayEnd;

    public void DayEnd()
    {
        IsTransitioning = true;
        OnDayEnd?.Invoke();
    }

    public event Action OnDayStart;
    public void DayStart()
    {
        IsTransitioning = false;
        OnDayStart?.Invoke();
    }

    public event Action OnEnterWater;

    public void EnterWater()
    {
        OnEnterWater?.Invoke();
    }

    public event Action OnJumpIntoWater;

    public void JumpIntoWater()
    {
        OnJumpIntoWater?.Invoke();
    }

    public event Action OnStartDayTimer;

    public void StartDayTimer()
    {
        OnStartDayTimer?.Invoke();
    }

    public event Action OnRespawnPlayer;

    public void RespawnPlayer()
    {
        OnRespawnPlayer?.Invoke();
    }
}
