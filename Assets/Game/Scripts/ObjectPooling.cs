using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling> 
{
    Dictionary<GameObject, List<GameObject>> poolingObjects = new Dictionary<GameObject, List<GameObject>>();

    public GameObject GetObject(GameObject obj)
    {
        if(poolingObjects.ContainsKey(obj))
        {
            foreach(GameObject o  in poolingObjects[obj])
            {
                if(o.activeSelf == false)
                {
                    return o;
                }
            }
            GameObject g1 = Instantiate(obj, this.transform.position, this.transform.rotation);
            g1.SetActive(false);
            poolingObjects[obj].Add(g1);
            return g1;
        }
        List<GameObject> list = new List<GameObject>();
        GameObject g2 = Instantiate(obj, this.transform.position, this.transform.rotation);
        g2.SetActive(false);
        list.Add(g2);
        poolingObjects.Add(obj, list);
        return g2;
    }
}
