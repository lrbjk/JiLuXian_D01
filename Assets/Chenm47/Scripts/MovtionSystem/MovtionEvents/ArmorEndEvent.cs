using AI.FSM.Framework;
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
    public class ArmorEndEvent : MovtionEventBase
    {
        FSMBase fSMBase;

        private void Awake()
        {
            fSMBase = GetComponentInParent<FSMBase>(true);
        }
        public void ArmorEndFired()
        {
            eventBehaviour.GetEventHandler(MovtionEventType.ArmorEnd)?.Invoke(this, new AnimationEventArgs(fSMBase));
        }
    }
}
