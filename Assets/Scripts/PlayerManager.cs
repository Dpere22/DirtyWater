using System;
using System.Collections.Generic;

public static class PlayerManager
{
    //TrashData
    public static Dictionary<string, List<string>> TrashData = new()
    {
        {
            "PlasticTrash",
            new List<string>
        {"Trash", "Plastic", "3", "0", "5"}
        },
        {
            "RustyBarrel",
            new List<string>
        {"Trash", "Metal", "2", "1", "20"}
        },
        {
            "RustyCan",
            new List<string>
        {"Trash", "Metal", "1", "2", "5"}
        },
        {
            "WaterBottle",
            new List<string>
        {"Trash", "Plastic", "1", "3", "2"}
        },
        {
            "WoodenCrate",
            new List<string>
        {"Trash", "Wood", "3", "4", "15"}
        },
        {
            "WoodenPlank",
            new List<string>
        {"Trash", "Wood", "1", "5", "5"}
        }
    };
    public static List<string> keys = new(TrashData.Keys);


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


    public static float MaxTime = MaxTimeDefault;
    public static int MaxWeight = MaxWeightDefault;

    public static int currentWeight = 0;
    public static int speed = SpeedDefault;

    public static int speedCost = SpeedCostDefault;
    

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
