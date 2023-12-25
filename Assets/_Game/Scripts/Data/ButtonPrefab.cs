using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPrefab : MonoBehaviour
{
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _lockPanel;
    private SkinData _skinData;
    private void OnEnable()
    {
        _button.onClick.AddListener(ButtonClick);
    }
    private void ButtonClick()
    {
        _button.Select();
        SkinShopUI.Instance.CheckSkinStatus(_skinData.id);
        SkinShopUI.Instance.currentSkinData = _skinData;
        SkinShopUI.Instance.chosenSkinBtn = this;
    }
    public void SetData(SkinData data)
    {
        this._skinData = data;
        this._rawImage.texture = data.image;
        CheckLockStatus(_skinData.id);
    }
    public void CheckLockStatus(int id)
    {
        if (!SkinShopUI.Instance.tempList.Contains(id)) return;
        _lockPanel.SetActive(false);
    }

}
