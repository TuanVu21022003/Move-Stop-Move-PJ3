using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SkinDetailSelection : BaseDetailSelection<SkinType>
{
    protected override PoolType poolEqui => PoolType.SKIN_DETAIL;

    protected override SkinType equiDefault => SkinType.NORMAL;

    protected override SkinType typeOwn => LoadDataPlayer.Instance.LoadData().skinPlayer;

    protected override bool[] listOwn => LoadDataPlayer.Instance.LoadData().listSkinOwn;
}
