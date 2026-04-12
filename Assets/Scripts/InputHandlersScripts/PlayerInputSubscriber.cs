using System;
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
            
            _player._playerInput.actions["Interact"].started += OnInteract;
            // _player._playerInput.actions["Interact"].canceled += OnInteract;

            // GameManager.PlayerPrefabs.Add(PlayerInputManager.playerPrefab.gameObject);
            // Debug.Log(PlayerInputManager.playerPrefab.gameObject+ " Join the game " );
        }
        
        private void OnDisable()
        {
            _player._playerInput.actions["Move"].performed -= OnMoving;
            
            _player._playerInput.actions["Interact"].started -= OnInteract;
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