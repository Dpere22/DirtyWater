using System;
using Input;
using UnityEngine;

namespace Events
{
    public class InputEvents
    {
        public event Action<Vector2> MoveAction;
        public event Action JumpAction;
        
        public event Action OnInteractPressed;

        public InputEventContext InputEventContext { get; private set; } = InputEventContext.Default;

        public event Action<InputEventContext> OnSubmitPressed;

        public event Action OnInventoryPressed;

        public void ChangeInputEventContext(InputEventContext newContext)
        {
            InputEventContext = newContext;
        }

        public void PausePressed()
        {
            if (GameEventsManager.Instance.PauseEvents.IsPaused)
            {
                GameEventsManager.Instance.PauseEvents.Resume();
            }
            else
            {
                GameEventsManager.Instance.PauseEvents.Pause();
            }
        }

        public void MovePressed(Vector2 dir)
        {
            MoveAction?.Invoke(dir);
        }

        public void JumpPressed()
        {
            JumpAction?.Invoke();
        }

        public void InteractPressed()
        {
            OnInteractPressed?.Invoke();
        }

        public void SubmitPressed()
        {
            if (GameEventsManager.Instance.DayEvents.IsTransitioning) return;
            OnSubmitPressed?.Invoke(InputEventContext);
        }

        public void InventoryPressed()
        {
            OnInventoryPressed?.Invoke();
        }

        public event Action OnCancelPressed;
        public void CancelPressed()
        {
            OnCancelPressed?.Invoke();
        }
    }
}