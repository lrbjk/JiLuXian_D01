using Common;

namespace ns.Movtion
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PreMovtionEndEvent : MovtionEventBase
    {
        public void PreMovtionEndFired()
        {
            print("前摇结束帧事件触发");
            eventBehaviour.GetEventHandler(MovtionEventType.PreMovtionEnd)?.Invoke(this, new AnimationEventArgs());
        }
    }
}
