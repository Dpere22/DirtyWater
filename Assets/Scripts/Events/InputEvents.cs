using System;
using UnityEngine;

namespace Events
{
    public class InputEvents
    {
        public event Action PauseGameAction;
        public event Action ResumeGameAction;

        public event Action<Vector2> MoveAction;
        public event Action JumpAction;
        
        public event Action InteractAction;

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
            InteractAction?.Invoke();
        }
    }
}