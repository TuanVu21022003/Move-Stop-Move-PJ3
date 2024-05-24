using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    [SerializeField] private Text textCount;
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonSetting;

    private void Start()
    {
        buttonSetting.onClick.AddListener(() =>
        {
            SettingButton();
        });

        buttonExit.onClick.AddListener(() =>
        {
            ExitButton();
        });

        buttonRestart.onClick.AddListener(() =>
        {
            RestartButton();
        });
    }

    

    public override void Setup()
    {
        base.Setup();
        SettingPanel.SetActive(false);
        textCount.text = "Alive " + PlayManager.Instance.countEnemyByLevel;
    }

    public void UpdateTextCount(int count)
    {
        textCount.text = "Alive " + count;
    }

    public void SettingButton()
    {
        SettingPanel.SetActive(true);
        PlayManager.Instance.PauseGame();

    }
    private void ExitButton()
    {
        SettingPanel.SetActive(false);
        PlayManager.Instance.ResumeGame();
    }

    private void RestartButton()
    {
        SettingPanel.SetActive(false);
        UIManager.Instance.CloseUI<UIGamePlay>(0);
        PlayManager.Instance.ResumeGame();
        PlayManager.Instance.SpawnLevel(PlayManager.Instance.zoneCurrent.ToString());
    }
}
