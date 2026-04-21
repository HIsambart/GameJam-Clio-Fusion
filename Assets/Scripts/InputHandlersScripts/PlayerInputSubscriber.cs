using PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputHandlersScripts
{
    public class PlayerInputSubscriber : MonoBehaviour
    {
        private bool _isControllerConnected;
        
        private Player _player;
        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
         
            _player._playerInput.actions["Move"].performed += OnMoving;
            _player._playerInput.actions["Move"].canceled += OnMoving;
            
            _player._playerInput.actions["Interact"].performed += OnInteracting;
            // _player._playerInput.actions["Interact"].canceled += OnInteract;
        }
        
        private void OnDisable()
        {
            _player._playerInput.actions["Move"].performed -= OnMoving;
            _player._playerInput.actions["Move"].canceled -= OnMoving;
            
            _player._playerInput.actions["Interact"].performed -= OnInteracting;
            // _player._playerInput.actions["Interact"].canceled -= OnInteract;
        }
    
        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
            { 
                DetectCurrentInputDevice();
            }
        }
        
        private void DetectCurrentInputDevice()
        {
            _isControllerConnected = Gamepad.all.Count > 0;
        }
        
        private void OnMoving(InputAction.CallbackContext context)
        {
            _player.move = context.ReadValue<Vector2>();
        }
        
        private void OnInteracting(InputAction.CallbackContext context)
        {
            _player.Interact();
        }
    }
}