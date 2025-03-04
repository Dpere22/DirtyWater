using UnityEngine;
using System.Collections.Generic;


public class TrashSpawner : MonoBehaviour
{
    //Empty Object for spawning
    public GameObject objectToSpawn;

    public float circleRadius = 1;
    public float replenishRate = 3;
    public float maxTrash = 1;
    public string Name = "";

    public static List<List<string>> currentTrashList = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        currentTrashList = TrashManager.TrashSpawnerData[Name];


        respawnTrash(currentTrashList);


        for(int I = 0; I<replenishRate; I++)
        {
            spawnRandomTrash();
        }

        



    }

    private void respawnTrash(List<List<string>> currentTrashList)
    {
        foreach(List<string> list in currentTrashList)
        {

        }
    }

    private void Update()
    {
        //update list in storage
        TrashManager.TrashSpawnerData[Name] = currentTrashList;

    }

    public void spawnRandomTrash()
    {
        

        Vector2 randomPosition = Random.insideUnitCircle * circleRadius;

        Vector2 spawnPoint = new Vector2(transform.position.x, transform.position.y) + randomPosition;

        GameObject temp = Instantiate(objectToSpawn, spawnPoint, Quaternion.identity) as GameObject;

        temp.transform.parent = this.transform;
        
    }


    public void getInfo(string Name, string X, string Y)
    {
        List<string> temp = new()
        {
            Name,
            X,
            Y
        };

        currentTrashList.Add(temp);




        
    }


}
