using System.Collections;
using Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject screenCover;
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
        timer.StartTimer(PlayerManager.MaxTime);
    }
    
    private void OnDayEnd()
    {
        GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();
        screenCover.SetActive(true);
        Recycle();
        StartCoroutine(WaitForGame());
        player.transform.position = spawnPoint.position;
        GameEventsManager.Instance.PlayerEvents.SetPlayerWalking();
    }

    private IEnumerator WaitForGame()
    {
        yield return new WaitForSeconds(3f);
        screenCover.SetActive(false);
    }

    private void Recycle()
    {
        PlayerManager.inventory["Metal"] += PlayerManager.currentDayTrash["Metal"];
        PlayerManager.inventory["Wood"] += PlayerManager.currentDayTrash["Wood"];
        PlayerManager.inventory["Plastic"] += PlayerManager.currentDayTrash["Plastic"];
        PlayerManager.currentWeight = 0;

        PlayerManager.currentDayTrash["Metal"] = 0;
        PlayerManager.currentDayTrash["Wood"] = 0;
        PlayerManager.currentDayTrash["Plastic"] = 0;
    }
}
