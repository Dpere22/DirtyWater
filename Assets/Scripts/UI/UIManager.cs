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
        [SerializeField] private GameObject playerDropOffCrates;
        [SerializeField] private TextMeshProUGUI crateCountText;
        [SerializeField] private GameObject infoMenu;
        private bool _inventoryOpen;


        private bool _hasStarted;

        private void Update()
        {
            if (_hasStarted) UpdateTimeText();
            UpdateCrateCountText();
        }

        private void UpdateCrateCountText()
        {
            if (GameEventsManager.Instance.PlayerManager.DropOffCrates == 0 || !GameEventsManager.Instance.PlayerManager.Swimming)
            {
                if(playerDropOffCrates)
                    playerDropOffCrates.SetActive(false);
            }
            else
            {
                if (playerDropOffCrates)
                {
                    playerDropOffCrates.SetActive(true);
                    crateCountText.text = GameEventsManager.Instance.PlayerManager.DropOffCrates.ToString();
                }
            }
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
            GameEventsManager.Instance.InputEvents.OnInfoBoard += EnableInfoBoard;
            GameEventsManager.Instance.InputEvents.OnInfoBoardClosed += DisableInfoBoard;
        }

        private void OnDisable()
        {
            timer.OnTimerStart -= EnableTimerText;
            GameEventsManager.Instance.PauseEvents.OnPause -= PauseHandler;
            GameEventsManager.Instance.PauseEvents.OnResume -= ResumeHandler;
            GameEventsManager.Instance.DayEvents.OnDayEnd += DisableTimerText;
            GameEventsManager.Instance.InputEvents.OnInventoryPressed -= ToggleInventory;
            GameEventsManager.Instance.InputEvents.OnInfoBoard -= EnableInfoBoard;
            GameEventsManager.Instance.InputEvents.OnInfoBoardClosed -= DisableInfoBoard;
        }

        private void EnableInfoBoard()
        {
            infoMenu.SetActive(true);
        }

        private void DisableInfoBoard()
        {
            infoMenu.SetActive(false);
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
            UpdateInventoryUI();
            _inventoryOpen = !_inventoryOpen;
            inventory.SetActive(_inventoryOpen);
            
        }


        private void UpdateInventoryUI()
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
