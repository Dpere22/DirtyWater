using UnityEngine;

public class CurrentDayInventory : MonoBehaviour
{






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void RemoveTrash(string SpecificKey)
    {
        PlayerManager.currentDayTrash.Remove(SpecificKey);
    }




}
