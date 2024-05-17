using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialWeaponOS", menuName = "ScriptableObjects/MaterialWeaponOS")]

public class MaterialWeaponOS : ScriptableObject
{
    public List<Material> listWeaponsAdvance = new List<Material>();

}
