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
            ConfigFullFileName = ghoulConfigFileName;
            
            defaultStateID = FSMStateID.GhoulIdle;
            
            ghoul = GetComponent<Ghoul>();

            Debug.Log($"GhoulFSMBase: 开始初始化，TestMode={TestMode}, DefaultStateID={defaultStateID}");
            
            base.Start();

            Debug.Log($"GhoulFSMBase: 初始化完成，CurrentState={CurrentState?.StateID.ToString() ?? "NULL"}");
        }

        
    }
}