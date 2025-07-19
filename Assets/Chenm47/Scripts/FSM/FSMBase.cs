using Common;
using ns.Character;
using ns.Movtion;
using System;
using System.Collections.Generic;
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
                Type t = Type.GetType("AI.FSM." + currentStateName + "State");
                FSMState currentState = Activator.CreateInstance(t) as FSMState;
                states.Add(currentState);
                foreach (var item in state.Value)
                {//条件状态映射
                    // item.Key条件
                    //item.Value对应状态
                    FSMTriggerID fSMTriggerID = (FSMTriggerID)Enum.Parse(typeof(FSMTriggerID), item.Key);
                    FSMStateID fSMStateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), item.Value);
                    currentState.AddMap(fSMTriggerID, fSMStateID);
                }
            }
        }

        private void FSMConfigTest()
        {
            Type t = Type.GetType("AI.FSM.TestState");
            FSMState currentState = Activator.CreateInstance(t) as FSMState;
            states = new List<FSMState> { currentState };
        }

        private FSMState defulatState;

        private void SetDefaultState()
        {
            //设置默认状态
            defulatState = states.Find(s => s.StateID == defaultStateID);
            currentState = defulatState;
            CurrentStateName = currentState.StateID.ToString();
            currentState.EnterState(this);
        }

        public void SwitchState(FSMStateID stateId)
        {
            FSMState state =
                stateId == FSMStateID.Default ?
                defulatState :
                states.Find(s => s.StateID == stateId);
            print(Time.frameCount + CurrentStateName.ToString() + "=>" + stateId.ToString());
            //当前状态退出
            currentState.ExitState(this);
            currentState = state;
            CurrentStateName = state.StateID.ToString();
            //进入下一状态
            currentState.EnterState(this);
        }

    }
}
