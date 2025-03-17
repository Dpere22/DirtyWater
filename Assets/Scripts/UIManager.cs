using Events;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tm;

    [SerializeField] private Timer timer;
    
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject currentDayInventory;


    private bool _hasStarted;

    private void Update()
    {
        if (_hasStarted) UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        tm.text = $"Time Left: {timer.GetRemainingTime():F2}";
    }
    

    private void Start()
    {
        timer.OnTimerStart += EnableTimerText;
        GameEventsManager.Instance.InputEvents.PauseGameAction += OnPause;
        GameEventsManager.Instance.InputEvents.ResumeGameAction += ResumeHandler;
    }

    private void OnDestroy()
    {
        timer.OnTimerStart -= EnableTimerText;
        GameEventsManager.Instance.InputEvents.PauseGameAction -= OnPause;
        GameEventsManager.Instance.InputEvents.ResumeGameAction -= ResumeHandler;
    }
    private void EnableTimerText()
    {
        _hasStarted = true;
    }
 
    private void OnPause()
    {
        pauseMenu.SetActive(true);
    }


    /// <summary>
    /// For when ESC is pressed - thus the event is fired
    /// </summary>
    private void ResumeHandler()
    {
        pauseMenu.SetActive(false);
    }
    
    
    /// <summary>
    /// For when the button is pressed instead of ESC
    /// </summary>
    public void OnResume()
    {
        pauseMenu.SetActive(false);
        PauseManager.ResumeGame();
    }

    public void MainMenuButton()
    {
        PlayerManager.ResetGame();
        SceneManager.LoadScene("MainMenu");
    }
    
}
