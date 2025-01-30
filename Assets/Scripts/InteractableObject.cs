using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public string Name = "";
    public string Type = "";
    public string Material = "";
    public int ValueOfMaterial = 1;

    public bool canCollect = false;
    public bool isQuestItem = false;

    public void Start()
    {
        if (!isQuestItem)
        {
            randomGenerate();
        }

    }

    private void randomGenerate()
    {
        Debug.Log("new trash");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log(this.Name);
        Destroy(gameObject);
    }
}

