using System;
using Input;
using UnityEngine;

namespace Events
{
    public class InputEvents
    {
        public event Action PauseGameAction;
        public event Action ResumeGameAction;

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
            if (PauseManager.GamePaused)
            {
                PauseManager.ResumeGame();
                ResumeGameAction?.Invoke();
            }
            else
            {
                PauseManager.PauseGame();
                PauseGameAction?.Invoke();
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
    }
}