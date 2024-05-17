using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponData
{
    [SerializeField] private PoolType _weaponType;
    [SerializeField] private float _weaponSpeed;
    [SerializeField] private float _weaponSpeedRotation;
    


    public PoolType weaponType => _weaponType;
    public float weaponSpeed => _weaponSpeed;
    public float weaponSpeedRotation => _weaponSpeedRotation;
    
}