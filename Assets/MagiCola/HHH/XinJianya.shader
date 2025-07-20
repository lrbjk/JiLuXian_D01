Shader "Custom/OBJ"
{
    Properties
    {
        [KeywordEnum(None,Face,Eye,Body)]_Domain("Domain(区域)",Float) = 0
        [Header(Test)]
        [Toggle(_Lazer_ON)]_Lazer_ON("_Lazer_ON",Float) = 0
        _CubeMap("Env",Cube) = "CUBE"{}
        [Hearder(Main Maps)]
        _Color("Color",Color) = (1,1,1,1)
        [NoScaleOffset]_MainTex("ColorTexture",2D) = "white"{}
        [NoScaleOffset]_LightTex("LightTexture",2D) = "Bump"{}
        _BumpScale("BumpScale",Float) = 1
        [NoScaleOffset]_FaceLightTex("FaceLightTex",2D) = "white"{}
        [NoScaleOffset]_OtherDataTex1("Other Data Tex1",2D) = "white"{}
        [NoScaleOffset]_OtherDataTex2("Other Data Tex2",2D) = "white"{}

        [Header(Outline)]
        [Toggle(_OUTLINE_PASS)]_Outline("Outline描边",Float) = 0
        _OutlineWidth("OutlineWidth",Range(1,10)) = 1
        _OutlineColor1("OutlineColor",Color)=(0,0,0,1)
        _OutlineColor2("OutlineColor",Color)=(0,0,0,1)
        _OutlineColor3("OutlineColor",Color)=(0,0,0,1)
        _OutlineColor4("OutlineColor",Color)=(0,0,0,1)
        _OutlineColor5("OutlineColor",Color)=(0,0,0,1)
        _OutlineZOffset("OutlineZOffset",Range(0,1)) = 0.01

        _NoseLineKDnDisp("NoseLineKDnDisp",Float) = 1
        _NoseLineHoriDisp("NoseLineHoriDisp",Float) = 1

        [Header(Diffuse)]
        _AlbedoSmoothness("AlbedoSmoothness",Float) = 1.0

        //pbr
        _Metallic("Metallic",Range(0,10)) = 0
        _Glossiness("Glossiness",Range(0,1)) = 0
        [Toggle]_HightlightShape1("HightlightShape1",Float) = 0
        [Toggle]_HightlightShape2("HightlightShape2",Float) = 0
        [Toggle]_HightlightShape3("HightlightShape3",Float) = 0
        [Toggle]_HightlightShape4("HightlightShape4",Float) = 0
        [Toggle]_HightlightShape5("HightlightShape5",Float) = 0

        _HeadSphereRang("HeadSphereRang",Range(0,1)) = 0

        _ShapeSoftness1("ShapeSoftness1",Range(0,1)) = 1
        _ShapeSoftness2("ShapeSoftness2",Range(0,1)) = 1
        _ShapeSoftness3("ShapeSoftness3",Range(0,1)) = 1
        _ShapeSoftness4("ShapeSoftness4",Range(0,1)) = 1
        _ShapeSoftness5("ShapeSoftness5",Range(0,1)) = 1

        _SpecularRange1("SpecularRange1",Range(0,2)) = 1
        _SpecularRange2("SpecularRange2",Range(0,2)) = 1
        _SpecularRange3("SpecularRange3",Range(0,2)) = 1
        _SpecularRange4("SpecularRange4",Range(0,2)) = 1
        _SpecularRange5("SpecularRange5",Range(0,2)) = 1

        _ToonSpecular1("ToonSpecular1",Range(0,1)) = 0.01
        _ToonSpecular2("ToonSpecular2",Range(0,1)) = 0.01
        _ToonSpecular3("ToonSpecular3",Range(0,1)) = 0.01
        _ToonSpecular4("ToonSpecular4",Range(0,1)) = 0.01
        _ToonSpecular5("ToonSpecular5",Range(0,1)) = 0.01

        _ModelSize1("ModelSize1",Range(0,100)) = 1
        _ModelSize2("ModelSize2",Range(0,100)) = 1
        _ModelSize3("ModelSize3",Range(0,100)) = 1
        _ModelSize4("ModelSize4",Range(0,100)) = 1
        _ModelSize5("ModelSize5",Range(0,100)) = 1

        _SpecularIntensity("SpecularIntensity",Range(0,1)) = 0.1

        [HDR]_SpecularColor1("SpecularColor1",Color) = (1,1,1,1)
        [HDR]_SpecularColor2("SpecularColor2",Color) = (1,1,1,1)
        [HDR]_SpecularColor3("SpecularColor3",Color) = (1,1,1,1)
        [HDR]_SpecularColor4("SpecularColor4",Color) = (1,1,1,1)
        [HDR]_SpecularColor5("SpecularColor5",Color) = (1,1,1,1)

        //SH 
        [Header(Ambient)]
        _AmbientColorIntensity("AmbientColorIntensity",Range(0,1)) = 0.1

        //RimColor
        [Header(RimColor)]
        [Toggle(_SCREEN_SAPCE_RIM)]_ScreenSpaceRim("ScreenSpace Rim",Float) = 1
        [Enum(S0,0,S1,1,S2,2,S3,3,S4,4,NoSkin,5)] _SkinMatID("SkinMatID",Float) = 0

        [HDR]_UISunColor1("_UISunColor1",Color) = (1,1,1,1)
        [HDR]_UISunColor2("_UISunColor2",Color) = (1,1,1,1)
        [HDR]_UISunColor3("_UISunColor3",Color) = (1,1,1,1)
        [HDR]_UISunColor4("_UISunColor4",Color) = (1,1,1,1)
        [HDR]_UISunColor5("_UISunColor5",Color) = (1,1,1,1)

        [HDR]_RimGlowLightColor1("RimGlowLightColor1",Color) = (1,1,1,1)
        [HDR]_RimGlowLightColor2("RimGlowLightColor2",Color) = (1,1,1,1)
        [HDR]_RimGlowLightColor3("RimGlowLightColor3",Color) = (1,1,1,1)
        [HDR]_RimGlowLightColor4("RimGlowLightColor4",Color) = (1,1,1,1)
        [HDR]_RimGlowLightColor5("RimGlowLightColor5",Color) = (1,1,1,1)

        _ScreenSpaceRimWidth("ScreenSpaceRimWidth",Range(0,5)) = 1
        _ScreenSpaceRimThreshold("ScreenSpaceRimThreshold",Range(0,1)) = 0.01
        _ScreenSpaceRimFadeout("ScreenSpaceRimFadeout",Range(0,10)) = 0.5
        _ScreenSpaceRimBrightness("ScreenSpaceRimBrightness",Range(0,10)) = 1

        //shadow
        [Header(Shadow)]
        _ShadowColor1("_ShadowColor1",Color) = (1,1,1,1)
        _ShadowColor2("_ShadowColor2",Color) = (1,1,1,1)
        _ShadowColor3("_ShadowColor3",Color) = (1,1,1,1)
        _ShadowColor4("_ShadowColor4",Color) = (1,1,1,1)
        _ShadowColor5("_ShadowColor5",Color) = (1,1,1,1)

        _ShallowColor1("ShallowColor1",Color) = (1,1,1,1)
        _ShallowColor2("ShallowColor2",Color) = (1,1,1,1)
        _ShallowColor3("ShallowColor3",Color) = (1,1,1,1)
        _ShallowColor4("ShallowColor4",Color) = (1,1,1,1)
        _ShallowColor5("ShallowColor5",Color) = (1,1,1,1)

        _PostShadowFadeTint("PostShadowFadeTint",Color) = (1,1,1,1)
        _PostShadowTint("PostShadowTint",Color) = (1,1,1,1)

        _PostShallowFadeTint("PostShallowFadeTint",Color) = (1,1,1,1)
        _PostShallowTint("PostShallowTint",Color) = (1,1,1,1)
        _PostSSSTint("PostSSSTint",Color) = (1,1,1,1)
        _PostFrontTint("PostFrontTint",Color) = (1,1,1,1)

        //depthShadow
        [Header(ScreenSpaceRim)]
        [Toggle(_SCREEN_SPACE_SHADOW)]_ScreenSpaceShadow("Screen Space Shadow",Float) = 1
        _ScreenSpaceShadowWidth("Screen Space Shadow Width",Range(0,1)) = 0.2
        _ScreenSpaceShadowThreshold("Screen Space Shadow Threshold",Range(0,1)) = 0.015
        _ScreenSpaceShadowFadeout("Screen Space Shadow Fadeout",Range(0,10)) = 0.2

        //sdf
        [Header(SDF)]
        [NoScaleOffset]_SDFTex("SDF Texture",2D) = "white"{}
        _HeadCenter("HeadCenter",Vector) = (0,0,0,0)
        _HeadForward("HeadForward",Vector) = (0,0,0,0)
        _HeadRight("HeadRight",Vector) = (0,0,0,0)

        [Header(MatCap)]
        [Toggle(_MATCAP_ON)]_MatCap_ON("_MatCap_ON",Float) = 0
        [NoScaleOffset]_MatCapTex1("_MatCapTex1",2D) = "white"{}
        [NoScaleOffset]_MatCapTex2("_MatCapTex2",2D) = "white"{}
        [NoScaleOffset]_MatCapTex3("_MatCapTex3",2D) = "white"{}
        [NoScaleOffset]_MatCapTex4("_MatCapTex4",2D) = "white"{}
        [NoScaleOffset]_MatCapTex5("_MatCapTex5",2D) = "white"{}

        _MatCapTintColor1("MatCapTintColor1",Color) = (1,1,1,1)
        _MatCapTintColor2("MatCapTintColor2",Color) = (1,1,1,1)
        _MatCapTintColor3("MatCapTintColor3",Color) = (1,1,1,1)
        _MatCapTintColor4("MatCapTintColor4",Color) = (1,1,1,1)
        _MatCapTintColor5("MatCapTintColor5",Color) = (1,1,1,1)

        _MatCapColorBrust1("MatCapColorBrust1",Range(0,10)) = 1
        _MatCapColorBrust2("MatCapColorBrust2",Range(0,10)) = 1
        _MatCapColorBrust3("MatCapColorBrust3",Range(0,10)) = 1
        _MatCapColorBrust4("MatCapColorBrust4",Range(0,10)) = 1
        _MatCapColorBrust5("MatCapColorBrust5",Range(0,10)) = 1

        _MatCapAlphaBrust1("MatCapAlphaBrust1",Range(0,10)) = 1
        _MatCapAlphaBrust2("MatCapAlphaBrust2",Range(0,10)) = 1
        _MatCapAlphaBrust3("MatCapAlphaBrust3",Range(0,10)) = 1
        _MatCapAlphaBrust4("MatCapAlphaBrust4",Range(0,10)) = 1
        _MatCapAlphaBrust5("MatCapAlphaBrust5",Range(0,10)) = 1

        [Toggle]_MatCapRefract1("MatCapRefract1",Float) = 0.5
        [Toggle]_MatCapRefract2("MatCapRefract2",Float) = 0.5
        [Toggle]_MatCapRefract3("MatCapRefract3",Float) = 0.5
        [Toggle]_MatCapRefract4("MatCapRefract4",Float) = 0.5
        [Toggle]_MatCapRefract5("MatCapRefract5",Float) = 0.5

        _MatCapDepth1("MatCapDepth1",Float) = 0.5
        _MatCapDepth2("MatCapDepth2",Float) = 0.5
        _MatCapDepth3("MatCapDepth3",Float) = 0.5
        _MatCapDepth4("MatCapDepth4",Float) = 0.5
        _MatCapDepth5("MatCapDepth5",Float) = 0.5

        [Enum(AlphaBlend,0,Add,1,OverLay,2)]_MatCapBlendMode1("MatCapBlendMode1",Float) = 0
        [Enum(AlphaBlend,0,Add,1,OverLay,2)]_MatCapBlendMode2("MatCapBlendMode2",Float) = 0
        [Enum(AlphaBlend,0,Add,1,OverLay,2)]_MatCapBlendMode3("MatCapBlendMode3",Float) = 0
        [Enum(AlphaBlend,0,Add,1,OverLay,2)]_MatCapBlendMode4("MatCapBlendMode4",Float) = 0
        [Enum(AlphaBlend,0,Add,1,OverLay,2)]_MatCapBlendMode5("MatCapBlendMode5",Float) = 0

        _MatCapParam1("MatCapWrapOffset1",Vector) = (5,5,0,0)
        _MatCapParam2("MatCapWrapOffset2",Vector) = (5,5,0,0)
        _MatCapParam3("MatCapWrapOffset3",Vector) = (5,5,0,0)
        _MatCapParam4("MatCapWrapOffset4",Vector) = (5,5,0,0)
        _MatCapParam5("MatCapWrapOffset5",Vector) = (5,5,0,0)

        [Header(Option)]
        _AlphaClip("AlphaClip",Float) = 0.5
        [Enum(Off,0,On,1)]_ZWrite("ZWrite",Float) = 1
        [Enum(UnityEngine.Rendering.CullMode)]_Cull("Cull",int) = 2

        [Enum(UnityEngine.Rendering.BlendMode)]_BlendSrc("SrcAlpha混合原因子",int) = 1
        [Enum(UnityEngine.Rendering.BlendMode)]_BlendDst("DstAlpha混合目标乘子",int) = 0
        [Enum(UnityEngine.Rendering.BlendOp)]_BlendOp("Alpha混合算符",int) = 0

        _StencilRef("蒙版值",int) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)]_StencilComp("蒙版判断条件",int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_StencilPassOp("蒙版测试通过",int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_StencilFailOp("蒙版测试失败",int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_StencilZPassOp("深度Z测试失败",int) = 0

        //眼睛重绘
        [Header(EyeReDrawPassOption)]
        [Toggle(_SRP_DEFAULT_PASS)]_SRP_DEFAULT_PASS("SRP Default Pass",int) = 0
        [Enum(UnityEngine.Rendering.BlendMode)]_SRPBlendSrc("SRPSrcAlpha混合原因子",int) = 1
        [Enum(UnityEngine.Rendering.BlendMode)]_SRPBlendDst("SRPDstAlpha混合目标乘子",int) = 0
        [Enum(UnityEngine.Rendering.BlendOp)]_SRPBlendOp("SRPAlpha混合算符",int) = 0

        _SRPStencilRef("SRP蒙版值",int) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)]_SRPStencilComp("SRP蒙版判断条件",int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_SRPStencilPassOp("SRP蒙版测试通过",int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_SRPStencilFailOp("SRP蒙版测试失败",int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_SRPStencilZPassOp("SRP深度Z测试失败",int) = 0



    }

    SubShader
    {

        Tags
        {
            "RenderPipeline" = "UniversalRenderPipeline"
            "RenderType" = "Opaque"
        }

        HLSLINCLUDE
        #pragma shader_feature_local _DOMAIN_FACE
        #pragma shader_feature_local _DOMAIN_EYE
        #pragma shader_feature_local _DOMAIN_BODY

        #pragma shader_feature_local _SCREEN_SPACE_SHADOW
        #pragma shader_feature_local _MATCAP_ON
        #pragma shader_feature_local _SCREEN_SAPCE_RIM
        #pragma shader_feature_local _SRP_DEFAULT_PASS
        #pragma shader_feature_local _Lazer_ON

        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
        #pragma multi_compile _ _MAIN_LIGHT_CALCULATE_SHADOWS


        //软阴影
        #pragma multi_compile _ _SHADOWS_SOFT

        #pragma multi_compile_fog

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

        CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float4 _Color;
            float _BumpScale;
            float _AlbedoSmoothness;
            float _AlphaClip;
            float _ZWrite;
            float _Cull;
            float _ScreenSpaceShadow;
            float _ScreenSpaceShadowWidth;
            float _ScreenSpaceShadowThreshold;
            float _ScreenSpaceShadowFadeout;
            float _ScreenSpaceRimWidth;
            float4 _ShadowColor1;
            float4 _ShadowColor2;
            float4 _ShadowColor3;
            float4 _ShadowColor4;
            float4 _ShadowColor5;
            float4 _ShallowColor1;
            float4 _ShallowColor2;
            float4 _ShallowColor3;
            float4 _ShallowColor4;
            float4 _ShallowColor5;

            float _OutlineWidth;
            float3 _OutlineColor1;
            float3 _OutlineColor2;
            float3 _OutlineColor3;
            float3 _OutlineColor4;
            float3 _OutlineColor5;
            float _OutlineZOffset;

            float _NoseLineKDnDisp;
            float _NoseLineHoriDisp;


            float4 _PostShadowFadeTint;
            float4 _PostShadowTint;
            float4 _PostShallowFadeTint;
            float4 _PostShallowTint;
            float4 _PostSSSTint;
            float4 _PostFrontTint;

            float3 _HeadCenter;
            float3 _HeadForward;
            float3 _HeadRight;

            float3 _MatCapTintColor1;
            float3 _MatCapTintColor2;
            float3 _MatCapTintColor3;
            float3 _MatCapTintColor4;
            float3 _MatCapTintColor5;

            float _MatCapColorBrust1;
            float _MatCapColorBrust2;
            float _MatCapColorBrust3;
            float _MatCapColorBrust4;
            float _MatCapColorBrust5;


            float _MatCapAlphaBrust1;
            float _MatCapAlphaBrust2;
            float _MatCapAlphaBrust3;
            float _MatCapAlphaBrust4;
            float _MatCapAlphaBrust5;

            float _MatCapRefract1;
            float _MatCapRefract2;
            float _MatCapRefract3;
            float _MatCapRefract4;
            float _MatCapRefract5;

            float _MatCapDepth1;
            float _MatCapDepth2;
            float _MatCapDepth3;
            float _MatCapDepth4;
            float _MatCapDepth5;

            int _MatCapBlendMode1;
            int _MatCapBlendMode2;
            int _MatCapBlendMode3;
            int _MatCapBlendMode4;
            int _MatCapBlendMode5;

            float4 _MatCapParam1;
            float4 _MatCapParam2;
            float4 _MatCapParam3;
            float4 _MatCapParam4;
            float4 _MatCapParam5;

            //pbr
            float _Metallic;
            float _HightlightShape1;
            float _HightlightShape2;
            float _HightlightShape3;
            float _HightlightShape4;
            float _HightlightShape5;

            float _HeadSphereRang;

            float _ShapeSoftness1;
            float _ShapeSoftness2;
            float _ShapeSoftness3;
            float _ShapeSoftness4;
            float _ShapeSoftness5;

            float _SpecularRange1;
            float _SpecularRange2;
            float _SpecularRange3;
            float _SpecularRange4;
            float _SpecularRange5;


            float _Glossiness;
            float _ToonSpecular1;
            float _ToonSpecular2;
            float _ToonSpecular3;
            float _ToonSpecular4;
            float _ToonSpecular5;

            float _ModelSize1;
            float _ModelSize2;
            float _ModelSize3;
            float _ModelSize4;
            float _ModelSize5;

            float _SpecularIntensity;

            float3 _SpecularColor1;
            float3 _SpecularColor2;
            float3 _SpecularColor3;
            float3 _SpecularColor4;
            float3 _SpecularColor5;

            //SH
            float _AmbientColorIntensity;

            //rim
            float _SkinMatID;

            float3 _UISunColor1;
            float3 _UISunColor2;
            float3 _UISunColor3;
            float3 _UISunColor4;
            float3 _UISunColor5;

            float3 _RimGlowLightColor1;
            float3 _RimGlowLightColor2;
            float3 _RimGlowLightColor3;
            float3 _RimGlowLightColor4;
            float3 _RimGlowLightColor5;

            float ScreenSpaceRimWidth;
            float _ScreenSpaceRimThreshold;
            float _ScreenSpaceRimFadeout;
            float _ScreenSpaceRimBrightness;

            //蒙版测试
            int _StencilRef;
            //眼部重绘
            int _SRPStencilRef;

        CBUFFER_END

        Texture2D _MainTex;
        sampler sampler_MainTex;
        Texture2D _FaceLightTex;
        sampler sampler_FaceLightTex;
        Texture2D _OtherDataTex1;
        sampler sampler_OtherDataTex1;
        Texture2D _OtherDataTex2;
        sampler sampler_OtherDataTex2;
        Texture2D _LightTex;
        sampler sampler_LightTex;
        Texture2D _SDFTex;
        sampler sampler_SDFTex;

        Texture2D _MatCapTex1;
        sampler sampler_MatCapTex1;
        Texture2D _MatCapTex2;
        sampler sampler_MatCapTex2;
        Texture2D _MatCapTex3;
        sampler sampler_MatCapTex3;
        Texture2D _MatCapTex4;
        sampler sampler_MatCapTex4;
        Texture2D _MatCapTex5;
        sampler sampler_MatCapTex5;

        TextureCube _CubeMap;
        sampler sampler_CubeMap;


        struct UniversalAttributes
        {
            float4 positionOS : POSITION;
            float2 uv : TEXCOORD0;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
        };

        struct UniversalVaryings
        {
            float4 positionCS : SV_POSITION;
            float4 positionWSAndFogFactor : TEXCOORD0;
            float3 normalWS : TEXCOORD1;
            float4 tangentWS : TEXCOORD2;
            float3 viewDirWS : TEXCOORD3;
            float2 texcoord : TEXCOORD4;
        };

        float averageColor(float3 color)
        {
            return dot(color, float3(1.0, 1.0, 1.0)) / 3;
        }

        float3 normalizeColorByAverageColor(float3 color)
        {
            float average = averageColor(color);
            return color / (max(average, 1e-5).xxx);
        }

        float3 ScaleColorByMax(float3 color)
        {
            float maxComponment = max(max(color.r, color.g), color.b);
            maxComponment = min(maxComponment, 1.0);
            return color * maxComponment;
        }

        float3 ClampColorMax(float3 color)
        {
            float maxComponment = max(color.r, max(color.g, color.b));
            if (maxComponment > 1.0)
            {
                return color / maxComponment;
            }
            return color;
        }

        #define DFINE_SELECT(TYPE)\
         TYPE select(int id, TYPE e0, TYPE e1, TYPE e2, TYPE e3, TYPE e4)    {return TYPE(id > 0 ? (id > 1 ? (id > 2 ? (id > 3 ? e4 : e3) : e2) : e1) : e0);}\
         TYPE##2 select(int id, TYPE##2 e0, TYPE##2 e1, TYPE##2 e2, TYPE##2 e3, TYPE##2 e4)  {return TYPE##2(id > 0 ? (id > 1 ? (id > 2 ? (id > 3 ? e4 : e3) : e2) : e1) : e0);}\
         TYPE##3 select(int id, TYPE##3 e0, TYPE##3 e1, TYPE##3 e2, TYPE##3 e3, TYPE##3 e4)  {return TYPE##3(id > 0 ? (id > 1 ? (id > 2 ? (id > 3 ? e4 : e3) : e2) : e1) : e0);}\
         TYPE##4 select(int id, TYPE##4 e0, TYPE##4 e1, TYPE##4 e2, TYPE##4 e3, TYPE##4 e4)  {return TYPE##4(id > 0 ? (id > 1 ? (id > 2 ? (id > 3 ? e4 : e3) : e2) : e1) : e0);}

        DFINE_SELECT(bool)
        DFINE_SELECT(uint)
        DFINE_SELECT(int)
        DFINE_SELECT(float)
        DFINE_SELECT(half)

        #define DEFINE_POW(TYPE) \
        TYPE pow2(TYPE x) { return TYPE(x * x);} \
        TYPE##2 pow2(TYPE##2 x) { return TYPE##2(x * x);} \
        TYPE##3 pow2(TYPE##3 x) { return TYPE##3(x * x);} \
        TYPE##4 pow2(TYPE##4 x) { return TYPE##4(x * x);} \
        TYPE pow3(TYPE x) { return TYPE(x * x * x);} \
        TYPE##2 pow3(TYPE##2 x) { return TYPE##2(x * x * x);} \
        TYPE##3 pow3(TYPE##3 x) { return TYPE##3(x * x * x);} \
        TYPE##4 pow3(TYPE##4 x) { return TYPE##4(x * x * x);} \
        TYPE pow4(TYPE x) { TYPE xx = x * x; return TYPE(xx * xx);} \
        TYPE##2 pow4(TYPE##2 x) { TYPE##2 xx = x * x; return TYPE##2(xx * xx);} \
        TYPE##3 pow4(TYPE##3 x) { TYPE##3 xx = x * x; return TYPE##3(xx * xx);} \
        TYPE##4 pow4(TYPE##4 x) { TYPE##4 xx = x * x; return TYPE##4(xx * xx);} \
        TYPE pow5(TYPE x) { TYPE xx = x * x; return TYPE(xx * xx * x);} \
        TYPE##2 pow5(TYPE##2 x) { TYPE##2 xx = x * x; return TYPE##2(xx * xx * x);} \
        TYPE##3 pow5(TYPE##3 x) { TYPE##3 xx = x * x; return TYPE##3(xx * xx * x);} \
        TYPE##4 pow5(TYPE##4 x) { TYPE##4 xx = x * x; return TYPE##4(xx * xx * x);} \
        TYPE pow6(TYPE x) { TYPE xx = x * x; return TYPE(xx * xx * xx);} \
        TYPE##2 pow6(TYPE##2 x) { TYPE##2 xx = x * x; return TYPE##2(xx * xx * xx);} \
        TYPE##3 pow6(TYPE##3 x) { TYPE##3 xx = x * x; return TYPE##3(xx * xx * xx);} \
        TYPE##4 pow6(TYPE##4 x) { TYPE##4 xx = x * x; return TYPE##4(xx * xx * xx);}

        DEFINE_POW(bool)
        DEFINE_POW(uint)
        DEFINE_POW(int)
        DEFINE_POW(float)
        DEFINE_POW(half)

        // // 顶点着色器函数
        UniversalVaryings MainVS(UniversalAttributes input)
        {
            //获取世界空间下法线和位置等信息
            VertexPositionInputs positionInputs = GetVertexPositionInputs(input.positionOS.xyz);
            VertexNormalInputs normalInputs = GetVertexNormalInputs(input.normalOS, input.tangentOS);

            UniversalVaryings output;
            output.positionCS = positionInputs.positionCS;
            output.positionWSAndFogFactor = float4(positionInputs.positionWS,
                                                   ComputeFogFactor(positionInputs.positionCS.z));
            output.normalWS = normalInputs.normalWS;

            output.tangentWS.xyz = normalInputs.tangentWS;
            output.tangentWS.w = input.tangentOS.w * GetOddNegativeScale();
            output.viewDirWS = unity_OrthoParams.w == 0
                                   ? GetCameraPositionWS() - positionInputs.positionWS
                                   : GetWorldToViewMatrix()[2].xyz;

            output.texcoord = input.uv;

            return output;
        }

        // 片元着色器函数
        float4 MainPS(UniversalVaryings input, bool isFrontFace : SV_IsFrontFace):SV_TARGET
        {
            float4 var_MainTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.texcoord);
            var_MainTex *= _Color;
            float3 baseCol = var_MainTex.rgb * _Color.xyz;
            float baseAlpha = 1.0;

            #if _DOMAIN_BODY || _DOMAIN_EYE
            {
               baseAlpha = var_MainTex.a;
            }
            #endif

            int materialId = 0;

            float3 normalWS = normalize(input.normalWS);
            float3 positionWS = input.positionWSAndFogFactor.xyz;

            float3 viewDirWS = normalize(input.viewDirWS);

            float4 shadowCoord = TransformWorldToShadowCoord(positionWS);
            Light mainLight = GetMainLight(shadowCoord);
            float3 lightColor = mainLight.color;
            float3 lightDirectionWS = normalize(mainLight.direction);

            //TBN
            float sign = input.tangentWS.w;
            float3 tangentWS = normalize(input.tangentWS.xyz);
            float3 bitangentWS = sign * cross(normalWS, tangentWS);

            float3 pixelNormalWS = normalWS;

            float matcapMask = 0;
            float diffuseBais = 0;

            float metallic = 0;
            float specularMask = 0;

            float smoothness = 0.58;

            #if _DOMAIN_BODY
           {
               float4 var_lightTex = SAMPLE_TEXTURE2D(_LightTex,sampler_LightTex,input.texcoord);
               var_lightTex = var_lightTex * 2.0 - 1.0;
               
               diffuseBais = var_lightTex.z * 2.0;

               float3 pixelNormalTS = float3(var_lightTex.xy,0.0);
               pixelNormalTS *= _BumpScale;
               pixelNormalTS.z = sqrt(1.0 - min(0.0,dot(pixelNormalTS.xy,pixelNormalTS.xy)));
           
               pixelNormalWS = TransformTangentToWorld(pixelNormalTS, float3x3(tangentWS, bitangentWS, normalWS));
               pixelNormalWS = normalize(pixelNormalWS);
               float4 var_OtherDataTex1= SAMPLE_TEXTURE2D(_OtherDataTex1,sampler_OtherDataTex1,input.texcoord);
               materialId = max(0,4 - floor(var_OtherDataTex1.r * 5));

               float4 var_OtherDataTex2 = SAMPLE_TEXTURE2D(_OtherDataTex2,sampler_OtherDataTex2,input.texcoord);
               matcapMask = var_OtherDataTex2.b;

               metallic = _Metallic * var_OtherDataTex1.g;
               specularMask = var_OtherDataTex1.b;

               smoothness = _Glossiness * var_OtherDataTex2.g;
           }
            #endif


            normalWS *= isFrontFace ? 1.0 : -1.0;
            pixelNormalWS *= isFrontFace ? 1.0 : -1.0;

            float3 MatCapColor = baseCol;
            #if _MATCAP_ON
                {
               float mask = matcapMask;
               float3 normalVS = TransformWorldToViewDir(pixelNormalWS);
               float2 matcapUV = normalVS.xy * 0.5 + 0.5;

               float refract = select(materialId,
                  _MatCapRefract1,
                  _MatCapRefract2,
                  _MatCapRefract3,
                  _MatCapRefract4,
                  _MatCapRefract5
                  );

               if (refract > 0.5)
               {
                  float4 param = select(materialId,
                     _MatCapParam1,
                     _MatCapParam2,
                     _MatCapParam3,
                     _MatCapParam4,
                     _MatCapParam5
                     );
                  float depth = select(materialId,
                     _MatCapDepth1,
                     _MatCapDepth2,
                     _MatCapDepth3,
                     _MatCapDepth4,
                     _MatCapDepth5
                     );

                  matcapUV = matcapUV * depth + param.xy * input.texcoord + param.zw;

                  MatCapColor = select(materialId,
                     SAMPLE_TEXTURE2D(_MatCapTex1,sampler_MatCapTex1,matcapUV).rgb,
                     SAMPLE_TEXTURE2D(_MatCapTex2,sampler_MatCapTex2,matcapUV).rgb,
                     SAMPLE_TEXTURE2D(_MatCapTex3,sampler_MatCapTex3,matcapUV).rgb,
                     SAMPLE_TEXTURE2D(_MatCapTex4,sampler_MatCapTex4,matcapUV).rgb,
                     SAMPLE_TEXTURE2D(_MatCapTex5,sampler_MatCapTex5,matcapUV).rgb
                  );

                  float3 tintColor = select(materialId,
                     _MatCapTintColor1,
                     _MatCapTintColor2,
                     _MatCapTintColor3,
                     _MatCapTintColor4,
                     _MatCapTintColor5
                  );

                  float alphaBrust = select(materialId,
                     _MatCapAlphaBrust1,
                     _MatCapAlphaBrust2,
                     _MatCapAlphaBrust3,
                     _MatCapAlphaBrust4,
                     _MatCapAlphaBrust5
                     );

                  float colorBrust = select(materialId,
                     _MatCapColorBrust1,
                     _MatCapColorBrust2,
                     _MatCapColorBrust3,
                     _MatCapColorBrust4,
                     _MatCapColorBrust5
                     );

                  int blendMode = select(materialId,
                     _MatCapBlendMode1,
                     _MatCapBlendMode2,
                     _MatCapBlendMode3,
                     _MatCapBlendMode4,
                     _MatCapBlendMode5
                     );

                  if (blendMode == 0)
                  {
                     float alpha = saturate(alphaBrust * mask);
                     float3 blendColor = tintColor * MatCapColor * colorBrust;
                     MatCapColor = lerp(baseCol,blendColor,alpha);
                  }
                  else if (blendMode == 1)
                  {
                     float alpha = saturate(alphaBrust * mask);
                     float3 blendColor = tintColor * MatCapColor * colorBrust;
                     MatCapColor = baseCol + blendColor * alpha;
                  }
                  else if (blendMode == 2)
                  {
                     float alpha = saturate(alphaBrust * mask);
                     float3 blendColor = saturate((MatCapColor * tintColor - 0.5) * colorBrust + MatCapColor * tintColor);
                     blendColor = lerp(0.5,blendColor,alpha);
                     MatCapColor = lerp(blendColor * baseCol * 2,1 - 2 * (1 - baseCol) * (1 - blendColor),baseCol >=0.5);
                  }
               }
            }
            #endif


            float baseAttenuation = 1.0;
            {
                float NoL = dot(pixelNormalWS, lightDirectionWS);
                baseAttenuation = NoL + diffuseBais;
            }

            float shadowAttenuation = 1.0;
            #if _SCREEN_SPACE_SHADOW
            {
               float lineEyeDepth = input.positionCS.w;
               float perspective = 1.0 / lineEyeDepth;
               float offsetMul = _ScreenSpaceShadowWidth * 5.0 * perspective / 100.0;

               float3 lightDirectionVS = TransformWorldToViewDir(lightDirectionWS);
               float2 offset = lightDirectionVS.xy * offsetMul;
               int2 coord = input.positionCS.xy + offset * _ScaledScreenParams.xy;

               coord = min(max(0,coord),_ScaledScreenParams.xy - 1);
               float offsetSceneDepth = LoadSceneDepth(coord);
               float offsetSceneLinearEyeDepth = LinearEyeDepth(offsetSceneDepth,_ZBufferParams);

               float fadeout = max(1e-5,_ScreenSpaceShadowFadeout);
               shadowAttenuation = saturate((offsetSceneLinearEyeDepth - (lineEyeDepth - _ScreenSpaceShadowThreshold)) * 50 / fadeout);
            }
            #endif

            float3 albedoSmoothness = max(1e-5, _AlbedoSmoothness);
            float albedoShadowFade = 1.0; //较深阴影
            float albedoShadow = 1.0; //较浅阴影
            float albedoShallowFade = 1.0; //中间过渡部分较深阴影
            float albedoShallow = 1.0; //中间过渡部分较浅阴影
            float albedoSSS = 1.0; //中间过渡部分较浅阴影向上偏移出的次表面部分
            float albedoFront = 1.0; //最亮区域，接近没有衰减的部分
            float albedoForward = 1.0; //最强反射部分

            {
                float Attenuation = baseAttenuation * 1.5; //-1.5~1.5
                //光滑系数调整
                float s0 = albedoSmoothness * 1.5; //0~1.5
                //锐利系数(粗糙度？）
                float s1 = 1.0 - s0; //-0.5~1

                //将明暗分成六个部分，每0.5一段，1.5~-1
                float aRamp[6] = {
                    (Attenuation + 1.5) / s1 + 0.0, //aRamp[0]，强光衰减部分，表示最强的衰弱和最深的阴影的负值
                    (Attenuation + 0.5) / s0 + 0.5, //aRamp[1],相对较弱的衰减，表示较浅的阴影
                    (Attenuation + 0.0) / s1 + 0.5, //aRamp[2],中等衰减，逐渐过度到正常的阴影
                    (Attenuation - 0.5) / s0 + 0.5, //aRamp[3],较弱阴影，较弱衰减区域
                    (Attenuation - 0.5) / s0 - 0.5, //aRamp[4]，衰减较少，表示反射或光照强度较强的次表面的区域
                    (Attenuation - 2.0) / s1 + 1.5 //aRamp[5],最亮区域，接近没有衰减的区域
                };
                albedoShadowFade = saturate(1 - aRamp[0]);
                albedoShadow = saturate(min(1 - aRamp[1], aRamp[0]));
                albedoShallowFade = saturate(min(1 - aRamp[2], aRamp[1]));
                albedoShallow = saturate((min(1 - aRamp[3], aRamp[2])));
                albedoSSS = saturate((min(1 - aRamp[4], aRamp[3])));
                albedoFront = saturate(min(1 - aRamp[5], aRamp[4]));
                albedoForward = saturate(aRamp[5]);
            }

            float sRamp[2] = {
                2 * shadowAttenuation,
                2 * shadowAttenuation - 1
            };

            albedoShallowFade *= saturate(sRamp[0]);
            albedoShallowFade += (1 - albedoShadowFade - albedoShallow) * saturate(1 - sRamp[0]);
            albedoShadow *= saturate(min(sRamp[0], 1 - sRamp[1])) + saturate(sRamp[1]);
            albedoSSS *= saturate(min(sRamp[0], 1 - sRamp[1])) + saturate(sRamp[1]);
            albedoSSS += (albedoFront + albedoForward) * saturate(min(sRamp[0], 1 - sRamp[1]));
            albedoFront *= saturate(sRamp[1]);
            albedoForward *= saturate(sRamp[1]);

            float angleThreshold = 0;
            float angleMapping = 0;
            float angleFunction = 0;
            float angleMaskMap = 0;
            //SDF
            #if _DOMAIN_FACE
            {
                float3 headForward = normalize(_HeadForward - _HeadCenter);
                float3 headRight = -normalize(_HeadRight - _HeadCenter);
                float3 headUp = normalize(cross(headForward, headRight));

                float3 lightDirectionProjHeadWS = lightDirectionWS - dot(lightDirectionWS, headUp) * headUp;
                lightDirectionProjHeadWS = normalize(lightDirectionProjHeadWS);

                float sX = dot(lightDirectionProjHeadWS, headRight);
                float sZ = dot(lightDirectionProjHeadWS, -headForward);

                angleThreshold = atan2(sX, sZ) / 3.14159265359;
                angleThreshold = angleThreshold > 0 ? (1 - angleThreshold) : (1 + angleThreshold);

                float2 angleUV = input.texcoord;
                if (dot(lightDirectionProjHeadWS, headRight) > 0)
                {
                    angleUV.x = 1.0 - angleUV.x;
                }

                float4 angleData = SAMPLE_TEXTURE2D(_SDFTex, sampler_SDFTex, angleUV);

                angleMapping = angleData.r;
                angleFunction = angleData.g;
                angleMaskMap = angleData.a;

                float3 outlineColor = _OutlineColor1.rgb * 0.2;
                float viewDotHeadUp = dot(headUp, viewDirWS);
                float viewDotHeadForward = dot(headForward, viewDirWS);
                float dispValue = lerp(_NoseLineKDnDisp, _NoseLineHoriDisp,
                                       smoothstep(
                                           0, 0.75, saturate(viewDotHeadUp + 0.85)));
                dispValue = viewDotHeadForward - dispValue;
                dispValue = smoothstep(0, 0.02, dispValue);
                dispValue -= var_MainTex.a;
                baseCol = lerp(baseCol, outlineColor, saturate(dispValue));

                metallic *= _Metallic;
                smoothness *= _Glossiness;

                float s = lerp(_AlbedoSmoothness, 0.025, saturate(2.5 * (angleFunction - 0.5)));
                s = max(1e-5, s);

                float angleAttenuation = 0.6 + (angleMapping * 1.2 - 0.6) / (s * 4 + 1) - angleThreshold;

                float aRamp[3] =
                {
                    angleAttenuation / s,
                    angleAttenuation / s - 1,
                    angleAttenuation / 0.125 - 16 * s
                };

                float angleShadowFade = saturate(1 - aRamp[0]);
                float angleShadow = 0;
                float angleShallowFade = 0;
                float angleShallow = 0;
                float angleSSS = min(saturate(1 - aRamp[1]), saturate(aRamp[0]));
                float angleFront = min(saturate(1 - aRamp[2]), saturate(aRamp[1]));
                float angleForward = saturate(aRamp[2]);

                float sRamp[1] =
                {
                    2 * shadowAttenuation
                };

                angleShadowFade *= saturate(1 - sRamp[0]);
                angleShallowFade += (1 - angleForward - angleFront - angleSSS - angleShallow) * saturate(sRamp[0]);
                angleShallowFade += (angleSSS + angleFront + angleForward) * saturate(1 - sRamp[0]);
                angleSSS *= saturate(sRamp[0]);
                angleFront *= saturate(sRamp[0]);
                angleForward *= saturate(sRamp[0]);

                albedoShadowFade = lerp(albedoShadowFade, angleShadowFade, angleMaskMap);
                albedoShadow = lerp(albedoShadow, angleShadow, angleMaskMap);
                albedoShallowFade = lerp(albedoShallowFade, angleShallowFade, angleMaskMap);
                albedoShallow = lerp(albedoShallow, angleShallow, angleMaskMap);
                albedoSSS = lerp(albedoSSS, angleSSS, angleMaskMap);
                albedoFront = lerp(albedoFront, angleFront, angleMaskMap);
                albedoForward = lerp(albedoForward, angleForward, angleMaskMap);
            }
            #endif


            float ShadowFadeColor = 1.0; //较深阴影
            float ShadowColor = 1.0; //较浅阴影
            float ShallowFadeColor = 1.0; //中间过渡部分较深阴影
            float ShallowColor = 1.0; //中间过渡部分较浅阴影
            float SSSColor = 1.0; //中间过渡部分较浅阴影向上偏移出的次表面部分
            float FrontColor = 1.0; //最亮区域，接近没有衰减的部分
            float ForwardColor = 1.0; //最强反射部分

            float zFade = saturate(input.positionCS.w * 0.43725);
            float3 shadowColor = float3(0, 0, 0);
            float3 shadowFadeColor = float3(0, 0, 0);
            shadowColor = select(materialId, _ShadowColor1, _ShadowColor2, _ShadowColor3, _ShadowColor4, _ShadowColor5);

            shadowColor = lerp(normalizeColorByAverageColor(shadowColor), shadowColor, zFade);
            shadowFadeColor = shadowColor * _PostShallowFadeTint;
            shadowColor = shadowColor * _PostShallowTint;

            SSSColor = _PostSSSTint;
            FrontColor = _PostFrontTint;
            ForwardColor = 1.0;

            float3 lightColorScaledByMax = ScaleColorByMax(lightColor);
            float3 albedo = (albedoForward * ForwardColor + albedoFront * FrontColor + albedoSSS * SSSColor) *
                lightColor;
            albedo += (albedoShadowFade * shadowFadeColor + albedoShadow * shadowColor + albedoShallowFade *
                ShallowFadeColor + albedoShallow * ShallowColor) * lightColorScaledByMax;


            float3 gammaColor = MatCapColor;
            {
                float pixelNdotL = dot(pixelNormalWS, lightDirectionWS);
                float NdotL = dot(normalWS, lightDirectionWS);

                float occlusion = saturate(1 - 3 * (NdotL - pixelNdotL)) * 2;
                occlusion *= sqrt(occlusion);
                occlusion = min(1, occlusion);

                float attenuation = lerp((pixelNdotL * 0.5 + 0.5) * occlusion, saturate(pixelNdotL), 0.5);

                float3 matCapColorClamped = ClampColorMax(MatCapColor);

                float luminance = Luminance(MatCapColor);
                float gamma = lerp(luminance * 0.2875 + 1.4375, 1, attenuation);

                float3 matCapColorGamma = pow(max(1e-5, matCapColorClamped), gamma);
                float3 matCapGammaHalf = lerp(MatCapColor, matCapColorGamma, 0.5);

                gammaColor = lerp(matCapGammaHalf, matCapColorGamma, saturate(NdotL));
            }

            float3 pbrDiffuseColor = lerp(0.96 * gammaColor, 0, metallic);
            float3 pbrSpecularColor = lerp(0.04, gammaColor, metallic);
            float3 specularColor = 0;

            #if _DOMAIN_BODY
             {
               float shape = select(materialId,
                  _HightlightShape1,
                  _HightlightShape2,
                  _HightlightShape3,
                  _HightlightShape4,
                  _HightlightShape5
                  );
               float range = select(materialId,
                  _SpecularRange1,
                  _SpecularRange2,
                  _SpecularRange3,
                  _SpecularRange4,
                  _SpecularRange5
               );

               float halfWS = normalize(viewDirWS + lightDirectionWS);

               float LoH = dot(lightDirectionWS,halfWS);
               float rangeLoH = saturate(range * LoH * 0.75 + 0.25);
               float rangeLoH2 = max(0.1,rangeLoH * rangeLoH);

               float NoL = dot(pixelNormalWS,lightDirectionWS);
               float rangeNoL = saturate(range * NoL * 0.75 + 0.25);

               float specular = 0;

               if (shape > 0.5)
               {
                  bool useSphere = _HeadSphereRang > 0;
                  float sphereNormalWS = positionWS - _HeadCenter;
                  float len = length(sphereNormalWS);
                  sphereNormalWS = normalize(sphereNormalWS);
                  float sphereUsage = 1.0 - saturate(len - _HeadSphereRang) * 20;
                  float3 shapeNormalWS = lerp(pixelNormalWS,sphereNormalWS,sphereUsage);

                  float atteuation = saturate(baseAttenuation * 1.5 + 0.5);
                  float shapeNoL = dot(lightDirectionWS,shapeNormalWS);
                  float shapeAttenuation = sqrt(saturate(shapeNoL * 0.5 + 0.5));

                  shapeNormalWS = useSphere?shapeNormalWS : pixelNormalWS;
                  shapeAttenuation = useSphere?shapeAttenuation : atteuation;

                  float NdotH = dot(shapeNormalWS,halfWS);
                  float NdotH01 = saturate(NdotH * 0.5 + 0.5);

                  specular = NdotH01 * shapeAttenuation + specularMask - 1;

                  float softness = select(materialId,
                     _ShapeSoftness1,
                     _ShapeSoftness2,
                     _ShapeSoftness3,
                     _ShapeSoftness4,
                     _ShapeSoftness5
                     );

                  specular = saturate(specular / softness);
                  specular = specular *  min(1.0,1.0 / (6.0 * rangeLoH2)) * rangeNoL;
               }else
               {
                  float perceptualRoughness = 1 - smoothness;
                  float roughness = perceptualRoughness * perceptualRoughness;

                  float normalizationTerm = roughness * 4 + 2;
                  float roughness2 = roughness * roughness;

                  float roughness2Minus1 = roughness2 - 1;
                  float NdotH = dot(pixelNormalWS,halfWS);
                  float rangeNdotH = saturate(range * NdotH * 0.75 + 0.25);

                  float d = rangeNdotH * rangeNdotH * roughness2Minus1 + 1.0;
                  float ggx = roughness2 / ((d * d) * rangeLoH2 * normalizationTerm);
                  
                  specular = saturate((ggx - smoothness) * rangeNoL);
                  specular = specular / max(1e-5,roughness);

                  float toon = select(materialId,
                     _ToonSpecular1,
                     _ToonSpecular2,
                     _ToonSpecular3,
                     _ToonSpecular4,
                     _ToonSpecular5
                     );
                  float size = select(materialId,
                     _ModelSize1,
                     _ModelSize2,
                     _ModelSize3,
                     _ModelSize4,
                     _ModelSize5
                     );

                  specular *= toon * size * specularMask;
                  specular *= 10;
                  specular = saturate(specular);
               }

               specular *= 100 ;
               specular *= _SpecularIntensity;

               float3 tintColor = select(materialId,
                  _SpecularColor1,
                  _SpecularColor2,
                  _SpecularColor3,
                  _SpecularColor4,
                  _SpecularColor5
                  );

               specularColor = specular * tintColor;
            }           
            #endif


            float3 rimGlowColor = 0;

            bool isSkin = materialId == _SkinMatID;
            //背光衰减
            float LoV = dot(lightDirectionWS, viewDirWS);
            float viewAttenuation = -LoV * 0.5 + 0.5;
            viewAttenuation = pow2(viewAttenuation);
            viewAttenuation = viewAttenuation * 0.5 + 0.5;

            //法线方向垂直衰减
            float verticalAttenuation = pixelNormalWS.y * 0.5 + 0.5;
            verticalAttenuation = isSkin ? verticalAttenuation : pow2(viewAttenuation);
            verticalAttenuation = smoothstep(0, 1, verticalAttenuation);

            float lightAtteuation = saturate(dot(pixelNormalWS, lightDirectionWS)) * shadowAttenuation;

            //菲涅尔
            float cameraDistance = length(input.viewDirWS);
            float NoV = dot(pixelNormalWS, viewDirWS);
            float fresnelDistanceFade = (isSkin ? 0.75 : 0.65) - 0.45 * min(1, cameraDistance / 12.0);
            float fresnelAttenuation = 1 - NoV - fresnelDistanceFade;

            float fresnelSoftness = isSkin ? 0.2 : 0.3;
            fresnelAttenuation = smoothstep(0, fresnelSoftness, fresnelAttenuation);

            float distanceAttenuation = 1 - 0.7 * saturate(cameraDistance * 0.2 - 1);
            float edgeAttenuation = 1 - pow4(pow5(viewAttenuation));

            float3 sunColor = select(materialId,
                                       _UISunColor1,
                                       _UISunColor2,
                                       _UISunColor3,
                                       _UISunColor4,
                                       _UISunColor5
            );

            float sunLuminance = Luminance(sunColor);
            sunColor = isSkin ? sunColor : sunLuminance.xxx;

            //缩放系数
            float sunColorScaled = pow2(pow4(sunColor));
            sunColorScaled /= max(1e-5, dot(sunColorScaled, 0.7));
            // 缩放控制
            sunColor = averageColor(sunColor) * sunColorScaled;

            sunColor = lerp(albedo, sunColor, shadowAttenuation);
            sunColor = lerp(albedo, sunColor, edgeAttenuation);

            float3 rimDiffuse = pow(max(1e-5, pbrDiffuseColor), 0.2);
            rimDiffuse = normalize(rimDiffuse);

            //平均漫反射和边缘光
            float diffuseBrightness = averageColor(pbrDiffuseColor);
            diffuseBrightness = (1 - 0.2 * pow2(diffuseBrightness)) * 0.1;
            rimDiffuse *= diffuseBrightness;

            float3 rimSpecular = pbrSpecularColor;
            float3 rimColor = lerp(rimDiffuse, rimSpecular, metallic);

            rimColor *= 48;
            rimColor *= fresnelAttenuation * verticalAttenuation * viewAttenuation * lightAtteuation *
                distanceAttenuation * sunColor;

            float3 glowColor = select(materialId,
                    _RimGlowLightColor1,
                    _RimGlowLightColor2,
                    _RimGlowLightColor3,
                    _RimGlowLightColor4,
                    _RimGlowLightColor5
            );

            rimColor *= glowColor;

            float3 rimColorBrightness = averageColor(rimColor);
            rimColorBrightness = pow2(rimColorBrightness);
            rimColorBrightness = 1 + 0.5 * rimColorBrightness;
            rimColor *= rimColorBrightness;

            rimGlowColor = rimColor;

            //屏幕空间边缘光
            float screenSpaceRim = 0.0;
            #if _SCREEN_SAPCE_RIM
               {
                  float linearEyeDepth = input.positionCS.w;
                  float3 normalVS = TransformWorldToViewDir(normalWS);
                  float2 uvOffset = float2(normalize(normalVS.xy)) * _ScreenSpaceRimWidth / linearEyeDepth;
                  int2 texPos = input.positionCS.xy + uvOffset;
                  texPos = min(max(0,texPos),_ScaledScreenParams.xy -1);
                  float offsetSceneDepth = LoadSceneDepth(texPos);
                  float offsetSceneLinearEteDepth = LinearEyeDepth(offsetSceneDepth,_ZBufferParams);
                  screenSpaceRim = saturate((offsetSceneLinearEteDepth - (linearEyeDepth + _ScreenSpaceRimThreshold)) * 10 / _ScreenSpaceRimFadeout);
                  screenSpaceRim*= _ScreenSpaceRimBrightness;
               }
            #endif

            rimGlowColor = rimColor * screenSpaceRim;

            #if _Lazer_ON
            float faceRatio = max(0, dot(pixelNormalWS, lightDirectionWS));
            faceRatio = saturate(faceRatio) * 2 - 1;
            faceRatio = abs(saturate(faceRatio) * 2 - 1);
            faceRatio = 1 - abs(saturate(faceRatio) * 2 - 1);

            float3 reflectDir = reflect(-viewDirWS, normalWS);
            float4 _CubeMapColor = 1;
            _CubeMapColor.r = SAMPLE_TEXTURECUBE(
            _CubeMap, sampler_CubeMap, float3(reflectDir.x + faceRatio, reflectDir.y, reflectDir.z));
            _CubeMapColor.g = SAMPLE_TEXTURECUBE(_CubeMap, sampler_CubeMap,
                                                  float3(reflectDir.x, reflectDir.y, reflectDir.z));
            _CubeMapColor.b = SAMPLE_TEXTURECUBE(_CubeMap, sampler_CubeMap,
            float3(reflectDir.x, reflectDir.y + faceRatio, reflectDir.z));
            pbrSpecularColor = _CubeMapColor;
            #endif

            float3 ambientColor = SampleSH(pixelNormalWS) * gammaColor * _AmbientColorIntensity;
            float3 color = ambientColor;


            color += pbrDiffuseColor * albedo + pbrSpecularColor * specularColor * albedo;
            color += max(0, pbrSpecularColor * specularColor * albedo - 1);
            color += rimGlowColor;

            color = MixFog(color, input.positionWSAndFogFactor.w);


            return float4(color, baseAlpha);
        }
        ENDHLSL



        Pass
        {
            Name"Base Pass"


            Tags
            {
                "LightMode" = "UniversalForward"
            }

            BlendOp [_BlendOp]
            Blend[_BlendSrc][_BlendDst]
            ZWrite [_ZWrite]
            Cull [_Cull]

            Stencil
            {
                Ref[_StencilRef]
                Comp[_StencilComp]
                Pass[_StencilPassOp]
                Fail [_StencilFailOp]
                ZFail [_StencilZPassOp]
            }

            HLSLPROGRAM
            #pragma vertex MainVS
            #pragma fragment MainPS
            ENDHLSL
        }


        Pass
        {
            Name"Outline Pass"
            Tags
            {
                "LightMode"="UniversalForwardOnly"
            }
            Cull Front
            HLSLPROGRAM
            #pragma shader_feature_local _OUTLINE_PASS
            #pragma vertex vert
            #pragma fragment frag
            #include "./NiloOutlineUtil.hlsl"


            float3 OctouniVector(float2 oct)
            {
                float3 N = float3(oct, 1 - dot(1, abs(oct)));
                float t = max(-N.z, 0);
                N.x += N.x >= 0 ? (-t) : t;
                N.y += N.y >= 0 ? (-t) : t;
                return normalize(N);
            }

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float FogFactor : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            Varyings vert(Attributes IN)
            {
                #if !_OUTLINE_PASS
                return (Varyings)0;
                #endif
                VertexPositionInputs positionInputs = GetVertexPositionInputs(IN.positionOS.xyz);
                VertexNormalInputs NormalInputs = GetVertexNormalInputs(IN.normalOS, IN.tangentOS);

                float outlineWidth = _OutlineWidth;
                outlineWidth *= GetOutlineCameraFovAndDistanceFixMultiplier(positionInputs.positionVS.z);

                //法线外扩
                float3 positionWS = positionInputs.positionWS.xyz;
                //获取平滑法线
                float3 smoothNormal = OctouniVector(IN.texcoord1);
                float3x3 TBN = {
                    NormalInputs.tangentWS,
                    NormalInputs.bitangentWS,
                    NormalInputs.normalWS
                };
                smoothNormal = mul(smoothNormal, TBN);
                positionWS += smoothNormal * outlineWidth;

                Varyings OUT = (Varyings)0;
                OUT.positionCS = NiloGetNewClipPosWithZOffset(TransformWorldToHClip(positionWS), _OutlineZOffset);
                OUT.FogFactor = ComputeFogFactor(positionInputs.positionCS.z);
                OUT.uv = IN.texcoord0;
                return OUT;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                #if !_OUTLINE_PASS
                clip(-1);
                #endif

                float3 outlineColor = 0;

                #if _DOMAIN_FACE
            {
               outlineColor = _OutlineColor1.rgb;
            }
                #elif _DOMAIN_BODY
            {
               outlineColor = _OutlineColor2.rgb;
               float4 var_OtherDataTex1 = SAMPLE_TEXTURE2D(_OtherDataTex1,sampler_OtherDataTex1,IN.uv);
               int materialId = max(0,4 - floor(var_OtherDataTex1.r * 5));
               outlineColor = select(materialId,_OutlineColor1,_OutlineColor2,_OutlineColor3,_OutlineColor4,_OutlineColor5);
            }
                #endif

                outlineColor *= 0.2;

                float4 color = float4(outlineColor, 1);
                color.rgb = MixFog(color.rgb, IN.FogFactor);
                return color;
            }
            ENDHLSL
        }

        Pass
        {
            Name "EyeReDrawPass"
            Tags
            {
                "LightMode" = "SRPDefaultUnlit"
            }

            ZWrite [_ZWrite]
            Cull[_Cull]
            BlendOp [_SRPBlendOp]
            Blend [_SRPBlendSrc][_SRPBlendDst]
            Stencil
            {
                Ref[_SRPStencilRef]
                Comp[_SRPStencilComp]
                Pass[_SRPStencilPassOp]
                Fail [_SRPStencilFailOp]
                ZFail [_SRPStencilZPassOp]
            }

            HLSLPROGRAM
            #pragma vertex MainVS2
            #pragma fragment MainPS2
            #pragma multi_compile_fog

            #if _SRP_DEFAULT_PASS
         UniversalVaryings MainVS2(UniversalAttributes input){return MainVS(input);}
         float4 MainPS2(UniversalVaryings input , bool isFrontFace : SV_IsFrontFace) : SV_Target{return MainPS(input,isFrontFace);}
            #else
            void MainVS2()
            {
            }

            void MainPS2()
            {
            }
            #endif
            ENDHLSL
        }
        
        UsePass "Universal Render Pipeline/Lit/DEPTHNORMALS"
        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
    }
}