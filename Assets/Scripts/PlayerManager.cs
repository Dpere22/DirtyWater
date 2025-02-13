using System;
using System.Collections.Generic;

public static class PlayerManager
{
    //Total trash collected overall
    public static Dictionary<String, int> inventory = new()
    {
        {"Metal", 0},
        {"Plastic", 1},
        {"Wood", 0}
    };

    public static float MaxTime = 10f;

    //Current player Trash inventory for the day
    public static Dictionary<String, int> currentDayTrash = new()
    {

    };


    public static List<String> Questitems = new()
    {

    };


}
