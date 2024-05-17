using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnquiListOS", menuName = "ScriptableObjects/EnquiListOS")]
public class EnquiListOS : ScriptableObject
{
    public List<DataHairType> dataHairs = new List<DataHairType>();
    public List<DataPantType> dataPants = new List<DataPantType>();
    public List<DataSkinType> dataSkins = new List<DataSkinType>();
}

public abstract class DataBaseType<T> where T : System.Enum
{
    public Sprite spriteType;
    public T equiType;
    public int coinEqui;
}

[Serializable]

public class DataHairType : DataBaseType<HairType>
{
    
}

[Serializable]

public class DataPantType : DataBaseType<PantType>
{

}

[Serializable]

public class DataSkinType : DataBaseType<SkinType>
{

}