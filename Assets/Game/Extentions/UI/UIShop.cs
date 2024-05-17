using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UICanvas
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonWeapon;
    [SerializeField] private Button buttonSkin;
    [SerializeField] private Text zone;
    [SerializeField] private Text coin;
    [SerializeField] private InputField nameInput;

    private void Start()
    {
        buttonPlay.onClick.AddListener(() =>
        {
            PlayButton();
        });
        buttonBack.onClick.AddListener(() =>
        {
            BackButton();
        });
        buttonWeapon.onClick.AddListener(() =>
        {
            WeaponButton();
        });
        buttonSkin.onClick.AddListener(() =>
        {
            SkinButton();
        });
        nameInput.onValueChanged.AddListener(delegate { ChangTextName(); });
    }

    public void ChangTextName()
    {
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        dataPlayer.namePlayer = nameInput.text;
        PlayManager.Instance.player.ChangeName(dataPlayer.namePlayer);
        LoadDataPlayer.Instance.SaveDataPlayer(dataPlayer);
    }

    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<UIGamePlay>();
        PlayManager.Instance.SetPlayGame(true);
    }

    public void BackButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<UISelectionMenu>();
    }

    public void WeaponButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<UIWeaponShop>();
        PlayManager.Instance.player.gameObject.SetActive(false);
        
    }

    public void SkinButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<UISkinShop>();
        

    }

    public override void Setup()
    {
        base.Setup();
        PlayManager.Instance.cameraPlayer.ChangePositionSkinToShop();
        PlayManager.Instance.player.ChangeAnim(KeyConstants.Anim_Idle);
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        coin.text = dataPlayer.coinPlayer.ToString();
        zone.text = "Zone " + PlayManager.Instance.zoneCurrent;
    }
}
