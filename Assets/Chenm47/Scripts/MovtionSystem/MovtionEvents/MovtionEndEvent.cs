using Common;

namespace ns.Movtion
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class MovtionEndEvent : MovtionEventBase
    {
        public void MovtionEndFired()
        {
            print("动作结束帧事件触发");
            eventBehaviour.GetEventHandler(MovtionEventType.MovtionEnd)?.Invoke(this, new AnimationEventArgs());
        }
    }
}
