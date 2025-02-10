using System;
using System.Collections.Generic;

public static class PlayerManager
{
    public static Dictionary<String, int> inventory = new()
    {
        {"Metal", 0},
        {"Plastic", 1},
        {"Wood", 0}
    };

    public static float MaxTime = 10f;
}
