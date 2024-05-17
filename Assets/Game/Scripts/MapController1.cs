using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController1 : Singleton<MapController1>
{
    [SerializeField] private List<Transform> listPos = new List<Transform>();
    private List<Transform> listPosTMP = new List<Transform>();


    public Vector3 RandomPos()
    {
        if(listPosTMP.Count == 0)
        {
            for(int i = 0; i < listPos.Count; i++)
            {
                listPosTMP.Add(listPos[i]);
            }
        }
        Transform tmp =  listPosTMP[Random.Range(0, listPosTMP.Count)];
        listPosTMP.Remove(tmp);
        return tmp.position;
       
    }

    public void ResetlistPos()
    {
        listPosTMP = new List<Transform>(); 
    }
}
