using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class HairDetailSelection : BaseDetailSelection<HairType>
{
    protected override PoolType poolEqui => PoolType.HAIR_DETAIL;

    protected override HairType equiDefault => HairType.DEFAULT;

    protected override HairType typeOwn => LoadDataPlayer.Instance.LoadData().hairPlayer;

    protected override bool[] listOwn => LoadDataPlayer.Instance.LoadData().listHairOwn;
}
