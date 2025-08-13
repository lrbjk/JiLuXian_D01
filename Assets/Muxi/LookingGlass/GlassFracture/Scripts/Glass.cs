using System;
using System.Collections.Generic;
using System.Linq;
using GlassSystem.Scripts;
using MathNet.Spatial.Euclidean;
using UnityEngine;
using UnityEngine.Rendering;
using static GlassSystem.Scripts.MathNetUtils;
using Random = UnityEngine.Random;

namespace GlassSystem
{
    public class Glass : Fracturable
    {
        protected GlassPanel _parentPanel;

        private const float MicroShardSurface = 0.07f;
        private const float MicroShardTimer = 4f;
        private const float SmallShardSurface = 0.15f;
        private const float SmallShardTimer = 8f;
        protected const float Tolerance = 0.001f;
        
        protected Transform _transform;
        protected float _thickness;
        protected Polygon2D _polygon;
        protected Vector2[] _uvs;

        public override void TriggerFracture(Vector3 impactPoint, Vector3 impactForce, int patternIndex = -1, float rotation = float.NaN)
        {
            _transform = transform;
            
            Vector3 localPosition = transform.InverseTransformPoint(impactPoint);
            var scale = _transform.lossyScale;
            localPosition.x *= scale.x;
            localPosition.y *= scale.y;

            _polygon ??= BuildPolygon(localPosition.z);
            if (_polygon is null)
                return;

            if (FracturePatterns == null || FracturePatterns.Length == 0)
            {
                Debug.LogError("No fracture patterns have been assigned.", this);
                return;
            }

            if (patternIndex == -1)
                patternIndex = Random.Range(0, FracturePatterns.Length);
            if (float.IsNaN(rotation))
                rotation = Random.Range(0, 360f);
            var lines = ClipPattern.Clip(FracturePatterns[patternIndex], _polygon, localPosition, rotation);

            List<Polygon2D> shardPolygons = ShardPolygonBuilder.Build(lines, Tolerance);
            var materials = GetComponent<Renderer>().sharedMaterials;
            foreach (Polygon2D shardPolygon in shardPolygons)
            {
                var center = Point2D.Centroid(shardPolygon.Vertices);
                var centeredShardPolygon = shardPolygon.TranslateBy(-center.ToVector2D());
                Vector2[] uvs = null;
                if (_uvs is not null)
                    uvs = shardPolygon.Vertices.Select(InterpolateUv).ToArray();
                var shardMesh = CreateShardMesh(centeredShardPolygon, uvs, _thickness);
                var glassShard = SpawnShard(shardMesh, impactForce, new Vector3((float)center.X, (float)center.Y, 0), materials);
                if (glassShard is not null)
                    glassShard.InitializeShard(_parentPanel, centeredShardPolygon, uvs, _thickness);
            }
        }

        protected virtual Polygon2D BuildPolygon(float localPositionZ)
        {
            return _polygon;
        }
        
        Shard SpawnShard(Mesh mesh, Vector3 impactForce, Vector3 offset, Material[] materials)
        {
            float shardSurface = mesh.bounds.size.x * mesh.bounds.size.y;

            var go = new GameObject(mesh.name);

            go.tag = gameObject.tag;
            var rotation = _transform.rotation;
            go.transform.position = _transform.position + rotation * offset;
            go.transform.rotation = rotation;
        
            var meshFilter = go.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = mesh;
        
            var meshRenderer = go.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterials = materials;
        
            var meshCollider = go.AddComponent<MeshCollider>();
            meshCollider.convex = true;
            meshCollider.sharedMesh = mesh;

            Shard shard = null;
            if (shardSurface > SmallShardSurface)
            {
                shard = go.AddComponent<Shard>();
                shard.FracturePatterns = FracturePatterns;
                go.transform.parent = transform.parent;
                _parentPanel.RegisterNewShard(shard);
            }
            else
            {
                var shardRigidbody = go.AddComponent<Rigidbody>();
                shardRigidbody.mass = shardSurface;
                shardRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
                shardRigidbody.AddForce(impactForce);
                Destroy(go, shardSurface > MicroShardSurface ? SmallShardTimer : MicroShardTimer);
            }

            return shard;
        }

        public void Fall()
        {
            // TODO make shard start falling
        }
        
        struct Vertex
        {
            public Vector3 Position;
            public Vector2 Uv;
        }

        private Vector2 InterpolateUv(Point2D p)
        {
            var weight = BarycentricInterpolation(p, _polygon.Vertices.ToArray());
            return _uvs[0] * weight.x + _uvs[1] * weight.y + _uvs[2] * weight.z;
        }
        
        private Mesh CreateShardMesh(Polygon2D polygon, Vector2[] uvs, float thickness)
        {
            var mesh = new Mesh { name = "Shard" };
            var indices = new List<int>();

            var sideSize = polygon.Vertices.Count();
            var vertices = polygon.Vertices.Select(v => new Vector3((float)v.X, (float)v.Y, 0)).ToList();
            for (int i = 1; i < sideSize - 1; i++)
                indices.AddRange(new[] {0, i + 1, i});

            vertices.AddRange(polygon.Vertices.Select(v => new Vector3((float)v.X, (float)v.Y, -thickness)));
            for (int i = 1; i < sideSize - 1; i++)
                indices.AddRange(new[] {sideSize, sideSize + i, sideSize + i + 1});

            var sideIndexStart = indices.Count;
            var sideVertexStart = vertices.Count;
            for (int i = 0; i < sideSize; i++)
            {
                indices.AddRange(new[]
                {
                    sideVertexStart + i,
                    sideVertexStart + sideSize + i,
                    sideVertexStart * 2 + (i + 1) % sideSize, 
                    sideVertexStart + sideSize + i,
                    sideVertexStart * 2 + sideSize + (i + 1) % sideSize,
                    sideVertexStart * 2 + (i + 1) % sideSize
                });
            
            }

            var faceVertices = vertices.ToList();
            vertices.AddRange(faceVertices);
            vertices.AddRange(faceVertices);

            if (uvs == null)
            {
                var layout = new VertexAttributeDescriptor[] { new (VertexAttribute.Position) };
                mesh.SetVertexBufferParams(vertices.Count, layout);
                mesh.SetVertexBufferData(vertices, 0, 0, vertices.Count);
            }
            else
            {
                var layout = new VertexAttributeDescriptor[] { new (VertexAttribute.Position), new (VertexAttribute.TexCoord0, dimension:2) };
                var verticesStruct = vertices.Select((x, i) => new Vertex { Position = x, Uv = uvs[i % uvs.Length] }).ToList();
                mesh.SetVertexBufferParams(vertices.Count, layout);
                mesh.SetVertexBufferData(verticesStruct, 0, 0, vertices.Count);
            }

            mesh.SetIndexBufferParams(indices.Count, IndexFormat.UInt32);
            mesh.SetIndexBufferData(indices,0, 0, indices.Count);
            mesh.subMeshCount = 2;
            mesh.SetSubMesh(0, new SubMeshDescriptor(0, sideIndexStart));
            mesh.SetSubMesh(1, new SubMeshDescriptor(sideIndexStart, indices.Count - sideIndexStart));

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }
    }
}