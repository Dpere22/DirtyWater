using System;
using System.Collections.Generic;

public class PlayerManager
{
    //Total trash collected overall
    public readonly Dictionary<String, int> Inventory = new()
    {
        {"Metal", 0},
        {"Plastic", 0},
        {"Wood", 0}
    };
    //Current player Trash inventory for the day
    public readonly Dictionary<string, int> CurrentDayTrash = new()
    {
        {"Metal", 0},
        {"Plastic", 0},
        {"Wood", 0}
    };

    public float MaxTime;
    public int MaxWeight;

    public int CurrentWeight = 0;
    public int WalkingSpeed;
    public int SwimmingSpeed;

    public int WeightCost = 5;
    public int SpeedCost = 20;
    

    public PlayerManager()
    {
        var playerSettings = UnityEngine.Resources.Load<PlayerSettingsSO>("PlayerSettingsSO");
        MaxTime = playerSettings.maxTime;
        MaxWeight = playerSettings.maxWeight;
        WalkingSpeed = playerSettings.walkingSpeed;
        SwimmingSpeed = playerSettings.swimmingSpeed;
    }
    //Recycle Trash
    public void RecycleTrash()
    {
        foreach (var item in CurrentDayTrash.Keys)
        {
            Inventory[item] = CurrentDayTrash[item];
        }

        ResetCurrentDayTrash();
    }

    private void ResetCurrentDayTrash()
    {
        CurrentDayTrash["Metal"] = 0;
        CurrentDayTrash["Plastic"] = 0;
        CurrentDayTrash["Wood"] = 0;
    }

}
