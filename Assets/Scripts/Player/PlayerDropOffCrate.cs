using Events;
using UnityEngine;

public class PlayerDropOffCrate : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private void OnEnable()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd += KillOnDayEnd;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd -= KillOnDayEnd;
    }
    
    private void Start()
    {
        rb.linearVelocity = new Vector2(0, 6);
        GameEventsManager.Instance.SoundEvents.PlaySoundEffect("bubbles_rise");
        GameEventsManager.Instance.PlayerManager.DroppedOffTrash();
    }

    private void KillOnDayEnd()
    {
        Destroy(gameObject);
    }
}
