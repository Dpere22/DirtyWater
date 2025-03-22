using Events;
using Input;
using UnityEngine;

public class Shop : MonoBehaviour
{

    private bool _playerInRange;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject interactIcon;
    private bool _inShop;
    public bool shopAvailable;


    private void OnEnable()
    {
        GameEventsManager.Instance.ShopEvents.OnEnableShopEvent += EnableShop;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.ShopEvents.OnEnableShopEvent -= EnableShop;
    }
    private void Start()
    {
        GameEventsManager.Instance.InputEvents.OnSubmitPressed += InteractHandler;
    }

    private void OnDestroy()
    {
        if(player) GameEventsManager.Instance.InputEvents.OnSubmitPressed -= InteractHandler;
    }

    private void InteractHandler(InputEventContext context)
    {
        if (!_playerInRange || !context.Equals(InputEventContext.Default)) return;
        interactIcon.SetActive(false);
        if (!shopAvailable)
        {
            GameEventsManager.Instance.DialogueEvents.EnterDialogue("shopNotAvailable");
            return;
        }
        _inShop = !_inShop;
        shopUI.SetActive(_inShop);
        
        if (!player) return;
        
        if (_inShop)
            player.GetComponent<PlayerMovement>().RestrictMovement();
        else
            player.GetComponent<PlayerMovement>().EnableMovement();

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactIcon.SetActive(true);
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactIcon.SetActive(false);
            _playerInRange = false;
        }
    }

    private void EnableShop()
    {
        shopAvailable = true;
    }
}
