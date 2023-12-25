using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : Singleton<WinUI>
{
    [SerializeField] private Button _nextBtn;
    [SerializeField] private Image _countCircle;
    [SerializeField] private TMP_Text _counter;

    public float timeDelay = 10f;
    private void Start()
    {
        _nextBtn.onClick.AddListener(Restart);
    }
    private void Restart()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
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
