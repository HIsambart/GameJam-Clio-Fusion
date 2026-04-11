using System;
using Julien.Script.PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private bool _isControllerConnected;
        public static event Action<bool> OnInputDeviceChanged;
        
        private Player _player;
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
         
            _playerInput.actions["Move"].performed += OnMoving;
            _playerInput.actions["Move"].canceled += OnMoving;
            
            _playerInput.actions["Interact"].performed += OnInteract;
            _playerInput.actions["Interact"].canceled += OnInteract;

            // GameManager.PlayerPrefabs.Add(PlayerInputManager.playerPrefab.gameObject);
            // Debug.Log(PlayerInputManager.playerPrefab.gameObject+ " Join the game " );
        }
        
        private void OnDisable()
        {
            _playerInput.actions["Move"].performed -= OnMoving;
            
            _playerInput.actions["Interact"].performed -= OnInteract;
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
            OnInputDeviceChanged?.Invoke(_isControllerConnected);
            
            //Debug.Log(_isControllerConnected
            //? "Controller connected: Switching to Gamepad controls."
            //: "No controller connected: Switching to Keyboard/Mouse controls.");
        }
        
        private void OnMoving(InputAction.CallbackContext context)
        {
            _player.move = context.ReadValue<Vector2>();
        }
        
        private void OnInteract(InputAction.CallbackContext context)
        {
            _player.Interact();
        }
    }
}