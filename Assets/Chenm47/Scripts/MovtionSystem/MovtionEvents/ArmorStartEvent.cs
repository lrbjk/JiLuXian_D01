using AI.FSM.Framework;
using Common;

namespace ns.Movtion
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ArmorStartEvent : MovtionEventBase
    {
        FSMBase fSMBase;

        private void Awake()
        {
            fSMBase = GetComponentInParent<FSMBase>(true);
        }
        public void ArmorStartFired()
        {
            eventBehaviour.GetEventHandler(MovtionEventType.ArmorStart)?.Invoke(this, new AnimationEventArgs(fSMBase));
        }
    }
}
