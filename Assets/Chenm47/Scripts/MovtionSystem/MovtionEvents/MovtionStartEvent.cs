using AI.FSM.Framework;
using Common;

namespace ns.Movtion
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class MovtionStartEvent : MovtionEventBase
    {
        FSMBase fSMBase;

        private void Awake()
        {
            fSMBase = GetComponentInParent<FSMBase>(true);
        }
        public void MovtionStartFired()
        {
            // 触发动作开始事件
            print("动作开始事件触发");
            var handler = eventBehaviour.GetEventHandler(MovtionEventType.MovtionStart);
            handler?.Invoke(this, new AnimationEventArgs(fSMBase));
        }
    }
}
