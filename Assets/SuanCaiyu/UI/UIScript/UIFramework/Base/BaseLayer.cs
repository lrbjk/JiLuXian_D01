using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有UI面板的父类，包寒UI面板的状态信息
/// </summary>
public class BaseLayer
{  
    
    /// <summary>
    /// UI 信息
    /// </summary>
    public UIType uitype {  get; private set; }

    /// <summary>
    /// UI管理工具
    /// </summary>
    public UITool UITool { get; private set; }

    /// <summary>
    /// 面板管理工具
    /// </summary>
    public LayerManager LayerManager { get; private set; }

    /// <summary>
    /// UI管理器
    /// </summary>
    public UIManager_1 UIManager { get; private set; }
    
    /// <summary>
    /// 初始化UItype
    /// </summary>
    /// <param name="uitype"></param>
    public BaseLayer(UIType uitype)
    {
        this.uitype = uitype;
    }

    /// <summary>
    ///  初始化UITool
    /// </summary>
    /// <param name="tool"></param>
    public void Initialize(UITool tool)
    {
        UITool = tool;
    }
   /// <summary>
   /// 初始化Layermanager
   /// </summary>
   /// <param name="manager"></param>
    public void Initialize(LayerManager manager)
    {
        LayerManager = manager;
    }

     /// <summary>
    /// 初始化UIMaanager
    /// </summary>
    /// <param name="uiManager"></param>
    public void Initialize(UIManager_1 uIManager)
    {
        UIManager = uIManager;
    }

    /// <summary>
    /// UI进入时执行的操作，只会执行一次
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// UI暂停时执行的操作
    /// </summary>
    public virtual void OnPause() 
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;//创建面板组组件并设置交互为False实现暂停
    }

    /// <summary>
    /// UI继续时执行的操作
    /// </summary>
    public virtual void OnResume() 
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
    }

    /// <summary>
    /// UI退出时执行的操作
    /// </summary>
    public virtual void OnExit()
    { 
       UIManager.DestroyUI(uitype);
    }
}
