using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    [SerializeField] private Text textCount;

    public override void Setup()
    {
        base.Setup();
        textCount.text = "Alive " + PlayManager.Instance.countEnemyByLevel;
    }

    public void UpdateTextCount(int count)
    {
        textCount.text = "Alive " + count;
    }
}
