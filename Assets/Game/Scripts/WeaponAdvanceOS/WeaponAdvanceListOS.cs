using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponAdvanceListOS", menuName = "ScriptableObjects/WeaponAdvanceListOS")]

public class WeaponAdvanceListOS : ScriptableObject
{
    public List<WeaponAdvanceOS> listWeaponsAdvance = new List<WeaponAdvanceOS>();
    
}