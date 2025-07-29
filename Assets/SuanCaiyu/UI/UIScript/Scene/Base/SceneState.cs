using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景状态，抽象类
/// </summary>
public abstract class SceneState
{
    /// <summary>
    /// 场景进入时的操作
    /// </summary>
    public abstract void OnEnter();
    /// <summary>
    /// 场景退出时的操作
    /// </summary>
    public abstract void OnExit();

}
