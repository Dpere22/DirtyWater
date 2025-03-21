using Events;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(CircleCollider2D))]
    
    public abstract class Trash : MonoBehaviour
    {
        [SerializeField] private int weight;
        [SerializeField] private GameObject collectIcon;
        private string _trashId;

        private bool _inRange;

        private void Start()
        {
            _trashId = SetTrashId();
        }
        private void OnEnable()
        {
            GameEventsManager.Instance.InputEvents.OnInteractPressed += OnSubmit;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.InputEvents.OnInteractPressed -= OnSubmit;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            collectIcon.SetActive(true);
            _inRange = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            collectIcon.SetActive(false);
            _inRange = false;
        }

        private void ShowCollectIcon()
        {
            
        }

        private void CollectTrash()
        {
            if (!CheckCanCollect())
            {
                DisplayCannotCollect();
                return;
            }
            PlayerManager.currentWeight += weight;
            if(gameObject)
                Destroy(gameObject);
        }

        private void DisplayCannotCollect()
        {
            GameEventsManager.Instance.DialogueEvents.EnterDialogue("cannotCollectTrash");
        }

        private void OnSubmit()
        {
            if (!_inRange) return;
            CollectTrash();
        }

        protected abstract string SetTrashId();

        protected abstract bool CheckCanCollect();
    }
}
