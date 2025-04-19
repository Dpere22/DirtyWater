using Events;
using Input;
using UnityEngine;

namespace UI
{
    public class FlavorText : MonoBehaviour
    {
        private bool _inRange;
        public string filename;

        private void OnEnable()
        {
            GameEventsManager.Instance.InputEvents.OnSubmitPressed += InteractText;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.InputEvents.OnSubmitPressed -= InteractText;

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

        private void InteractText(InputEventContext context)
        {
            if (context != InputEventContext.Default) return;
            if (!_inRange) return;
            if (filename == "") return;
            GameEventsManager.Instance.DialogueEvents.EnterDialogue(filename);
        
        }
    }
}
