using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Timer timer;
    void Start()
    {
        timer.OnTimerComplete += OnDayEnd;
    }
    public void StartDayTimer()
    {
        timer.StartTimer(PlayerManager.MaxTime);
    }
    
    private void OnDayEnd()
    {
        SceneManager.LoadScene("RecycleScreen");
    }
}
