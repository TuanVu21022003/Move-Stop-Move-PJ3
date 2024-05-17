using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAdvanceSelection : MonoBehaviour
{
    [SerializeField] private List<WeaponAdvanceItem> listWeapon;


    public void OnInit(WeaponAdvanceOS dataWeapon , Action<int> actionWeapon)
    {
        for (int i = 0; i < 5; i++)
        {
            if(i >= 1)
            {
                listWeapon[i].OnInit(i, dataWeapon.list[i - 1].weaponAdvanceSprite, actionWeapon);

            }
            else
            {
                listWeapon[i].OnInit(i, dataWeapon.list[0].weaponAdvanceSprite, actionWeapon);
            }
        }
    }

    public void SetWeaponSelected(int index)
    {
        listWeapon[index].SetOutline(true);
        for (int i = 0; i < listWeapon.Count; i++)
        {
            if (i != index)
            {
                listWeapon[i].SetOutline(false);
            }
        }
    }
}
