using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class IsGrounded : MonoBehaviour
    {
        public List<Collider> colliders = new List<Collider>();
        [SerializeField] private Player _player;
    
        private void Start()
        {
            _player = GetComponentInParent<Player>();
        }

        private void Update()
        {
            if (colliders.Count <= 0)
            {
                _player.IsGrounded = false;
            }
            else
            {
                _player.IsGrounded = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                colliders.Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                colliders.Remove(other);
            }
        }
    }
}
