using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Events;
using Interactables;


public class TrashSpawner : MonoBehaviour
{
    //Trash Objects this spawner should spawn
    public List<TrashInfo> objectsToSpawn;
    public List<SpawnPoint> spawnPointList;
    private readonly List<TrashInfo> _weightedTrashList = new();
    
    private void OnEnable()
    {
        GameEventsManager.Instance.DayEvents.OnDayStart += RespawnTrash;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.DayEvents.OnDayStart -= RespawnTrash;
    }

    private void Start()
    {
        CreateWeightedList();
        RespawnTrash();
    }

    private void CreateWeightedList()
    {
        foreach (var trash in objectsToSpawn)
        {
            for (int i = 0; i < trash.spawnRate; i++)
            {
                _weightedTrashList.Add(trash);
            }
        }
    }
    
    private void RespawnTrash()
    {
        foreach (var sp in spawnPointList.Where(t => !t.isOccupied))
        {
            sp.isOccupied = true;
            Vector2 spawnPos = sp.Transform.position;
            GameObject go = Instantiate(GetRandomTrash(), spawnPos, Quaternion.identity);
            sp.SetGameObject(go);
        }
    }

    private GameObject GetRandomTrash()
    {
        int index = Random.Range(0, _weightedTrashList.Count-1);
        return _weightedTrashList[index].info.prefab;
    }
}
