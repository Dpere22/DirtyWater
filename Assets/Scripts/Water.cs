using UnityEngine;
using System;
using Events;

public class Water : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BoxCollider2D waterCollider;
    

    private void OnEnable()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd += DisableTrigger;
        GameEventsManager.Instance.DayEvents.OnDayStart += EnableTrigger;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd -= DisableTrigger;
        GameEventsManager.Instance.DayEvents.OnDayStart -= EnableTrigger;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameEventsManager.Instance.DayEvents.EnterWater();
        GameEventsManager.Instance.PlayerEvents.SetPlayerSwimming();
        gameManager.StartDayTimer();
        waterCollider.enabled = false;
    }

    private void EnableTrigger()
    {
        waterCollider.enabled = true;
    }

    private void DisableTrigger()
    {
        waterCollider.enabled = false;
    }
}
