using System;

public class ShopEvents
{
    public event Action OnEnableShopEvent;

    public void EnableShop()
    {
        OnEnableShopEvent?.Invoke();
    }
}
