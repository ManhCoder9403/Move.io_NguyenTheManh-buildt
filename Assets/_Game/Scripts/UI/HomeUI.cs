using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : Singleton<HomeUI>
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _weaponShopBtn;
    [SerializeField] private Button _skinShopBtn;
    [SerializeField] private Button _muteBtn;
    [SerializeField] private Button _shakeBtn;
    [SerializeField] private Button _noAdsBtn;
    [SerializeField] private TMP_Text _goldTxt;
    [SerializeField] private Image _muteIcon;
    [SerializeField] private Image _shakeIcon;

    [SerializeField] private Sprite _muteIconSprite;
    [SerializeField] private Sprite _unMuteIconSprite;
    [SerializeField] private Sprite _shakeIconSprite;
    [SerializeField] private Sprite _unShakeIconSprite;

    private bool isMuting = false;
    private bool isShaking = true;
    public TMP_Text GoldTxt { get { return _goldTxt; } set { _goldTxt = value; } }
    // Start is called before the first frame update
    void Start()
    {
        _playBtn.onClick.AddListener(StartGame);
        _weaponShopBtn.onClick.AddListener(OpenWeaponShop);
        _skinShopBtn.onClick.AddListener(OpenSkinShop);
        _muteBtn.onClick.AddListener(Mute);
        _shakeBtn.onClick.AddListener(Vibrate);
        _noAdsBtn.onClick.AddListener(PayToWin);

    }
    private void OnEnable()
    {
        GoldTxt.text = GameManager.Instance.PlayerData.Gold.ToString();
    }
    private void PayToWin()
    {
        GameManager.Instance.ChangeGameState(GameState.PayToWin);
    }

    private void Vibrate()
    {
        _shakeIcon.sprite = isShaking ? _unShakeIconSprite : _shakeIconSprite;
        isShaking = !isShaking;
    }

    private void Mute()
    {
        _muteIcon.sprite = isMuting ? _unMuteIconSprite : _muteIconSprite;
        isMuting = !isMuting;
        GameManager.Instance.IsSoundEnabled = !GameManager.Instance.IsSoundEnabled;
    }

    private void OpenSkinShop()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
        GameManager.Instance.ChangeGameState(GameState.SkinShop);
    }

    private void OpenWeaponShop()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
        GameManager.Instance.ChangeGameState(GameState.WeaponShop);
    }

    private void StartGame()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
        GameManager.Instance.ChangeGameState(GameState.Playing);
    }
}
