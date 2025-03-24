using Events;
using UnityEngine;

public class ScreenCoverUI : MonoBehaviour
{
    private static readonly int FadeOutTrigger = Animator.StringToHash("FadeOutTrigger");
    private static readonly int FadeInTrigger = Animator.StringToHash("FadeInTrigger");
    [SerializeField] private Animator animator;

    private void Awake()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd += FadeIn;
        GameEventsManager.Instance.DayEvents.OnDayStart += FadeOut;
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
