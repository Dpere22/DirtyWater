using System.Collections;
using Events;
using Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shop
{
    public class Shop : MonoBehaviour
    {

        private bool _playerInRange;
        [SerializeField] private GameObject shopUI;
        [SerializeField] private GameObject firstButton;
        [SerializeField] private GameObject newUpgradesIcon;
        public bool shopAvailable;
        private bool _inShop;
        private bool _checkedNewUpgrades = true;

        private void OnEnable()
        {
            GameEventsManager.Instance.ShopEvents.OnEnableShopEvent += EnableShop;
            GameEventsManager.Instance.InputEvents.OnSubmitPressed += InteractHandler;
            GameEventsManager.Instance.InputEvents.OnCancelPressed += CancelHandler;
            GameEventsManager.Instance.QuestEvents.OnFinishQuest += HandleQuestFinished;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.ShopEvents.OnEnableShopEvent -= EnableShop;
            GameEventsManager.Instance.InputEvents.OnSubmitPressed -= InteractHandler;
            GameEventsManager.Instance.InputEvents.OnCancelPressed -= CancelHandler;
            GameEventsManager.Instance.QuestEvents.OnFinishQuest -= HandleQuestFinished;
        }

        private void HandleQuestFinished(string questId)
        {
            if (questId is "GetToolboxQuest" or "CollectWoodQuest")
            {
                DisplayNewUpgradesIcon();
            }
        }
        private void DisplayNewUpgradesIcon()
        {
            _checkedNewUpgrades = false;
            newUpgradesIcon.SetActive(true);
        }

        private void InteractHandler(InputEventContext context)
        {
            if (!_playerInRange || !context.Equals(InputEventContext.Default) || _inShop) return;
            if (!_checkedNewUpgrades)
            {
                _checkedNewUpgrades = true;
                newUpgradesIcon.SetActive(false);
            }
            if (!shopAvailable)
            {
                GameEventsManager.Instance.DialogueEvents.EnterDialogue("shopNotAvailable");
                return;
            }
            EnterShop();
        }

        private void CancelHandler()
        {
            if (!_inShop) return;
            ExitShop();
        }
        private void EnterShop()
        {
            EventSystem.current.SetSelectedGameObject(null);
            StartCoroutine(SelectButtonAfterDelay()); //necessary to avoid a button being clicked on this frame
            GameEventsManager.Instance.InputEvents.ChangeInputEventContext(InputEventContext.Shop);
            GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();
            _inShop = true;
            shopUI.SetActive(true);
        }
        private IEnumerator SelectButtonAfterDelay()
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
        private void ExitShop()
        {
            GameEventsManager.Instance.InputEvents.ChangeInputEventContext(InputEventContext.Default);
            GameEventsManager.Instance.PlayerEvents.EnablePlayerMovement();
            _inShop = false;
            shopUI.SetActive(false);
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            _playerInRange = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            _playerInRange = false;
        }

        private void EnableShop()
        {
            shopAvailable = true;
        }
    }
}
