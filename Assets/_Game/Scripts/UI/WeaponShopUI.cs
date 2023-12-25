using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopUI : Singleton<WeaponShopUI>
{
    //Button
    [SerializeField] private Button _exitBtn;
    [SerializeField] private Button _nextBtn;
    [SerializeField] private Button _previousBtn;
    [SerializeField] private Button _purchaseBtn;
    [SerializeField] private Button _UsedBtn;
    [SerializeField] private Button _UseBtn;

    //Display
    [SerializeField] private TMP_Text _nameWeaponTxt;
    [SerializeField] private TMP_Text _purchaseTxt;
    [SerializeField] private TMP_Text _bonusTxt;
    [SerializeField] private WeaponDataManager _wpDataManger;
    [SerializeField] private Transform _weaponSample;

    private int _currentWeaponIndex;
    private WeaponData _currentWeaponData;
    private PlayerData _playerData;

    private void Start()
    {
        _exitBtn.onClick.AddListener(ExitShop);
        _nextBtn.onClick.AddListener(NextWeapon);
        _previousBtn.onClick.AddListener(PreviousWeapon);
        _purchaseBtn.onClick.AddListener(PurchaseWeapon);
        _UseBtn.onClick.AddListener(EquipWeapon);
        _playerData = GameManager.Instance.PlayerData;
        _currentWeaponIndex = _playerData.CurrentWeapon;
        ShowWeapon();
    }
    private void ShowWeapon()
    {
        if (_currentWeaponData is not null)
        {
            Destroy(_weaponSample.GetChild(0).gameObject);
            _currentWeaponData = null;
        }
        CheckWeaponStatus();
        //get weapons
        _currentWeaponData = _wpDataManger.GetWeaponDataByIndex(_currentWeaponIndex);
        //set UI
        Instantiate(_currentWeaponData.weaponOnHand, _weaponSample);
        _nameWeaponTxt.text = _currentWeaponData.weaponType.ToString();
        _purchaseTxt.text = _currentWeaponData.price.ToString();
        _bonusTxt.text = (_currentWeaponData.speed != 0) ? $"+ {_currentWeaponData.speed.ToString()} speed" :
                                                            $"+ {_currentWeaponData.range.ToString()} range";
    }
    private void PurchaseWeapon()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
        if (_playerData.Gold < _currentWeaponData.price) return;
        _playerData.Gold -= _currentWeaponData.price;
        _playerData.MyWeapons.Add(_currentWeaponIndex);
        EquipWeapon();
    }

    private void CheckWeaponStatus()
    {
        if (_playerData.MyWeapons.Contains(_currentWeaponIndex))
        {
            if (_currentWeaponIndex == _playerData.CurrentWeapon)
            {
                ChangeButton(_UsedBtn, _purchaseBtn, _UseBtn);
            }
            else
            {
                ChangeButton(_UseBtn, _UsedBtn, _purchaseBtn);
            }
        }
        else
        {
            ChangeButton(_purchaseBtn, _UseBtn, _UsedBtn);
        }
    }
    private void ChangeButton(Button on, Button off1, Button off2)
    {
        on.gameObject.SetActive(true);
        off1.gameObject.SetActive(false);
        off2.gameObject.SetActive(false);
    }

    private void EquipWeapon()
    {
        LevelManager.Instance.Player.ChangeWeapon(((int)_currentWeaponData.weaponType));
        _playerData.CurrentWeapon = _currentWeaponIndex;
        DynamicDataManager.Instance.SaveData(_playerData);
        CheckWeaponStatus();
    }

    private void PreviousWeapon()
    {
        if (_currentWeaponIndex == 0) return;
        _currentWeaponIndex--;
        ShowWeapon();
    }

    private void NextWeapon()
    {
        if (_currentWeaponIndex == _wpDataManger.GetWPDataCount() - 1) return;
        _currentWeaponIndex++;
        ShowWeapon();
    }

    private void ExitShop()
    {
        GameManager.Instance.ChangeGameState(GameState.None);
    }
}
