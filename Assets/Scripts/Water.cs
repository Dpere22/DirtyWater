using UnityEngine;
using System;
using Events;

public class Water : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BoxCollider2D waterCollider;

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
        if (!other.CompareTag("Player")) return;
        
        PlayerEnteredWater?.Invoke();
        GameEventsManager.Instance.PlayerEvents.SetPlayerSwimming();
        gameManager.StartDayTimer();
        waterCollider.enabled = false;
    }

    private void EnableTrigger()
    {
        waterCollider.enabled = true;
    }
}
