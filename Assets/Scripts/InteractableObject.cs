using UnityEngine;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{

    public Dictionary<string, List<string>> setTrashData = new()
    {
        {"TrashBag", new List<string>
        { "TrashBag", "Trash", "Generic", "3", "Sam_Sprite"}}



    };

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

        ValueOfMaterial = int.Parse(setTrashData["TrashBag"][3]);


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log(ValueOfMaterial);
        Destroy(gameObject);
    }
}

