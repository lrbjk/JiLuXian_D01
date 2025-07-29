using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 存储所有UI信息，并可以创建或销毁UI
/// </summary>
public class UIManager_1 : MonoBehaviour
{
    private Dictionary<UIType, GameObject> dicUI;

    public UIManager_1() 
    { 
      dicUI = new Dictionary<UIType, GameObject>(); 
    }

    /// <summary>
    /// 获取一个UI对象
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetSingleUI(UIType type)
    {
        GameObject parent = GameObject.Find("Canvas");
        if (!parent) 
            {
            Debug.LogError("画布不存在，请仔细查找");
                return null;
            }
        if (dicUI.ContainsKey(type))
        {
            return dicUI[type];
        }
        GameObject ui = GameObject.Instantiate(Resources.Load<GameObject>(type.Path), parent.transform);
        ui.name = type.Name;
        dicUI.Add(type, ui);
        return ui;
    }

    /// <summary>
    /// 销毁一个UI对象
    /// </summary>
    /// <param name="type"></param>
    public void DestroyUI(UIType type)
    {
        if (dicUI.ContainsKey(type))
        {
          GameObject.Destroy(dicUI[type]);
            dicUI.Remove(type);
        }
    }
}
