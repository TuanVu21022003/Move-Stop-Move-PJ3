using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMaterialItem : MonoBehaviour
{
    [SerializeField] private Button buttonMaterial;
    [SerializeField] private Image imageMaterial;
    [SerializeField] private Outline outlineMaterial;

    public void OnInit(int indexMaterial, Color colorButton, Action<int> actionMaterial)
    {
        SetOutline(false);
        imageMaterial.color = colorButton;
        buttonMaterial.onClick.AddListener(() =>
        {
            actionMaterial?.Invoke(indexMaterial);
            SetOutline(true);
        });
    }

    public void SetOutline(bool check)
    {
        outlineMaterial.enabled = check;
    }

    public void ChangeColor(Color color)
    {
        imageMaterial.color = color;
    }
}
