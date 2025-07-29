using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI的管理工具，包括获取某个子对象组件的操作
/// </summary>
public class UITool : MonoBehaviour
{   /// <summary>
/// 当前的活动面板
/// </summary>
    GameObject activeLayer;

    public UITool(GameObject layer)
    {
        activeLayer = layer; 
    }

    /// <summary>
    /// 给当前活动面板获取或者添加一个组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetOrAddComponent<T>() where T : Component
    {
        if (activeLayer.GetComponent<T>() == null)
        {
            activeLayer.AddComponent<T>();
        }
        return (T)activeLayer.GetComponent<T>();
    }

    /// <summary>
    /// 根据名称查找一个子对象
    /// </summary>
    /// <param name="name">子对象名称</param>
    /// <returns></returns>
    public GameObject FindChildGameObject(string name)
    {
        Transform[] transforms = activeLayer.GetComponentsInChildren<Transform>();

        foreach (Transform item in transforms)
        {
            if(item.name == name)
            {
                return item.gameObject;
            }
        }
        Debug.LogWarning($"{activeLayer.name}里找不到名为{name}的子对象");
        return null;
    }

    /// <summary>
    /// 根据名称获取一个子对象的组件
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="name">子对象的名称</param>
    /// <returns></returns>
    public T GetOrAddComponentInChildren<T>(string name) where T : Component
    { 
      GameObject child = FindChildGameObject(name);
        if (child != null) 
        {
            if (child.GetComponent<T>() == null)
            {
                child.AddComponent<T>();
            }
            return (T)child.GetComponent<T>();
        }
        return null;
    }


}
