using UnityEngine;
using System.Collections.Generic;
using Events;


public class TrashSpawner : MonoBehaviour
{
    //Trash Objects this spawner should spawn
    public List<TrashInfo> objectsToSpawn;

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
        RespawnTrash();
    }
    
    void RespawnTrash()
    {
        if (objectsToSpawn.Count == 0) return;

        foreach (var entry in objectsToSpawn)
        {
            int amountToSpawn = entry.spawnRate;

            for (int i = 0; i < amountToSpawn; i++)
            {
                Vector2 spawnPosition = GetRandomPointInCircle();
                Instantiate(entry.prefab, spawnPosition, Quaternion.identity);
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
