using System;

public class DayEvents
{
    public event Action OnDayEnd;

    public void DayEnd()
    {
        OnDayEnd?.Invoke();
    }
}
