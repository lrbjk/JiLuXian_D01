using ns.Character.Player;
using System.Collections.Generic;
using UnityEngine;

namespace ns.ItemInfos
{
    /// <summary>
    /// 描述：装备信息
    /// </summary>
    public class EquipmentInfo : ItemInfo
    {
        //防御力
        public int DEF;
        protected override void InitializeDefaults()
        {
            base.InitializeDefaults();
            MaxCount = 1;
        }
    }
}
