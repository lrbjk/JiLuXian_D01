using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Spatial.Euclidean;
using UnityEngine;
using static GlassSystem.Scripts.MathNetUtils;

namespace GlassSystem
{
    public class GlassPanel : Glass
    {
        protected List<Shard> _shards;

        public int ShardCount => _shards?.Count ?? 0;

        [Header("Multi-Break Settings")]
        [Tooltip("Number of hits required to completely destroy the panel")]
        public int MaxHealth = 3;

        public int CurrentHealth { get; private set; }

        [Tooltip("Allow breaking individual shards multiple times")]
        public bool AllowShardReBreaking = true;

        [Tooltip("Minimum size for a shard to be breakable again")]
        public float MinBreakableShardSize = 0.2f;
        
        protected void Start()
        {
            _parentPanel = this;
            CurrentHealth = MaxHealth;
        }

        protected override Polygon2D BuildPolygon(float side)
        {
            var targetMeshFilter = GetComponent<MeshFilter>();
            if (targetMeshFilter == null)
                return null;

            var targetMesh = targetMeshFilter.sharedMesh;
            var targetVertices = targetMesh.vertices;
            if (targetVertices.Length is > 100 or < 3)
            {
                Debug.LogWarning($"Invalid mesh ({targetVertices.Length})");
                return null;
            }

            // Scale
            var scale = _transform.lossyScale;
            var scalingMatrix = new DiagonalMatrix(2, 2, new double[] { scale.x, scale.y });
            
            // Thickness
            var verticesZ = targetVertices.Select(p => p.z).ToList();
            _thickness = (verticesZ.Max() + Mathf.Abs(verticesZ.Min())) * scale.z;
            
            // Vertices to polygon
            var targetPoints = targetVertices.Select((p, i) =>  new IndexedPoint(p, i)).ToList();
            targetPoints.RemoveAll(p => Mathf.Abs(p.Z - side) > Tolerance); // Discard backface
            targetPoints = targetPoints.Distinct(new Point2DComparer(Tolerance)).ToList(); // Discard side submesh vertex duplicates
            foreach (var point in targetPoints)
                point.TransformBy(scalingMatrix);
            
            // Build convex polygon
            targetPoints.Sort((a, b) => CompareVectorAngle(new Point2D(0, 0), a, b));
            Polygon2D targetPolygon = new Polygon2D(targetPoints.Select(p => p.Point2D));

            // UVs
            var uvs = targetMesh.uv;
            if (uvs != null && uvs.Length > 0)
            {
                _uvs = new Vector2[targetPoints.Count];
                for (int i = 0; i < targetPoints.Count; i++)
                    _uvs[i] = uvs[targetPoints[i].Index];
            }

            return targetPolygon;
        }
         
         public override void TriggerFracture(Vector3 impactPoint, Vector3 impactForce, int patternIndex = -1, float rotation = float.NaN)
         {
             if (_shards is null)
             {
                 _shards = new();
                 base.TriggerFracture(impactPoint, impactForce, patternIndex, rotation);

                 var meshRenderer = GetComponent<MeshRenderer>();
                 if (meshRenderer != null)
                     meshRenderer.enabled = false;

                 CurrentHealth--;
                 Debug.Log($"Glass panel first break! Health: {CurrentHealth}/{MaxHealth}");
             }
             else
             {
                 BreakNearbyShards(impactPoint, impactForce, patternIndex, rotation);
                 CurrentHealth--;
                 Debug.Log($"Glass panel additional break! Health: {CurrentHealth}/{MaxHealth}");
             }

             if (CurrentHealth <= 0)
             {
                 DestroyPanel();
             }
         }
         
         private void BreakNearbyShards(Vector3 impactPoint, Vector3 impactForce, int patternIndex = -1, float rotation = float.NaN)
         {
             if (_shards == null || _shards.Count == 0)
                 return;

             float breakRadius = 1.0f;
             List<Shard> shardsToBreak = new List<Shard>();

             foreach (var shard in _shards)
             {
                 if (shard == null) continue;

                 float distance = Vector3.Distance(shard.transform.position, impactPoint);
                 if (distance <= breakRadius)
                 {
                     var bounds = shard.GetComponent<Renderer>()?.bounds;
                     if (bounds.HasValue && bounds.Value.size.magnitude >= MinBreakableShardSize)
                     {
                         shardsToBreak.Add(shard);
                     }
                 }
             }

             foreach (var shard in shardsToBreak)
             {
                 if (AllowShardReBreaking)
                 {
                    shard.FracturePatterns = FracturePatterns;
                    shard.TriggerFracture(impactPoint, impactForce, patternIndex, rotation);
                 }
             }

             Debug.Log($"Broke {shardsToBreak.Count} additional shards");
         }

         private void DestroyPanel()
         {
             Debug.Log("Glass panel completely destroyed!");

             if (_shards != null)
             {
                 foreach (Shard s in _shards)
                 {
                     if (s != null)
                         s.Fall();
                 }
             }

             Destroy(GetComponent<MeshFilter>());
             Destroy(GetComponent<MeshRenderer>());
             Destroy(GetComponent<Collider>());
         }

         public void UnregisterShard(Shard shard)
         {
             if (_shards != null)
                 _shards.Remove(shard);
         }

         public void RegisterNewShard(Shard shard)
         {
             if (_shards == null)
                 _shards = new List<Shard>();
             _shards.Add(shard);
         }

         public void ResetPanel()
         {
             CurrentHealth = MaxHealth;

             if (_shards != null)
             {
                 foreach (var shard in _shards)
                 {
                     if (shard != null)
                         Destroy(shard.gameObject);
                 }
                 _shards.Clear();
                 _shards = null;
             }

             var meshRenderer = GetComponent<MeshRenderer>();
             if (meshRenderer != null)
                 meshRenderer.enabled = true;

             Debug.Log("Glass panel reset to original state");
         }
    }
}
