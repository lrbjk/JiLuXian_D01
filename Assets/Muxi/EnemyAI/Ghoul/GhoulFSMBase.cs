using AI.FSM.Framework;
using EnemyAIBase;
using UnityEngine;

namespace AI.FSM
{
    [RequireComponent(typeof(EnemyInfo))]
    [RequireComponent(typeof(Ghoul))]
    public class GhoulFSMBase : FSMBase
    {
        [Header("Ghoul FSM Settings")]
        [Tooltip("Ghoul FSM配置文件名")]
        public string ghoulConfigFileName = "GhoulFSMConfig.txt";

        // Ghoul特有的组件引用
        [HideInInspector]
        public Ghoul ghoul;

        protected override void Start()
        {
            // 设置配置文件名
            ConfigFullFileName = ghoulConfigFileName;

            // 设置默认状态为GhoulIdle
            defaultStateID = FSMStateID.GhoulIdle;

            // 获取Ghoul组件
            ghoul = GetComponent<Ghoul>();

            Debug.Log($"GhoulFSMBase: 开始初始化，TestMode={TestMode}, DefaultStateID={defaultStateID}");

            // 调用基类的Start方法，这会初始化所有组件并配置FSM
            base.Start();

            Debug.Log($"GhoulFSMBase: 初始化完成，CurrentState={CurrentState?.StateID.ToString() ?? "NULL"}");
        }



        // protected override void Update()
        // {
        //     // 调用基类的Update方法来处理状态机逻辑
        //     base.Update();
        // }
    }
}