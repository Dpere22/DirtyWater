using UnityEngine;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{



    public List<Sprite> SpriteList = new()
    {
    };

    [Range(0f, 100f)] public float spawnRate;
    public GameObject prefab;


    public string Name = "";
    public string Type = "";
    public string Material = "";
    public int ValueOfMaterial = 1;
    public Sprite currentSprite = null;
    public int weight;

    public bool canCollect = false;

    public List<string> spawnKeys = new();

    //Trash = true, Quest = false
    public bool QuestOrTrash = true;


    public void Start()
    {
        if (transform.parent is not null)
        {
            //Debug.Log("got keys");
            //spawnKeys = transform.parent.gameObject.GetComponent<TrashSpawner>().setSpawns();
        }


        if (QuestOrTrash)
        {
            RandomTrash();
        }



        string X = this.transform.position.x.ToString();
        string Y = this.transform.position.y.ToString();

        //transform.parent.gameObject.GetComponent<TrashSpawner>().getInfo(Name, X, Y);
        
    }

    

    private void RandomTrash()
    {
        int choice = Random.Range(0, spawnKeys.Count);

        string chosenkey = spawnKeys[choice];

        Name = chosenkey;

        Type = TrashManager.TrashData[chosenkey][0];

        Material = TrashManager.TrashData[chosenkey][1];

        ValueOfMaterial = int.Parse(TrashManager.TrashData[chosenkey][2]);

        GetComponent<SpriteRenderer>().sprite = SpriteList[int.Parse(TrashManager.TrashData[chosenkey][3])];
        
        weight = int.Parse(TrashManager.TrashData[chosenkey][4]);

        

    }

    public void SpecificSpawn(string specificKey)
    {

        Name = specificKey;

        Type = TrashManager.TrashData[specificKey][0];

        Material = TrashManager.TrashData[specificKey][1];

        ValueOfMaterial = int.Parse(TrashManager.TrashData[specificKey][2]);

        GetComponent<SpriteRenderer>().sprite = SpriteList[int.Parse(TrashManager.TrashData[specificKey][3])];

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (!CanHoldWeight()) return;

        if (QuestOrTrash)
        {
            TrashManager.CollectTrash(Name);

        }
        else
        {
            PlayerManager.Questitems.Add(Name);
        }


        //transform.parent.gameObject.GetComponent<TrashSpawner>().removeTrashFromList(Name);

        Destroy(gameObject);
    }

    private bool CanHoldWeight()
    {
        return weight + PlayerManager.currentWeight < PlayerManager.MaxWeight;
    }
}

