using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _startPos;
    [SerializeField] private LevelDataManager _levelDataManager;

    public Player Player;
    public int botMax;
    public Joystick joystick;
    public int currentLevel;
    void Awake()
    {
        botMax = _levelDataManager.GetLevelData(currentLevel).maxBot;
        //Instantiate(Resources.Load($"{Constants.LEVEL_TXT}{currentLevel}"));
        Player = Instantiate(_playerPrefab, _startPos.position, Quaternion.identity);
    }
    public void SetCamera()
    {
        CameraFollow.Instance.target = Player?.transform;
    }
}
