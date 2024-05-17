using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PantDetailSelection : BaseDetailSelection<PantType>
{
    protected override PoolType poolEqui => PoolType.PANT_DETAIL;

    protected override PantType equiDefault => PantType.DEFAULT;

    protected override PantType typeOwn => LoadDataPlayer.Instance.LoadData().pantPlayer;

    protected override bool[] listOwn => LoadDataPlayer.Instance.LoadData().listPantOwn;
}
