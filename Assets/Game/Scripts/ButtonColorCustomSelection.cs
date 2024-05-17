using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColorCustomSelection : MonoBehaviour
{
    [SerializeField] private List<ButtonColorCustomItem> list;
    public void OnInit(Action<int> actionMaterial)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].OnInit(i, VisualManager.Instance.ToColor((ColorType)i), actionMaterial);
        }
    }
}
