using Common;
using UnityEngine;

/*

*/
namespace ns.Movtion
{
    [RequireComponent(typeof(AnimationEventBehaviour))]
    /// <summary>
    /// 描述：类命名规则：ns.Movtion.MovtionEventType+Event，方法命名：MovtionEventType+Fired
    /// </summary>
    public class MovtionEventBase : MonoBehaviour
    {
        protected AnimationEventBehaviour eventBehaviour;
        private void Start()
        {
            eventBehaviour = GetComponent<AnimationEventBehaviour>();
        }
    }
}
