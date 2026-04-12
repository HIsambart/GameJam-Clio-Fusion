using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputHandlersScripts
{
    public class PlayerInputWheel : MonoBehaviour
    {
        private bool _isControllerConnected;
        
        public PlayerScripts.Player _player;

        private void OnEnable()
        {
            _player = GetComponent<PlayerScripts.Player>();
            
            InputSystem.onDeviceChange += OnDeviceChange;
         
            _player._playerInput.actions["MoveWheel"].performed += OnMovingWheel;
            // GameManager.PlayerPrefabs.Add(PlayerInputManager.playerPrefab.gameObject);
            // Debug.Log(PlayerInputManager.playerPrefab.gameObject+ " Join the game " );
        }
        
        private void OnDisable()
        {
            _player._playerInput.actions["MoveWheel"].performed -= OnMovingWheel;
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
        
        private void OnMovingWheel(InputAction.CallbackContext context)
        {
            _player.MoveWheel(context.ReadValue<Vector2>().x);
        }
    }
}