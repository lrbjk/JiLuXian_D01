using Pixeye.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Object), true)]
[CanEditMultipleObjects]
public class CustomEditorManager : Editor
{
    private List<CustomEditorSelector> Editors = new();

    private void OnEnable()
    {

        Editors.Add(new EditorOverride());
        Editors.Add(new NewGizmos());
        //Editors.Add(new ProbabilitySettingEditorSo());
        //Editors.Add(new NewCustomTileDataEditorSo());
        //Editors.Add(new NewTileMapObjectDataEditorSo());
        //Editors.Add(new NewBuffShowDataEditorSo());
        //Editors.Add(new EnemySettingEditorSo());
        foreach (var e in Editors)
            e.OnEnable(target, this);
    }

    private void OnDisable()
    {
        foreach (var e in Editors)
            e.OnDisable(target, this);
        Editors.Clear();

    }

    private void OnSceneGUI()
    {
        foreach (var e in Editors)
        {
            if (e.IsMatch(target, this))
            {
                e.OnSceneGUI(target, this);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        var has = false;
        foreach (var e in Editors)
        {
            if (e.IsMatch(target, this))
            {
                e.OnInspectorGUI(target, this);
                has = true;
            }
        }
        if (!has)
            base.OnInspectorGUI();
    }

    public override bool RequiresConstantRepaint()
    {
        var ret = false;
        foreach (var e in Editors)
            ret |= e.RequiresConstantRepaint(target, this);
        return ret;
    }
}
public interface CustomEditorSelector
{
    public bool IsMatch(object target, Editor editor);
    public void OnInspectorGUI(object target, Editor editor);
    public void OnSceneGUI(object target, Editor editor);
    public void OnEnable(object target, Editor editor);
    public void OnDisable(object target, Editor editor);
    public bool RequiresConstantRepaint(object target, Editor editor);
}
