using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class DialogTrigger : MonoBehaviour
{
    [Header("Visual Cue")] [SerializeField]
    private GameObject visualCue;

    [FormerlySerializedAs("inkJSON")]
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJson;


    private PlayerMovement player;
    
    private bool playerInRange;
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }
    
    private void Start()
    {
        

        player = FindFirstObjectByType<PlayerMovement>();
        player.InteractAction += InteractHandler;
    }

    private void Update()
    {
        visualCue.SetActive(playerInRange);
    }
    
    

    private void InteractHandler()
    {
        if (!playerInRange || DialogManager.GetInstance().DialogIsPlaying){}
        DialogManager.GetInstance().EnterDialogMode(inkJson);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
