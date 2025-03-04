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


    public bool spawnsPlastic = false;
    public bool spawnsWood = false;
    public bool spawnsMetal = false;


    public List<string> spawnKeys = new();


    public static List<List<string>> currentTrashList = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        getSpawns();


        if(TrashManager.TrashSpawnerData.ContainsKey(Name))
        {
            //Gets list from dictionary if key exists
            currentTrashList = TrashManager.TrashSpawnerData[Name];  
        }
        else
        {
            //Makes a list if no key exists
            TrashManager.TrashSpawnerData[Name] = currentTrashList;
        }
        

        if(currentTrashList.Count > 0)
        {
            respawnTrash(currentTrashList);
        }
        


        for(int I = 0; I<replenishRate; I++)
        {
            spawnRandomTrash();
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

    public void spawnSpecifcTrash(string Name, float X, float Y)
    {

        Vector2 spawnPoint = new Vector2(X, Y);

        GameObject temp = Instantiate(objectToSpawn, spawnPoint, Quaternion.identity) as GameObject;

        temp.transform.parent = this.transform;

        temp.GetComponent<Interactable>().SpecificSpawn(Name);

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

    public List<string> setSpawns()
    {
        return spawnKeys;
    }

    public void removeTrashFromList(string target)
    {
        for(int I = 0; I < currentTrashList.Count; I++)
        {
            if (currentTrashList[I][0] == target)
            {
                currentTrashList.RemoveAt(I);
                return;
            }
        }
    }

    private void getSpawns()
    {
        if (spawnsPlastic)
        {
            spawnKeys.Add(TrashManager.keys[0]);
            spawnKeys.Add(TrashManager.keys[1]);
        }

        if (spawnsWood)
        {
            spawnKeys.Add(TrashManager.keys[4]);
            spawnKeys.Add(TrashManager.keys[5]);
        }

        if (spawnsMetal)
        {
            spawnKeys.Add(TrashManager.keys[2]);
            spawnKeys.Add(TrashManager.keys[3]);
        }



    }

    private void respawnTrash(List<List<string>> currentTrashList)
    {
        foreach (List<string> list in currentTrashList)
        {
            spawnSpecifcTrash(list[0], float.Parse(list[1]), float.Parse(list[2]));
        }
    }


}
