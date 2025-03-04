public static class OceanHealth
{
    private const float MaxHealth = 100;
    private static float _currentHealth;

    public static float GetHealthRatio()
    {
        return _currentHealth / MaxHealth;
    }

    public static void AddHealth(int amount)
    {
        _currentHealth += amount;
    }

    public static float GetMaxHealth()
    {
        return MaxHealth;
    }

    public static float GetCurrentHealth()
    {
        return _currentHealth;
    }
}
