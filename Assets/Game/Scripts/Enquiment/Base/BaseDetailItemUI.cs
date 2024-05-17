using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseDetailItemUI<T> : GameUnit where T : System.Enum
{
    [SerializeField] private Button buttonEqui;
    [SerializeField] private Image imageEqui;
    [SerializeField] private GameObject imageActive;
    [SerializeField] private GameObject imageLock;
    [SerializeField] private GameObject imageEquipped;

    private T _equiType;
    public T equiType => _equiType;

    private int _coinEqui;
    public int coinEqui => _coinEqui;

    protected abstract T typeOwn { get; }
    protected abstract bool[] listOwn { get; }
    public void OnInit(T equiType, int coinEqui, Sprite spriteEqui, Action<T> actionButton)
    {
        _equiType = equiType;
        _coinEqui = coinEqui;
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        SetLock(listOwn[(int)(object)equiType]);
        SetEquipped(equiType, typeOwn);
        Active(equiType.Equals(typeOwn));
        imageEqui.sprite = spriteEqui;
        buttonEqui.onClick.AddListener(() =>
        {
            actionButton?.Invoke(equiType);
        });
    }

    public void Active(bool check)
    {
        imageActive.SetActive(check);
    }

    public void SetLock(bool check)
    {
        imageLock.SetActive(!check);
    }

    public void SetEquipped(T hairType, T hairTypeOwn)
    {
        imageEquipped.SetActive(hairType.Equals(hairTypeOwn));
    }
}

//public class HairDetailItemUI2 : DetailItemUI<HairType>
//{
//    protected override HairType Own => LoadDataPlayer.Instance.LoadData().hairPlayer;
//}

//public abstract class DetailItemUI<T> : GameUnit where T : System.Enum
//{
//    [SerializeField] private Button buttonEqui;
//    [SerializeField] private Image imageEqui;
//    [SerializeField] private GameObject imageActive;
//    [SerializeField] private GameObject imageLock;
//    [SerializeField] private GameObject imageEquipped;

//    private T t;
//    public T Type => t;

//    private int _coinEqui;
//    public int coinEqui => _coinEqui;
//    protected abstract T Own { get; }
//    public void OnInit(T t, int coinEqui, Sprite spriteEqui, Action<T> actionButton)
//    {
//        this.t = t;
//        _coinEqui = coinEqui;
//        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
//        SetLock(dataPlayer.listHairOwn[(int)(object)t]);
//        SetEquipped(t, Own);
//        Active(t.Equals(Own));
//        imageEqui.sprite = spriteEqui;
//        buttonEqui.onClick.AddListener(() =>
//        {
//            actionButton?.Invoke(t);
//        });
//    }

//    public void Active(bool check)
//    {
//        imageActive.SetActive(check);
//    }

//    public void SetLock(bool check)
//    {
//        imageLock.SetActive(!check);
//    }

//    public void SetEquipped(T hairType, T hairTypeOwn)
//    {
//        imageEquipped.SetActive(hairType.Equals(hairTypeOwn));
//    }
//}
