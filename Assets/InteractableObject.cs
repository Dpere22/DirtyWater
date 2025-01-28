using UnityEngine;

public class Interactable : MonoBehaviour
{

    public string Name = "";
    public string Type = "";
    public string Material = "";
    public int ValueOfMaterial = 1;

    public bool canCollect = false;




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("wowowowow");
        Destroy(gameObject);
    }
}

