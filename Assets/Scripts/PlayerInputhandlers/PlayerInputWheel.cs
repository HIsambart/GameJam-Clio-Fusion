using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputhandlers
{
    public class PlayerInputWheel : MonoBehaviour
    {
        public PlayerInput _playerInput;
        private bool _isControllerConnected;
        public static event Action<bool> OnInputDeviceChanged;
        
        public Player.Player _player;
        
        private void Start()
        {
            
        }

        private void OnEnable()
        {
            _playerInput = GetComponent<PlayerInput>();
            _player = GetComponent<Player.Player>();
            
            InputSystem.onDeviceChange += OnDeviceChange;
         
            _playerInput.actions["MoveWheel"].performed += OnMovingWheel;
            // GameManager.PlayerPrefabs.Add(PlayerInputManager.playerPrefab.gameObject);
            // Debug.Log(PlayerInputManager.playerPrefab.gameObject+ " Join the game " );
        }
        
        private void OnDisable()
        {
            _playerInput.actions["MoveWheel"].performed -= OnMovingWheel;
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
        
        private void OnMovingWheel(InputAction.CallbackContext context)
        {
            _player.MoveWheel(context.ReadValue<Vector2>().x);
        }
    }
}