using System;
using Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInput playerInput;
        
        private void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            playerInput.onControlsChanged += OnControlsChanged;
        }

        private void OnControlsChanged(PlayerInput pInput)
        {
            ControllerInfo.ControllerName = pInput.currentControlScheme;
        }
        private void OnPause()
        {
            GameEventsManager.Instance.InputEvents.PausePressed();
        }
        public void OnMove(InputValue value)
        {
            GameEventsManager.Instance.InputEvents.MovePressed(value.Get<Vector2>());
        }

        public void OnJump(InputValue value)
        {
            GameEventsManager.Instance.InputEvents.JumpPressed();
        }
    
        public void OnInteract(InputValue value)
        {
            GameEventsManager.Instance.InputEvents.InteractPressed();
        }

        public void OnSubmit(InputValue value)
        {
            GameEventsManager.Instance.InputEvents.SubmitPressed();
        }

        public void OnOpenInventory(InputValue value)
        {
            GameEventsManager.Instance.InputEvents.InventoryPressed();
        }

        public void OnCancel(InputValue value)
        {
            GameEventsManager.Instance.InputEvents.CancelPressed();
        }
    
    }
}
