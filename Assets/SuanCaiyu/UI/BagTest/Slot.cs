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
            Debug.Log(idx);
            //修改文本
            text.text = Inventory.Instance.inventoryItems[idx].itemName;
            //修改项名称
            transform.name = Inventory.Instance.inventoryItems[idx].itemName;
            //展示图片
            image.sprite = Inventory.Instance.inventoryItems[idx].icon;

        }

        void ScrollCellReturn()
        {
            Debug.Log("回收触发");
        }
    }

