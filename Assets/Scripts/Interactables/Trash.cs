using Events;
using Input;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(CircleCollider2D))]
    
    public class Trash : MonoBehaviour
    {
        [SerializeField] private GameObject collectIcon;
        [SerializeField] private GarbageInfoSO garbageInfo;

        private bool _inRange;
        
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
            if (!TrashManager.CheckCanCollect(garbageInfo.garbageId))
            {
                DisplayCannotCollect();
                return;
            }

            if (!CheckCanHoldWeight())
            {
                DisplayTooHeavy();
                return;
            }
            PlayerManager.currentWeight += garbageInfo.weight;
            AddItemToCurrentDayInventory();
            if(gameObject)
                Destroy(gameObject);
        }

        private void AddItemToCurrentDayInventory()
        {
            PlayerManager.currentDayTrash["Plastic"] += garbageInfo.plasticValue;
            PlayerManager.currentDayTrash["Metal"] += garbageInfo.metalValue;
            PlayerManager.currentDayTrash["Wood"] += garbageInfo.woodValue;
        }

        private bool CheckCanHoldWeight()
        {
            return garbageInfo.weight + PlayerManager.currentWeight <= PlayerManager.MaxWeight;
        }

        private void DisplayTooHeavy()
        {
            GameEventsManager.Instance.DialogueEvents.EnterDialogue("trashTooHeavy");
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
    }
}
