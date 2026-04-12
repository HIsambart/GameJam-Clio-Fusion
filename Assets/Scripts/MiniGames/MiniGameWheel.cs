using InputHandlersScripts;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using EventBus = Utils.EventBus;

namespace MiniGames
{
    public class MiniGameWheel : MiniGameTrigger
    {
        [SerializeField] private bool _gameStarted;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fillBar;

        [SerializeField] private float _maxTimerInCenter;
        [SerializeField] private float _currentTimerInCenter;
    
        [Range(0, 100)] public float WheelValue;
        [SerializeField] private Vector2 _center;
    
        private void Awake()
        {
            EventBus.MovingWheel += MovingValueWheel;
        }

        private void Update()
        {
            if (!_gameStarted) return;
            MovingValueWheel(Player.MoveWheelDirection);
            _slider.value = WheelValue / 100;
            if (WheelValue > _center.x && WheelValue < _center.y)
            {
                _currentTimerInCenter += Time.deltaTime;
                _fillBar.fillAmount = _currentTimerInCenter / _maxTimerInCenter;
                if (_currentTimerInCenter >= _maxTimerInCenter) GameOver();
            }
            else
            {
                _fillBar.fillAmount = 0;
                _currentTimerInCenter = 0;
            }
        }

        public override void Interact(PlayerScripts.Player player)
        {
            if(!IsNeedToPlay) return;
            Player = player;
            PanelWarning.SetActive(false);
            PlayerInputS = Player.GetComponent<PlayerInputSubscriber>();
            GameStart();
        }

        public override void GameStart()
        {
            if (Player.GetComponent<PlayerInputWheel>() == null) Player.AddComponent<PlayerInputWheel>();
            PlayerInputS.enabled = false;
            _gameStarted = true;
            _currentTimerInCenter = _maxTimerInCenter;
            _panel.SetActive(true);
            WheelValue = 0;
        }

        [ContextMenu("End game")]
        public override void GameOver()
        {
            Destroy(Player.GetComponent<PlayerInputCooking>());
            PlayerInputS.enabled = true;
            _gameStarted = false;
            _panel.SetActive(false);
            IsNeedToPlay = false;
            GameTrigerManager.Instance.GameWarningCount--;
            
            EventBus.OnWheelWin?.Invoke();
        }

        private void MovingValueWheel(float value)
        {
            WheelValue += value * Time.deltaTime;
            WheelValue = Mathf.Clamp(WheelValue, 0, 100);
        }
    }
}
