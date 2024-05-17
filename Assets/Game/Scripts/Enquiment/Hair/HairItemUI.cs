using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairItemUI : BaseItemUI<HairType>
{
    protected override List<DataBaseType<HairType>> dataBaseTypes
    {
        get { return ConvertToBaseTypeList(enquiListData.dataHairs); }
    }

    public List<DataBaseType<HairType>> ConvertToBaseTypeList(List<DataHairType> dataList)
    {
        // Tạo một danh sách mới để chứa các đối tượng DataBaseType<HairType>
        List<DataBaseType<HairType>> convertedList = new List<DataBaseType<HairType>>();

        // Duyệt qua từng phần tử trong danh sách dataList
        foreach (var data in dataList)
        {
            convertedList.Add(data);
        }

        // Trả về danh sách đã chuyển đổi
        return convertedList;
    }
}
