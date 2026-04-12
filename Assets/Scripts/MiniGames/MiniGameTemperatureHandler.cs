using UnityEngine;
using DG.Tweening;

namespace MiniGames
{
    public class MiniGameTemperatureHandler : ContinueMiniGame
    {
        [ContextMenu("Interact")]
        public override void Interact(PlayerScripts.Player player)
        {
            if (player.EquipedCollectable)
            {
                Destroy(player.EquipedCollectable.gameObject);
                player.EquipedCollectable = null;
                
                CurrentLevel += AddAmount;
                
                if (PanelUI.activeInHierarchy) PanelUI.SetActive(false);
            }
        }
        
        private void Start()
        {
            CurrentLevel = MinimumLevel;
            
            if (Material != null)
            {
                Material.EnableKeyword("_EMISSION");
                Material.SetColor("_EmissionColor", _defaultEmissionColor);
            }
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