using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpOS", menuName = "ScriptableObjects/LevelUpOS")]

public class LevelUpOS : ScriptableObject
{
    public List<int> list = new List<int>();
}