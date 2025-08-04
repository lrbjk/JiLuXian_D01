using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// 要求组件必须附加UnityEngine.UI.LoopScrollRect组件
[RequireComponent(typeof(UnityEngine.UI.LoopScrollRect))]
// 禁止在同一个GameObject上附加多个该组件
[DisallowMultipleComponent]
public class BagScroller : MonoBehaviour, LoopScrollPrefabSource, LoopScrollDataSource
{
    public GameObject item;// 列表项的预制体
    // public int totalCount = -1;// 列表总项数，-1表示无限数量
    // 对象池实现（示例用Stack实现）
    Stack<Transform> pool = new Stack<Transform>();

    /// <summary>
    /// 获取列表项对象（LoopScrollPrefabSource接口实现）
    /// </summary>
    /// <param name="index">项索引</param>
    /// <returns>列表项GameObject</returns>
    public GameObject GetObject(int index)
    {
        // 如果对象池为空，则新实例化一个对象
        if (pool.Count == 0)
        {
            return Instantiate(item);
        }

        // 从对象池中取出一个对象
        Transform candidate = pool.Pop();
        candidate.gameObject.SetActive(true);// 激活对象
        return candidate.gameObject;
    }

    /// <summary>
    /// 回收列表项对象（LoopScrollPrefabSource接口实现）
    /// </summary>
    /// <param name="trans">要回收的Transform</param>
    public void ReturnObject(Transform trans)
    {
        // 发送回收消息（可选）
        trans.SendMessage("ScrollCellReturn", SendMessageOptions.DontRequireReceiver);

        // 禁用对象并重置父节点
        trans.gameObject.SetActive(false);
        trans.SetParent(transform, false);

        // 将对象放回池中
        pool.Push(trans);
    }

    /// <summary>
    /// 为列表项提供数据（LoopScrollDataSource接口实现）
    /// </summary>
    /// <param name="transform">列表项的Transform</param>
    /// <param name="idx">项索引</param>
    public void ProvideData(Transform transform, int idx)
    {
        // 发送索引消息，通知列表项更新显示
        transform.SendMessage("ScrollCellIndex", idx);
    }

    void Start()
    {
        // 获取LoopScrollRect组件并初始化
        var ls = GetComponent<LoopScrollRect>();
        ls.prefabSource = this;// 设置预制体源
        ls.dataSource = this;// 设置数据源
        ls.totalCount = Inventory.Instance.inventoryItems.Count;// 设置总数量
        ls.RefillCells();// 填充单元格
    }
}
