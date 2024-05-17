using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMaterialSelection : MonoBehaviour
{
    [SerializeField] private List<ButtonMaterialItem> list;
    public void OnInit(int countMaterial, int indexWeapon,  Action<int> actionMaterial)
    {
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        for(int i = 0; i < list.Count; i++)
        {
            if(i < countMaterial)
            {
                list[i].gameObject.SetActive(true);
                list[i].OnInit(i, VisualManager.Instance.ToColor((ColorType)dataPlayer.listCustom[indexWeapon].data[i]), actionMaterial);
            }
            else
            {
                list[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetButtonSelected(int index)
    {
        list[index].SetOutline(true);
        for(int i = 0; i < list.Count; i++)
        {
            if(i !=  index)
            {
                list[i].SetOutline(false);
            }
        }
    }

    public void ChangeColorAtSelect(int i, Color color)
    {
        list[i].ChangeColor(color);
    }
}
