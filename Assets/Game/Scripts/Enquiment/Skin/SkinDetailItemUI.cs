using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinDetailItemUI : BaseDetailItemUI<SkinType>
{
    protected override SkinType typeOwn => LoadDataPlayer.Instance.LoadData().skinPlayer;

    protected override bool[] listOwn => LoadDataPlayer.Instance.LoadData().listSkinOwn;
}
