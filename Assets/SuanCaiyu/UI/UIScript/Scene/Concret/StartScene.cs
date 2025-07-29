using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 开始场景
/// </summary>
public class StartScene : SceneState
{

    readonly string sceneName = SceneManager.GetActiveScene().name;
    LayerManager layerManager;

    /// <summary>
    /// 进入时执行的操作
    /// </summary>
    public override void OnEnter()
    {
        layerManager=new LayerManager();
        if(SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
            SceneManager.sceneLoaded += SceneLoaded;
        }
        else
        {
            layerManager.Push(new StartLayer());
        }
    }

    /// <summary>
    /// 退出时执行的操作
    /// </summary>
    public override void OnExit()
    {
        SceneManager.sceneLoaded -= SceneLoaded;//取消方法绑定
       
    }

    /// <summary>
    /// 场景加载完毕之后执行的方法
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="load"></param>
    public void SceneLoaded(Scene scene,LoadSceneMode load)
    {
        layerManager.Push(new StartLayer());
        Debug.Log($"{sceneName}场景加载完毕" );
    }
}
    

