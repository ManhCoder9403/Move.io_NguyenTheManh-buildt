using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Axe = 0,
    Arrow = 1,
    Lollipop = 2,
}
[Serializable]
public class WeaponData
{
    public WeaponType weaponType;
    public Weapon weapon;
    public GameObject weaponOnHand;
    public float range;
    public float speed;
    public int price;
}
