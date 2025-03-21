using Events;
using Input;
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
            GameEventsManager.Instance.InputEvents.OnSubmitPressed += OnSubmit;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.InputEvents.OnSubmitPressed -= OnSubmit;
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

        private void OnSubmit(InputEventContext context)
        {
            if (!_inRange || !context.Equals(InputEventContext.Default)) return;
            CollectTrash();
        }

        protected abstract string SetTrashId();

        protected abstract bool CheckCanCollect();
    }
}
