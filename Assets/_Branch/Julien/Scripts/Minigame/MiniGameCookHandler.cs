using Julien.Script.PlayerScripts;
using Script.Input;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGameCookHandler : MiniGameTrigger
{
    public PlayerInputHandler PlayerInputHandler;
    
    public int DeuteriumCount;
    public int TriteriumCount;
    
    public int Deuterium;
    public int Triterium;
    
    public override void Interact(Player player)
    {
        Player = player;
        PlayerInputHandler = Player.GetComponent<PlayerInputHandler>();
        GameStart();
    }

    public override void GameStart()
    {
        DeuteriumCount = Random.Range(5, 11);
        TriteriumCount = Random.Range(5, 11);
        
        Player.AddComponent<PlayerInputCooking>();
        PlayerInputHandler.enabled = false;
        Debug.Log("Game Cook start");
    }

    [ContextMenu("Game finish")]
    public override void GameOver()
    {
        Destroy(Player.GetComponent<PlayerInputCooking>());
        PlayerInputHandler.enabled = true;
    }
}
