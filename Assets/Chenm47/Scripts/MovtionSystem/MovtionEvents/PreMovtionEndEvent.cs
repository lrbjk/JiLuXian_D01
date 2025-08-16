using AI.FSM.Framework;
using Common;

namespace ns.Movtion
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PreMovtionEndEvent : MovtionEventBase
    {
        FSMBase fSMBase;

        private void Awake()
        {
            fSMBase = GetComponentInParent<FSMBase>(true);
        }
        public void PreMovtionEndFired()
        {
            print("前摇结束帧事件触发");
            eventBehaviour.GetEventHandler(MovtionEventType.PreMovtionEnd)?.Invoke(this, new AnimationEventArgs(fSMBase));
        }
    }
}
