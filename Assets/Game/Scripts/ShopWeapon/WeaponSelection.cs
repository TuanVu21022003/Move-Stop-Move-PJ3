using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    //[SerializeField] GameObject[] weapons;
    //[SerializeField] GameObject[] weaponsCustom;
    [SerializeField] Button prev;
    [SerializeField] Button next;
    [SerializeField] Button select;
    [SerializeField] Button buy;
    [SerializeField] private Button buttonExit;
    [SerializeField] Text textName;
    [SerializeField] Text textCoin;
    [SerializeField] WeaponOSVisual weaponVisualData;
    [SerializeField] WeaponAdvanceSelection weaponAdvanceSelection;
    [SerializeField] ButtonMaterialSelection buttonMaterialData;
    [SerializeField] ButtonColorCustomSelection buttonColorCustomData;
    [SerializeField] Transform parentWeaponOn;
    [SerializeField] Transform parentWeaponCustom;

    int index;
    int indexMaterialCurrent = 0;
    int indexWeaponAdvanceCurrent = 0;
    private GameUnit weaponOn, weaponCustom;

    private void Start()
    {
        
        next.onClick.AddListener(() =>
        {
   
            Next();
        });

        prev.onClick.AddListener(() =>
        {
    

            Prev();
        });
        select.onClick.AddListener(() =>
        {
            

            Select();
        });
        buy.onClick.AddListener(() =>
        {


            Buy();
        });

        buttonExit.onClick.AddListener(() =>
        {
            ExitButton();
        });
    }

    public void OnInit()
    {
        SetCustom(false);
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        index = (int)dataPlayer.weaponPlayer;
        indexMaterialCurrent = dataPlayer.indexWeaponAdvance;
        SpawnWeaponAdvance();
        CheckIndex();
        UpdateInfoWeapon(dataPlayer);
        UpdateWeaponAdvance();
        LoadSelectCustom(dataPlayer);
        LoadSelectColor();
        SetWeaponAdvanceStart(dataPlayer);
        buttonMaterialData.SetButtonSelected(0);
    }

    public void SpawnWeaponAdvance()
    {
        WeaponAdvanceOS weapon = VisualManager.Instance.GetWeaponAdvanceAvailable(index);
        weaponOn = SimplePool.SpawnByParent<GameUnit>(weapon.poolTypeOn, parentWeaponOn);
        weaponCustom = SimplePool.SpawnByParent<GameUnit>(weapon.poolTypeCustom, parentWeaponCustom);
    }

    public void ClearWeaponAdvance()
    {
        SimplePool.Despawn(weaponOn.GetComponent<GameUnit>());
        SimplePool.Despawn(weaponCustom.GetComponent<GameUnit>());
    }

    public void CheckIndex()
    {
        if (index >= VisualManager.Instance.weaponsAdvanceData.listWeaponsAdvance.Count - 1)
        {
            next.interactable = false;
        }
        else
        {
            next.interactable = true;
        }
        if (index <= 0)
        {
            prev.interactable = false;
        }
        else
        {
            prev.interactable = true;
        }
    }

    public void ExitButton()
    {
        UIManager.Instance.CloseUI<UIWeaponShop>(0);
        UIManager.Instance.OpenUI<UIShop>();
        PlayManager.Instance.player.gameObject.SetActive(true);
        ClearWeaponAdvance();
    }

    public void Next()
    {
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        SetCustom(false);
        ClearWeaponAdvance();
        index++;
        SpawnWeaponAdvance();
        CheckIndex();
        UpdateInfoWeapon(dataPlayer);
        UpdateWeaponAdvance();
        LoadSelectCustom(dataPlayer);
        LoadSelectColor();
        SetWeaponAdvanceStart(dataPlayer);

    }

    public void Prev()
    {
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        SetCustom(false);
        //weapons[index].SetActive(false);
        //weaponsCustom[index].SetActive(false);
        ClearWeaponAdvance();
        index--;
        SpawnWeaponAdvance();
        //weapons[index].SetActive(true);
        //weaponsCustom[index].SetActive(true);
        CheckIndex();
        UpdateInfoWeapon(dataPlayer);
        UpdateWeaponAdvance();
        LoadSelectCustom(dataPlayer);
        LoadSelectColor();
        SetWeaponAdvanceStart(dataPlayer);

    }

    public void Select()
    {
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        dataPlayer.WeaponPlayer = weaponVisualData.list[index].weaponType;
        dataPlayer.indexWeaponAdvance = indexWeaponAdvanceCurrent;
        //dataPlayer.materialsWeapon = weapons[index].GetComponent<MeshRenderer>().materials;
        if(indexWeaponAdvanceCurrent != 0)
        {
            dataPlayer.materialsWeapon = VisualManager.Instance.weaponsAdvanceData.listWeaponsAdvance[index].list[indexWeaponAdvanceCurrent - 1].materials;

        }
        LoadDataPlayer.Instance.SaveDataPlayer(dataPlayer);
        PlayManager.Instance.player.gameObject.SetActive(true);
        SetWeaponCurrent(dataPlayer);
        UIManager.Instance.CloseUI<UIWeaponShop>(0);
        UIManager.Instance.OpenUI<UIShop>();
        ClearWeaponAdvance();
    }

    public void SetWeaponCurrent(PlayerData dataPlayer)
    {
        PlayManager.Instance.player.SelectWeapon(dataPlayer.WeaponPlayer, dataPlayer.GetMaterialsWeapon());
    }

    public void Buy()
    {
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        int coinCurrent = dataPlayer.coinPlayer;
        int costWeapon = weaponVisualData.list[index].weaponCoin;
        if(coinCurrent >= costWeapon)
        {
            dataPlayer.coinPlayer = coinCurrent - costWeapon;
            dataPlayer.listWeaponOwn[index] = 2;
            if(index < dataPlayer.listWeaponOwn.Count() - 1)
            {
                dataPlayer.listWeaponOwn[index + 1] = 1;

            }
            indexWeaponAdvanceCurrent = 2;
            dataPlayer.indexWeaponAdvance = indexWeaponAdvanceCurrent;
            LoadDataPlayer.Instance.SaveDataPlayer(dataPlayer);
            select.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
            UIManager.Instance.GetUI<UIWeaponShop>().UpdateCoin();
            weaponAdvanceSelection.gameObject.SetActive(true);
        }
    }

    public void UpdateInfoWeapon(PlayerData dataPlayer)
    {
        WeaponDataVisual dataweapon = weaponVisualData.list[index];
        textName.text = dataweapon.weaponName;
        textCoin.text = dataweapon.weaponCoin.ToString();
        
        if (dataPlayer.listWeaponOwn[index] == 2)
        {
            select.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
            weaponAdvanceSelection.gameObject.SetActive(true);
        }
        else if(dataPlayer.listWeaponOwn[index] == 1)
        {
            select.gameObject.SetActive(false);
            buy.gameObject.SetActive(true);
            buy.interactable = true;
            weaponAdvanceSelection.gameObject.SetActive(false);

        }
        else if(dataPlayer.listWeaponOwn[index] == 0)
        {
            select.gameObject.SetActive(false);
            buy.gameObject.SetActive(true);
            buy.interactable = false;
            weaponAdvanceSelection.gameObject.SetActive(false);

        }
    }

    public void SetWeaponAdvanceStart(PlayerData dataPlayer)
    {
        if (index != (int)dataPlayer.weaponPlayer)
        {
            weaponAdvanceSelection.SetWeaponSelected(2);
            ChangeMaterial(VisualManager.Instance.weaponsAdvanceData.listWeaponsAdvance[index].list[1].materials);
            for (int i = 0; i < weaponVisualData.list[index].countMaterial; i++)
            {
                ChangeMaterialAtPos(weaponCustom, i, VisualManager.Instance.GetColorCharacter((ColorType)dataPlayer.listCustom[index].data[i]));
            }
        }
        else
        {
            int tmp = dataPlayer.indexWeaponAdvance;
            if(tmp == 0)
            {
                indexMaterialCurrent = 0;
                SetCustom(true);
                for (int i = 0; i < weaponVisualData.list[index].countMaterial; i++)
                {
                    ChangeMaterialAtPos(weaponOn, i, VisualManager.Instance.GetColorCharacter((ColorType)dataPlayer.listCustom[index].data[i]));
                }
            }
            else
            {
                SetCustom(false);
                ChangeMaterial(VisualManager.Instance.weaponsAdvanceData.listWeaponsAdvance[index].list[tmp-1].materials);
            }
            weaponAdvanceSelection.SetWeaponSelected(tmp);
            for (int i = 0; i < weaponVisualData.list[index].countMaterial; i++)
            {
                ChangeMaterialAtPos(weaponCustom, i, VisualManager.Instance.GetColorCharacter((ColorType)dataPlayer.listCustom[index].data[i]));
            }
        }
    }

    public void UpdateWeaponAdvance()
    {
        weaponAdvanceSelection.OnInit(VisualManager.Instance.weaponsAdvanceData.listWeaponsAdvance[index], LoadWeaponAdvance);
    }

    public void LoadWeaponAdvance(int i)
    {
        if(i == 0)
        {
            SetCustom(true);
            buttonMaterialData.SetButtonSelected(0);
            indexMaterialCurrent = 0;
            weaponOn.GetComponent<MeshRenderer>().materials = weaponCustom.GetComponent<MeshRenderer>().materials;

        }
        else
        {
            ChangeMaterial(VisualManager.Instance.weaponsAdvanceData.listWeaponsAdvance[index].list[i-1].materials);
            SetCustom(false);
        }
        weaponAdvanceSelection.SetWeaponSelected(i);
        indexWeaponAdvanceCurrent = i;
    }

    public void ChangeMaterial(List<MaterialWeaponType> list)
    {
        //weapons[index].GetComponent<MeshRenderer>().materials = list.ToArray();
        weaponOn.GetComponent<MeshRenderer>().materials = VisualManager.Instance.GetListMaterialWeapon(list);
    } 

    public void LoadSelectCustom(PlayerData dataPlayer)
    {
        buttonMaterialData.OnInit(weaponVisualData.list[index].countMaterial, index, SaveIndexMaterial);
        

    }

    public void SaveIndexMaterial(int i)
    {
        indexMaterialCurrent = i;
        buttonMaterialData.SetButtonSelected(i);
    }

    public void LoadSelectColor()
    {
        buttonColorCustomData.OnInit(OnHandleClickColor);
    }

    public void OnHandleClickColor(int i)
    {
        buttonMaterialData.ChangeColorAtSelect(indexMaterialCurrent, VisualManager.Instance.ToColor((ColorType)i));
        ChangeMaterialAtPos(weaponOn, indexMaterialCurrent, VisualManager.Instance.GetColorCharacter((ColorType)i));
        ChangeMaterialAtPos(weaponCustom, indexMaterialCurrent, VisualManager.Instance.GetColorCharacter((ColorType)i));
        PlayerData dataPlayer = LoadDataPlayer.Instance.LoadData();
        dataPlayer.Indata(index);
        dataPlayer.listCustom[index].data[indexMaterialCurrent] = i;
        dataPlayer.Indata(index);
        LoadDataPlayer.Instance.SaveDataPlayer(dataPlayer);
    }

    public void ChangeMaterialAtPos(GameUnit weapon, int i, Material material)
    {
        MeshRenderer meshRenderer = weapon.GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;
        materials[i] = material;
        meshRenderer.materials = materials;
    }

    public void SetCustom(bool check)
    {
        buttonMaterialData.gameObject.SetActive(check);
        buttonColorCustomData.gameObject.SetActive(check);
    }
}
