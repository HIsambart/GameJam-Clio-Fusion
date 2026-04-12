using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Debris
{
    public class DebrisHandler : MonoBehaviour, ICollectable
    {
        [Header("===== VISUAL =====")]
        [SerializeField] private List<GameObject> _debrisVisual = new();
        
        [Header("===== DEBUG =====")]
        [SerializeField] private Transform _target;

        private void Awake()
        {
            int index = Random.Range(0, _debrisVisual.Count);
            _debrisVisual[index].SetActive(true);
        }

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