using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantItemUI : BaseItemUI<PantType>
{
    protected override List<DataBaseType<PantType>> dataBaseTypes
    {
        get { return ConvertToBaseTypeList(enquiListData.dataPants); }
    }

    public List<DataBaseType<PantType>> ConvertToBaseTypeList(List<DataPantType> dataList)
    {
        // Tạo một danh sách mới để chứa các đối tượng DataBaseType<HairType>
        List<DataBaseType<PantType>> convertedList = new List<DataBaseType<PantType>>();

        // Duyệt qua từng phần tử trong danh sách dataList
        foreach (var data in dataList)
        {
            convertedList.Add(data);
        }

        // Trả về danh sách đã chuyển đổi
        return convertedList;
    }
}
