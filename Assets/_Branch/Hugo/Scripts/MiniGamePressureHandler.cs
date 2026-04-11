using UnityEngine;

namespace _Branch.Hugo.Scripts
{
    public class MiniGamePressureHandler : ContinueMiniGame
    {
        [ContextMenu("Interact")]
        public override void Interact()
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
        }
    }
}