using UnityEngine;



public class TrashSpawner : MonoBehaviour
{
    //Empty Object for spawning
    public GameObject objectToSpawn;

    public float circleRadius = 1;
    public float maxTrash = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTrash();
        spawnTrash();
        spawnTrash();

    

    }

    private void Update()
    {
        float currentTrash = transform.childCount;

        if (currentTrash >= maxTrash)
        {
            return;
        }
        else
        {
            if (Random.Range(1, 100) == 1)
            {
                spawnTrash();
            }
        }

        
    }

    public void spawnTrash()
    {
        

        Vector2 randomPosition = Random.insideUnitCircle * circleRadius;

        Vector2 spawnPoint = new Vector2(transform.position.x, transform.position.y) + randomPosition;

        GameObject temp = Instantiate(objectToSpawn, spawnPoint, Quaternion.identity) as GameObject;

        temp.transform.parent = this.transform;
        
    }

}
