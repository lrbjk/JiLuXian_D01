using Common;
using ns.Character;
using ns.Movtion;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CharacterInfo = ns.Character.CharacterInfo;


namespace AI.FSM.Framework
{
    /// <summary>
    /// 描述：状态机，管理所有状态
    /// </summary>
    public class FSMBase : MonoBehaviour
    {
        [Tooltip("默认初始状态ID")]
        public FSMStateID defaultStateID;
        public string ConfigFullFileName;
        public bool TestMode = false; //测试模式，是否使用配置文件

        #region 为状态类提供的成员
        [HideInInspector]
        public Animator animator;
        [HideInInspector]
        public CharacterEquipmentManager equipmentManager;
        [HideInInspector]
        public AnimationEventBehaviour animationEventBehaviour;
        [HideInInspector]
        public CharacterMovtionManager movtionManager;
        [HideInInspector]
        public CharacterAnimationHandler animationHandler;
        [HideInInspector]
        public CharacterInfo characterInfo;
        #endregion

        private List<FSMState> states;
        private FSMState currentState;
        public FSMState CurrentState { get => currentState; }
        public string CurrentStateName;

        protected virtual void Start()
        {
            //获取组件
            equipmentManager = GetComponent<CharacterEquipmentManager>();
            animator = GetComponentInChildren<Animator>(true);
            animationEventBehaviour = GetComponentInChildren<AnimationEventBehaviour>(true);
            movtionManager = GetComponent<CharacterMovtionManager>();
            animationHandler = GetComponent<CharacterAnimationHandler>();
            characterInfo = GetComponent<CharacterInfo>();

            if (!TestMode)
                FSMConfig();
            else
                FSMConfigTest();
            SetDefaultState();
        }

        private void Update()
        {
            // 确保currentState不为null
            if (currentState == null)
            {
                Debug.LogError($"FSMBase.Update: currentState为null！GameObject: {gameObject.name}");
                return;
            }

            currentState.Reason(this);//判断当前状态
            currentState.ActionState(this);//行动
        }

        //配置状态机
        private void FSMConfig()
        {
            states = new List<FSMState>();

            //读取配置文件即可，例如AI_01.txt
            var configurationReader = AIConfigurationReaderFactory.GetStatesMap(ConfigFullFileName);

            foreach (var state in configurationReader.StatesMap)
            {
                string currentStateName = state.Key;
                //如果是末尾的AnyState，则对所有状态都添加
                if (currentStateName == "AnyState")
                {
                    foreach (var existedstate in states)
                    {
                        foreach (var transitionMap in state.Value)
                        {//条件状态映射
                         // transitionMap.Key条件
                         //transitionMap.Value对应状态
                            FSMTriggerID fSMTriggerID = (FSMTriggerID)Enum.Parse(typeof(FSMTriggerID), transitionMap.Key);
                            FSMStateID fSMStateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), transitionMap.Value);
                            existedstate.AddMap(fSMTriggerID, fSMStateID);
                        }
                    }
                    return;
                }

                string typeName = "AI.FSM." + currentStateName + "State";
                Type t = Type.GetType(typeName);

                Debug.Log($"FSMBase.FSMConfig: 尝试创建状态 {currentStateName} -> {typeName}");

                if (t == null)
                {
                    Debug.LogError($"FSMBase.FSMConfig: 找不到状态类型 {typeName}！GameObject: {gameObject.name}");          
                    continue;
                }

                FSMState currentState = Activator.CreateInstance(t) as FSMState;
                if (currentState == null)
                {
                    Debug.LogError($"FSMBase.FSMConfig: 无法创建状态实例 {typeName}！GameObject: {gameObject.name}");
                    continue;
                }
                currentState.Init();

                states.Add(currentState);
                // Debug.Log($"FSMBase.FSMConfig: 成功创建状态 {typeName}");

                //如果是转移表是空的，跳过添加转移
                if (state.Value.Count == 0)
                    continue;
                foreach (var transitionMap in state.Value)
                {//条件状态映射
                    // transitionMap.Key条件
                    //transitionMap.Value对应状态
                    FSMTriggerID fSMTriggerID = (FSMTriggerID)Enum.Parse(typeof(FSMTriggerID), transitionMap.Key);
                    FSMStateID fSMStateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), transitionMap.Value);
                    currentState.AddMap(fSMTriggerID, fSMStateID);
                }

            }
        }

        private void FSMConfigTest()
        {
            Type t = Type.GetType("AI.FSM.TestState");
            FSMState currentState = Activator.CreateInstance(t) as FSMState;

            // 重要：必须调用Init方法来设置StateID
            if (currentState != null)
            {
                currentState.Init();
            }

            states = new List<FSMState> { currentState };
        }

        private FSMState defulatState;

        private void SetDefaultState()
        {
            //设置默认状态
            if (states == null || states.Count == 0)
            {
                Debug.LogError($"FSMBase.SetDefaultState: states列表为空！GameObject: {gameObject.name}");
                return;
            }

            defulatState = states.Find(s => s.StateID == defaultStateID);
            if (defulatState == null)
            {
                Debug.LogError($"FSMBase.SetDefaultState: 找不到默认状态 {defaultStateID}！GameObject: {gameObject.name}");
                Debug.Log($"可用状态: {string.Join(", ", states.ConvertAll(s => s.StateID.ToString()))}");
                return;
            }

            currentState = defulatState;
            CurrentStateName = currentState.StateID.ToString();
            currentState.EnterState(this);

            // Debug.Log($"FSMBase.SetDefaultState: 成功设置默认状态 {defaultStateID}");
        }

        public void SwitchState(FSMStateID stateId)
        {
            FSMState state =
                stateId == FSMStateID.Default ?
                defulatState :
                states.Find(s => s.StateID == stateId);

            // 检查目标状态是否存在
            if (state == null)
            {
                Debug.LogError($"FSMBase.SwitchState: 找不到状态 {stateId}！GameObject: {gameObject.name}");
                return;
            }

        
            if (currentState != null)
            {
                currentState.ExitState(this);
            }
            else
            {
                Debug.LogWarning($"FSMBase.SwitchState: currentState为null，跳过ExitState。GameObject: {gameObject.name}");
            }

            currentState = state;
            CurrentStateName = state.StateID.ToString();

            //进入下一状态
            currentState.EnterState(this);
        }

    }
}
