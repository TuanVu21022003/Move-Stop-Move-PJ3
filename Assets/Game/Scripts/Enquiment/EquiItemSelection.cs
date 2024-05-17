using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquiItemSelection : MonoBehaviour
{
    [SerializeField] private Color colorBackgroundActive, colorBackgroundDeactive, colorImageActive, colorImageDeactive;
    [SerializeField] private Button buttonHair;
    [SerializeField] private HairItemUI hairItem;
    [SerializeField] private Button buttonPant;
    [SerializeField] private PantItemUI pantItem;
    [SerializeField] private Button buttonSkin;
    [SerializeField] private SkinItemUI skinItem;
    [SerializeField] private Text coinPlayer;

    private Equiment _equiState;
    public Equiment equiState => _equiState;
    private void Start()
    {
        buttonHair.onClick.AddListener(() =>
        {
            hairItem.Active(colorBackgroundActive, colorImageActive);
            pantItem.Deactive(colorBackgroundDeactive, colorImageDeactive);
            skinItem.Deactive(colorBackgroundDeactive, colorImageDeactive);
            _equiState = Equiment.HAIR;
        });

        buttonPant.onClick.AddListener(() =>
        {
            pantItem.Active(colorBackgroundActive, colorImageActive);
            hairItem.Deactive(colorBackgroundDeactive, colorImageDeactive);
            skinItem.Deactive(colorBackgroundDeactive, colorImageDeactive);

            _equiState = Equiment.PANT;
        });

        buttonSkin.onClick.AddListener(() =>
        {
            pantItem.Deactive(colorBackgroundDeactive, colorImageDeactive);
            hairItem.Deactive(colorBackgroundDeactive, colorImageDeactive);
            skinItem.Active(colorBackgroundActive, colorImageActive);

            _equiState = Equiment.SKIN;
        });
    }

    public void OnInit()
    {
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        coinPlayer.text = dataPlayer.coinPlayer.ToString();
        hairItem.Active(colorBackgroundActive, colorImageActive);
        pantItem.Deactive(colorBackgroundDeactive, colorImageDeactive);
        skinItem.Deactive(colorBackgroundDeactive, colorImageDeactive);
        _equiState = Equiment.HAIR;
    }

    public void ResetEqui()
    {
        pantItem.Deactive(colorBackgroundDeactive, colorImageDeactive);
        hairItem.Deactive(colorBackgroundDeactive, colorImageDeactive);
        skinItem.Deactive(colorBackgroundDeactive, colorImageDeactive);
    }
}

public enum Equiment
{
    HAIR,
    PANT,
    SKIN
}
