using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelCharacterOS", menuName = "ScriptableObjects/LevelCharacterOS")]

public class LevelCharacterOS : ScriptableObject
{
    public List<LevelCharacterData> list = new List<LevelCharacterData>();
}