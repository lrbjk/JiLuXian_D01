using ns.BagSystem;
using ns.ItemInfos;
using System;
using UnityEditor;
using UnityEngine;

namespace ns.Editor
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ReloadSOInfos
    {
        //[MenuItem("Tools/Resources/ReloadItemInfos")]
        //public static void ReloadItemInfos()
        //{
        //    string basePath = "SO/ItemInfos/";
        //    int currentID = 1;
        //    //读取配置文件
        //    foreach (var itypeName in Enum.GetNames(typeof(ItemType)))
        //    {
        //        var t = Type.GetType("ns.ItemInfos." + itypeName + "ItemInfo");
        //        string path = basePath + itypeName;
        //        foreach (ItemInfo iteminfo in Resources.LoadAll<ItemInfo>(path))
        //        {
        //            //自动配置ID从1开始
        //            iteminfo.ItemID = currentID;
        //            //Debug.Log(iteminfo.name);

        //            //自动配置名称为物品名
        //            if (string.IsNullOrEmpty(iteminfo.ItemName))
        //            {
        //                iteminfo.ItemName = iteminfo.name;
        //            }
        //            currentID++;
        //            nameToID.Add(iteminfo.ItemName, iteminfo.ItemID);
        //            itemInfos.Add(iteminfo.ItemID, iteminfo);
        //        }
        //    }

        //}
    }
}
