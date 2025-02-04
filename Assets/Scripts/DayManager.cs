public static class DayManager
{
    public static float DayDuration = 10f;
    private static float _currentTime;
    public static int DayCount = 1;

    public static void Initialize()
    {
        _currentTime = DayDuration;
    }

    public static void UpdateDayCycle(float deltaTime)
    {
        _currentTime -= deltaTime;

        if (_currentTime <= 0)
        {
            EndDay();
        }
    }

    private static void EndDay()
    {
        DayCount++;
        _currentTime = DayDuration;
    }

    public static float GetRemainingTime() => _currentTime;
}
