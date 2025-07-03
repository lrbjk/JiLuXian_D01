using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 状态基类
/// </summary>
public abstract class StateBase
{
    /// <summary>
    /// 初始化
    /// 只在状态第一次创建的时候调用
    /// </summary>
    /// <param name="owner">宿主</param>
    /// <param name="stateType">当前状态代表的实际枚举的Int值</param>
    public virtual void Init(IStateMachineOwner owner)
    {

    }
    /// <summary>
    /// 反初始化，释放资源
    /// </summary>
    public virtual void Unint()
    {

    }
    /// <summary>
    /// 状态每进入执行一次
    /// </summary>
    public virtual void Enter()
    {

    }
    /// <summary>
    /// 状态退出执行
    /// </summary>
    public virtual void Exit()
    {

    }
    public virtual void Update()
    {

    }
    public virtual void LateUpdate()
    {

    }
    public virtual void FixedUpdate()
    {

    }
}