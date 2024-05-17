using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVictory : UICanvas
{
    [SerializeField] private Text zone;
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Text coinCount;
    [SerializeField] private int awardCount;

    private void Start()
    {
        buttonContinue.onClick.AddListener(() =>
        {
            ContinueButton();
        });
    }

    public override void Setup()
    {
        base.Setup();
        zone.text = "YOU SURVIVED DAY " + PlayManager.Instance.zoneCurrent;
        coinCount.text = (PlayManager.Instance.countPerPlay + awardCount).ToString();
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        dataPlayer.coinPlayer += PlayManager.Instance.countPerPlay + awardCount;
        LoadDataPlayer.Instance.SaveDataPlayer(dataPlayer);
    }

    public void ContinueButton()
    {
        Close(0);
        PlayManager.Instance.SpawnLevel((PlayManager.Instance.zoneCurrent + 1).ToString());
    }
}
