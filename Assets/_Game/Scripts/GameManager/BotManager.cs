
using System.Collections.Generic;
using UnityEngine;

public class BotManager : Singleton<BotManager>
{
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private ObjectPool<Bot> _botPool;

    private bool _canSpawn => PlayingUI.Instance.botCount + _botPool.DeactiveObjectCount() > botMax + 1 || PlayingUI.Instance.botCount == 0 || GameManager.Instance.IsState(GameState.GameOver) || GameManager.Instance.IsState(GameState.Win);
    private float _spawnRange = 50f;
    public int botMax = 10;

    private void Awake()
    {
        _botPool = new ObjectPool<Bot>(_botPrefab.gameObject, this.transform, _spawnRange, botMax);
    }
    public void PushBotBack(Bot bot)
    {
        _botPool.PushObjectBack(bot);
        if (!_canSpawn) return;
        _botPool.Respawn(_spawnRange);
    }

}
