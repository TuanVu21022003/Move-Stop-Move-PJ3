using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponAdvanceOS", menuName = "ScriptableObjects/WeaponAdvanceOS")]

public class WeaponAdvanceOS : ScriptableObject
{
    public PoolType poolTypeCustom;
    public PoolType poolTypeOn;
    public List<WeaponAdvanceData> list = new List<WeaponAdvanceData>();
}