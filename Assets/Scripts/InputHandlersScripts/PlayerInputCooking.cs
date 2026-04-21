using UnityEngine;
using UnityEngine.InputSystem;

namespace InputHandlersScripts
{
    public class PlayerInputCooking : MonoBehaviour
    {
        private bool _isControllerConnected;
        
        public PlayerScripts.Player _player;

        private void OnEnable()
        {
            _player = GetComponent<PlayerScripts.Player>();
            
            InputSystem.onDeviceChange += OnDeviceChange;
         
            _player._playerInput.actions["CookDeuterium"].performed += OnPutDeuterium;
            
            _player._playerInput.actions["CookTriterium"].performed += OnPutTriterium;
        }
        
        private void OnDisable()
        {
            _player._playerInput.actions["CookDeuterium"].performed -= OnPutDeuterium;
            
            _player._playerInput.actions["CookTriterium"].performed -= OnPutTriterium;
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
        
        private void OnPutDeuterium(InputAction.CallbackContext context)
        {
            _player.PutDeuterium();
        }
        
        private void OnPutTriterium(InputAction.CallbackContext context)
        {
            _player.PutTriterium();
        }
    }
}