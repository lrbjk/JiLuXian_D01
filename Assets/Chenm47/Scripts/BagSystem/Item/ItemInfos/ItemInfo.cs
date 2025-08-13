using UnityEngine;

namespace ns.ItemInfos
{
    public enum ItemType
    {
        Consumable,    // 消耗品
        Material,      // 材料
        Currency,      // 换金道具
        HeadEquipMent,          // 头部
        BodyEquipment,          // 衣装
        KernelEquipment,          // 灰质核心
        Spell,         // 法术
        Key,       // 重要道具
        LeftHandWeapon,      // 左手武器
        RightHandWeapon      // 右手武器
    }


    /// <summary>
    /// 描述：
    /// </summary>
    public abstract class ItemInfo : ScriptableObject
    {
        public Sprite ItemIcon;
        public string ItemName;
        public string ItemDescription;
        [HideInInspector]
        public int ItemID;
        public ItemType iType;
        public int MaxCount;

        [SerializeField, HideInInspector]
        private bool _isInitialized = false; // 序列化标志位

        protected virtual void OnEnable()
        {
            // 仅在未初始化时执行
            if (!_isInitialized)
            {
                InitializeDefaults();
                _isInitialized = true;
            }
        }

        // 虚方法供子类覆盖
        protected virtual void InitializeDefaults() { }

    }
}
