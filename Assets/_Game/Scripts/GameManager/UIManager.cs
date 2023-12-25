using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private CanvasGroup _homeUI;
    [SerializeField] private CanvasGroup _playingUI;
    [SerializeField] private CanvasGroup _pauseUI;
    [SerializeField] private CanvasGroup _gameOverUI;
    [SerializeField] private CanvasGroup _winUI;
    [SerializeField] private CanvasGroup _weaponShopUI;
    [SerializeField] private CanvasGroup _skinShopUI;
    [SerializeField] private CanvasGroup _payToWinUI;

    private CanvasGroup _currentUI;
    private void Start()
    {
        _currentUI = _homeUI;
    }
    public void OnHomeState()
    {
        _homeUI.gameObject.SetActive(true);
        _homeUI.alpha = 1.0f;
        _homeUI.interactable = true;
        _playingUI.gameObject.SetActive(false);
        _pauseUI.gameObject.SetActive(false);
        _gameOverUI.gameObject.SetActive(false);
        _winUI.gameObject.SetActive(false);
        //_payToWinUI.gameObject.SetActive(false);
        _skinShopUI.gameObject.SetActive(false);
        _weaponShopUI.gameObject.SetActive(false);
    }
    public void OnPlayingState()
    {
        ChangeUI(_playingUI, true);
    }
    public void OnPauseState()
    {
        ChangeUI(_pauseUI, false);
    }
    public void BackHomeState()
    {
        ChangeUI(_homeUI, true);
    }
    public void ReloadState()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ContinueState()
    {
        ChangeUI(_playingUI, true);
    }
    public void OnWeaponShopState()
    {
        ChangeUI(_weaponShopUI, false);
    }
    public void OnSkinShopState()
    {
        ChangeUI(_skinShopUI, false);
    }
    public void OnPayToWinState()
    {
        ChangeUI(_payToWinUI, false);
    }
    public void OnGameOverState()
    {
        ChangeUI(_gameOverUI, false);
    }
    private void ChangeUI(CanvasGroup on, bool deactive)
    {
        CanvasGroup temp = _currentUI;
        on.interactable = true;
        on.alpha = 1.0f;
        on.gameObject.SetActive(true);
        _currentUI = on;
        temp.interactable = false;
        temp.alpha = 0f;
        if (!deactive) return;
        temp.gameObject.SetActive(false);
    }
}
