using DG.Tweening;
using Interfaces;
using PlayerScripts;
using UnityEngine;

namespace MiniGames
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
        public Vector2 RangeSize;
        
        [Header("===== MATERIAL =====")]
        public Material Material;
        
        protected Tween _alertTween;
        protected bool _isAlerting;
        protected Color _defaultEmissionColor = Color.red * 50f;
        
        public abstract void Interact(Player player);
    }
}