using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFail : UICanvas
{
    [SerializeField] private Text topText;
    [SerializeField] private Text nameKill;
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Text coinCount;
    private void Start()
    {
        buttonContinue.onClick.AddListener(() =>
        {
            ContinueButton();
        });
    }

    public void UpdateFail(int top, string name)
    {
        topText.text = "#" + top;
        nameKill.text = name;
        coinCount.text = (PlayManager.Instance.countPerPlay).ToString();
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        dataPlayer.coinPlayer += PlayManager.Instance.countPerPlay;
        LoadDataPlayer.Instance.SaveDataPlayer(dataPlayer);
    }

    public void ContinueButton()
    {
        Close(0);
        PlayManager.Instance.SpawnLevel(PlayManager.Instance.zoneCurrent.ToString());
    }
}
