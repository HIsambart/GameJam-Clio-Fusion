using Interfaces;
using Julien.Script.PlayerScripts;
using Script.Input;
using UnityEngine;

public abstract class MiniGameTrigger : MonoBehaviour, IInteractable
{
    public GameObject PanelWarning;
    public bool IsNeedToPlay;
    public Player Player;
    public PlayerInputHandler PlayerInputHandler;
    
    public abstract void Interact(Player player);
    public abstract void GameStart();
    public abstract void GameOver();
}
