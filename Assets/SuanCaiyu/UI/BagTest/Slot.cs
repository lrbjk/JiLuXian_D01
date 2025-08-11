using ns.BagSystem;
using ns.BagSystem.Freamwork;
using Scy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//背包插槽脚本
public class Slot : MonoBehaviour
{


    public Text text;

    public Image image;

    

    void ScrollCellIndex(int idx)
        {
             InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.Material, out var itemLst);
             Debug.Log(idx);
           
            //修改文本
            text.text = itemLst[idx].itemInfo.name;
            //修改项名称
            transform.name = itemLst[idx].itemInfo.name;
            //展示图片
            image.sprite = itemLst[idx].itemInfo.ItemIcon;

        }

        void ScrollCellReturn()
        {
            Debug.Log("回收触发");
        }
    }

