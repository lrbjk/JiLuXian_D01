using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IStateMachineOwner
{

}
public class StateMachine
{
    private IStateMachineOwner owner;
    private Dictionary<Type, StateBase> stateDic = new Dictionary<Type, StateBase>();
    public Type CurrentStateType { get => currentState.GetType(); }
    public bool HasState { get => currentState != null; }
    private StateBase currentState;
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="owner"></param>
    public void Init(IStateMachineOwner owner)
    {
        this.owner = owner;
    }
    /// <summary>
    /// 切换状态
    /// </summary>
    /// <typeparam name="T">具体要切换的状态类型</typeparam>
    /// <param name="reCurrstate">如果状态没变，是否需要刷新状态</param>
    /// <returns></returns>
    public bool ChangeState<T>(bool reCurrstate = false) where T : StateBase, new()
    {
        if (HasState && CurrentStateType == typeof(T) && !reCurrstate) return false;
        //退出当前状态
        if (currentState != null)
        {
            currentState.Exit();
            MonoManager.Instance.RemoveUpdateListener(currentState.Update);
            MonoManager.Instance.RemoveUpdateListener(currentState.LateUpdate);
            MonoManager.Instance.RemoveUpdateListener(currentState.FixedUpdate);
        }
        //进入新状态
        currentState = GetState<T>();
        currentState.Enter();
        MonoManager.Instance.AddUpdateListener(currentState.Update);
        MonoManager.Instance.AddUpdateListener(currentState.LateUpdate);
        MonoManager.Instance.AddUpdateListener(currentState.FixedUpdate);
        return false;
    }
    private StateBase GetState<T>() where T : StateBase, new()
    {
        Type type = typeof(T);
        //如果字典中不存在
        if (!stateDic.TryGetValue(type, out StateBase state))
        {
            state = new T();
            state.Init(owner);
            stateDic.Add(type, state);
        }
        return state;
    }
    /// <summary>
    /// 停止工作，释放资源
    /// </summary>
    public void Stop()
    {
        currentState.Exit();
        MonoManager.Instance.RemoveUpdateListener(currentState.Update);
        MonoManager.Instance.RemoveUpdateListener(currentState.LateUpdate);
        MonoManager.Instance.RemoveUpdateListener(currentState.FixedUpdate);

        currentState = null;

        foreach (var item in stateDic.Values)
        {
            item.Unint();
        }
        stateDic.Clear();
    }
}

