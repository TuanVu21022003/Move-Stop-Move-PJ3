using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    public string namePlayer;
    public WeaponType weaponPlayer;
    public HairType hairPlayer;
    public PantType pantPlayer;
    public SkinType skinPlayer;
    public int coinPlayer;
    public int countWeapon = Enum.GetValues(typeof(WeaponType)).Length;
    public int countHair = Enum.GetValues(typeof(HairType)).Length;
    public int countPant = Enum.GetValues(typeof(PantType)).Length;
    public int countSkin = Enum.GetValues(typeof(SkinType)).Length;
    public int[] listWeaponOwn;
    public bool[] listHairOwn;
    public bool[] listPantOwn;
    public bool[] listSkinOwn;
    public List<CustomData> listCustom;
    public int indexWeaponAdvance;
    public List<MaterialWeaponType> materialsWeapon;
    public PlayerData()
    {
        this.namePlayer = "Tuan Tuan";
        this.weaponPlayer = WeaponType.LOLLIPOP;
        this.hairPlayer = HairType.DEFAULT;
        this.pantPlayer = PantType.DEFAULT;
        this.skinPlayer = SkinType.NORMAL;
        this.coinPlayer = 100;
        this.listWeaponOwn = new int[countWeapon];
        this.listCustom = new List<CustomData>();
        this.indexWeaponAdvance = 1;
        List<MaterialWeaponType> startMaterials = VisualManager.Instance.GetMaterialAvailable((int)weaponPlayer, indexWeaponAdvance - 1);
        this.materialsWeapon = startMaterials.GetRange(0, startMaterials.Count);
        for (int i = 0; i < countWeapon; i++)
        {
            CustomData customData = new CustomData(3);
            for (int j = 0; j < 3; j++)
            {
                customData.data[j] = (int)ColorType.RED + j + i;
            }
            listCustom.Add(customData);
        }
        for (int i = 0; i<= (int)weaponPlayer; i++)
        {
            listWeaponOwn[i] = 2;
        }
        //Hair
        listWeaponOwn[(int)weaponPlayer + 1] = 1;
        this.listHairOwn = new bool[countHair];
        for(int i = 0; i < countHair; i++)
        {
            listHairOwn[i] = false;
        }
        listHairOwn[(int)hairPlayer] = true;
        //Pant
        this.listPantOwn = new bool[countPant];
        for (int i = 0; i < countPant; i++)
        {
            listPantOwn[i] = false;
        }
        listPantOwn[(int)pantPlayer] = true;
        //Skin
        this.listSkinOwn = new bool[countSkin];
        for (int i = 0; i < countSkin; i++)
        {
            listSkinOwn[i] = false;
        }
        listSkinOwn[(int)skinPlayer] = true;
    }

    public PlayerData(string name, WeaponType weapon, HairType hair, PantType pant)
    {
        this.namePlayer = name;
        this.weaponPlayer = weapon;
        this.hairPlayer = hair;
        this.pantPlayer = pant;
    }

    // Property cho 'name'
    public string NamePlayer
    {
        get { return namePlayer; }
        set { namePlayer = value; }
    }

    // Property cho 'weapon'
    public WeaponType WeaponPlayer
    {
        get { return weaponPlayer; }
        set { weaponPlayer = value; }
    }

    // Property cho 'hair'
    public HairType HairPlayer
    {
        get { return hairPlayer; }
        set { hairPlayer = value; }
    }

    // Property cho 'pant'
    public PantType PantPlayer
    {
        get { return pantPlayer; }
        set { pantPlayer = value; }
    }

    public int CoinPlayer
    {
        get { return coinPlayer; }
        set { coinPlayer = value; }
    }

    public void Indata(int index)
    {
        string tmp = "";
        for (int j = 0; j < 3; j++)
        {
            tmp += listCustom[index].data[j] + " ";
        }
        Debug.Log(tmp);

    }

    public Material[] GetMaterialsWeapon()
    {
        if(indexWeaponAdvance == 0)
        {
            return VisualManager.Instance.GetListMaterialWeapon(listCustom[(int)weaponPlayer].data);
        }
        else
        {
            return VisualManager.Instance.GetListMaterialWeapon(materialsWeapon);
        }
    }
}

[Serializable]
public class CustomData
{
    public int[] data;

    public CustomData(int size)
    {
        data = new int[size];
    }
}
