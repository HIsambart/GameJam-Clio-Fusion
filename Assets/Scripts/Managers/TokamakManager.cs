using System.Collections.Generic;
using MiniGames;
using UnityEngine;
using Utils;

namespace Managers
{
    public class TokamakManager : MonoBehaviourSingleton<TokamakManager>
    {
        [Header("===== DATA =====")]
        [Header("LEVEL")]
        public Vector2 RangeStability;
        public float LooseStabilityLevel;
        public float WinStabilityLevel;
        
        [Range(0f, 100f)] public float CurrentStabilityLevel;
        
        [Header("Speed")]
        public float IncreaseSpeed;
        public float DecreaseSpeed;
        
        [Header("===== REFERENCES =====")]
        public List<ContinueMiniGame> ContinueMiniGames = new();

        [Header("===== DEBUG =====")]
        public bool IsTriggerMiniGame;

        private void Awake()
        {
            CurrentStabilityLevel = RangeStability.y /2;
        }

        private void Update()
        {
            if (CurrentStabilityLevel > WinStabilityLevel)
            {
                GameManager.Instance.WinGame();
                return;
            }
            else if (CurrentStabilityLevel < LooseStabilityLevel)
            {
                GameManager.Instance.LoseGame();
                return;
            }
            
            if (IsTriggerMiniGame)
            {
                CurrentStabilityLevel -= DecreaseSpeed * Time.deltaTime;
                return;
            }
            
            foreach (var miniGame in ContinueMiniGames)
            {
                if (miniGame.CurrentLevel >= miniGame.MinimumLevel)
                {
                    CurrentStabilityLevel += IncreaseSpeed * Time.deltaTime;
                }
                else
                {
                    CurrentStabilityLevel -= DecreaseSpeed * Time.deltaTime;
                }
            }
        }
    }
}