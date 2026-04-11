using Interfaces;
using PlayerInputhandlers;
using UnityEngine;

namespace MiniGames
{
    public abstract class MiniGameTrigger : MonoBehaviour, IInteractable
    {
        public GameObject PanelWarning;
        public bool IsNeedToPlay;
        public Player.Player Player;
        public PlayerInputHandler PlayerInputHandler;
    
        public abstract void Interact(Player.Player player);
        public abstract void GameStart();
        public abstract void GameOver();
    }
}
