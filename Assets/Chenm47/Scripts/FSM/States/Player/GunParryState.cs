using AI.FSM.Framework;
using Common;
using ns.Character.Player;
using ns.Movtion;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class GunParryState : MovtionState
    {
        public override void Init()
        {
            StateID = FSMStateID.GunParry;
        }

        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            return PlayerFSMBase.Instance.movtionManager.GetMovtionInfo(PlayerFSMBase.Instance.playerInfo.GunParryMovtionID);
        }

        protected override void PlayAnimation(FSMBase fSMBase)
        {
            ////移动变慢
            //float multiplier = 0.8f;
            //fSMBase.animator.SetFloat("MoveAnimationSpeed", multiplier);
            //播放arms层级的动画
            PlayerFSMBase.Instance.playerMotor3D.StopMove();
            fSMBase.animationHandler.PlayTargetAnimation(movtionInfo.AnimationName, true, 0.1f);
        }

        protected override void OnMovtionStart(object sender, AnimationEventArgs e)
        {
            base.OnMovtionStart(sender, e);
            Debug.Log("生成子弹");
            //生成子弹
            GameObject go = Object.Instantiate(PlayerFSMBase.Instance.BulletPrefab);
            go.transform.position = PlayerFSMBase.Instance.BulletCreatPos.position;
            //设置方向
            //如果是锁定状态，直接获取锁定敌人方向
            var info = PlayerFSMBase.Instance.playerInfo;
            Vector3 dir = PlayerFSMBase.Instance.transform.forward;
            if (PlayerFSMBase.Instance.playerInput.LockViewTrigger)
            {
                dir = PlayerFSMBase.Instance.playerInfo.LockedTargetTF.position - PlayerFSMBase.Instance.BulletCreatPos.position;
            }
            go.GetComponent<PlayerBullet>().Init(dir, 10f, PlayerFSMBase.Instance.playerInfo);
        }

        //public override void ExitState(FSMBase fSMBase)
        //{
        //    base.ExitState(fSMBase);
        //    ////恢复移动速度
        //    ////fSMBase.animator.SetFloat("MoveAnimationSpeed", 1f);
        //    ////播放Arms的Empty状态
        //    //PlayerFSMBase.Instance.animationHandler.PlayTargetAnimation("Empty", false, 0.2f);
        //}

    }
}
