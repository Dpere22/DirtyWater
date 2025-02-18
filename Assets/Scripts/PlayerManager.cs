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
        {"Trash", "Plastic", "3", "0"}
        },
        {
            "RustyBarrel",
            new List<string>
        {"Trash", "Metal", "2", "1"}
        },
        {
            "RustyCan",
            new List<string>
        {"Trash", "Metal", "1", "2"}
        },
        {
            "WaterBottle",
            new List<string>
        {"Trash", "Plastic", "1", "3"}
        },
        {
            "WoodenCrate",
            new List<string>
        {"Trash", "Wood", "3", "4"}
        },
        {
            "WoodenPlank",
            new List<string>
        {"Trash", "Wood", "1", "5"}
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

    public static float MaxTime = 5f;

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


}
