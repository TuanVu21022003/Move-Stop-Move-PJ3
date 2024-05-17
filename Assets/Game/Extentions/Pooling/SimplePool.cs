using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public static class SimplePool
{
    private static Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();
    public static void Preload(GameUnit prefab, int amount, Transform parent)
    {
        if(prefab == null)
        {
            Debug.Log("Prefab is empty");
            return;
        }
        if(!poolInstance.ContainsKey(prefab.PoolType) || poolInstance[prefab.PoolType] == null)
        {
            Pool p = new Pool();
            p.Preload(prefab, amount, parent); ;
            poolInstance[prefab.PoolType] = p;
        }
    }

    public static T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if(!poolInstance.ContainsKey(poolType)) {
            Debug.Log(poolType + " is NOT Prefab");
            return null;
        }
        return poolInstance[poolType].Spawn(pos, rot) as T;
    }

    public static T SpawnByParent<T>(PoolType poolType, Transform paretnPosition) where T : GameUnit
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.Log(poolType + " is NOT Prefab");
            return null;
        }
        return poolInstance[poolType].SpawnByParent(paretnPosition) as T;
    }

    public static void Despawn(GameUnit unit)
    {
        if (!poolInstance.ContainsKey(unit.PoolType))
        {
            Debug.Log(unit.PoolType + " is NOT Prefab");
        }
        poolInstance[unit.PoolType].Despawn(unit);
    }

    public static void Collect(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.Log(poolType + " is NOT Prefab");
        }
        poolInstance[poolType].Collect();
    }
    
    public static void CollectAll()
    {
        foreach(var item in poolInstance.Values)
        {
            item.Collect();
        }

    }

    public static void Release(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.Log(poolType + " is NOT Prefab");
        }
        poolInstance[poolType].Release();
    }

    public static void ReleaseAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Release();
        }
    }
}

public class Pool
{
    Transform parent;
    GameUnit prefab;
    Queue<GameUnit> inactives = new Queue<GameUnit>();
    List<GameUnit> actives = new List<GameUnit>();
    //khoi tao pool
    public void Preload(GameUnit prefab, int amount, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        for(int i = 0;i < amount; i++)
        {
            Despawn(Spawn(Vector3.zero, Quaternion.identity));
        }
    }

    //lay phan tu tu pool
    public GameUnit Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit unit;
        if(inactives.Count <= 0)
        {
            unit = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            unit = inactives.Dequeue();
        }
        unit.TF.SetPositionAndRotation(pos, rot);
        unit.gameObject.SetActive(true);
        actives.Add(unit);
        return unit;
    }

    public GameUnit SpawnByParent(Transform parentPosition)
    {
        GameUnit unit;
        if (inactives.Count <= 0)
        {
            unit = GameObject.Instantiate(prefab, parentPosition);
        }
        else
        {
            unit = inactives.Dequeue();
        }
        unit.gameObject.SetActive(true);
        actives.Add(unit);
        return unit;
    }

    //tra phan tu vao trong pool
    public void Despawn(GameUnit unit)
    {
        if(unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inactives.Enqueue(unit);
            unit.gameObject.SetActive(false);
        }

    }

    //thu tap tat ca phan tu dang dung ve pool
    public void Collect()
    {
        while(actives.Count > 0)
        {
            Despawn(actives[0]);
        }
    }

    //Destroy tat ca phan tu
    public void Release()
    {
        Collect();
        while(inactives.Count > 0)
        {
            GameObject.Destroy(inactives.Dequeue());
        }
        inactives.Clear();
    }
}
