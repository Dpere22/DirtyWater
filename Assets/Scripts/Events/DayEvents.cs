using System;

public class DayEvents
{
    public event Action OnDayEnd;

    public void DayEnd()
    {
        OnDayEnd?.Invoke();
    }

    public event Action OnDayStart;
    public void DayStart()
    {
        OnDayStart?.Invoke();
    }
}
