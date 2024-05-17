using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataPlayer : Singleton<LoadDataPlayer>
{
    public PlayerData LoadData()
    {
        string dataJson = PlayerPrefs.GetString(KeyConstants.KEY_SAVEPLAYERDATA);
        if(string.IsNullOrEmpty(dataJson))
        {
            return new PlayerData();
        }
        return JsonUtility.FromJson<PlayerData>(dataJson);  
    }

    public void SaveDataPlayer(PlayerData data)
    {
        string dataString = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(KeyConstants.KEY_SAVEPLAYERDATA, dataString);
    }
}
