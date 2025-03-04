using System;
using System.Collections.Generic;

public static class PlayerManager
{
    


    //Total trash collected overall
    public static Dictionary<String, int> inventory = new()
    {
        {"Metal", 0},
        {"Plastic", 0},
        {"Wood", 0}
    };

    private const float MaxTimeDefault = 30f;
    private const int MaxWeightDefault = 100;
    private const int SpeedDefault = 5;
    private const int SpeedCostDefault = 20;
    private const int WeightCostDefault = 20;


    public static float MaxTime = MaxTimeDefault;
    public static int MaxWeight = MaxWeightDefault;

    public static int currentWeight = 0;
    public static int walkingSpeed = 5;
    public static int speed = SpeedDefault;

    public static int speedCost = SpeedCostDefault;
    public static int weightCost = WeightCostDefault;
    

    //Current player Trash inventory for the day
    public static Dictionary<String, int> currentDayTrash = new()
    {
        { "Metal", 0 },
        { "Plastic", 0 },
        { "Wood", 0 }
    };


    public static List<String> Questitems = new()
    {

    };


    public static void ResetGame()
    {
        MaxTime = MaxTimeDefault;
        MaxWeight = MaxWeightDefault;
        speed = SpeedDefault;
        speedCost = SpeedCostDefault;
        weightCost = WeightCostDefault;
        inventory["Metal"] = 0;
        inventory["Plastic"] = 0;
        inventory["Wood"] = 0;
        currentDayTrash["Metal"] = 0;
        currentDayTrash["Plastic"] = 0;
        currentDayTrash["Wood"] = 0;
        currentWeight = 0;
        PauseManager.gamePaused = false;
    }

}
