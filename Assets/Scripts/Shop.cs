using System.Collections;
using Events;
using Input;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{

    private bool _playerInRange;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject firstButton;
    [SerializeField] private GameObject interactIcon;
    public bool shopAvailable;
    private bool _inShop;

    private void OnEnable()
    {
        GameEventsManager.Instance.ShopEvents.OnEnableShopEvent += EnableShop;
        GameEventsManager.Instance.InputEvents.OnSubmitPressed += InteractHandler;
        GameEventsManager.Instance.InputEvents.OnCancelPressed += CancelHandler;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.ShopEvents.OnEnableShopEvent -= EnableShop;
        GameEventsManager.Instance.InputEvents.OnSubmitPressed -= InteractHandler;
        GameEventsManager.Instance.InputEvents.OnCancelPressed -= CancelHandler;
    }

    private void InteractHandler(InputEventContext context)
    {
        if (!_playerInRange || !context.Equals(InputEventContext.Default) || _inShop) return;
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
        GameEventsManager.Instance.InputEvents.ChangeInputEventContext(InputEventContext.Dialogue);
        GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();
        _inShop = true;
        interactIcon.SetActive(false);
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
        interactIcon.SetActive(true);
        _playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        interactIcon.SetActive(false);
        _playerInRange = false;
    }

    private void EnableShop()
    {
        shopAvailable = true;
    }
}
