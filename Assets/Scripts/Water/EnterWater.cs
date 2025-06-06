using Events;
using UnityEngine;

public class EnterWater : MonoBehaviour
{
    private bool _inRange;
    private bool _hasJumped;

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.JumpAction += Dive;
        GameEventsManager.Instance.DayEvents.OnDayStart += ResetJump;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.JumpAction -= Dive;
        GameEventsManager.Instance.DayEvents.OnDayStart -= ResetJump;

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        _inRange = true;

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        _inRange = false;
    }

    private void ResetJump()
    {
        _hasJumped = false;
    }
    private void Dive()
    {
        if (_inRange && !_hasJumped)
        {
            GameEventsManager.Instance.DayEvents.JumpIntoWater();
            _hasJumped = true;
        }
    }
    
}
