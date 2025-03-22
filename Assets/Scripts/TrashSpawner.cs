using UnityEngine;
using System.Collections.Generic;
using Events;


public class TrashSpawner : MonoBehaviour
{
    //Trash Objects this spawner should spawn
    public List<TrashInfo> objectsToSpawn;
    private readonly Dictionary<int, List<GameObject>> _currAmount = new(); //keeps track of all the currently spawned game objects

    public float spawnRadius = 1f;

    private void OnEnable()
    {
        GameEventsManager.Instance.DayEvents.OnDayStart += RespawnTrash;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.DayEvents.OnDayStart -= RespawnTrash;
    }

    void Start()
    {
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            _currAmount[i] = new List<GameObject>();
        }
        RespawnTrash();
    }

    void Update()
    {
        //Cleans up collected garbage, this is dangerous code but will work for now
        foreach (var lst in _currAmount.Values)
        {
            lst.RemoveAll(item => !item);
        }
    }
    
    void RespawnTrash()
    {
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            int amountToSpawn = Mathf.Abs(_currAmount[i].Count - objectsToSpawn[i].spawnRate);

            for (int j = 0; j < amountToSpawn; j++)
            {
                Vector2 spawnPosition = GetRandomPointInCircle();
                GameObject newTrash = Instantiate(objectsToSpawn[i].prefab, spawnPosition, Quaternion.identity);
                _currAmount[i].Add(newTrash);
            }
        }
    }

    Vector2 GetRandomPointInCircle()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float radius = Random.Range(0f, spawnRadius);
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        return (Vector2)transform.position + new Vector2(x, y);
    }
    
    /// <summary>
    /// For visualizing the radius in the scene view
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;  // Set color for the radius
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
