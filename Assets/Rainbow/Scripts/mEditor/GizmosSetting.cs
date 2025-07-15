using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

public enum DrawGizmosType { cube = 0, sphere, capsule }
[Serializable]
public class GizmosSetting
{
    public bool editorMode;
    public bool openGizmos;
    public DrawGizmosType GizmosType;
    public Transform point;
    public float radius;
    public Vector3 size, offSet;
    public Color gizmosColor;

    // 记录已经检测到的物体和它们的进入状态
    private Dictionary<Collider, bool> detectedColliders = new Dictionary<Collider, bool>();

    public Vector3 gizmosCenter { get { return point.position + offSet; } }

    public void AutoRange(Vector3 min, Vector3 max)
    {
        point.position = Vector3.zero;
        size = max - min;
        offSet = (max + min) / 2f;
    }

    /// <summary>
    /// 默认-1为所有layer
    /// </summary>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    public List<Collider> GetCollInRange(int layerMask = -1)
    {
        List<Collider> collidersInRange;

        if (GizmosType == DrawGizmosType.cube)
        {
            if (layerMask == -1) collidersInRange = Physics.OverlapBox(gizmosCenter, size / 2, point.rotation).ToList();
            else collidersInRange = Physics.OverlapBox(gizmosCenter, size / 2, point.rotation, layerMask).ToList();
        }
        else if (GizmosType == DrawGizmosType.sphere)
        {
            if (layerMask == -1) collidersInRange = Physics.OverlapSphere(gizmosCenter, radius).ToList();
            else collidersInRange = Physics.OverlapSphere(gizmosCenter, radius, layerMask).ToList();
        }
        else if (GizmosType == DrawGizmosType.capsule)
        {
            Vector3 point0 = gizmosCenter - new Vector3(0, size.y / 2, 0);
            Vector3 point1 = gizmosCenter + new Vector3(0, size.y / 2, 0);
            if (layerMask == -1) collidersInRange = Physics.OverlapCapsule(point0, point1, radius).ToList();
            else collidersInRange = Physics.OverlapCapsule(point0, point1, radius, layerMask).ToList();
        }
        else
        {
            return null;
        }

        // 过滤已经检测过的物体
        List<Collider> newColliders = new List<Collider>();
        foreach (var collider in collidersInRange)
        {
            if (!detectedColliders.ContainsKey(collider))
            {
                detectedColliders[collider] = true;
                newColliders.Add(collider);
            }
        }

        // 检查离开的物体
        List<Collider> removedColliders = new List<Collider>();
        foreach (var detectedCollider in detectedColliders.Keys.ToList())
        {
            if (!collidersInRange.Contains(detectedCollider))
            {
                removedColliders.Add(detectedCollider);
                detectedColliders.Remove(detectedCollider);
            }
        }

        // 合并新进入和离开的物体
        //newColliders.AddRange(removedColliders);

        return newColliders;
    }

    // 清空已经检测到的物体集合，例如在动画帧范围结束时调用
    public void ClearDetectedColliders()
    {
        detectedColliders.Clear();
    }
}
