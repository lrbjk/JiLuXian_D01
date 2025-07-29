using Common;

namespace ns.Movtion
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ArmorStartEvent : MovtionEventBase
    {
        public void ArmorStartFired()
        {
            eventBehaviour.GetEventHandler(MovtionEventType.ArmorStart)?.Invoke(this, new AnimationEventArgs());
        }
    }
}
