using DG.Tweening;
using UnityEngine;

namespace MiniGames
{
    public class MiniGamePressureHandler : ContinueMiniGame
    {
        [Header("===== PUMP =====")]
        [SerializeField] private Transform _pumpTransform;
        [SerializeField] private float _pumpSpeed = 0.5f;
        [SerializeField] private float _moveAmount = 0.5f;

        private Tween _pumpTween;
        private Vector3 _initialPumpPosition;

        private void Start()
        {
            CurrentLevel = MinimumLevel;
            _initialPumpPosition = _pumpTransform.position;
        }

        [ContextMenu("Interact")]
        public override void Interact(PlayerScripts.Player player)
        {
            CurrentLevel += AddAmount;

            if (_pumpTween != null && _pumpTween.IsActive())
            {
                _pumpTween.Kill();
            }

            _pumpTransform.position = _initialPumpPosition;

            _pumpTween = _pumpTransform.DOMoveY(_initialPumpPosition.y - _moveAmount, _pumpSpeed)
                .SetEase(Ease.OutQuad)
                .SetLoops(2, LoopType.Yoyo);
        }

        private void Update()
        {
            CurrentLevel -= DecreaseSpeed * Time.deltaTime;
            CurrentLevel = Mathf.Clamp(CurrentLevel, RangeLevel.x, RangeLevel.y);
            
            HandleAlertVisuals();
            
            float t = CurrentLevel / RangeLevel.y;
            float targetSize = Mathf.Lerp(RangeSize.x, RangeSize.y, t);
            foreach (var target in TargetsSize)
            {
                target.localScale = new Vector3(targetSize, targetSize, targetSize);
            }
        }
        
        private void HandleAlertVisuals()
        {
            if (Material == null) return;

            if (CurrentLevel < MinimumLevel && !_isAlerting)
            {
                _isAlerting = true;
                _alertTween = Material.DOColor(Color.red * 500f, "_EmissionColor", 0.2f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            }
            else if (CurrentLevel >= MinimumLevel && _isAlerting)
            {
                _isAlerting = false;
                Material.DOKill();
                Material.SetColor("_EmissionColor", _defaultEmissionColor);
            }
        }

        private void OnDestroy()
        {
            if (Material != null) Material.DOKill();
        }
    }
}