using Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialog
{
    public class DialogTrigger : MonoBehaviour
    {
        [Header("Visual Cue")] [SerializeField]
        private GameObject visualCue;

        [FormerlySerializedAs("inkJSON")]
        [Header("Ink JSON")]
        [SerializeField] private TextAsset inkJson;
        private bool _playerInRange;
        
        private void Awake()
        {
            _playerInRange = false;
            visualCue.SetActive(false);
        }
    
        private void Start()
        {
            GameEventsManager.Instance.InputEvents.OnInteractPressed += InteractHandler;
        }

        private void Update()
        {
            visualCue.SetActive(_playerInRange);
        }
    
    

        private void InteractHandler()
        {
            // if (!_playerInRange || DialogManager.GetInstance().DialogIsPlaying) return;
            // DialogManager.GetInstance().EnterDialogMode(inkJson);
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
}
