using Events;
using Input;
using UnityEngine;

namespace Interactables
{
    public class Chest : MonoBehaviour
    {
        private bool _inRange;
        private bool _questActive;
        private bool _finished;

        private void OnEnable()
        {
            GameEventsManager.Instance.InputEvents.OnSubmitPressed += HandleSubmit;
            GameEventsManager.Instance.QuestEvents.OnActivateChest += ActivateChest;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.InputEvents.OnSubmitPressed -= HandleSubmit;
            GameEventsManager.Instance.QuestEvents.OnActivateChest -= ActivateChest;
        }

        private void ActivateChest()
        {
            _questActive = true;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            _inRange = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            _inRange = false;
        }

        private void HandleSubmit(InputEventContext context)
        {
            if (context != InputEventContext.Default) return;
            if (!_inRange) return;
            if (_finished)
            {
                GameEventsManager.Instance.DialogueEvents.EnterDialogue("player_already_open_chest");
                return;
            }
            if (!_questActive)
            {
                GameEventsManager.Instance.DialogueEvents.EnterDialogue("player_cannot_open_chest");
                return;
            }
            _finished = true;
            GameEventsManager.Instance.SoundEvents.PlaySoundEffect("key");
            GameEventsManager.Instance.DialogueEvents.EnterDialogue("player_collect_chest");
            GameEventsManager.Instance.QuestEvents.GetChest();
        }
    }
}
