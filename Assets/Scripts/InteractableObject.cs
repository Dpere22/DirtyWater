using UnityEngine;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{

    public static Dictionary<string, List<string>> TrashData = new()
    {
        {
            "TrashBag", 
            new List<string>
        {"Trash", "Wood", "3", "Sam_Sprite"}
        },
        {
            "Cup",
            new List<string>
        {"Trash", "Plastic", "2", "Sam_Sprite"}
        },
        {
            "Metal Scrap",
            new List<string>
        {"Trash", "Metal", "1", "Sam_Sprite"}
        },


    };




    public List<string> keys = new(TrashData.Keys);

    public string Name = "";
    public string Type = "";
    public string Material = "";
    public int ValueOfMaterial = 1;
    public Sprite currentSprite = null;

    public bool canCollect = false;


    //Trash = true, Quest = false
    public bool QuestOrTrash = true;


    public void Start()
    {
        
        if (currentSprite != null)
        {
            GetComponent<SpriteRenderer>
        }


        if (QuestOrTrash)
        {
            RandomTrash();
        }
        
    }


    private void RandomTrash()
    {
        int choice = Random.Range(0, keys.Count);

        string chosenkey = keys[choice];

        Name = chosenkey;

        Type = TrashData[chosenkey][0];

        Material = TrashData[chosenkey][1];

        ValueOfMaterial = int.Parse(TrashData[chosenkey][2]);

        //Sprite chosenSprite = Resources.Load(setTrashData[chosenkey][3], typeof(Sprite)) as Sprite;

    }

    private void SpecificSpawn(string specificKey)
    {

        Name = specificKey;

        Type = TrashData[specificKey][0];

        Material = TrashData[specificKey][1];

        ValueOfMaterial = int.Parse(TrashData[specificKey][2]);

        //Sprite chosenSprite = Resources.Load(setTrashData[specificKey][3], typeof(Sprite)) as Sprite;

    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (QuestOrTrash)
        {
            PlayerManager.currentDayTrash.Add(Name, ValueOfMaterial);
        }
        else
        {
            PlayerManager.Questitems.Add(Name);
        }
        


        Destroy(gameObject);
    }
}

