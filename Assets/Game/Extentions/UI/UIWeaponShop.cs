using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponShop : UICanvas
{
    
    [SerializeField] private WeaponSelection weaponSelection;
    [SerializeField] private Text coin;

    

    public override void Setup()
    {
        base.Setup();
        UpdateCoin();
        weaponSelection.OnInit();
    }

    public void UpdateCoin()
    {
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        coin.text = dataPlayer.coinPlayer.ToString();
    }
}
