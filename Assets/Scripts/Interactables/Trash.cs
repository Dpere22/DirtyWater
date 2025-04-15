using Events;
using Input;
using UnityEngine;
using System.Collections.Generic;

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
            if (!GameEventsManager.Instance.TrashManager.CheckCanCollect(garbageInfo.garbageId))
            {
                DisplayCannotCollect();
                return;
            }

            if (!CheckCanHoldWeight())
            {
                DisplayTooHeavy();
                return;
            }
            PlayCollectSound();
            GameEventsManager.Instance.PlayerManager.CurrentWeight += garbageInfo.weight;
            OceanHealth.AddHealth(garbageInfo.weight);
            AddItemToCurrentDayInventory();
            if(gameObject)
                Destroy(gameObject);
        }

        private void PlayCollectSound()
        {
            int choice = Random.Range(0, 2);
            if (choice == 1)
            {
                GameEventsManager.Instance.SoundEvents.PlaySoundEffect("bubble_pop");
            }
            else
            {
                GameEventsManager.Instance.SoundEvents.PlaySoundEffect("bubble_pop2");
            }
        }

        private void AddItemToCurrentDayInventory()
        {
            List<string> trashinfo = new List<string> {garbageInfo.garbageId, garbageInfo.plasticValue.ToString(), garbageInfo.woodValue.ToString(), garbageInfo.metalValue.ToString()};

            GameEventsManager.Instance.PlayerManager.GarbageInInventory.Add(trashinfo);
        }

        private bool CheckCanHoldWeight()
        {
            return garbageInfo.weight + GameEventsManager.Instance.PlayerManager.CurrentWeight <= GameEventsManager.Instance.PlayerManager.MaxWeight;
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
