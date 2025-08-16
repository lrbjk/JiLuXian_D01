using AI.FSM.Framework;
using Common;

namespace ns.Movtion
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class MovtionRecoveryEvent : MovtionEventBase
    {
        FSMBase fSMBase;

        private void Awake()
        {
            fSMBase = GetComponentInParent<FSMBase>(true);
        }

        public void MovtionRecoveryFired()
        {
            print("后摇开始帧事件触发");
            eventBehaviour.GetEventHandler(MovtionEventType.MovtionRecovery)?.Invoke(this, new AnimationEventArgs(fSMBase));
        }
    }
}
