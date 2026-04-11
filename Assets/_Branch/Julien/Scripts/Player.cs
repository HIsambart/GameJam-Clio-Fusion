using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Julien.Script.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public float Speed;
        public Vector2 move;

        [SerializeField] private List<GameObject> _iCollectables = new List<GameObject>();
        public GameObject EquipedCollectable;
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _handTransform;
        
        private void Update()
        {
            OnMove();
        }
        
        public void OnMove()
        {
            Vector3 moveDirection = new Vector3(move.x, 0, move.y);
            _rigidbody.linearVelocity = moveDirection * Speed;
        }
        
        public void Interact()
        {
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
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<ICollectable>() != null)
            {
                _iCollectables.Remove(other.gameObject);
            }
        }
    }
}
