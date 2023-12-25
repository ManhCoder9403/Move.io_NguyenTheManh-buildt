using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : Singleton<GameOverUI>
{
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Image _countCircle;
    [SerializeField] private TMP_Text _counter;

    public float timeDelay = 5f;
    private void Start()
    {
        _continueBtn.onClick.AddListener(Restart);
    }
    private void Restart()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
        PlayingUI.Instance.SetBotCount(PlayingUI.Instance.botCount + 1);
        GameManager.Instance.ChangeGameState(GameState.Playing);
        LevelManager.Instance.Player.ChangeAnim(Action.Idle);
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(BackToHome());
    }
    IEnumerator BackToHome()
    {
        while (timeDelay >= 0)
        {
            _countCircle.transform.Rotate(Time.deltaTime * Vector3.back * 360);
            _counter.text = ((int)(timeDelay - Time.deltaTime)).ToString();
            timeDelay -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        GameManager.Instance.ChangeGameState(GameState.None);
        UIManager.Instance.ReloadState();
    }
}
