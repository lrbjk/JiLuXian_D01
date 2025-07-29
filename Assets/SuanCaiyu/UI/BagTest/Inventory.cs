using System.Collections.Generic;

//库存系统脚本
public class Inventory : SingletonMono<Inventory>
{
    public List<Item> inventoryItems = new List<Item>();//背包的物品列表
}
