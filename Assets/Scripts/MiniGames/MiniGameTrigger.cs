using InputHandlersScripts;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace MiniGames
{
    public abstract class MiniGameTrigger : MonoBehaviour, IInteractable
    {
        public GameObject PanelWarning;
        public bool IsNeedToPlay;
        public PlayerScripts.Player Player;
        public PlayerInputSubscriber PlayerInputS;
    
        public abstract void Interact(PlayerScripts.Player player);
        public abstract void GameStart();
        public abstract void GameOver();
    }
}
