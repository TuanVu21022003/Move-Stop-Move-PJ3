
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public abstract class BaseDetailSelection<T> : MonoBehaviour where T : System.Enum
{
    [SerializeField] private Transform parentPositon;
    [SerializeField] private HandleEquiment handleEquiment;
    private Dictionary<T, BaseDetailItemUI<T>> listEquis = new Dictionary<T, BaseDetailItemUI<T>>();
    private T _equiTypeCurrent;
    public T equiTypeCurrent => _equiTypeCurrent;

    protected abstract PoolType poolEqui { get; }
    protected abstract T equiDefault { get; }
    protected abstract T typeOwn { get; }

    protected abstract bool[] listOwn {get;}


    public void SelectEquiDetailItem(T equiType, int coinEqui, Sprite spriteEqui)
    {
        BaseDetailItemUI<T> equiDetailItem = SimplePool.SpawnByParent<BaseDetailItemUI<T>>(poolEqui, parentPositon);
        equiDetailItem.OnInit(equiType, coinEqui, spriteEqui, HandleClickEquiDetail);
        listEquis[equiType] = equiDetailItem;
    }

    public void SelectEquiDetailList(List<DataBaseType<T>> dataBaseType)
    {

        for(int i = 0; i < dataBaseType.Count; i++)
        {
            SelectEquiDetailItem(dataBaseType[i].equiType, dataBaseType[i].coinEqui, dataBaseType[i].spriteType);
        }
        if (!typeOwn.Equals(equiDefault))
        {
            HandleClickEquiDetail(typeOwn);
        }
        else
        {
            HandleClickEquiDetail(listEquis.First().Value.equiType);
        }
    }

    public void ClearEqui()
    {
        SimplePool.Collect(poolEqui);
        listEquis.Clear();
    }

    public void HandleClickEquiDetail(T equiType)
    {
        SetStateButton(equiType);
        PlayManager.Instance.player.ChangeEquipmentType(equiType);
    }

    public void SetStateButton(T equiType)
    {
        _equiTypeCurrent = equiType;
        handleEquiment.SetCoinItem(listEquis[equiType].coinEqui);
        SetActiveEquiAtPos(equiType);
        if (typeOwn.Equals(equiType))
        {
            handleEquiment.SetBuy(false);
            handleEquiment.SetSelect(false);
        }
        else
        {
            if (listOwn[(int)(object)equiType])
            {
                handleEquiment.SetBuy(false);
                handleEquiment.SetSelect(true);
            }
            else
            {
                handleEquiment.SetBuy(true);
            }
        }
    }

    public void SetActiveEquiAtPos(T equiTypeOwn)
    {
        KeyValuePair<T, BaseDetailItemUI<T>>[] keyValuePairs = listEquis.ToArray();
        for (int i = 0; i < keyValuePairs.Length; i++)
        {
            if (!equiTypeOwn.Equals(keyValuePairs[i].Value.equiType))
            {
                listEquis[keyValuePairs[i].Key].Active(false);
            }
            else
            {
                listEquis[keyValuePairs[i].Key].Active(true);
            }
        }
    }

    public void SetLockEquiAtPos(T equiTypeOwn)
    {
        listEquis[equiTypeOwn].SetLock(true);
    }
    public void SetEnquippedEquiAtPos(T equiTypeOwn)
    {
        KeyValuePair<T, BaseDetailItemUI<T>>[] keyValuePairs = listEquis.ToArray();
        for (int i = 0; i < keyValuePairs.Length; i++)
        {
            keyValuePairs[i].Value.SetEquipped(keyValuePairs[i].Key, equiTypeOwn);
        }
    }
}
