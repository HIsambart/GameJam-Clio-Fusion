using InputHandlersScripts;
using Interfaces;
using UnityEngine;

namespace MiniGames
{
    public abstract class MiniGameTrigger : MonoBehaviour, IInteractable
    {
        public bool AsPlayedOneTime;
        
        public GameObject PanelWarning;
        public GameObject PanelTutoriel;
        public bool IsNeedToPlay;
        public PlayerScripts.Player Player;
        public PlayerInputSubscriber PlayerInputS;
    
        public abstract void Interact(PlayerScripts.Player player);
        public abstract void GameStart();
        public abstract void GameOver();
        public abstract void PlayTutoriel();
    }
}
