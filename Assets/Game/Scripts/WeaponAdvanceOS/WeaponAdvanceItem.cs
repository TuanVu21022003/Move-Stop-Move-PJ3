using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAdvanceItem : MonoBehaviour
{
    [SerializeField] private Image _imageWeapon;
    [SerializeField] private Button _buttonWeapon;
    [SerializeField] private Outline _outlineWeapon;

    public void OnInit(int index, Sprite spriteWeapon, Action<int> actionWeapon)
    {
        SetOutline(false);
        if(index == 0)
        {

        }
        else
        {
            _imageWeapon.sprite = spriteWeapon;

        }
        _buttonWeapon.onClick.AddListener(() =>
        {
            SetOutline(true);
            actionWeapon?.Invoke(index);
        });
    }

    public void SetOutline(bool check)
    {
        _outlineWeapon.enabled = check;
    }
}
