using Interfaces;
using UnityEngine;

namespace _Branch.Hugo.Scripts
{
    public class DebrisHandler : MonoBehaviour, ICollectable
    {
        [Header("===== DEBUG =====")]
        [SerializeField] private Transform _target;
        
        public void Collect(Transform parent)
        {
            _target = parent;
        }

        public void Drop()
        {
            _target = null;
        }

        private void Update()
        {
            if (_target) transform.position = _target.position;
        }
    }
}