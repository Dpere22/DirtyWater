using UnityEngine;
using System;
using Events;

public class CurrentDayInventory : MonoBehaviour
{

    public static Action OpenInventory;
    public static Action CloseInventory;



    public static bool InventoryOpen;

    public static void showInventory()
    {
        InventoryOpen = true;
        OpenInventory?.Invoke();
    }

    public static void hideInventory()
    {
        InventoryOpen = false;
        CloseInventory?.Invoke();
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }







}
