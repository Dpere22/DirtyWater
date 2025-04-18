using Events;
using UnityEngine;

public class ScreenCoverUI : MonoBehaviour
{
    private static readonly int FadeOutTrigger = Animator.StringToHash("FadeOutTrigger");
    private static readonly int FadeInTrigger = Animator.StringToHash("FadeInTrigger");
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd += FadeIn;
        GameEventsManager.Instance.DayEvents.OnDayStart += FadeOut;
        GameEventsManager.Instance.DayEvents.OnJumpIntoWater += FadeIn;
        GameEventsManager.Instance.DayEvents.OnEnterWater += FadeOut;
        GameEventsManager.Instance.DayEvents.OnFadeOutUI += FadeIn;
        GameEventsManager.Instance.DayEvents.OnFadeInUI += FadeOut;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd += FadeIn;
        GameEventsManager.Instance.DayEvents.OnDayStart += FadeOut;
        GameEventsManager.Instance.DayEvents.OnJumpIntoWater += FadeIn;
        GameEventsManager.Instance.DayEvents.OnEnterWater += FadeOut;
        GameEventsManager.Instance.DayEvents.OnFadeOutUI -= FadeIn;
        GameEventsManager.Instance.DayEvents.OnFadeInUI -= FadeOut;
    }
    
    
    private void FadeOut()
    {
        animator.SetTrigger(FadeOutTrigger);
    }

    private void FadeIn()
    {
        animator.SetTrigger(FadeInTrigger);
    }
}
