using UnityEngine;
using System;
using Events;

public class Water : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EdgeCollider2D waterCollider;

    public Action PlayerEnteredWater;

    private void OnEnable()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd += EnableTrigger;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd -= EnableTrigger;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entering Water!");
        if (!other.CompareTag("Player")) return;
        
        PlayerEnteredWater?.Invoke();
        PlayerMovement playerMovement = other.gameObject.GetComponentInParent<PlayerMovement>();
        playerMovement.StartSwimming();
        playerMovement.isJumping = false;
        gameManager.StartDayTimer();
        waterCollider.enabled = false;
    }

    private void EnableTrigger()
    {
        Debug.Log("EnablingTrigger!");
        waterCollider.enabled = true;
    }
}
