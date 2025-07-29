using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutUsLayer : BaseLayer
{
    static readonly string path = "Prefabs/UI/Start/AboutUs";
    public AboutUsLayer() : base(new UIType(path)) { }



    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("BackBtn").onClick.AddListener(() =>
        {
            LayerManager.Pop();
        });
    }
}
