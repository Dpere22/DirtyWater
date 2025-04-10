using System.Collections;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tm;

        [SerializeField] private Timer timer;
    
        [SerializeField] private GameObject pauseMenu;

        [SerializeField] private GameObject inventory;
        [SerializeField] private GameObject pauseFirstButton;
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
            GameEventsManager.Instance.PauseEvents.OnPause += PauseHandler;
            GameEventsManager.Instance.PauseEvents.OnResume += ResumeHandler;
            GameEventsManager.Instance.DayEvents.OnDayEnd += DisableTimerText;
            GameEventsManager.Instance.InputEvents.OnInventoryPressed += ToggleInventory;
        }

        private void OnDisable()
        {
            timer.OnTimerStart -= EnableTimerText;
            GameEventsManager.Instance.PauseEvents.OnPause -= PauseHandler;
            GameEventsManager.Instance.PauseEvents.OnResume -= ResumeHandler;
            GameEventsManager.Instance.DayEvents.OnDayEnd += DisableTimerText;
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
 
        private void PauseHandler()
        {
            pauseMenu.SetActive(true);
            StartCoroutine(SelectButtonAfterDelay());
        }
        private IEnumerator SelectButtonAfterDelay()
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
        private void ResumeHandler()
        {
            pauseMenu.SetActive(false);
        }
        
        /// <summary>
        /// Used by the GUI button
        /// </summary>
        public void ResumeButtonHandler()
        {
            pauseMenu.SetActive(false);
            GameEventsManager.Instance.PauseEvents.Resume();
        }

        public void MainMenuButton()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void ToggleInventory()
        {
            updateInventoryUI();
            _inventoryOpen = !_inventoryOpen;
            inventory.SetActive(_inventoryOpen);
            
        }


        public void updateInventoryUI()
        {
            GameEventsManager.Instance.PlayerManager.WaterBottle = 0;
            GameEventsManager.Instance.PlayerManager.PlasticTrash = 0;
            GameEventsManager.Instance.PlayerManager.WoodenCrate = 0;
            GameEventsManager.Instance.PlayerManager.WoodenPlank = 0;


            foreach (List<string> item in GameEventsManager.Instance.PlayerManager.GarbageInInventory)
            {
                if (item[0] == "plasticBottle")
                {
                    GameEventsManager.Instance.PlayerManager.WaterBottle += 1;
                }
                if (item[0] == "plasticBag")
                {
                    GameEventsManager.Instance.PlayerManager.PlasticTrash += 1;
                }
                if (item[0] == "woodenCrate")
                {
                    GameEventsManager.Instance.PlayerManager.WoodenCrate += 1;
                }
                if (item[0] == "woodenPlank")
                {
                    GameEventsManager.Instance.PlayerManager.WoodenPlank += 1;
                }
            }
        }
    }
}
