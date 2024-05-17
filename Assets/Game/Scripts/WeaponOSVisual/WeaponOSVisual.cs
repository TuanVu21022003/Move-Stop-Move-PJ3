using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponOSVisual", menuName = "ScriptableObjects/WeaponOSVisual")]

public class WeaponOSVisual : ScriptableObject
{
    public List<WeaponDataVisual> list = new List<WeaponDataVisual>();
}