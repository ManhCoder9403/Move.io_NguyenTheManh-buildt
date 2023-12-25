using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopUI : Singleton<SkinShopUI>
{
    [SerializeField] private Button _hairBtn;
    [SerializeField] private Image _hairBtnImage;
    [SerializeField] private Button _pantBtn;
    [SerializeField] private Image _pantBtnImage;
    [SerializeField] private Button _armBtn;
    [SerializeField] private Image _armBtnImage;
    [SerializeField] private Button _skinBtn;
    [SerializeField] private Image _skinBtnImage;
    [SerializeField] private Color _chooseColor;
    [SerializeField] private Color _defaultColor;

    [SerializeField] private ButtonPrefab _prefabBtn;
    [SerializeField] private Button _exitBtn;
    [SerializeField] private Button _purchaseBtn;
    [SerializeField] private Button _UsedBtn;
    [SerializeField] private Button _UseBtn;
    [SerializeField] private Transform _buttonLocation;
    [SerializeField] private TMP_Text _purchaseTxt;
    [SerializeField] private TMP_Text _bonusTxt;
    [SerializeField] private SkinDataSO _hairData;
    [SerializeField] private SkinDataSO _pantData;
    [SerializeField] private SkinDataSO _armData;
    [SerializeField] private SkinDataSO _skinData;

    private List<ButtonPrefab> _currentBtnList = new List<ButtonPrefab>();
    private PlayerData _playerData;
    private SkinDataSO tempData;
    private int _currentSkinId;
    private SkinType _type;
    public SkinData currentSkinData;
    public ButtonPrefab chosenSkinBtn;
    public List<int> tempList = new List<int>();
    public SkinDataSO PantData { get => _pantData; set => _pantData = value; }
    public SkinDataSO HairData { get => _hairData; set => _hairData = value; }
    private void Awake()
    {
        _playerData = GameManager.Instance.PlayerData;
    }
    private void Start()
    {
        _exitBtn.onClick.AddListener(ExitShop);
        _hairBtn.onClick.AddListener(ShowHair);
        _pantBtn.onClick.AddListener(ShowPant);
        _armBtn.onClick.AddListener(ShowArm);
        _skinBtn.onClick.AddListener(ShowSkin);
        _purchaseBtn.onClick.AddListener(PurchaseSkin);
        _UseBtn.onClick.AddListener(WearSkin);
    }
    private void OnEnable()
    {
        ShowHair();
    }
    private void ExitShop()
    {
        UIManager.Instance.BackHomeState();
    }
    private void ShowSkin()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
        _skinBtn.Select();
        SetTempData(SkinType.Skin);
        SpawnButton(_skinData);
    }
    private void ShowArm()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
        _armBtn.Select();
        SetTempData(SkinType.Arm);
        SpawnButton(_armData);
    }
    private void ShowPant()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
        _pantBtn.Select();
        SetTempData(SkinType.Pant);
        SpawnButton(_pantData);
    }
    private void ShowHair()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
        _hairBtn.Select();
        SetTempData(SkinType.Hair);
        SpawnButton(_hairData);

    }
    private void SpawnButton(SkinDataSO data)
    {
        int listCount = data.GetItemCount();
        if (_currentBtnList.Count >= 0)
        {
            DespawnButton(_currentBtnList);
            _currentBtnList.Clear();
        }
        for (int i = 0; i < listCount; i++)
        {
            ButtonPrefab tempBtn = Instantiate(_prefabBtn, _buttonLocation);
            tempBtn.SetData(data.GetItemDataById(i));
            _currentBtnList.Add(tempBtn);
        }
    }
    private void DespawnButton(List<ButtonPrefab> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Destroy(list[i].gameObject);
        }
    }
    private void SetTempData(SkinType skinType)
    {
        this._type = skinType;
        switch (skinType)
        {
            case SkinType.Hair:
                this.tempData = _hairData;
                this._currentSkinId = _playerData.CurrentHair;
                this.tempList = _playerData.MyHairs;
                break;
            case SkinType.Pant:
                this.tempData = _pantData;
                this._currentSkinId = _playerData.CurrentPant;
                this.tempList = _playerData.MyPants;
                break;
            //case SkinType.Arm:
            //    break;
            //case SkinType.Skin:
            //    break;
            default:
                break;
        }
    }
    public void CheckSkinStatus(int id)
    {
        if (tempList.Contains(id))
        {
            if (id == _currentSkinId)
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
            _purchaseTxt.text = tempData.GetItemDataById(id).price.ToString();
            _bonusTxt.text = (tempData.GetItemDataById(id).speed != 0) ? $"+ {tempData.GetItemDataById(id).speed.ToString()} speed" :
                                                                $"+ {tempData.GetItemDataById(id).range.ToString()} range";
            ChangeButton(_purchaseBtn, _UseBtn, _UsedBtn);
        }
    }
    private void ChangeButton(Button on, Button off1, Button off2)
    {
        on.gameObject.SetActive(true);
        off1.gameObject.SetActive(false);
        off2.gameObject.SetActive(false);
    }
    private void PurchaseSkin()
    {
        SoundManager.Instance.PlaySFX(SFXType.ButtonClick);
        if (_playerData.Gold < currentSkinData.price) return;
        chosenSkinBtn.CheckLockStatus(currentSkinData.id);
        _playerData.Gold -= currentSkinData.price;
        WearSkin();
    }
    private void WearSkin()
    {
        switch (_type)
        {
            case SkinType.Hair:
                _playerData.MyHairs.Add(currentSkinData.id);
                LevelManager.Instance.Player.ChangeHair(currentSkinData.id);
                _currentSkinId = currentSkinData.id;
                _playerData.CurrentHair = _currentSkinId;
                break;
            case SkinType.Pant:
                _playerData.MyPants.Add(currentSkinData.id);
                LevelManager.Instance.Player.ChangePant(currentSkinData.id);
                _currentSkinId = currentSkinData.id;
                _playerData.CurrentPant = _currentSkinId;
                break;
            default:
                break;
        }
        DynamicDataManager.Instance.SaveData(_playerData);
        CheckSkinStatus(_currentSkinId);
    }

}
