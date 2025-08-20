using AI.FSM.Framework;
using Common;
using ns.BagSystem;
using ns.ItemInfos;
namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class TakeMedicineState : MovtionState
    {
        public override void Init()
        {
            StateID = FSMStateID.TakeMedicine;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            //修改当前动作为喝药动作
            fSMBase.characterInfo.CurrentMovtionID = PlayerFSMBase.Instance.playerInfo.TakeMedicineMovtionID;
            base.EnterState(fSMBase);
        }

        protected override void OnMovtionStart(object sender, AnimationEventArgs e)
        {
            base.OnMovtionStart(sender, e);
            //结算物品效果
            var item = PlayerFSMBase.Instance.playerEquipmentManager.GetCurrentItem();
            ConsumableItemInfo info = item.itemInfo as ConsumableItemInfo;
            foreach (var effect in info.ItemEffects)
            {
                effect.ApplyEffect(e.fSMBase.gameObject);
            }
            //减少物品
            InventoryManager.Instance.RemoveItem(info.ItemID);
        }

    }
}
