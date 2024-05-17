using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItemUI<T> : MonoBehaviour where T : System.Enum
{
    [SerializeField] private BaseDetailSelection<T> baseSelection;
    [SerializeField] protected EnquiListOS enquiListData;
    [SerializeField] private Image equiImage;
    [SerializeField] private Image equiBackground;

    private bool isActive = false;

    protected abstract List<DataBaseType<T>> dataBaseTypes { get; }
    public void Active(Color colorBackground, Color colorImage)
    {
        if (isActive == false)
        {
            equiBackground.color = colorBackground;
            equiImage.color = colorImage;
            baseSelection.SelectEquiDetailList(dataBaseTypes);
            isActive = true;
            baseSelection.gameObject.SetActive(true);

        }
    }

    public void Deactive(Color colorBackground, Color colorImage)
    {
        equiBackground.color = colorBackground;
        equiImage.color = colorImage;
        isActive = false;
        baseSelection.ClearEqui();
        baseSelection.gameObject.SetActive(true);

    }
}
