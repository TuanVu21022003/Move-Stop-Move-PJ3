using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : GameUnit
{
    [SerializeField] private Transform shopPosition;
    [SerializeField] private Transform skinPosition;

    private void Start()
    {
        TF.position = shopPosition.position;
        TF.rotation = shopPosition.rotation;
    }

    public void ChangePositionShopToSkin()
    {
        TF.position = skinPosition.position; TF.rotation = skinPosition.rotation;
    }

    public void ChangePositionSkinToShop()
    {
        TF.position = shopPosition.position; TF.rotation = shopPosition.rotation;
    }
}
