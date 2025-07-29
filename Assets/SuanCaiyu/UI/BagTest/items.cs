using UnityEngine;

//物品信息脚本
public class Item : MonoBehaviour
{
    public string itemName = "New Item";//物品名称
    [TextArea]
    public string description = "New Description";//物品描述
    public Sprite icon;//物品图标
}
