using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayingUI : Singleton<PlayingUI>
{
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private TMP_Text _botCountTxt;
    [SerializeField] private GameObject _guideBox;

    private float _timeTurnOffGuide;
    public int botCount;
    private void Awake()
    {
        _timeTurnOffGuide = 5f;
        SetBotCount(LevelManager.Instance.botMax);
        _pauseBtn.onClick.AddListener(OnPausedUI);
    }

    private void OnPausedUI()
    {
        GameManager.Instance.ChangeGameState(GameState.Paused);
    }

    private void OnEnable()
    {
        StartCoroutine(TurnOffGuide());
    }
    IEnumerator TurnOffGuide()
    {
        yield return new WaitForSeconds(_timeTurnOffGuide);
        _guideBox.SetActive(false);
    }
    public void SetBotCount(int botAlive)
    {
        _botCountTxt.text = $"{Constants.ALIVE_TXT} {botAlive}";
        this.botCount = botAlive;
        if (botCount != 1) return;
        GameManager.Instance.ChangeGameState(GameState.Win);
    }
}
