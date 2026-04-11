using System;
using _Branch.Hugo.Scripts;
using Julien.Script.PlayerScripts;
using Script.Input;
using Unity.VisualScripting;
using UnityEngine;
using EventBus = Utils.EventBus;

public class MiniGameWheel : MiniGameTrigger
{
    [SerializeField] private bool _gameStarted;
    
    public PlayerInputHandler PlayerInputHandler;

    [SerializeField] private float _maxTimerInCenter;
    [SerializeField] private float _currentTimerInCenter;
    
    [Range(-100, 100)] public float WheelValue;
    [SerializeField] private Vector2 _center;
    
    private void Awake()
    {
        EventBus.MovingWheel += MovingValueWheel;
    }

    private void Update()
    {
        if (!_gameStarted) return;
        MovingValueWheel(Player.MoveWheelDirection);
        if (WheelValue > _center.x && WheelValue < _center.y)
        {
            Debug.Log("Baisse le temp");
            _currentTimerInCenter -= Time.deltaTime;
            if (_currentTimerInCenter <= 0) GameOver();
        }
        else
        {
            Debug.Log("Recomence");
            _currentTimerInCenter = _maxTimerInCenter;
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
        if (Player.GetComponent<PlayerInputWheel>() == null) Player.AddComponent<PlayerInputWheel>();
        PlayerInputHandler.enabled = false;
        _gameStarted = true;
        TokamakManager.Instance.IsTriggerMiniGame = true;
        _currentTimerInCenter = _maxTimerInCenter;
        WheelValue = -100;
        Debug.Log("Game Cook start");
    }

    [ContextMenu("End game")]
    public override void GameOver()
    {
        Destroy(Player.GetComponent<PlayerInputCooking>());
        PlayerInputHandler.enabled = true;
        TokamakManager.Instance.IsTriggerMiniGame = false;
        _gameStarted = false;
        Debug.Log("Game finish");
    }

    private void MovingValueWheel(float value)
    {
        WheelValue += value;
        WheelValue = Mathf.Clamp(WheelValue, -100, 100);
    }
}
