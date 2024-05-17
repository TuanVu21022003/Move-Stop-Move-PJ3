using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelCharacterData
{
    [SerializeField] private float _scaleBody;

    public float scaleBody => _scaleBody;
}