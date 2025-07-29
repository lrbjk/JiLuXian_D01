using ns.Character;
using System;
using UnityEngine;

namespace ns.Movtion
{

    public enum MovtionEventType
    {
        [Tooltip("前摇结束")]
        PreMovtionEnd, // 前摇结束
        [Tooltip("动作开始")]
        MovtionStart, // 动作生效开始（攻击开始、道具使用开始......）
        [Tooltip("动作结束")]
        MovtionEnd, // 动作生效结束
        [Tooltip("动作后摇开始")]
        MovtionRecovery, // 动作后摇开始
    }
    [Serializable]
    public struct MovtionEventParams
    {
        public MovtionEventType EventType; // 事件类型
        [Tooltip("发生的动画帧数")]
        public int AnimationFrame; // 发生的动画帧帧数
    }

    [CreateAssetMenu(menuName = "Movtion/MovtionInfo")]
    /// <summary>
    /// 描述：
    /// </summary>
    public class MovtionInfo : ScriptableObject
    {
        [Tooltip("动作ID")]
        public int MovtionID;
        [Tooltip("动作名称")]
        public string MovtionName;
        [Tooltip("连击ID")]
        public int ComboMovtionID;
        [Tooltip("动画名称 ")]
        public string AnimationName;
        [Tooltip("动作倍率")]
        public float ActionMultiplier;
        [Tooltip("处决动作倍率")]
        public float ExecutionMultiplier;//注意非处决动作必须为0
        [Tooltip("动作韧性值")]
        public int MovtionPoise;
        [Tooltip("动作削韧值")]
        public int MovtionReducedPoise;
        [Tooltip("前摇期间转向速度")]
        public float PreMovtionRotateSpeed;
        [Tooltip("物理攻击属性")]
        public ResistanceType PhysicalResistanceType;

        //用动作事件来描述发生帧
        [Tooltip("动作事件列表")]
        public MovtionEventParams[] MovtionEvents;

        [Tooltip("正面命中目标受击动作")]
        public int FrontDamagedMovtionID;
        [Tooltip("背面命中目标受击动作")]
        public int BackDamagedMovtionID;
        [Tooltip("命中目标死亡动作")]
        public int DeadMovtionID;
    }
}
