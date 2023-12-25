using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : Singleton<PauseUI>
{
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _soundSwitchBtn;
    [SerializeField] private Image _soundBtnImage;
    [SerializeField] private Button _vibraSwitchBtn;
    [SerializeField] private Image _vibraBtnImage;
    [SerializeField] private Sprite _spriteOn;
    [SerializeField] private Sprite _spriteOff;

    private Transform _soundBtnTransform;
    private Transform _vibraBtnTransform;
    private float _duration = 0.2f;
    private bool _soundOn;
    private bool _vibraOn;

    private void Start()
    {
        _homeBtn.onClick.AddListener(BackHomeState);
        _continueBtn.onClick.AddListener(ContinueState);
        _soundSwitchBtn.onClick.AddListener(SwitchSound);
        _vibraSwitchBtn.onClick.AddListener(SwitchVibration);
    }
    private void OnEnable()
    {
        _soundBtnTransform = _soundSwitchBtn.transform;
        _vibraBtnTransform = _vibraSwitchBtn.transform;
    }
    private void SwitchVibration()
    {
        _vibraBtnTransform.DOLocalMoveX(-_vibraBtnTransform.localPosition.x, _duration);
        _vibraBtnImage.sprite = (_vibraOn) ? _spriteOn : _spriteOff;
        _vibraOn = (_vibraOn) ? false : true;
    }
    private void SwitchSound()
    {
        _soundBtnTransform.DOLocalMoveX(-_soundBtnTransform.localPosition.x, _duration);
        _soundBtnImage.sprite = (_soundOn) ? _spriteOn : _spriteOff;
        _soundOn = (_soundOn) ? false : true;
    }
    private void ContinueState()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
        GameManager.Instance.ChangeGameState(GameState.Playing);
    }
    private void BackHomeState()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
        GameManager.Instance.ChangeGameState(GameState.None);
        UIManager.Instance.ReloadState();
    }
}
