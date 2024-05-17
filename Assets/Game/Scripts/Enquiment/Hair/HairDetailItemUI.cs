using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairDetailItemUI : BaseDetailItemUI<HairType> 
{
    protected override HairType typeOwn => LoadDataPlayer.Instance.LoadData().hairPlayer;

    protected override bool[] listOwn => LoadDataPlayer.Instance.LoadData().listHairOwn;
}
