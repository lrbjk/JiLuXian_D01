using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoManager : SingletonMono<MonoManager>
{
    private Action updateAction;
    private Action lateUpdateAction;
    private Action fixedUpdateAction;

    public void AddLateUpdateListener(Action action)
    {
        lateUpdateAction += action;
    }
    public void RemoveLateUpdateListener(Action action)
    {
        lateUpdateAction -= action;
    }
    public void AddUpdateListener(Action action)
    {
        updateAction += action;
    }
    public void RemoveUpdateListener(Action action)
    {
        updateAction -= action;
    }
    public void AddFixedUpdateListener(Action action)
    {
        fixedUpdateAction += action;
    }
    public void RemoveFixedUpdateListener(Action action)
    {
        fixedUpdateAction -= action;
    }

    private void Update()
    {
        updateAction?.Invoke();
    }
    private void LateUpdate()
    {
        lateUpdateAction?.Invoke();
    }
    private void FixedUpdate()
    {
        fixedUpdateAction?.Invoke();
    }
}
