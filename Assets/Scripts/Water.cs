using UnityEngine;
using System;

public class Water : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public Action PlayerEnteredWater;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        PlayerEnteredWater?.Invoke();
        PlayerMovement playerMovement = other.gameObject.GetComponentInParent<PlayerMovement>();
        playerMovement.StartSwimming();
        playerMovement.isJumping = false;
        gameManager.StartDayTimer();
        gameObject.SetActive(false);
    }
}
