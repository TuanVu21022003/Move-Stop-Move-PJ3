using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapSelection : MonoBehaviour
{
    [SerializeField] private Transform parentPosition;
    [SerializeField] private LevelMapItem levelMapItemPrefab;

    private void Start()
    {
        SelectedLevelMapList();
    }

    public void SelectLevelMapItem(string textIndex)
    {
        LevelMapItem levelItem = Instantiate(levelMapItemPrefab, parentPosition);
        levelItem.OnInit(textIndex, OnHandleButtonLevel);
    }

    public void SelectedLevelMapList()
    {
        for(int i = 0; i < PlayManager.Instance.levelMapData.list.Count; i++)
        {
            SelectLevelMapItem(PlayManager.Instance.levelMapData.list[i].levelIndex.ToString());
        }
    }

    public void OnHandleButtonLevel(string textIndex)
    {
        UIManager.Instance.CloseUI<UISelectionMenu>(0);
        PlayManager.Instance.SpawnLevel(textIndex);
    }
}
