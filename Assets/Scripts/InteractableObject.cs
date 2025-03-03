using UnityEngine;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{



    public List<Sprite> SpriteList = new()
    {
    };


    public string Name = "";
    public string Type = "";
    public string Material = "";
    public int ValueOfMaterial = 1;
    public Sprite currentSprite = null;
    public int weight;

    public bool canCollect = false;


    //Trash = true, Quest = false
    public bool QuestOrTrash = true;


    public void Start()
    {


        if (QuestOrTrash)
        {
            RandomTrash();
        }
        
    }


    private void RandomTrash()
    {
        int choice = Random.Range(0, PlayerManager.keys.Count);

        string chosenkey = PlayerManager.keys[choice];

        Name = chosenkey;

        Type = PlayerManager.TrashData[chosenkey][0];

        Material = PlayerManager.TrashData[chosenkey][1];

        ValueOfMaterial = int.Parse(PlayerManager.TrashData[chosenkey][2]);

        GetComponent<SpriteRenderer>().sprite = SpriteList[int.Parse(PlayerManager.TrashData[chosenkey][3])];
        
        weight = int.Parse(PlayerManager.TrashData[chosenkey][4]);

    }

    private void SpecificSpawn(string specificKey)
    {

        Name = specificKey;

        Type = PlayerManager.TrashData[specificKey][0];

        Material = PlayerManager.TrashData[specificKey][1];

        ValueOfMaterial = int.Parse(PlayerManager.TrashData[specificKey][2]);

        GetComponent<SpriteRenderer>().sprite = SpriteList[int.Parse(PlayerManager.TrashData[specificKey][3])];

    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (!CanHoldWeight()) return;

        if (QuestOrTrash)
        {
            int newValue = PlayerManager.currentDayTrash[Material] + ValueOfMaterial;

            PlayerManager.currentDayTrash[Material] = newValue;

            PlayerManager.currentWeight += weight;
            
            OceanHealth.AddHealth(1);

        }
        else
        {
            PlayerManager.Questitems.Add(Name);
        }
        
        


        Destroy(gameObject);
    }

    private bool CanHoldWeight()
    {
        return weight + PlayerManager.currentWeight < PlayerManager.MaxWeight;
    }
}

