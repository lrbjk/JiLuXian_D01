using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理全局的操作，如切换场景
/// </summary>
public class GameControl : MonoBehaviour
{
 
    public static GameControl Instance { get; private set; }
    /// <summary>
    /// 场景管理器
    /// </summary>
    public SceneSystem SceneSystem { get; private set; }

    private void Awake()
    {
        Instance = this;
        SceneSystem = new SceneSystem();
    }

    private void Start()
    {
        SceneSystem.SetScene(new StartScene());
    }
}


