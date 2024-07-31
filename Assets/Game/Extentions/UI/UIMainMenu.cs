using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UICanvas
{
    [SerializeField] private Button buttonPlay;

    private void Start()
    {
        buttonPlay.onClick.AddListener(() =>
        {
            PlayButton();
        });
    }

    private void OnEnable()
    {
        PopupUtils.BubbleItem(buttonPlay.transform);
    }

    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<UISelectionMenu>();
    }
}
