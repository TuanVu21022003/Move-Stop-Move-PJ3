using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorCustomItem : MonoBehaviour
{
    [SerializeField] private Button buttonColor;
    [SerializeField] private Image imageColor;

    public void OnInit(int indexMaterial, Color colorButton, Action<int> actionMaterial)
    {
        imageColor.color = colorButton;
        buttonColor.onClick.AddListener(() =>
        {
            actionMaterial?.Invoke(indexMaterial);
        });
    }


}
