Shader "Custom/MGCA/OBJ"
{
    Properties
    {
        [Foldout(1,1,1,1)]
        _2COLORCHANNEL("双色材质_Foldout",Float) = 1
        _BaseNormal("Base Normal", 2D) = "white" {}
        _DirtRoughness("Dirt Roughness", 2D) = "white" {}
        _DetailNormal("Detail Normal", 2D) = "white" {}
        _DetailMask("Detail Mask", 2D) = "white" {}
        _BaseColor("Base Color", Color) = (0.6544118,0.6544118,0.6544118,0)
        _BaseColorOverlay("Base Color Overlay", Color) = (0.6544118,0.6544118,0.6544118,0)
        _BaseDirtColor("Base Dirt Color", Color) = (0,0,0,0)
        _DetailColor("Detail Color", Color) = (0,0,0,0)
        _BaseNormalStrength("Base Normal Strength", Range( 0 , 1)) = 0
        _BaseSmoothness("Base Smoothness", Range( 0 , 1)) = 0.5
        _BaseDirtStrength("Base Dirt Strength", Range( 0.001 , 3)) = 0
        _BaseMetallic("Base Metallic", Range( 0 , 1)) = 0
        _DetailEdgeWear("Detail Edge Wear", Range( 0 , 1)) = 0
        _DetailEdgeSmoothness("Detail Edge Smoothness", Range( 0 , 1)) = 0
        _DetailDirtStrength("Detail Dirt Strength", Range( 0 , 1)) = 0
        [Foldout_Out]
        _2COLORCHANNEL_out("离开测试_Foldout",Float) = 1

        //主贴图
        [Space(10)]
        [Foldout]
        _MainMaps("贴图_Foldout",Float) = 1
        _Color("主颜色",Color) = (1,1,1,1)
        [NoScaleOffset]_MainTex("Albedo",2D) = "white"{}
        [NoScaleOffset]_BumpMap("Normal",2D) = "bump"{}
        _BumpScale("法线缩放",Float) = 1
        [NoScaleOffset]_MetallicGlossMap("Metal",2D) = "black"{}
        _MetalIntensity("Metallic",Range(0.1,20)) = 1

        [NoScaleOffset]_Roughness("_Roughness",2D) = "white"{}
        _Glossiness("_RoughnessIntensity",Range(0.1,20)) = 1
        [NoScaleOffset]_SpecularMask("SpecularMask",2D) = "white"{}
        [NoScaleOffset]_EmissionMap("Emission",2D)="black"{}
        _EmissionIntensity("EmissionIntensity",Range(0.1,20)) = 1
        _AO("AO",2D) = "white"{}

        //Factory Shader


        [Foldout(2,2,1,1)]
        _Height("使用高度图_Foldout",Float) = 1
        _HeightMap("高度图",2D) = "black"{}
        _HeightIntensity("深度",Range(0.005,0.08)) = 0.005
        [Foldout_Out(2)]
        _Height_out("离开高度图_Foldout",Float) = 1
        [Foldout_Out]
        _MainMaps_Out("贴图离开_Foldout",Float) = 1


        //MatCap        
        [Space(10)]
        [Foldout(1,1,1,0)]
        _MatCap("开启MatCap_Foldout",Float) = 0
        [Toggle]_MatCapRefract("MatCapRefract",Float) = 0
        [NoScaleOffset]_MatCapTex("_MatCapTex",2D) = "white"{}
        _MatCapParam("MatCapWrapOffset",Vector) = (0,0,0,0)
        _MatCapDepth("MatCapDepth",Float) = 0.5
        _MatCapTintColor("MatCapTintColor",Color) = (1,1,1,1)
        _MatCapMask("MatCapMask",2D) = "white"{}
        [Enum(AlphaBlend,0,Add,1,OverLay,2)]_MatCapBlendMode1("MatCapBlendMode",Float) = 0
        _MatCapColorBrust("MatCapColorBrust",Range(0,10)) = 1
        _MatCapAlphaBrust("MatCapAlphaBrust",Range(0,10)) = 1
        [Foldout_Out]
        _MatCap_Out("离开MatCap_Foldout",Float) = 0


        //shadow
        [Space(10)]
        [Foldout]
        _ShadowSetting("阴影_Foldout",Float) = 1
        _AlbedoSmoothness("AlbedoSmoothness",Range(0,0.5)) = 0.5
        _ShadowColor("ShadowColor",Color) = (0.2,0.2,0.2,1)
        [Header(Shadow_Fade)]
        _PostSSSTint("灰面颜色",Color) = (1,1,1,1)
        _PostFrontTint("灰面面过度",Color) = (1,1,1,1)
        _PostShallowTint("交界线阴影",Color) = (0,0,0,1)
        _PostShallowFadeTint("交界线阴影过度",Color) = (0,0,0,1)
        _PostShadowTint("反射阴影",Color) = (0,0,0,1)
        _PostShadowFadeTint("反射阴影阴影过度",Color) = (0,0,0,1)
        [Foldout_Out]
        _ShadowSetting_Out("离开阴影_Foldout",Float) = 1



        //pbr
        [Space(10)]
        [Foldout]
        _SpecularSetting("高光设置_Foldout",Float) = 1
        [HDR]_SpecularColor("SpecularColor",Color) = (1,1,1,1)
        _SpecularRange("SpecularRange",Range(0,2)) = 1
        _ToonSpecular("ToonSpecular",Range(0,1)) = 0.01
        _ModelSize("ModelSize",Range(0,100)) = 1
        _SpecularIntensity("SpecularIntensity",Range(0,1)) = 0.1
        [Foldout_Out]
        _SpecularSetting_Out("离开高光设置_Foldout",Float) = 1

        //Outline
        [Space(10)]
        [Foldout(1,1,1,0)]
        _OUTLINE_PASS("开启外描边_Foldout",Float) = 1
        [Toggle(_SMOOTHNORMAL_UV7)]_UV7("使用平滑法线:UV7",Float) = 0
        _OutlineWidth("OutlineWidth",Range(1,10)) = 1
        _OutlineColor("OutlineColor",Color)=(0,0,0,1)
        _OutlineZOffset("OutlineZOffset",Range(0,1)) = 0.01
        _NoseLineKDnDisp("NoseLineKDnDisp",Float) = 1
        _NoseLineHoriDisp("NoseLineHoriDisp",Float) = 1
        [Foldout_Out]
        _OUTLINE_PASS_Out("开启外描边_Foldout",Float) = 1

        //SH 
        [Space(10)]
        [Foldout]
        _SH("环境光_Foldout",Float) = 1
        _AmbientColorIntensity("AmbientColorIntensity",Range(0,1)) = 0.5
        [Foldout_Out]
        _SH_Out("离开环境光_Foldout",Float) = 1

        //Option
        [Space(10)]
        [Foldout]
        _Option("其他设置_Foldout",Float) = 1
        [Toggle(_AlphaClip_ON)] _AlphaClip("__clip", Float) = 0.0
        _Cutoff("AlphaClip",Float) = 0.5
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
        [Foldout_Out]
        _Option_Out("离开其他设置_Foldout",Float) = 1
    }

    SubShader
    {

        Tags
        {
            "RenderPipeline" = "UniversalRenderPipeline"
            "RenderType" = "Opaque"
        }

        HLSLINCLUDE
        #pragma multi_compile_instancing
        #pragma shader_feature_local _SCREEN_SPACE_SHADOW
        #pragma shader_feature_local _MATCAP_ON
        #pragma shader_feature_local _2COLORCHANNEL_ON
        #pragma shader_feature_local _AlphaClip_ON
        #pragma shader_feature_local _SCREEN_SAPCE_RIM
        #pragma shader_feature_local _SRP_DEFAULT_PASS
        #pragma shader_feature_local _HEIGHT_ON
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
        #pragma multi_compile _ _MAIN_LIGHT_CALCULATE_SHADOWS


        // 多光源和阴影
        #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
        #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS

        // forward+模式
        #pragma multi_compile _ _FORWARD_PLUS

        //软阴影
        #pragma multi_compile _ _SHADOWS_SOFT
        #pragma multi_compile_fog

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
        #include "./OBJ_Dependce.hlsl"

        CBUFFER_START(UnityPerMaterial)
            half4 _MainTex_ST;
            half4 _Color;
            half4 _ShadowColor;
            half3 _OutlineColor;
            half _BumpScale;
            half _AlbedoSmoothness;
            half _Cutoff;
            half _ZWrite;
            half _Cull;
            half _ScreenSpaceRimWidth;
            half _OutlineWidth;
            half _OutlineZOffset;
            half _NoseLineKDnDisp;
            half _NoseLineHoriDisp;
            half _EmissionIntensity;
            half _HeightIntensity;

            half4 _PostShadowFadeTint;
            half4 _PostShadowTint;
            half4 _PostShallowFadeTint;
            half4 _PostShallowTint;
            half4 _PostSSSTint;
            half4 _PostFrontTint;
            half3 _HeadCenter;
            half3 _HeadForward;
            half3 _HeadRight;
            half3 _MatCapTintColor;
            half _MatCapColorBrust;
            half _MatCapAlphaBrust;
            half _MatCapRefract;
            half _MatCapDepth;
            int _MatCapBlendMode1;
            half4 _MatCapParam;

            //pbr
            half _MetalIntensity;
            half _SpecularRange;
            half _Glossiness;
            half _ToonSpecular;
            half _ModelSize;
            half _SpecularIntensity;
            half3 _SpecularColor;

            //SH
            half _AmbientColorIntensity;

            //蒙版测试
            int _StencilRef;


            //内测
            half4 _BaseDirtColor;
            half4 _DirtRoughness_ST;
            half _BaseDirtStrength;
            half4 _BaseColor;
            half4 _BaseColorOverlay;
            half4 _DetailColor;
            half4 _DetailMask_ST;
            half _DetailDirtStrength;
            half _DetailEdgeWear;
            half4 _DetailNormal_ST;
            half4 _BaseNormal_ST;
            half _BaseNormalStrength;
            half _BaseMetallic;
            half _BaseSmoothness;
            half _DetailEdgeSmoothness;
        CBUFFER_END
        
        

        Texture2D _MainTex;
        sampler sampler_MainTex;
        Texture2D _FaceLightTex;
        sampler sampler_FaceLightTex;
        Texture2D _OtherDataTex1;
        sampler sampler_OtherDataTex1;
        Texture2D _OtherDataTex2;
        sampler sampler_OtherDataTex2;
        Texture2D _BumpMap;
        sampler sampler_BumpMap;
        Texture2D _HeightMap;
        sampler sampler_HeightMap;
        Texture2D _MatCapTex;
        sampler sampler_MatCapTex;
        Texture2D _EmissionMap;
        sampler sampler_EmissionMap;
        Texture2D _AO;
        sampler sampler_AO;

        Texture2D _MetallicGlossMap;
        sampler sampler_MetallicGlossMap;
        Texture2D _Roughness;
        sampler sampler_Roughness;
        Texture2D _SpecularMask;
        sampler sampler_SpecularMask;
        Texture2D _matcapMask;
        sampler sampler_matcapMask;

        //内测
        Texture2D _DirtRoughness;
        sampler sampler_DirtRoughness;
        Texture2D _DetailMask;
        sampler sampler_DetailMask;
        Texture2D _DetailNormal;
        sampler sampler_DetailNormal;
        Texture2D _BaseNormal;
        sampler sampler_BaseNormal;


        half3 TriShadow(half baseAttenuation, half shadowAttenuation)
        {
            //级联阴影
            half albedoSmoothness = max(1e-5, _AlbedoSmoothness);
            half albedoShadowFade = 1.0; //较深阴影
            half albedoShadow = 1.0; //较浅阴影
            half albedoShallowFade = 1.0; //中间过渡部分较深阴影
            half albedoShallow = 1.0; //中间过渡部分较浅阴影
            half albedoSSS = 1.0; //中间过渡部分较浅阴影向上偏移出的次表面部分
            half albedoFront = 1.0; //最亮区域，接近没有衰减的部分
            half albedoForward = 1.0; //最强反射部分
            {
                half Attenuation = baseAttenuation * 1.5; //-1.5~1.5
                //光滑系数调整
                half s0 = albedoSmoothness * 1.5; //0~1.5
                //锐利系数(粗糙度？）
                half s1 = 1.0 - s0; //-0.5~1
                //将阴影明暗分成六个部分，每0.5一段，1.5~-1
                half aRamp[6] = {
                    (Attenuation + 1.5) / s1 + 0.0, //aRamp[0]，最深
                    (Attenuation + 0.5) / s0 + 0.5, //aRamp[1],较深
                    (Attenuation + 0.0) / s1 + 0.5, //aRamp[2],中深
                    (Attenuation - 0.5) / s0 + 0.5, //aRamp[3],中浅
                    (Attenuation - 0.5) / s0 - 0.5, //aRamp[4]，较浅
                    (Attenuation - 2.0) / s1 + 1.5 //aRamp[5],最浅
                };
                albedoShadowFade = saturate(1 - aRamp[0]); //最深
                albedoShadow = saturate(min(1 - aRamp[1], aRamp[0])); //最深-较深
                albedoShallowFade = saturate(min(1 - aRamp[2], aRamp[1])); //较深-中深
                albedoShallow = saturate((min(1 - aRamp[3], aRamp[2]))); //中深-中浅
                albedoSSS = saturate((min(1 - aRamp[4], aRamp[3]))); //中浅-较浅
                albedoFront = saturate(min(1 - aRamp[5], aRamp[4])); //较浅-最浅
                albedoForward = saturate(aRamp[5]); //最浅
            }


            //叠加屏幕空间阴影
            half sRamp[2] = {
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


            half3 SSSColor = 1.0; //中间过渡部分较浅阴影向上偏移出的次表面部分
            half3 FrontColor = 1.0; //最亮区域，接近没有衰减的部分
            half3 ForwardColor = 1.0; //最强反射部分
            half3 shadowColor = half3(0, 0, 0);
            half3 shadowFadeColor = half3(0, 0, 0);
            half3 ShallowFadeColor = 1.0; //中间过渡部分较深阴影
            half3 ShallowColor = 1.0; //中间过渡部分较浅阴影
            // half zFade = saturate(positionCS.w * 0.43725);
            shadowColor = _ShadowColor;
            // shadowColor = lerp(normalizeColorByAverageColor(shadowColor), shadowColor, zFade);
            shadowFadeColor = shadowColor * _PostShadowFadeTint;
            shadowColor = shadowColor * _PostShadowTint;
            ShallowFadeColor = shadowColor * _PostShallowFadeTint;
            ShallowColor = shadowColor * _PostShallowTint;
            SSSColor = _PostSSSTint;
            FrontColor = _PostFrontTint;
            ForwardColor = 1.0;


            half3 albedo = (albedoForward * ForwardColor + albedoFront * FrontColor + albedoSSS * SSSColor); //亮面颜色
            albedo += (albedoShadowFade * shadowFadeColor + albedoShadow * shadowColor + (albedoShallowFade) *
                ShallowFadeColor + albedoShallow * ShallowColor); //暗面颜色


            return albedo;
        }


        struct UniversalAttributes
        {
            half4 positionOS : POSITION;
            half3 normalOS : NORMAL;
            half4 tangentOS : TANGENT;
            half2 uv : TEXCOORD0;
            half2 texcoord1 : TEXCOORD1;
            half4 texcoord3 : TEXCOORD3;
            half2 staticLightmapUV : TEXCOORD4;
            half2 dynamicLightmapUV : TEXCOORD5;
            UNITY_VERTEX_INPUT_INSTANCE_ID
        };

        struct UniversalVaryings
        {
            half4 positionCS : SV_POSITION;
            half4 positionWSAndFogFactor : TEXCOORD0;
            half3 normalWS : TEXCOORD1;
            half4 tangentWS : TEXCOORD2;
            half3 viewDirWS : TEXCOORD3;
            half2 texcoord : TEXCOORD4;
            half4 texcoord7 : TEXCOORD5;

            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
            half4 shadowCoord              : TEXCOORD6;
            #endif
            DECLARE_LIGHTMAP_OR_SH(staticLightmapUV, vertexSH, 7);
            #ifdef DYNAMICLIGHTMAP_ON
            half2  dynamicLightmapUV : TEXCOORD8; // Dynamic lightmap UVs
            #endif
            UNITY_VERTEX_INPUT_INSTANCE_ID
        };


        // 顶点着色器函数
        UniversalVaryings MainVS(UniversalAttributes input)
        {
            UniversalVaryings output = (UniversalVaryings)0;
            UNITY_SETUP_INSTANCE_ID(input);
            UNITY_TRANSFER_INSTANCE_ID(input, output);
            //获取世界空间下法线和位置等信息
            VertexPositionInputs positionInputs = GetVertexPositionInputs(input.positionOS.xyz);
            VertexNormalInputs normalInputs = GetVertexNormalInputs(input.normalOS, input.tangentOS);

            output.positionCS = positionInputs.positionCS;
            output.positionWSAndFogFactor = half4(positionInputs.positionWS,
                                                  ComputeFogFactor(positionInputs.positionCS.z));
            output.normalWS = normalInputs.normalWS;

            output.tangentWS.xyz = normalInputs.tangentWS;
            output.tangentWS.w = input.tangentOS.w * GetOddNegativeScale();
            output.viewDirWS = unity_OrthoParams.w == 0
             ? GetCameraPositionWS() - positionInputs.
             positionWS
             : GetWorldToViewMatrix()[2].xyz;

            output.texcoord = TRANSFORM_TEX(input.uv, _MainTex);


            OUTPUT_LIGHTMAP_UV(input.staticLightmapUV, unity_LightmapST, output.staticLightmapUV);
            #ifdef DYNAMICLIGHTMAP_ON
                output.dynamicLightmapUV = input.dynamicLightmapUV.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
            #endif
                OUTPUT_SH(output.normalWS.xyz, output.vertexSH);

            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
                output.shadowCoord = GetShadowCoord(positionInputs);
            #endif

            output.texcoord7.xy = input.texcoord3.xy;
            output.texcoord7.zw = input.uv.xy;

            return output;
        }

        // 片元着色器函数
        half4 MainPS(UniversalVaryings input, bool isFrontFace : SV_IsFrontFace):SV_TARGET
        {
            UNITY_SETUP_INSTANCE_ID(input)
            #if defined(_HEIGHT_ON)
            //beforeNormalize
            half3 unnormalizedNormalWS = input.normalWS;
            const half renormFactor = 1.0 / length(unnormalizedNormalWS);

            half crossSign = (input.tangentWS.w > 0.0 ? 1.0 : -1.0);
            half3 bitang = crossSign * cross(input.normalWS.xyz, input.tangentWS.xyz);

            half3 WorldSpaceNormal = renormFactor * input.normalWS.xyz;
            // we want a unit length Normal Vector node in shader graph

            // to preserve mikktspace compliance we use same scale renormFactor as was used on the normal.
            // This is explained in section 2.2 in "surface gradient based bump mapping framework"
            half3 WorldSpaceTangent = renormFactor * input.tangentWS.xyz;
            half3 WorldSpaceBiTangent = renormFactor * bitang;

            half3x3 tangentSpaceTransform = half3x3(WorldSpaceTangent, WorldSpaceBiTangent, WorldSpaceNormal);
            half3 viewDirTS = mul(tangentSpaceTransform, input.viewDirWS);

            half h = SAMPLE_TEXTURE2D(_HeightMap, sampler_HeightMap, input.texcoord).g;
            h = h * _HeightIntensity - _HeightIntensity / 2.0;
            half3 v = normalize(viewDirTS);
            v.z += 0.42;
            half2 Offset = h * (v.xy / v.z);
            input.texcoord += Offset;
            #endif


            half3 normalWS = normalize(input.normalWS);
            half3 positionWS = input.positionWSAndFogFactor.xyz;
            half3 viewDirWS = normalize(input.viewDirWS);
            viewDirWS = GetCameraPositionWS() - positionWS;
            viewDirWS = normalize(viewDirWS);
            
            half4 shadowCoord = TransformWorldToShadowCoord(positionWS);
            half2 normalizedScreenSpaceUV = input.positionCS.xy * rcp(GetScaledScreenParams().xy);
            TransformNormalizedScreenUV(normalizedScreenSpaceUV);

            Light mainLight = GetMainLight(shadowCoord);
            half3 lightColor = mainLight.color;
            half3 lightDirectionWS = normalize(mainLight.direction);

            //MainTex 
            half4 var_MainTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.texcoord);

            var_MainTex *= _Color;
            half3 baseCol = var_MainTex.rgb * _Color.xyz;
            half baseAlpha = 1.0;
            baseAlpha = var_MainTex.a * _Color.a;

            half ao = SAMPLE_TEXTURE2D(_AO, sampler_AO, input.texcoord).r;
            half3 emission = SAMPLE_TEXTURE2D(_EmissionMap, sampler_EmissionMap, input.texcoord) * _EmissionIntensity;

            half matcapMask = 0;
            half metallic = 0;
            half specularMask = 0;
            half smoothness = 0.58;

            matcapMask = SAMPLE_TEXTURE2D(_matcapMask, sampler_matcapMask, input.texcoord);
            metallic = SAMPLE_TEXTURE2D(_MetallicGlossMap, sampler_MetallicGlossMap, input.texcoord).b;
            metallic = pow(metallic, _MetalIntensity);
            // metallic = 1;
            smoothness =
                saturate(_Glossiness * (1 - SAMPLE_TEXTURE2D(_Roughness, sampler_Roughness, input.texcoord).g));
            // smoothness = 0;
            specularMask = SAMPLE_TEXTURE2D(_SpecularMask, sampler_SpecularMask, input.texcoord);
            //TBN
            half sign = input.tangentWS.w;
            half3 tangentWS = normalize(input.tangentWS.xyz);
            half3 bitangentWS = sign * (cross(normalWS, tangentWS));
            half3 pixelNormalWS = normalWS;
            half4 var_Normal = SAMPLE_TEXTURE2D(_BumpMap, sampler_BumpMap, input.texcoord);
            var_Normal = var_Normal * 2.0 - 1.0;
            half diffuseBais = 0;
            diffuseBais = specularMask * 2.0;
            half3 pixelNormalTS = half3(var_Normal.xy, 0.0);
            pixelNormalTS *= _BumpScale;
            pixelNormalTS.z = sqrt(1.0 - min(0.0, dot(pixelNormalTS.xy, pixelNormalTS.xy)));
            pixelNormalWS = TransformTangentToWorld(pixelNormalTS, half3x3(tangentWS, bitangentWS, normalWS));
            pixelNormalWS = normalize(pixelNormalWS);
            normalWS *= isFrontFace ? 1.0 : -1.0;
            pixelNormalWS *= isFrontFace ? 1.0 : -1.0;

            pixelNormalWS = normalWS;
            //Shadow  
            half3 albedo = 0;
            half shadowAttenuation = 1;

            //------------------------------------------------------------------------------------

            #if _2COLORCHANNEL_ON
            half2 Test_uv  = input.texcoord;
            half4 Test_DirtRoughness = SAMPLE_TEXTURE2D(_DirtRoughness,sampler_DirtRoughness,input.texcoord7.xy * _DirtRoughness_ST.xy + _DirtRoughness_ST.zw);
            half Test_DirtStrength =  clamp( pow( Test_DirtRoughness.g , _BaseDirtStrength ) , 0.0 , 1.0 );
            half4 Test_DirtColor =  lerp( _BaseDirtColor , half4( 1,1,1,0 ) , Test_DirtStrength);
            half4 Test_BaseColor =  lerp( _BaseColor , _BaseColorOverlay , Test_DirtRoughness.r);
            half4 Test_Detail = SAMPLE_TEXTURE2D(_DetailMask,sampler_DetailMask,input.texcoord7.zw  * _DetailMask_ST.xy + _DetailMask_ST.zw);
            half4 Test_DetailColor = lerp(Test_BaseColor,_DetailColor,ceil(((1.0 - Test_Detail.b) - 0.95)));
            half Test_DetailCeil = ceil( ( Test_Detail.b + -0.8 ) );
            Test_DetailColor = lerp( Test_DetailColor , half4( half3(1,0.95,0.9) , 0.0 ) , Test_DetailCeil);
            half Test_DetailAddIntensity = clamp( ( ( Test_Detail.r + -0.55 ) * 2.0 ) , 0.0 , 1.0 );
            half4 Test_FinalDetail = lerp(Test_DetailColor, clamp( ( Test_DetailAddIntensity + Test_DetailColor ) , half4( 0,0,0,0 ) , half4( 1,1,1,0 ) ),_DetailEdgeWear);

            
            half3 Test_DetailedNormal = UnpackNormalScale(SAMPLE_TEXTURE2D(_DetailNormal,sampler_DetailNormal,input.texcoord7.zw * _DetailNormal_ST.xy + _DetailNormal_ST.zw),1.0f);
            half3 Test_NormalMap = lerp(half3(0,0,1),UnpackNormalScale(SAMPLE_TEXTURE2D(_BaseNormal,sampler_BaseNormal,input.texcoord7.xy * _BaseNormal_ST.xy + _BaseNormal_ST.zw),1.0f),_BaseNormalStrength);

            half Test_Detailedge1 = lerp(0.0,Test_DetailAddIntensity,_DetailEdgeWear);
            half Test_Detailedge2 = clamp(Test_Detailedge1 + Test_DetailCeil,0.0,1.0);
            half Test_Metallic = clamp( ( Test_Detailedge2 + _BaseMetallic ) , 0.0 , 1.0 );

            half Test_Smoothness = lerp( ( ( max( Test_DetailAddIntensity , Test_DirtRoughness.a ) * Test_DirtColor ) * _BaseSmoothness ) , _DetailEdgeSmoothness , Test_Detailedge2);

            half3 Test_Albedo = Test_DirtColor * Test_FinalDetail;
            half3 Test_Normal = BlendNormal(Test_DetailedNormal,Test_NormalMap);
            

            baseCol.xyz = Test_Albedo;
            pixelNormalWS = normalize(TransformTangentToWorld(Test_Normal, half3x3(tangentWS, bitangentWS, normalWS)));
            smoothness = Test_Smoothness;
            metallic = Test_Metallic;
            #endif
            //---------------------------------------------------------------------------------------------------------


            half3 reflectVector = reflect(-viewDirWS, pixelNormalWS);
            half NoV = saturate(dot(pixelNormalWS, viewDirWS));
            half fresnelTerm = Pow4(1.0 - NoV);


            // //CreateAmbientOcclusionFactor
            half indirectAmbientOcclusion;
            half directAmbientOcclusion;
            #if defined(_SCREEN_SPACE_OCCLUSION) && !defined(_SURFACE_TYPE_TRANSPARENT)
            half2 uv = UnityStereoTransformScreenSpaceTex(normalizedScreenSpaceUV);
            half ssao = SampleAmbientOcclusion(normalizedScreenSpaceUV);
            indirectAmbientOcclusion = ssao;
            directAmbientOcclusion = lerp(half(1.0), ssao, _AmbientOcclusionParam.w);
            #else
            directAmbientOcclusion = 1;
            indirectAmbientOcclusion = 1;
            #endif
            indirectAmbientOcclusion = min(ao, indirectAmbientOcclusion);


            // albedo = (albedo * 0.5 + 0.5) * baseCol;
            // return half4(albedo, 1);

            //MatCap
            half3 MatCapColor = baseCol;
            #if _MATCAP_ON
            {
                half mask = matcapMask;
                half3 normalVS = TransformObjectToWorldNormal(pixelNormalWS);
                half2 matcapUV = normalVS.xy * 0.5 + 0.5;
                half refract = _MatCapRefract;
                if (refract > 0.5)
                {
                    half4 param = _MatCapParam;
                    half depth = _MatCapDepth;
                    matcapUV = matcapUV * depth + param.xy * input.texcoord + param.zw;
                    MatCapColor = SAMPLE_TEXTURE2D(_MatCapTex, sampler_MatCapTex, matcapUV).rgb;
                    half3 tintColor = _MatCapTintColor;
                    half alphaBrust = _MatCapAlphaBrust;
                    half colorBrust = _MatCapColorBrust;
                    int blendMode = _MatCapBlendMode1;
                    if (blendMode == 0)
                    {
                        half alpha = saturate(alphaBrust * mask);
                        half3 blendColor = tintColor * MatCapColor * colorBrust;
                        MatCapColor = lerp(baseCol, blendColor, alpha);
                    }
                    else if (blendMode == 1)
                    {
                        half alpha = saturate(alphaBrust * mask);
                        half3 blendColor = tintColor * MatCapColor * colorBrust;
                        MatCapColor = baseCol + blendColor * alpha;
                    }
                    else if (blendMode == 2)
                    {
                        half alpha = saturate(alphaBrust * mask);
                        half3 blendColor = saturate(
                            (MatCapColor * tintColor - 0.5) * colorBrust + MatCapColor * tintColor);
                        blendColor = lerp(0.5, blendColor, alpha);
                        MatCapColor = lerp(blendColor * baseCol * 2, 1 - 2 * (1 - baseCol) * (1 - blendColor),
          baseCol >= 0.5);
                    }
                }
            }
            #endif
            // return half4(MatCapColor,1);
            //-------------------------------------------------------------------------------------------------------

            half3 gammaColor = MatCapColor;
            {
                half pixelNdotL = dot(pixelNormalWS, lightDirectionWS);
                half NdotL = dot(normalWS, lightDirectionWS);
                half occlusion = saturate(1 - 3 * (NdotL - pixelNdotL)) * 2;
                occlusion *= sqrt(occlusion);
                occlusion = min(1, occlusion);

                half attenuation = lerp((pixelNdotL * 0.5 + 0.5) * occlusion, saturate(pixelNdotL), 0.5);
                half3 matCapColorClamped = ClampColorMax(MatCapColor);
                half luminance = Luminance(MatCapColor);
                half gamma = lerp(luminance * 0.2875 + 1.4375, 1, attenuation);
                half3 matCapColorGamma = pow(max(1e-5, matCapColorClamped), gamma);
                half3 matCapGammaHalf = lerp(MatCapColor, matCapColorGamma, 0.5);
                gammaColor = lerp(matCapGammaHalf, matCapColorGamma, saturate(NdotL));
            }
            // return float4(albedo,1);
            //--------------------------------------------------------------------------------------------
            //PBR
            BRDFData brdfData;
            InitializeBRDFData(baseCol, metallic, 0, smoothness, baseAlpha, brdfData);


            half3 F0 = (half3)0.04; //设定的非金属F0
            F0 = lerp(F0, baseCol, metallic);

            //LightMap
            half3 bakedGI = 0;
            #if defined(DYNAMICLIGHTMAP_ON)
            bakedGI = SAMPLE_GI(input.staticLightmapUV, input.dynamicLightmapUV, input.vertexSH, pixelNormalWS);
            #else
            bakedGI = SAMPLE_GI(input.staticLightmapUV, input.vertexSH, normalWS);
            #endif


            //CalculateShadowMask
            half4 shadowMask = 0;
            shadowMask = SAMPLE_SHADOWMASK(input.staticLightmapUV);
            #if defined(SHADOWS_SHADOWMASK) && defined(LIGHTMAP_ON)
            shadowMask = shadowMask;
            #elif !defined (LIGHTMAP_ON)
            shadowMask = unity_ProbesOcclusion;
            #else
            shadowMask = half4(1, 1, 1, 1);
            #endif


            // return half4(pbrSpecularColor,1);
            half3 specularColor = 0;
            // Additional Highlight

            #if defined(_ADDITIONAL_LIGHTS)
            InputData inputData = (InputData)0;
            inputData.positionWS = positionWS;
            inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);
            uint pixelLightCount = GetAdditionalLightsCount();
                LIGHT_LOOP_BEGIN(pixelLightCount)
                Light light = GetAdditionalLight(lightIndex, positionWS, shadowMask);
                half AdditionalShadow = saturate(dot(pixelNormalWS, light.direction));
                // albedo += light.color * light.distanceAttenuation * shadowAttenuation;
                half3 UnityLight = light.color * light.shadowAttenuation * light
                    .distanceAttenuation;
                albedo += TriShadow(AdditionalShadow, shadowAttenuation) * UnityLight;
                specularColor += CalculatePBRSpecular(viewDirWS, light.direction, pixelNormalWS, smoothness, F0) * light
                    .shadowAttenuation * light.color * light.distanceAttenuation;
            LIGHT_LOOP_END
            #endif

            half shadowStrength = GetMainLightShadowStrength();
            half contributionTerm = saturate(dot(mainLight.direction, normalWS));
            half3 lambert = mainLight.color * contributionTerm;
            half3 estimatedLightContributionMaskedByInverseOfShadow = lambert * (1.0 - mainLight.shadowAttenuation);
            half3 subtractedLightmap = bakedGI - estimatedLightContributionMaskedByInverseOfShadow;
            half3 realtimeShadow = max(subtractedLightmap, _SubtractiveShadowColor.xyz);
            realtimeShadow = lerp(bakedGI, realtimeShadow, shadowStrength);
            bakedGI = min(bakedGI, realtimeShadow);


            half mainShadow = dot(pixelNormalWS, mainLight.direction);
            half3 mainLightColor = (mainLight.color) * mainLight.shadowAttenuation * mainLight.
                distanceAttenuation;
            albedo += TriShadow(mainShadow, shadowAttenuation) * mainLightColor;

            specularColor += CalculatePBRSpecular(viewDirWS, mainLight.direction, pixelNormalWS, smoothness, F0) *
                mainLight.shadowAttenuation * mainLight.color;

            half3 halfDir = normalize(viewDirWS + lightDirectionWS);
            half VoH = dot(viewDirWS, halfDir);
            half3 fTerm = Fresnel_Schlick(VoH, F0);
            half3 Ks = fTerm;
            half3 Kd = (1 - Ks) * (1 - metallic);
            half3 diffuseColor = Kd * ao;

            half3 finalColor = 0;
            finalColor += (diffuseColor + specularColor) * albedo * gammaColor;
            finalColor += emission;
            

            //Unity Lit

            half3 indirectDiffuse = bakedGI;
            half3 indirectSpecular = GlossyEnvironmentReflection1(reflectVector, positionWS,
   brdfData.perceptualRoughness, 1.0h);

            half3 color = EnvironmentBRDF(brdfData, indirectDiffuse, indirectSpecular, fresnelTerm);

            if (IsOnlyAOLightingFeatureEnabled())
            {
                color = half3(1, 1, 1); // "Base white" for AO debug lighting mode
            }

            half3 giColor = color * indirectAmbientOcclusion;


            finalColor += giColor * _AmbientColorIntensity;
            finalColor = MixFog(finalColor, input.positionWSAndFogFactor.w);

            #ifdef _AlphaClip_ON
            clip(baseAlpha - _Cutoff);
            #endif


            return half4(finalColor, baseAlpha);
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
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
            #pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
            #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
            #pragma multi_compile_fragment _ _LIGHT_LAYERS
            #pragma multi_compile_fragment _ _LIGHT_COOKIES
            #pragma multi_compile _ _CLUSTERED_RENDERING

            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SHADOWMASK
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma multi_compile_fragment _ DEBUG_DISPLAY
            #pragma multi_compile_instancing


            #pragma vertex MainVS
            #pragma fragment MainPS
            ENDHLSL
        }


//        Pass
//        {
//            Name"Outline Pass"
//            Tags
//            {
//                "LightMode"="SRPDefaultUnlit"
//            }
//            Cull Front
//            HLSLPROGRAM
//            #pragma shader_feature_local _OUTLINE_PASS_ON
//            #pragma shader_feature_local _SMOOTHNORMAL_UV7
//            #pragma vertex vert
//            #pragma fragment frag
//        
//            struct Attributes
//            {
//                half4 positionOS : POSITION;
//                half3 normalOS : NORMAL;
//                half4 tangentOS : TANGENT;
//                half2 texcoord0 : TEXCOORD0;
//                half2 texcoord1 : TEXCOORD1;
//                half4 uv7 : TEXCOORD7;
//            };
//        
//            struct Varyings
//            {
//                half4 positionCS : SV_POSITION;
//                half FogFactor : TEXCOORD0;
//                half2 uv : TEXCOORD1;
//            };
//        
//            Varyings vert(Attributes IN)
//            {
//                #if !_OUTLINE_PASS_ON
//                return (Varyings)0;
//                #endif
//                VertexPositionInputs positionInputs = GetVertexPositionInputs(IN.positionOS.xyz);
//                VertexNormalInputs NormalInputs = GetVertexNormalInputs(IN.normalOS, IN.tangentOS);
//        
//                half outlineWidth = _OutlineWidth;
//                outlineWidth *= GetOutlineCameraFovAndDistanceFixMultiplier(positionInputs.positionVS.z);
//        
//                //法线外扩
//                half3 positionWS = positionInputs.positionWS.xyz;
//                half3 normal = NormalInputs.normalWS;
//                #if _SMOOTHNORMAL_UV7
//                half3x3 tbn = half3x3(NormalInputs.tangentWS,NormalInputs.bitangentWS,NormalInputs.normalWS);
//                normal = mul(IN.uv7.rgb, tbn);
//                #endif
//        
//                positionWS += normal * outlineWidth;
//        
//                Varyings OUT = (Varyings)0;
//                OUT.positionCS = NiloGetNewClipPosWithZOffset(TransformWorldToHClip(positionWS), _OutlineZOffset);
//                OUT.FogFactor = ComputeFogFactor(positionInputs.positionCS.z);
//                OUT.uv = IN.texcoord0;
//                return OUT;
//            }
//        
//            half4 frag(Varyings IN) : SV_Target
//            {
//                #if !_OUTLINE_PASS_ON
//                clip(-1);
//                #endif
//        
//                half3 outlineColor = 0;
//                outlineColor = _OutlineColor.rgb;
//                half alpha = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,IN.uv).a * _Color.a;
//                half4 color = half4(outlineColor, alpha);
//                clip(alpha - _Cutoff);
//                color.rgb = MixFog(color.rgb, IN.FogFactor);
//                return color;
//            }
//            ENDHLSL
//        }
//        UsePass "Universal Render Pipeline/Lit/DEPTHONLY"
//        UsePass "Universal Render Pipeline/Lit/DEPTHNORMALS"
//        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
//        UsePass "Universal Render Pipeline/Lit/GBUFFER"
//        UsePass "Universal Render Pipeline/Lit/META"
    }
    CustomEditor "Scarecrow.SimpleShaderGUI"
}