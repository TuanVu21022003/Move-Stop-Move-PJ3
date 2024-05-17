using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    [SerializeField] PoolAmount[] poolAmounts;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < poolAmounts.Length; i++)
        {
            SimplePool.Preload(poolAmounts[i].prefab, poolAmounts[i].amount, poolAmounts[i].parent);
        }
    }

    
}
[System.Serializable]

public class PoolAmount
{
    public GameUnit prefab;
    public Transform parent;
    public int amount;
}

public enum PoolType
{
    //Character
    PLAYER = 0,
    ENEMY = 1,

    //Bullet
    HAMMER_BULLET = 2,
    LOLLIPOP_BULLET = 3,
    KNIFT_BULLET = 4,
    CANDY_BULLET = 5,
    BOOMERANG_BULLET = 6,
    SWIRLY_BULLET = 7,
    AXE_BULLET = 8,
    ICE_BULLET = 9,
    BATTLE_BULLET = 10,
    Z_BULLET = 11,
    ARROW_BULLET = 12,
    UZI_BULLET = 13,


    //Gift
    GIFT = 14,

    //Weapon On
    HAMMER_ON = 15,
    LOLLIPOP_ON = 16,
    KNIFT_ON = 17,
    CANDY_ON = 18,
    BOOMERANG_ON = 19,
    SWIRLY_ON = 20,
    AXE_ON = 21,
    ICE_ON = 22,
    BATTLE_ON = 23,
    Z_ON = 24,
    ARROW_ON = 25,
    UZI_ON = 26,

    //WeaponCustom
    HAMMER_CUSTOM = 27,
    LOLLIPOP_CUSTOM = 28,
    KNIFT_CUSTOM = 29,
    CANDY_CUSTOM = 30,
    BOOMERANG_CUSTOM = 31,
    SWIRLY_CUSTOM = 32,
    AXE_CUSTOM = 33,
    ICE_CUSTOM = 34,
    BATTLE_CUSTOM = 35,
    Z_CUSTOM = 36,
    ARROW_CUSTOM = 37,
    UZI_CUSTOM = 38,

    //Skin
    HAIR_DETAIL = 39,
    PANT_DETAIL = 40,
    SKIN_DETAIL = 41
}
