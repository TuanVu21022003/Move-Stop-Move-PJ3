using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkinShop : UICanvas
{
    [SerializeField] private EquiItemSelection equiSelection;
    public override void Setup()
    {
        base.Setup();
        PlayManager.Instance.cameraPlayer.ChangePositionShopToSkin();
        PlayManager.Instance.player.ChangeAnim(KeyConstants.Anim_Dance);
        equiSelection.OnInit();
    }
}
