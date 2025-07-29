using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 面板管理器，用栈来存储UI
/// </summary>
public class LayerManager 
{
    /// <summary>
    /// 存储UI面板的栈
    /// </summary>
    private Stack<BaseLayer> stackLayer;
    /// <summary>
    /// UI管理器
    /// </summary>
    private UIManager_1 uIManager;
    private BaseLayer layer;

    public LayerManager()
    {
        stackLayer = new Stack<BaseLayer>();
        uIManager = new UIManager_1();
    }

   /// <summary>
   /// UI入栈操作，显示一个面板
   /// </summary>
   /// <param name="nextlayer"></param>
    public void Push(BaseLayer nextlayer)
    {
        if(stackLayer.Count>0)
        {
            layer = stackLayer.Peek();
            layer.OnPause();
        }
        stackLayer.Push(nextlayer);
        GameObject layerget = uIManager.GetSingleUI(nextlayer.uitype);
        nextlayer.Initialize(new UITool(layerget));
        nextlayer.Initialize(this);
        nextlayer.Initialize(uIManager);
        nextlayer.OnEnter();//可否进行优化用单例 
    }
    
    /// <summary>
    /// 执行面板的出栈操作
    /// </summary>
    public void Pop()
    {
        if (stackLayer.Count > 0)
        {
           stackLayer.Peek().OnExit();
           stackLayer.Pop();
        }
        if(stackLayer.Count>0)
        {
           stackLayer.Peek().OnResume();
        }
    }
}

