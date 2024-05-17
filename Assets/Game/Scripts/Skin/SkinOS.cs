using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkinOS", menuName = "ScriptableObjects/SkinOS")]
public class SkinOS : ScriptableObject
{
    public List<SkinData> list = new List<SkinData>();
}
