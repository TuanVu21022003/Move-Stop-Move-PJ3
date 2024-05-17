using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponOS", menuName = "ScriptableObjects/WeaponOS")]

public class WeaponOS : ScriptableObject
{
    public List<WeaponData> list = new List<WeaponData>();
}