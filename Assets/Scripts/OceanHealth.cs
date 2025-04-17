public static class OceanHealth
{
    private const int MaxHealth = 300;
    private static int _currentHealth;

    public static int GetHealthRatio()
    {
        return _currentHealth / MaxHealth;
    }

    public static void AddHealth(int amount)
    {
        _currentHealth += amount;
    }

    public static int GetMaxHealth()
    {
        return MaxHealth;
    }

    public static int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public static void ResetHealth()
    {
        _currentHealth = 0;
    }
}
