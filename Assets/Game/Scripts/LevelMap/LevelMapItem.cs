using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapItem : MonoBehaviour
{
    [SerializeField] private Text textLevel;
    [SerializeField] private Button buttonLevel;

    public void OnInit(string textIndex, Action<String> actionLevel)
    {
        textLevel.text = "Level " + textIndex;
        buttonLevel.onClick.AddListener(() =>
        {
            actionLevel?.Invoke(textIndex);
        });
    } 
}
