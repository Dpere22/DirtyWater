using Events;
using UnityEngine;

public class Shop : MonoBehaviour
{

    private bool _playerInRange;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject player;
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
        GameEventsManager.Instance.InputEvents.OnInteractPressed += InteractHandler;
    }

    private void OnDestroy()
    {
        if(player) GameEventsManager.Instance.InputEvents.OnInteractPressed -= InteractHandler;
    }

    private void InteractHandler()
    {
        //Debug.Log("Attemtping to enter shop");
        if (!_playerInRange) return;
        if (!shopAvailable)
        {
            //Debug.Log("Shop isn't avaiable attempting to display dialog");
            GameEventsManager.Instance.DialogueEvents.EnterDialogue("shopNotAvailable");
            return;
        }
        //Debug.Log("Entering Shop!");
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
            //Debug.Log("In range!");
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }

    private void EnableShop()
    {
        shopAvailable = true;
    }
}
