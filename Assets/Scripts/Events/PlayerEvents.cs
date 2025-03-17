using System;

namespace Events
{
    public class PlayerEvents
    {
        public event Action OnDisablePlayerMovement;

        public void DisablePlayerMovement()
        {
            OnDisablePlayerMovement?.Invoke();
        }
        
        public event Action OnEnablePlayerMovement;

        public void EnablePlayerMovement()
        {
            OnEnablePlayerMovement?.Invoke();
        }
    }
}
