using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/
namespace ns.Movtion
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ArmorEndEvent: MovtionEventBase
    {
        public void ArmorEndFired()
        {
            eventBehaviour.GetEventHandler(MovtionEventType.ArmorEnd)?.Invoke(this, new AnimationEventArgs());
        }
    }
}
