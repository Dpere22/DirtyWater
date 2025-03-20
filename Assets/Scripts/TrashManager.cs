using System;
using System.Collections.Generic;

public static class TrashManager
{


    //TrashData (Type, Material, Value, Sprite, Weight)
    public static Dictionary<string, List<string>> TrashData = new()
    {
        {
            "PlasticTrash",
            new List<string>
        {"Trash", "Plastic", "3", "0", "5"}
        },
        {
            "WaterBottle",
            new List<string>
        {"Trash", "Plastic", "1", "3", "2"}
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


    public static Dictionary<string, List<List<string>>> TrashSpawnerData = new()
    {

    };












}
