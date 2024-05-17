using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisualManager : Singleton<VisualManager>
{
    [SerializeField] private WeaponOSVisual _weaponDataVisual;
    [SerializeField] private WeaponOS _weaponData;
    [SerializeField] private ColorCharacterOS _colorCharacterData;
    [SerializeField] private PantOS _pantData;
    [SerializeField] private HairOS _hairData;
    [SerializeField] private WeaponAdvanceListOS _weaponsAdvanceData;
    [SerializeField] private SkinOS _skinData;
    public WeaponAdvanceListOS weaponsAdvanceData => _weaponsAdvanceData;
    [SerializeField] public MaterialWeaponOS materialWeaponData;
    [SerializeField] public int countRandom = 8;


    public List<MaterialWeaponType> GetMaterialAvailable(int indexWeapon, int indexWeaponAdvance)
    {
        return _weaponsAdvanceData.listWeaponsAdvance[indexWeapon].list[indexWeaponAdvance].materials;
    }

    public WeaponAdvanceOS GetWeaponAdvanceAvailable(int indexWeapon)
    {
        return _weaponsAdvanceData.listWeaponsAdvance[indexWeapon];
    }

    public GameObject GetWeaponVisual(WeaponType weaponType)
    {
        return _weaponDataVisual.list[(int)weaponType].weaponVisual;
    }

    public GameObject GetHair(HairType hairType)
    {
        return _hairData.list[(int)hairType];
    }

    public WeaponData GetWeaponData(WeaponType weaponType)
    {
        return _weaponData.list[(int)weaponType];
    }

    public Material GetColorCharacter(ColorType colorType)
    {
        return _colorCharacterData.list[(int)colorType];
    }

    public Material GetPant(PantType pantType)
    {
        return _pantData.list[(int)pantType];
    }

    public SkinData GetSkinData(SkinType skinType)
    {
        return _skinData.list[(int)skinType];
    }

    public WeaponType RandomWeapon()
    {
        // Lấy số lượng các giá trị trong enum
        Array weaponTypes = Enum.GetValues(typeof(WeaponType));

        // Tạo một số nguyên ngẫu nhiên từ 0 đến weaponTypes.Length - 1
        int randomIndex = UnityEngine.Random.Range(0, weaponTypes.Length);

        // Chuyển số nguyên này thành một WeaponType và trả về
        return (WeaponType)weaponTypes.GetValue(randomIndex);
    }

    public HairType RandomHair()
    {
        // Lấy số lượng các giá trị trong enum
        Array hairTypes = Enum.GetValues(typeof(HairType));

        // Tạo một số nguyên ngẫu nhiên từ 0 đến weaponTypes.Length - 1
        int randomIndex = UnityEngine.Random.Range(0, hairTypes.Length);

        // Chuyển số nguyên này thành một WeaponType và trả về
        return (HairType)hairTypes.GetValue(randomIndex);
    }

    public SkinType RandomSkin()
    {
        // Lấy số lượng các giá trị trong enum
        Array skinTypes = Enum.GetValues(typeof(SkinType));

        // Tạo một số nguyên ngẫu nhiên từ 0 đến weaponTypes.Length - 1
        int randomIndex = UnityEngine.Random.Range(0, skinTypes.Length + countRandom);

        // Chuyển số nguyên này thành một WeaponType và trả về
        return (SkinType)skinTypes.GetValue(randomIndex < skinTypes.Length ? randomIndex : 0);
    }

    public ColorType RandomColor()
    {
        // Lấy số lượng các giá trị trong enum
        Array colorTypes = Enum.GetValues(typeof(ColorType));

        // Tạo một số nguyên ngẫu nhiên từ 0 đến weaponTypes.Length - 1
        int randomIndex = UnityEngine.Random.Range(0, colorTypes.Length);

        // Chuyển số nguyên này thành một WeaponType và trả về
        return (ColorType)colorTypes.GetValue(randomIndex);
    }

    public PantType RandomPant()
    {
        // Lấy số lượng các giá trị trong enum
        Array pantTypes = Enum.GetValues(typeof(PantType));

        // Tạo một số nguyên ngẫu nhiên từ 0 đến weaponTypes.Length - 1
        int randomIndex = UnityEngine.Random.Range(0, pantTypes.Length);

        // Chuyển số nguyên này thành một WeaponType và trả về
        return (PantType)pantTypes.GetValue(randomIndex);
    }

    public Material[] GetListMaterialWeapon(List<MaterialWeaponType> listMaterialWeaponType)
    {
        Material[] tmp = new Material[listMaterialWeaponType.Count];
        for(int i = 0; i < tmp.Length; i++)
        {
            tmp[i] = materialWeaponData.listWeaponsAdvance[(int)listMaterialWeaponType[i]];
        }
        return tmp;
    }

    public Material[] GetListMaterialWeapon(int[] listMaterialWeaponType)
    {
        Material[] tmp = new Material[listMaterialWeaponType.Length];
        for (int i = 0; i < tmp.Length; i++)
        {
            tmp[i] = _colorCharacterData.list[listMaterialWeaponType[i]];
        }
        return tmp;
    }

    public Color ToColor(ColorType colorType)
    {
        switch (colorType)
        {
            case ColorType.RED:
                return Color.red;
            case ColorType.GREEN:
                return Color.green;
            case ColorType.BLUE:
                return Color.blue;
            case ColorType.MAGENTA:
                return Color.magenta;
            case ColorType.YELLOW:
                return Color.yellow;
            case ColorType.CYAN:
                return Color.cyan;
            case ColorType.ORANGE:
                return new Color(1.0f, 0.5f, 0.0f); // Orange
            case ColorType.PURPLE:
                return new Color(0.5f, 0.0f, 0.5f); // Purple
            case ColorType.BROWN:
                return new Color(0.6f, 0.3f, 0.0f); // Brown
            case ColorType.PINK:
                return new Color(1.0f, 0.5f, 0.5f); // Pink
            case ColorType.TEAL:
                return new Color(0.0f, 0.5f, 0.5f); // Teal
            case ColorType.INDIGO:
                return new Color(0.29f, 0.0f, 0.51f); // Indigo
            case ColorType.GOLD:
                return new Color(1.0f, 0.84f, 0.0f); // Gold
            case ColorType.AQUAMARINE:
                return new Color(0.5f, 1.0f, 0.83f); // Aquamarine
            case ColorType.VIOLET:
                return new Color(0.93f, 0.51f, 0.93f); // Violet
            case ColorType.BLACK:
                return Color.black;
            default:
                return Color.white; // Default color if enum value is not recognized
        }
    }
}

public enum WeaponType
{
    HAMMER = 0,
    LOLLIPOP = 1,
    KNIFT = 2,
    CANDY = 3,
    BOOMERANG = 4,
    SWIRLY = 5,
    AXE = 6,
    ICE = 7,
    BATTLE = 8,
    Z = 9,
    ARROW = 10,
    UZI = 11,
}

public enum PantType
{
    BATMAN = 0,
    CHAMBI = 1,
    COMY = 2,
    DABAO = 3,
    ONION = 4,
    POKEMON = 5,
    RAINBOW = 6,
    SKULL = 7,
    VANTIM = 8,
    DEFAULT = 9,
}

public enum HairType
{
    ARROW = 0,
    EAR = 1,
    FLOWER = 2,
    HAIR = 3,
    HAT = 4,
    HATCAP = 5,
    HORN = 6,
    RAU = 7,
    DEFAULT = 8,
}

public enum SkinType
{
    NORMAL = 0,
    THOR = 1,
    DEADPOOD = 2,
    ANGLE = 3
}

public enum ColorType
{
    RED = 0,
    GREEN = 1,
    BLUE = 2,
    MAGENTA = 3,
    YELLOW = 4,
    CYAN = 5,
    ORANGE = 6,
    PURPLE = 7,
    BROWN = 8,
    PINK = 9,
    TEAL = 10,     // Add new color TEAL
    INDIGO = 11,   // Add new color INDIGO
    GOLD = 12,     // Add new color GOLD
    AQUAMARINE = 13, // Add new color AQUAMARINE
    VIOLET = 14,
    BLACK = 15// Add new color VIOLET
}

public enum MaterialWeaponType
{
    HAMMER_1_1 = 0,
    HAMMER_2_1 = 1,
    HAMMER_2_2 = 2,
    HAMMER_3_1 = 3,
    HAMMER_4_1 = 4,
    LOLLIPOP_1_1 = 5,
    LOLLIPOP_1_2 = 6,
    LOLLIPOP_1_3 = 7,
    LOLLIPOP_2_1 = 8,
    LOLLIPOP_2_2 = 9,
    LOLLIPOP_2_3 = 10,
    LOLLIPOP_3_1 = 11,
    LOLLIPOP_3_2 = 12,
    LOLLIPOP_3_3 = 13,
    LOLLIPOP_4_1 = 14,
    LOLLIPOP_4_2 = 15,
    LOLLIPOP_4_3 = 16,
    KNIFT_1_1 = 17,
    KNIFT_1_2 = 18,
    KNIFT_2_1 = 19,
    KNIFT_3_1 = 20,
    KNIFT_4_1 = 21,
    CANDY_1_1 = 22,
    CANDY_1_2 = 23,
    CANDY_2_1 = 24,
    CANDY_3_1 = 25,
    CANDY_4_1 = 26,
    BOOMERANG_1_1 = 27,
    BOOMERANG_2_1 = 28,
    BOOMERANG_3_1 = 29,
    BOOMERANG_4_1 = 30,
    SWIRLY_1_1 = 31,
    SWIRLY_1_2 = 32,
    SWIRLY_1_3 = 33,
    SWIRLY_2_1 = 34,
    SWIRLY_2_2 = 35,
    SWIRLY_2_3 = 36,
    SWIRLY_3_1 = 37,
    SWIRLY_3_2 = 38,
    SWIRLY_3_3 = 39,
    SWIRLY_4_1 = 40,
    SWIRLY_4_2 = 41,
    SWIRLY_4_3 = 42,
    AXE_1_1 = 43,
    AXE_1_2 = 44,
    AXE_2_1 = 45,
    AXE_3_1 = 46,
    AXE_4_1 = 47,
    ICE_1_1 = 48,
    ICE_2_1 = 49,
    ICE_3_1 = 50,
    ICE_4_1 = 51,
    BATTLE_1_1 = 52,
    BATTLE_2_1 = 53,
    BATTLE_3_1 = 54,
    BATTLE_4_1 = 55,
    Z_1_1 = 56,
    Z_2_1 = 57,
    Z_3_1 = 58,
    Z_4_1 = 59,
    ARROW_1_1 = 60,
    ARROW_1_2 = 61,
    ARROW_1_3 = 62,
    ARROW_2_1 = 63,
    ARROW_3_1 = 64,
    ARROW_4_1 = 65,
    UZI_1_1 = 66,
    UZI_2_1 = 67,
    UZI_3_1 = 68,
    UZI_4_1 = 69,
}


