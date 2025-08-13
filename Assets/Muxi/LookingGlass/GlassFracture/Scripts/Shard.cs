using System;
using MathNet.Spatial.Euclidean;
using UnityEngine;

namespace GlassSystem
{
    public class Shard : Glass
    {
        public void InitializeShard(GlassPanel parentPanel, Polygon2D polygon, Vector2[] uvs, float thickness)
        {
            _parentPanel = parentPanel;
            _polygon = polygon;
            _thickness = thickness;
            _uvs = uvs;
        }

        public override void TriggerFracture(Vector3 impactPoint, Vector3 impactForce, int patternIndex = -1, float rotation = float.NaN)
        {
            base.TriggerFracture(impactPoint, impactForce, patternIndex, rotation);
            _parentPanel.UnregisterShard(this);
            Destroy(gameObject);
        }
    }
}
