using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataSO")]
public class WeaponDataManager : ScriptableObject
{
    [SerializeField] private List<WeaponData> weapons;

    public WeaponData GetWeaponData(WeaponType wp) => weapons[((int)wp)];
    public WeaponData GetWeaponDataByIndex(int index) => weapons[index];
    public int GetWPDataCount() => weapons.Count;
}
