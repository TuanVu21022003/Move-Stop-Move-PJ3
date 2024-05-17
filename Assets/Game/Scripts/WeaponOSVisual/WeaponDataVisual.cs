using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponDataVisual
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private GameObject _weaponVisual;
    [SerializeField] private string _weaponName;
    [SerializeField] private int _weaponCoin;
    [SerializeField] private int _countMaterial;

    public GameObject weaponVisual => _weaponVisual;
    public string weaponName => _weaponName;
    public int weaponCoin => _weaponCoin;
    public WeaponType weaponType => _weaponType;
    public int countMaterial => _countMaterial;
}