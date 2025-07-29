using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景的状态管理系统
/// </summary>
public class SceneSystem 
{
    /// <summary>
    /// 场景状态类
    /// </summary>
    SceneState sceneState;

    /// <summary>
    /// 设置并进入当前场景
    /// </summary>
    /// <param name="sceneState"></param>
    public void SetScene(SceneState state)
    {
        sceneState?.OnExit();
        sceneState=state;
        sceneState?.OnEnter();

    }
}
