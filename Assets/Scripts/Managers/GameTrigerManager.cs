using System.Collections.Generic;
using MiniGames;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    public class GameTrigerManager : MonoBehaviourSingleton<GameTrigerManager>
    {
        public List<MiniGameTrigger> MiniGameTriggers = new List<MiniGameTrigger>();

        public int GameWarningCount;
    
        [SerializeField] private float _maxTimeBefforMinigame;
        [SerializeField] private float _currentTimeBefforMinigame;

        private void Start()
        {
            SetTime();
        }

        private void Update()
        {
            if (GameWarningCount >= MiniGameTriggers.Count) return;
            _currentTimeBefforMinigame -= Time.deltaTime;
            
            TokamakManager.Instance.IsTriggerMiniGame = GameWarningCount > 0;
            if (_currentTimeBefforMinigame <= 0)
            {
                ChoiceMiniGame();
                SetTime();
            }
        }

        private void ChoiceMiniGame()
        {
            int index = Random.Range(0, MiniGameTriggers.Count);
            MiniGameTrigger game =  MiniGameTriggers[index];
            if (game.IsNeedToPlay)
            {
                ChoiceMiniGame();
            }
            else
            {
                game.IsNeedToPlay = true;
                game.PanelWarning.SetActive(true);
                GameWarningCount++;
            }
        }

        private void SetTime()
        {
            _maxTimeBefforMinigame = Random.Range(10f, 25f);
            _currentTimeBefforMinigame = _maxTimeBefforMinigame;
        }
    
    }
}
