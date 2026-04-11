using System;
using System.Collections;
using _Branch.Hugo.Scripts;
using Julien.Script.PlayerScripts;
using Script.Input;
using Unity.VisualScripting;
using UnityEngine;
using EventBus = Utils.EventBus;
using Random = UnityEngine.Random;

public class MiniGameCookHandler : MiniGameTrigger
{
    [SerializeField] private bool _gameStarted;
    
    public PlayerInputHandler PlayerInputHandler;
    
    public int DeuteriumCount;
    public int TriteriumCount;
    
    public int Deuterium;
    public int Triterium;

    public float MaxTime;
    [SerializeField] private float _currentTime;

    private void Awake()
    {
        EventBus.PutDeuterium += AddDeuterium;
        EventBus.PutTriterium += AddTriterium;
    }

    private void Update()
    {
        if(!_gameStarted) return;
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            GameOver();
        }
    }

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

        Deuterium = 0;
        Triterium = 0;

        _currentTime = MaxTime;
        _gameStarted = true;
        TokamakManager.Instance.IsTriggerMiniGame =  true;
        
        if (Player.GetComponent<PlayerInputCooking>() == null) Player.AddComponent<PlayerInputCooking>();
        PlayerInputHandler.enabled = false;
        Debug.Log("Game Cook start");
    }

    [ContextMenu("Game finish")]
    public override void GameOver()
    {
        Debug.Log("Game over");
        if (Deuterium == DeuteriumCount &&  Triterium == TriteriumCount)
        {
            GameWin();
        }
        else
        {
            Retry();
        }
        
    }

    public void Retry()
    {
        GameStart();
        Debug.Log("Retry");
    }

    public void GameWin()
    {
        Destroy(Player.GetComponent<PlayerInputCooking>());
        PlayerInputHandler.enabled = true;
        _gameStarted = false;
        TokamakManager.Instance.IsTriggerMiniGame =  false;
        Debug.Log("Game win");
    }

    private void AddDeuterium()
    {
        Deuterium += 1;
        if (Deuterium > DeuteriumCount)
        {
            Retry();
        }
    }

    private void AddTriterium()
    {
        Triterium += 1;
        if (Triterium > TriteriumCount)
        {
            Retry();
        }
    }
}
