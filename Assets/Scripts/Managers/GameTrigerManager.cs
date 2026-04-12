using System.Collections.Generic;
using DG.Tweening;
using MiniGames;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    public class GameTrigerManager : MonoBehaviourSingleton<GameTrigerManager>
    {
        public List<MiniGameTrigger> MiniGameTriggers = new();

        public int GameWarningCount;
    
        [SerializeField] private float _maxTimeBefforMinigame;
        [SerializeField] private float _currentTimeBefforMinigame;
        
        [Header("===== MATERIAL REFERENCES =====")]
        [SerializeField] private Material _lightWheel;
        [SerializeField] private Material _lightCoocking;

        private readonly Color _defaultEmissionColor = Color.red * 20f;

        private void Start()
        {
            SetTime();
            ResetMaterials();
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
                if (GameWarningCount > MiniGameTriggers.Count) return;
                ChoiceMiniGame();
            }
            else
            {
                game.IsNeedToPlay = true;
                game.PanelWarning.SetActive(true);
                if (!game.AsPlayedOneTime) game.PlayTutoriel();
                GameWarningCount++;
                AudioManager.Instance.PlaySound(AudioManager.Instance.BruitWarning);
                

                if (index == 0)
                {
                    _lightCoocking.DOColor(Color.red * 500f, "_EmissionColor", 0.2f)
                        .SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.InOutSine);
                }
                else if (index == 1)
                {
                    _lightWheel.DOColor(Color.red * 500f, "_EmissionColor", 0.2f)
                        .SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.InOutSine);
                }
            }
        }

        private void SetTime()
        {
            _maxTimeBefforMinigame = Random.Range(10f, 25f);
            _currentTimeBefforMinigame = _maxTimeBefforMinigame;
        }

        private void ResetMaterialEmission(Material mat)
        {
            mat.DOKill();
            mat.SetColor("_EmissionColor", _defaultEmissionColor);
        }

        private void ResetMaterials()
        {
            _lightWheel.EnableKeyword("_EMISSION");
            _lightCoocking.EnableKeyword("_EMISSION");
            ResetMaterialEmission(_lightWheel);
            ResetMaterialEmission(_lightCoocking);
        }

        #region ===== EVENTS =====

        private void OnEnable()
        {
            EventBus.OnCoockingWin += OnCoockingWin;
            EventBus.OnWheelWin += OnWheelWin;
        }

        private void OnWheelWin()
        {
            ResetMaterialEmission(_lightWheel);
        }

        private void OnCoockingWin()
        {
            ResetMaterialEmission(_lightCoocking);
        }
        
        private void OnDisable()
        {
            EventBus.OnCoockingWin -= OnCoockingWin;
            EventBus.OnWheelWin -= OnWheelWin;
        }

        #endregion
    }
}