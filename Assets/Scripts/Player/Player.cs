using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using EventBus = Utils.EventBus;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public float Speed;
        public Vector2 move;
        public float MoveWheelDirection;
        
        
        [SerializeField] private List<GameObject> _iCollectables = new List<GameObject>();
        [SerializeField] private List<GameObject> _iInteractables = new List<GameObject>();
        
        public GameObject MiniGameInteractable;
        public GameObject EquipedCollectable;
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _handTransform;
        
        private Vector3 _lastDirection;
        
        private void Update()
        {
            OnMove();
            
            _iCollectables.RemoveAll(collider => collider == null);
            _iInteractables.RemoveAll(collider => collider == null);
        }
        
        public void OnMove()
        {
            Vector3 moveDirection = new Vector3(move.x, 0, move.y);

            // Conserver la vélocité horizontale
            Vector3 currentVelocity = _rigidbody.linearVelocity;
            Vector3 newVelocity = new Vector3(moveDirection.x * Speed, currentVelocity.y, moveDirection.z * Speed);

            _rigidbody.linearVelocity = newVelocity;

            // Si on a une direction, on la sauvegarde
            if (moveDirection != Vector3.zero)
            {
                _lastDirection = moveDirection;
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
            else if (_lastDirection != Vector3.zero)
            {
                // Sinon on garde la dernière rotation
                transform.rotation = Quaternion.LookRotation(_lastDirection);
            }
        }
        
        public void Interact()
        {
            if (_iInteractables.Count > 0)
            {
                _iInteractables[0].GetComponent<IInteractable>().Interact(this);
                Debug.Log("Interact with game / " + _iInteractables[0].name);
                return;
            }
            
            if (EquipedCollectable)
            {
                Debug.Log("drop");
                EquipedCollectable.GetComponent<ICollectable>().Drop();
                EquipedCollectable = null;
                return;
            }
            
            if (_iCollectables.Count > 0)
            {
                Debug.Log("Collect");
                EquipedCollectable = _iCollectables[0];
                EquipedCollectable.GetComponent<ICollectable>().Collect(_handTransform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<ICollectable>() != null)
            {
                _iCollectables.Add(other.gameObject);
            }

            if (other.gameObject.GetComponent<IInteractable>() != null)
            {
                _iInteractables.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<ICollectable>() != null)
            {
                _iCollectables.Remove(other.gameObject);
            }

            if (other.gameObject.GetComponent<IInteractable>() != null)
            {
                _iInteractables.Remove(other.gameObject);
            }
        }
        
        // Mini game Cooking methodes
        public void PutDeuterium()
        {
            EventBus.PutDeuterium?.Invoke();
            Debug.Log("PutDeuterium");
        }
        
        public void PutTriterium()
        {
            EventBus.PutTriterium?.Invoke();
            Debug.Log("PutTriterium");
        }
        
        // Mini game move Wheel

        public void MoveWheel(float context)
        {
            MoveWheelDirection = context;
            if (MoveWheelDirection > -0.2f && MoveWheelDirection < 0) MoveWheelDirection = -0.2f;
            if (MoveWheelDirection < 0.2f && MoveWheelDirection > 0) MoveWheelDirection = 0.2f;
        }
        
    }
}
