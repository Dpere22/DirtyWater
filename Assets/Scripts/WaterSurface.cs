using Events;
using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    [SerializeField] private BoxCollider2D waterSurfaceCollider;

    private void OnEnable()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd += DisableWaterSurfaceCollider;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd -= DisableWaterSurfaceCollider;
    }

    private void Start()
    {
        GameEventsManager.Instance.DayEvents.OnEnterWater += PlayerEnteredWater;
    }

    private void PlayerEnteredWater()
    {
        //waterSurfaceCollider.enabled = true;
    }

    private void DisableWaterSurfaceCollider()
    {
        //waterSurfaceCollider.enabled = false;
    }

}
