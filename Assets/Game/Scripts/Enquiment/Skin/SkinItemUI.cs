using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinItemUI : BaseItemUI<SkinType>
{
    protected override List<DataBaseType<SkinType>> dataBaseTypes
    {
        get { return ConvertToBaseTypeList(enquiListData.dataSkins); }
    }

    public List<DataBaseType<SkinType>> ConvertToBaseTypeList(List<DataSkinType> dataList)
    {
        // Tạo một danh sách mới để chứa các đối tượng DataBaseType<HairType>
        List<DataBaseType<SkinType>> convertedList = new List<DataBaseType<SkinType>>();

        // Duyệt qua từng phần tử trong danh sách dataList
        foreach (var data in dataList)
        {
            convertedList.Add(data);
        }

        // Trả về danh sách đã chuyển đổi
        return convertedList;
    }
}
