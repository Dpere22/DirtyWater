using UnityEngine;

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
        Debug.Log(GetHealthRatio());
    }
}
