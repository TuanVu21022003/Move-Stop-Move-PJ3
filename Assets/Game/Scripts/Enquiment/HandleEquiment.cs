using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleEquiment : MonoBehaviour
{
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonSelect;
    [SerializeField] private Button buttonUnenquid;
    [SerializeField] private Button buttonUnlock;
    [SerializeField] private Button buttonBuy;
    [SerializeField] private Text textCoin;
    [SerializeField] private Text textCoinPlayer;
    [SerializeField] private HairDetailSelection hairSelection;
    [SerializeField] private PantDetailSelection pantSelection;
    [SerializeField] private SkinDetailSelection skinSelection;
    [SerializeField] private EquiItemSelection equiSelection;

    private void Start()
    {
        //OnInit();
        buttonExit.onClick.AddListener(() =>
        {
            ExitButton();
        });
        buttonSelect.onClick.AddListener(() =>
        {
            SelectButton();
        });
        buttonUnenquid.onClick.AddListener(() =>
        {
            UnenquidButton();
        });
        buttonUnlock.onClick.AddListener(() =>
        {
            UnlockButton();
        });
        buttonBuy.onClick.AddListener(() =>
        {
            BuyButton();
        });
    }

    public void OnInit()
    {
        SetSelect(true);
        buttonUnenquid.gameObject.SetActive(false);
        buttonUnlock.gameObject.SetActive(false);
        buttonBuy.gameObject.SetActive(false);
    }

    public void SelectButton()
    {
        SetSelect(false);
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        switch (equiSelection.equiState)
        {
            case Equiment.HAIR:
                dataPlayer.hairPlayer = hairSelection.equiTypeCurrent;
                PlayManager.Instance.player.ChangeEquipment(dataPlayer.hairPlayer);
                hairSelection.SetEnquippedEquiAtPos(dataPlayer.hairPlayer);
                break;
            case Equiment.PANT:
                dataPlayer.pantPlayer = pantSelection.equiTypeCurrent;
                PlayManager.Instance.player.ChangeEquipment(dataPlayer.pantPlayer);
                pantSelection.SetEnquippedEquiAtPos(dataPlayer.pantPlayer);
                break;
            case Equiment.SKIN:
                dataPlayer.skinPlayer = skinSelection.equiTypeCurrent;
                PlayManager.Instance.player.ChangeEquipment(dataPlayer.skinPlayer);
                skinSelection.SetEnquippedEquiAtPos(dataPlayer.skinPlayer);
                break;
        }
        LoadDataPlayer.Instance.SaveDataPlayer(dataPlayer);
    }

    public void UnenquidButton()
    {
        SetSelect(true);
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        switch (equiSelection.equiState)
        {
            case Equiment.HAIR:
                dataPlayer.hairPlayer = HairType.DEFAULT;
                PlayManager.Instance.player.ChangeEquipment(dataPlayer.hairPlayer);
                hairSelection.SetEnquippedEquiAtPos(dataPlayer.hairPlayer);
                break;
            case Equiment.PANT:
                dataPlayer.pantPlayer = PantType.DEFAULT;
                PlayManager.Instance.player.ChangeEquipment(dataPlayer.pantPlayer);
                pantSelection.SetEnquippedEquiAtPos(dataPlayer.pantPlayer);
                break;
            case Equiment.SKIN:
                dataPlayer.skinPlayer = SkinType.NORMAL;
                PlayManager.Instance.player.ChangeEquipment(dataPlayer.skinPlayer);
                skinSelection.SetEnquippedEquiAtPos(dataPlayer.skinPlayer);
                break;
        }
        LoadDataPlayer.Instance.SaveDataPlayer(dataPlayer);
    }

    public void UnlockButton()
    {
        SetBuy(false);
        SetSelect(true);
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        switch (equiSelection.equiState)
        {
            case Equiment.HAIR:
                dataPlayer.listHairOwn[(int)hairSelection.equiTypeCurrent] = true;
                hairSelection.SetLockEquiAtPos(hairSelection.equiTypeCurrent);
                break;
            case Equiment.PANT:
                dataPlayer.listPantOwn[(int)pantSelection.equiTypeCurrent] = true;
                pantSelection.SetLockEquiAtPos(pantSelection.equiTypeCurrent);
                break;
            case Equiment.SKIN:
                dataPlayer.listSkinOwn[(int)skinSelection.equiTypeCurrent] = true;
                skinSelection.SetLockEquiAtPos(skinSelection.equiTypeCurrent);
                break;
        }
        LoadDataPlayer.Instance.SaveDataPlayer(dataPlayer);
    }

    public void BuyButton()
    {
        int coinEqui = int.Parse(textCoin.text);
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        if(dataPlayer.coinPlayer >= coinEqui)
        {
            SetBuy(false);
            SetSelect(true);
            switch (equiSelection.equiState)
            {
                case Equiment.HAIR:
                    dataPlayer.listHairOwn[(int)hairSelection.equiTypeCurrent] = true;
                    hairSelection.SetLockEquiAtPos(hairSelection.equiTypeCurrent);
                    break;
                case Equiment.PANT:
                    dataPlayer.listPantOwn[(int)pantSelection.equiTypeCurrent] = true;
                    pantSelection.SetLockEquiAtPos(pantSelection.equiTypeCurrent);
                    break;
                case Equiment.SKIN:
                    dataPlayer.listSkinOwn[(int)skinSelection.equiTypeCurrent] = true;
                    skinSelection.SetLockEquiAtPos(skinSelection.equiTypeCurrent);
                    break;
            }
            dataPlayer.coinPlayer -= coinEqui;
            LoadDataPlayer.Instance.SaveDataPlayer(dataPlayer);
            textCoinPlayer.text = dataPlayer.coinPlayer.ToString();
        }
    }

    public void ExitButton()
    {
        UIManager.Instance.OpenUI<UIShop>();
        UIManager.Instance.CloseUI<UISkinShop>(0);
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        PlayManager.Instance.player.ChangeEquipment(dataPlayer.skinPlayer);
        if (dataPlayer.skinPlayer == SkinType.NORMAL)
        {
            PlayManager.Instance.player.ChangeEquipment(dataPlayer.hairPlayer);
            PlayManager.Instance.player.ChangeEquipment(dataPlayer.pantPlayer);

        }
        equiSelection.ResetEqui();
    }

    public void SetSelect(bool check)
    {
        buttonSelect.gameObject.SetActive(check);
        buttonUnenquid.gameObject.SetActive(!check);
    }

    public void SetBuy(bool check)
    {
        buttonBuy.gameObject.SetActive(check);
        buttonUnlock.gameObject.SetActive(check);
        buttonSelect.gameObject.SetActive(!check);
        buttonUnenquid.gameObject.SetActive(!check);
    }

    public void SetCoinItem(int coinEqui)
    {
        textCoin.text = coinEqui.ToString();
    }
}
