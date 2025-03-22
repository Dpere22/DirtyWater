using System.Collections.Generic;

public static class TrashManager
{
    public static readonly Dictionary<string, bool> CanCollectInfo = new()
    {
        {"plasticBottle", true },
        {"plasticBag", false},
        {"metalBarrel", false},
    };

    public static bool CheckCanCollect(string garbageId)
    {
        return CanCollectInfo[garbageId];
    }












}
