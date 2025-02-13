using UnityEngine;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{

    public static Dictionary<string, List<string>> setTrashData = new()
    {
        {
            "TrashBag", 
            new List<string>
        {"Trash", "Generic", "3", "Sam_Sprite"}
        },
        {
            "Cup",
            new List<string>
        {"Trash", "Generic", "2", "Sam_Sprite"}
        },
        {
            "Metal Scrap",
            new List<string>
        {"Trash", "Generic", "1", "Sam_Sprite"}
        },


    };

    
    public List<string> keys = new(setTrashData.Keys);

    public string Name = "";
    public string Type = "";
    public string Material = "";
    public int ValueOfMaterial = 1;

    public bool canCollect = false;
    public bool randomObject = true;


    public void Start()
    {
        

        if (randomObject)
        {
            randommaterial();
        }
    }


    private void randommaterial()
    {
        int choice = Random.Range(0, keys.Count);

        string chosenkey = keys[choice];

        Name = chosenkey;

        Type = setTrashData[chosenkey][0];

        Material = setTrashData[chosenkey][1];

        ValueOfMaterial = int.Parse(setTrashData[chosenkey][2]);

        //Sprite chosenSprite = Resources.Load(setTrashData[chosenkey][3], typeof(Sprite)) as Sprite;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log(Name + Type + Material);
        Destroy(gameObject);
    }
}

