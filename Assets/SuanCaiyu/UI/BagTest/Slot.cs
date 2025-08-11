using UnityEngine;
using UnityEngine.UI;

// 背包插槽脚本
public class Slot : MonoBehaviour
{
    public Text text;
    public Text description;
    public Image image;
    public int number;

    // 不再需要存储BagList引用，因为我们会通过父对象获取
    private BagList bagList;

    void ScrollCellIndex(int idx)
    {
        // 从父对象获取BagList组件
        if (bagList == null)
        {
            bagList = GetComponentInParent<BagList>();
            if (bagList == null)
            {
                Debug.LogError("无法找到父对象上的BagList组件");
                return;
            }
        }

        // 检查索引是否有效
        if (idx < 0 || idx >= bagList.bagItems.Count)
        {
            Debug.LogError($"无效的索引: {idx}, 列表长度: {bagList.bagItems.Count}");
            return;
        }

        // 获取对应物品
        var item = bagList.bagItems[idx];
        if (item == null)
        {
            Debug.LogError($"索引 {idx} 处的物品为null");
            return;
        }

        // 更新UI
        if (text != null)
        {
            text.text = item.text_1.text; // 假设BagItem有itemName属性
            transform.name = item.text_1.text;
        }

        if (image != null && item.image_1 != null) // 假设BagItem有icon属性
        {
            image.sprite = item.image_1.sprite;
        }

        // 更新其他属性
        if (description != null)
        {
            //description.text = item.text.text; // 假设BagItem有description属性
        }

        number = idx; // 存储当前索引
        //Debug.Log(number);
    }

    void ScrollCellReturn()
    {
        Debug.Log("回收触发");
        // 可以在这里重置插槽状态
        if (text != null) text.text = "";
        if (description != null) description.text = "";
        if (image != null) image.sprite = null;
        number = -1;
    }
}
