using Events;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tm;

        [SerializeField] private Timer timer;
    
        [SerializeField] private GameObject pauseMenu;

        [SerializeField] private GameObject inventory;
        private bool _inventoryOpen;


        private bool _hasStarted;

        private void Update()
        {
            if (_hasStarted) UpdateTimeText();
        }

        private void UpdateTimeText()
        {
            tm.text = $"Time Left: {timer.GetRemainingTime():F2}";
        }

        private void OnEnable()
        {
            timer.OnTimerStart += EnableTimerText;
            GameEventsManager.Instance.DayEvents.OnDayEnd += DisableTimerText;
            GameEventsManager.Instance.InputEvents.PauseGameAction += OnPause;
            GameEventsManager.Instance.InputEvents.ResumeGameAction += ResumeHandler;
            GameEventsManager.Instance.InputEvents.OnInventoryPressed += ToggleInventory;
        }

        private void OnDisable()
        {
            timer.OnTimerStart -= EnableTimerText;
            GameEventsManager.Instance.DayEvents.OnDayEnd += DisableTimerText;
            GameEventsManager.Instance.InputEvents.PauseGameAction -= OnPause;
            GameEventsManager.Instance.InputEvents.ResumeGameAction -= ResumeHandler;
            GameEventsManager.Instance.InputEvents.OnInventoryPressed -= ToggleInventory;
        }
        private void EnableTimerText()
        {
            _hasStarted = true;
        }

        private void DisableTimerText()
        {
            tm.text = "";
            _hasStarted = false;
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
            SceneManager.LoadScene("MainMenu");
        }

        private void ToggleInventory()
        {
            _inventoryOpen = !_inventoryOpen;
            inventory.SetActive(_inventoryOpen);
        }
    
    }
}
