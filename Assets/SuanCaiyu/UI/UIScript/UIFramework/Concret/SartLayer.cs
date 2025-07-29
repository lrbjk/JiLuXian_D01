using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// 开始主面板
/// </summary>
public class StartLayer :BaseLayer
{
    static readonly string path = "Prefabs/UI/Start/StartUI";
  public StartLayer():base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("设置").onClick.AddListener(()=>
        {
            //点击事件可以写在这
           UnityEngine.Debug.Log("设置按钮点击");
            LayerManager.Push(new SettingLayer());
        });

        UITool.GetOrAddComponentInChildren<Button>("继续游戏").onClick.AddListener(() =>
        {
            //点击事件可以写在这
            UnityEngine.Debug.Log("开始游戏按钮点击");
            GameControl.Instance.SceneSystem.SetScene(new MainScene());
        });
    }
 

}