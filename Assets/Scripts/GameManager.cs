using System.Collections;
using Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;
    void Start()
    {
        timer.OnTimerComplete += GameEventsManager.Instance.DayEvents.DayEnd;
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd += OnDayEnd;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.DayEvents.OnDayEnd -= OnDayEnd;
    }
    public void StartDayTimer()
    {
        timer.StartTimer(GameEventsManager.Instance.PlayerManager.MaxTime);
    }
    
    private void OnDayEnd()
    {
        GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();
        GameEventsManager.Instance.PlayerManager.RecycleTrash();
        StartCoroutine(WaitForAnim());
        StartCoroutine(WaitForGame());
    }
    
    //This is not great code design, but for this small feature it should be ok
    private IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(1.2f);
        player.transform.position = spawnPoint.position;
        GameEventsManager.Instance.PlayerEvents.SetPlayerWalking();
    }
    private IEnumerator WaitForGame()
    {
        yield return new WaitForSeconds(3f);

        GameEventsManager.Instance.PlayerEvents.EnablePlayerMovement();
        GameEventsManager.Instance.DayEvents.DayStart();
    }
}
