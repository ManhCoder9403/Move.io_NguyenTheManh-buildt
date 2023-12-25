using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState _currentGameState;
    private PlayerData _playerData;
    private bool isSoundEnabled;
    public PlayerData PlayerData { get => _playerData; set => _playerData = value; }
    public bool IsSoundEnabled { get => isSoundEnabled; set => isSoundEnabled = value; }
    private void Awake()
    {
        isSoundEnabled = true;
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (DynamicDataManager.Instance.ExistData<PlayerData>())
        {
            _playerData = DynamicDataManager.Instance.LoadData<PlayerData>();
        }
        else
        {
            _playerData = new PlayerData();
            DynamicDataManager.Instance.SaveData<PlayerData>(_playerData);
        }
        ChangeGold(0);
    }
    public bool IsState(GameState state) => _currentGameState == state;
    private void Start()
    {
        ChangeGameState(GameState.Home);
    }
    internal void ChangeGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Home:
                //Change UI
                UIManager.Instance.OnHomeState();
                //Disable joystick
                LevelManager.Instance.joystick.gameObject.SetActive(false);
                DynamicDataManager.Instance.SaveData<PlayerData>(_playerData);
                break;
            case GameState.Playing:
                //Change UI
                UIManager.Instance.OnPlayingState();
                //Change Camera Position
                LevelManager.Instance.SetCamera();
                //Enable joystick
                LevelManager.Instance.joystick.gameObject.SetActive(true);
                break;
            case GameState.Paused:
                //Change UI
                UIManager.Instance.OnPauseState();
                break;
            case GameState.GameOver:
                //Change UI
                SoundManager.Instance.PlaySFX(SFXType.Lose);
                UIManager.Instance.OnGameOverState();
                break;
            case GameState.WeaponShop:
                //Change UI
                UIManager.Instance.OnWeaponShopState();
                break;
            case GameState.SkinShop:
                //Change UI
                UIManager.Instance.OnSkinShopState();
                break;
            case GameState.PayToWin:
                //Change UI
                UIManager.Instance.OnPayToWinState();
                break;
            case GameState.Win:
                SoundManager.Instance.PlaySFX(SFXType.Win);
                UIManager.Instance.OnGameOverState();
                break;
            default:
                UIManager.Instance.BackHomeState();
                break;
        }
        _currentGameState = gameState;
    }
    public void ChangeGold(int gold)
    {
        _playerData.Gold += gold;
        DynamicDataManager.Instance.SaveData<PlayerData>(_playerData);
        HomeUI.Instance.GoldTxt.text = _playerData.Gold.ToString();
    }
}
