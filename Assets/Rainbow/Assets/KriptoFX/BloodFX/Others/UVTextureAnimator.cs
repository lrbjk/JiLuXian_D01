using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

internal class UVTextureAnimator : MonoBehaviour
{
  public int Rows = 4; //tilesY
  public int Columns = 4; //tilesX
  public float Fps = 20;
  public int OffsetMat = 0;

  public float StartDelay = 0;

    public bool IsInterpolateFrames = true;
    public BFX_TextureShaderProperties[] TextureNames = { BFX_TextureShaderProperties._MainTex };

    public AnimationCurve FrameOverTime = AnimationCurve.Linear(0, 1, 1, 1);

    private Renderer currentRenderer;
    private Projector projector;
    private Material instanceMaterial;
    private float animationStartTime;
    private bool canUpdate;
    private int previousIndex;
    private int totalFrames;
    private float currentInterpolatedTime;
    private int currentIndex;
    private Vector2 size;
    private bool isInitialized;
    private bool startDelayIsBroken = false;

    private void OnEnable()
    {
        if (isInitialized) InitDefaultVariables();
    }

    private void Start()
    {
        InitDefaultVariables();
        isInitialized = true;
    }

    void Update()
    {
        if (startDelayIsBroken) ManualUpdate();
    }

    void ManualUpdate()
    {
        if (!canUpdate) return;
        UpdateMaterial();
        SetSpriteAnimation();
        if (IsInterpolateFrames)
            SetSpriteAnimationIterpolated();
    }

    void StartDelayFunc()
    {
        startDelayIsBroken = true;
        animationStartTime = Time.time;
    }

    private void InitDefaultVariables()
    {
        InitializeMaterial();

        totalFrames = Columns * Rows;
        previousIndex = 0;
        canUpdate = true;
        var offset = Vector3.zero;
        size = new Vector2(1f / Columns, 1f / Rows);
        animationStartTime = Time.time;
        if (StartDelay > 0.00001f)
        {
            startDelayIsBroken = false;
            Invoke("StartDelayFunc", StartDelay);
        }
        else startDelayIsBroken = true;
        if (instanceMaterial != null)
        {
            foreach (var textureName in TextureNames)
            {
                instanceMaterial.SetTextureScale(textureName.ToString(), size);
                instanceMaterial.SetTextureOffset(textureName.ToString(), offset);
            }
        }
    }

    private void InitializeMaterial()
    {
        GetComponent<MeshRenderer>().enabled = true;
        currentRenderer = GetComponent<Renderer>();

        if (currentRenderer == null)
        {
            projector = GetComponent<Projector>();
            if (projector != null)
            {
                if (!projector.material.name.EndsWith("(Instance)"))
                    projector.material = new Material(projector.material) { name = projector.material.name + " (Instance)" };
                instanceMaterial = projector.material;
            }
        }
        else
            instanceMaterial = currentRenderer.material;
    }

    private void UpdateMaterial()
    {
        if (currentRenderer == null)
        {
            if (projector != null)
            {
                if (!projector.material.name.EndsWith("(Instance)"))
                    projector.material = new Material(projector.material) { name = projector.material.name + " (Instance)" };
                instanceMaterial = projector.material;
            }
        }
        else
            instanceMaterial = currentRenderer.material;
    }

    void SetSpriteAnimation()
    {
        int index = (int)((Time.time - animationStartTime) * Fps);
        index = index % totalFrames;

        if (index < previousIndex)
        {
            canUpdate = false;
            GetComponent<MeshRenderer>().enabled = false;
            return;
        }

        if (IsInterpolateFrames && index != previousIndex)
        {
            currentInterpolatedTime = 0;
        }
        previousIndex = index;

        var uIndex = index % Columns;
        var vIndex = index / Columns;

        float offsetX = uIndex * size.x;
        float offsetY = (1.0f - size.y) - vIndex * size.y;
        var offset = new Vector2(offsetX, offsetY);

        if (instanceMaterial != null)
        {
            foreach (var textureName in TextureNames)
            {
                instanceMaterial.SetTextureScale(textureName.ToString(), size);
                instanceMaterial.SetTextureOffset(textureName.ToString(), offset);
            }
        }
    }

    private void SetSpriteAnimationIterpolated()
    {
        currentInterpolatedTime += Time.deltaTime;

        var nextIndex = previousIndex + 1;
        if (nextIndex == totalFrames)
            nextIndex = previousIndex;

        var uIndex = nextIndex % Columns;
        var vIndex = nextIndex / Columns;

        float offsetX = uIndex * size.x;
        float offsetY = (1.0f - size.y) - vIndex * size.y;
        var offset = new Vector2(offsetX, offsetY);
        if (instanceMaterial != null)
        {
            instanceMaterial.SetVector("_Tex_NextFrame", new Vector4(size.x, size.y, offset.x, offset.y));
            instanceMaterial.SetFloat("InterpolationValue", Mathf.Clamp01(currentInterpolatedTime * Fps));
        }
    }
}

public enum BFX_TextureShaderProperties
{
    _MainTex,
    _DistortTex,
    _Mask,
    _Cutout,
    _CutoutTex,
    _Bump,
    _BumpTex,
    _EmissionTex
}