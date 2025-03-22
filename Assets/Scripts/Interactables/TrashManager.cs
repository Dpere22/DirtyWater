using System.Collections.Generic;
using Interactables;

public class TrashManager
{
    private readonly Dictionary<string, bool> _canCollectInfo = new();
    /// <summary>
    /// Constructor for the TrashManager
    /// </summary>
    public TrashManager()
    {
        GarbageInfoSO[] allTrash = UnityEngine.Resources.LoadAll<GarbageInfoSO>("Interactables");
        CreateCanCollectDict(allTrash);
    }
    private void CreateCanCollectDict(GarbageInfoSO[] allTrash)
    {
        foreach (GarbageInfoSO trash in allTrash)
        {
            _canCollectInfo.Add(trash.garbageId, trash.canCollect);
        }
    }
    public bool CheckCanCollect(string garbageId)
    {
        return _canCollectInfo[garbageId];
    }












}
