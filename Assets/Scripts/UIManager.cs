using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tm;
    [SerializeField] private TextMeshProUGUI weightText;

    [SerializeField] private Timer timer;
    
    [SerializeField] private GameObject pauseMenu;
    

    private bool _hasStarted;

    private void Update()
    {
        if (_hasStarted) UpdateTimeText();
        UpdateWeightText();
    }

    private void UpdateTimeText()
    {
        tm.text = $"Time Left: {timer.GetRemainingTime():F2}";
    }

    private void UpdateWeightText()
    {
        weightText.text = $"Weight: {PlayerManager.currentWeight}/{PlayerManager.MaxWeight}";
    }

    private void Start()
    {
        timer.OnTimerStart += EnableTimerText;
        PauseManager.PauseGameAction += OnPause;
        PauseManager.ResumeGameAction += ResumeHandler;
    }

    private void OnDestroy()
    {
        timer.OnTimerStart -= EnableTimerText;
        PauseManager.PauseGameAction -= OnPause;
        PauseManager.ResumeGameAction -= ResumeHandler;
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
    
}
