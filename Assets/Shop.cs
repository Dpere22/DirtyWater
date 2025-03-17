using Dialog;
using Events;
using UnityEngine;

public class Shop : MonoBehaviour
{

    private bool _playerInRange;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject player;
    private bool _inShop;

    private void Start()
    {
        GameEventsManager.Instance.InputEvents.InteractAction += InteractHandler;
    }

    private void OnDestroy()
    {
        if(player) GameEventsManager.Instance.InputEvents.InteractAction -= InteractHandler;
    }

    private void InteractHandler()
    {
        //if (!_playerInRange || DialogManager.GetInstance().DialogIsPlaying) return;
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
}
