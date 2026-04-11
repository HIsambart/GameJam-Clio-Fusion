using Julien.Script.PlayerScripts;
using UnityEngine;

namespace MiniGames
{
    public class MiniGamePressureHandler : ContinueMiniGame
    {
        [ContextMenu("Interact")]
        public override void Interact(Player player)
        {
            CurrentLevel += AddAmount;
        }

        private void Start()
        {
            CurrentLevel = MinimumLevel;
        }

        private void Update()
        {
            CurrentLevel -= DecreaseSpeed * Time.deltaTime;
            
            float t = CurrentLevel / RangeLevel.y;
            float targetSize = Mathf.Lerp(RangeSize.x, RangeSize.y, t);
            TargetSize.localScale = new Vector3(targetSize, targetSize, targetSize);
        }
    }
}