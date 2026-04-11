using UnityEngine;

namespace MiniGames
{
    public class MiniGameTemperatureHandler : ContinueMiniGame
    {
        [ContextMenu("Interact")]
        public override void Interact(Player.Player player)
        {
            if (player.EquipedCollectable)
            {
                Destroy(player.EquipedCollectable);
                player.EquipedCollectable = null;
                
                CurrentLevel += AddAmount;
            }
        }
        
        private void Start()
        {
            CurrentLevel = MinimumLevel;
        }

        private void Update()
        {
            CurrentLevel -= DecreaseSpeed * Time.deltaTime;
            CurrentLevel = Mathf.Clamp(CurrentLevel, RangeLevel.x, RangeLevel.y);
            
            float t = CurrentLevel / RangeLevel.y;
            float targetSize = Mathf.Lerp(RangeSize.x, RangeSize.y, t);
            TargetSize.localScale = new Vector3(targetSize, targetSize, targetSize);
        }
    }
}