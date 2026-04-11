using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Julien.Script.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public float Speed;
        public Vector2 move;

        [SerializeField] private List<IInteractable> _interactables;
        [SerializeField] private Rigidbody _rigidbody;
        
        private void Update()
        {
            OnMove();
            // OnAim(aim);
        }
        
        public void OnMove()
        {
            Vector2 dir = move.normalized;
            
            //Debug.Log($"animationDir : {animationDir.x}, {animationDir.y}");

            Vector3 moveDirection = new Vector3(move.x, 0, move.y);
            _rigidbody.linearVelocity = moveDirection * Speed;
        }
        
        private void TurnIKBones()
        {
        }
        
        public void Interact()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<IInteractable>() != null)
            {
                _interactables.Add(other.gameObject.GetComponent<IInteractable>());
                if ( _interactables[0] != null)
                {
                   _interactables[0].Interact();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<IInteractable>() != null)
            {
                _interactables.Remove(other.gameObject.GetComponent<IInteractable>());
            }
        }
    }
}
