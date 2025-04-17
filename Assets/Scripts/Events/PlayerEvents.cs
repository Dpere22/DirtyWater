using System;
using UnityEngine;

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
            Debug.Log("Enabling Movement");
            OnEnablePlayerMovement?.Invoke();
        }

        public event Action OnPlayerSetWalk;

        public void SetPlayerWalking()
        {
            OnPlayerSetWalk?.Invoke();
        }

        public event Action OnPlayerSetSwim;

        public void SetPlayerSwimming()
        {
            OnPlayerSetSwim?.Invoke();
        }
    }
}
