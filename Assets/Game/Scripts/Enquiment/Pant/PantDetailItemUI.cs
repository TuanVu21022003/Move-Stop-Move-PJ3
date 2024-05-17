using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantDetailItemUI : BaseDetailItemUI<PantType>
{
    protected override PantType typeOwn => LoadDataPlayer.Instance.LoadData().pantPlayer;

    protected override bool[] listOwn => LoadDataPlayer.Instance.LoadData().listPantOwn;
}