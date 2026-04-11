using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Julien.Script.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public float Speed;
        public Vector2 move;

        [SerializeField] private List<GameObject> _iCollectables = new List<GameObject>();
        [SerializeField] private GameObject _equipedCollectable;
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _handTransform;
        
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
        
        public void Interact()
        {
            if (_equipedCollectable)
            {
                Debug.Log("drop");
                _equipedCollectable.GetComponent<ICollectable>().Drop();
                _equipedCollectable = null;
                return;
            }
            
            if (_iCollectables.Count > 0)
            {
                Debug.Log("Collect");
                _equipedCollectable = _iCollectables[0];
                _equipedCollectable.GetComponent<ICollectable>().Collect(_handTransform);
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
