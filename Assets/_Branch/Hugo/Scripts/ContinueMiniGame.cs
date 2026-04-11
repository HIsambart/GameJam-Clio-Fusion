using Interfaces;
using UnityEngine;

namespace _Branch.Hugo.Scripts
{
    public abstract class ContinueMiniGame : MonoBehaviour, IInteractable
    {
        [Header("===== DATA =====")]
        public Vector2 RangeLevel;
        public float MinimumLevel;
        
        [Range(0f, 100f)] public  float CurrentLevel;

        [Header("===== SETTINGS =====")]
        public float DecreaseSpeed;
        public float AddAmount;

        [Header("===== REFERENCES =====")]
        public Transform TargetSize;
        
        public abstract void Interact();
    }
}