using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestGUI : ShaderGUI
{
    //折叠页缩进等级
    public const int FoldoutIndent = 1;
    //折叠页标记，在折叠页属性 显示名字后面务必添加他，他将用来标识该属性为折叠页。其他属性务必不要添加
    public const string FoldoutSign = "_Foldout";
    
    //当前折叠等级，他将用来描述PropertyGUI绘制在那级折叠页中
    public int FoldoutLevel { get { return _foldoutLevel; } }
    //折叠页编辑等级
    public int FoldoutLevel_Editor { get { return _foldoutLevel_Editor; } }
    //折叠页状态, true展开, false折叠
    public bool FoldoutOpen { get { return _foldoutOpen; } }
    //折叠页中的属性是否可以被编辑
    //需要吗
    public bool FoldoutEditor { get { return _foldoutEditor; } }
    
    //面板切换列表
    public List<string> SwitchList = new List<string>();
    
    
    
    //当前折叠等级，他将用来描述PropertyGUI绘制在那级折叠页中
    private int _foldoutLevel = 0;
    //折叠页编辑等级
    private int _foldoutLevel_Editor = 0;
    //折叠页状态, true展开, false折叠
    private bool _foldoutOpen = true;
    //折叠页中的属性是否可以被编辑
    private bool _foldoutEditor = true;
    //绘制的所有材质属性
    private MaterialProperty[] _allProperties;
    
    
    //混合模式的两个选项
    private MaterialProperty _SrcBlend;
    private MaterialProperty _DstBlend;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        
        
    }
}