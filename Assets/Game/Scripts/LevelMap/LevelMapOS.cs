using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelMapOS", menuName = "ScriptableObjects/LevelMapOS")]

public class LevelMapOS : ScriptableObject
{
    public List<LevelMapData> list = new List<LevelMapData>();
}
