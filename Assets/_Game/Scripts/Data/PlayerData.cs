using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private string _name;
    [SerializeField] private int _stage;
    [SerializeField] private int _gold;
    [SerializeField] private int _highScore;
    [SerializeField] private List<int> _myWeapons = new List<int>();
    [SerializeField] private List<int> _myPants = new List<int>();
    [SerializeField] private List<int> _myHairs = new List<int>();
    [SerializeField] private int _currentWeapon;
    [SerializeField] private int _currentPant;
    [SerializeField] private int _currentHair;
    public string Name { get => _name; set => _name = value; }
    public int Stage { get => _stage; set => _stage = value; }
    public int Gold { get => _gold; set => _gold = value; }
    public int HighScore { get => _highScore; set => _highScore = value; }
    public List<int> MyWeapons { get => _myWeapons; set => _myWeapons = value; }
    public List<int> MyPants { get => _myPants; set => _myPants = value; }
    public List<int> MyHairs { get => _myHairs; set => _myHairs = value; }
    public int CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }
    public int CurrentPant { get => _currentPant; set => _currentPant = value; }
    public int CurrentHair { get => _currentHair; set => _currentHair = value; }

    public PlayerData()
    {
        //default
        _name = "Unknown Player";
        _stage = 0;
        _gold = 0;
        _myWeapons.Add(0);
        _myPants.Add(0);
        _myHairs.Add(0);
        _currentWeapon = 0;
        _currentPant = 0;
        _currentHair = 0;
    }
}
