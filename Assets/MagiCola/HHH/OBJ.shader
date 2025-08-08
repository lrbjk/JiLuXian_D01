Shader "Custom/MGCA/OBJ"
{
    Properties
    {
        //主贴图
        [Space(10)]
        [Hearder(Main Maps)]
        _Color("主颜色",Color) = (1,1,1,1)
        [NoScaleOffset]_MainTex("Albedo",2D) = "white"{}
        [NoScaleOffset]_Normal("Normal",2D) = "Bump"{}
        _BumpScale("法线缩放",Float) = 1
        [NoScaleOffset]_Metal("Metal",2D) = "Black"{}
        _MetalIntensity("Metallic",Range(0.1,20)) = 0
        [NoScaleOffset]_Roughness("_Roughness",2D) = "Black"{}
        _Glossiness("_RoughnessIntensity",Range(0.1,20)) = 0
        [NoScaleOffset]_SpecularMask("SpecularMask",2D) = "white"{}
        [NoScaleOffset]_Emission("Emission",2D)="black"{}
        _EmissionIntensity("EmissionIntensity",Range(0.1,20)) = 1
        _AO("AO",2D) = "white"{}


        //MatCap        
        [Space(10)]
        [Header(MatCap)]
        [Toggle(_MATCAP_ON)]_MatCap_ON("_MatCap_ON",Float) = 0
        [Toggle]_MatCapRefract1("MatCapRefract1",Float) = 0
        [NoScaleOffset]_MatCapTex1("_MatCapTex1",2D) = "white"{}
        _MatCapParam1("MatCapWrapOffset1",Vector) = (5,5,0,0)
        _MatCapDepth1("MatCapDepth1",Float) = 0.5
        _MatCapTintColor1("MatCapTintColor1",Color) = (1,1,1,1)
        _MatCapMask("MatCapMask",2D) = "white"{}
        [Enum(AlphaBlend,0,Add,1,OverLay,2)]_MatCapBlendMode1("MatCapBlendMode1",Float) = 0
        _MatCapColorBrust1("MatCapColorBrust1",Range(0,10)) = 1
        _MatCapAlphaBrust1("MatCapAlphaBrust1",Range(0,10)) = 1

        //shadow
        [Space(10)]
        [Header(Shadow)]
        _AlbedoSmoothness("AlbedoSmoothness",Range(0,0.5)) = 0.3
        _ShadowColor("ShadowColor",Color) = (1,1,1,1)
        [Header(Shadow_Fade)]
        _PostFrontTint("亮面过度",Color) = (1,1,1,1)
        _PostSSSTint("亮面颜色",Color) = (1,1,1,1)
        _PostShallowFadeTint("中间阴影过度",Color) = (1,1,1,1)
        _PostShallowTint("中间阴影",Color) = (1,1,1,1)
        _PostShadowFadeTint("深处阴影阴影过度",Color) = (1,1,1,1)
        _PostShadowTint("深处阴影",Color) = (1,1,1,1)



        //pbr
        [Space(10)]
        [Header(Specular)]
        [HDR]_SpecularColor("SpecularColor",Color) = (1,1,1,1)
        _SpecularRange("SpecularRange",Range(0,2)) = 1
        _ToonSpecular("ToonSpecular",Range(0,1)) = 0.01
        _ModelSize("ModelSize",Range(0,100)) = 1
        _SpecularIntensity("SpecularIntensity",Range(0,1)) = 0.1

        //Outline
        [Space(10)]
        [Header(Outline)]
        [Toggle(_OUTLINE_PASS)]_Outline("Outline描边",Float) = 0
        _OutlineWidth("OutlineWidth",Range(1,10)) = 1
        _OutlineColor("OutlineColor",Color)=(0,0,0,1)
        _OutlineZOffset("OutlineZOffset",Range(0,1)) = 0.01
        _NoseLineKDnDisp("NoseLineKDnDisp",Float) = 1
        _NoseLineHoriDisp("NoseLineHoriDisp",Float) = 1

        //SH 
        [Space(10)]
        [Header(Ambient)]
        _AmbientColorIntensity("AmbientColorIntensity",Range(0,1)) = 0.1

        //Option
        [Space(10)]
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
    }

    SubShader
    {

        Tags
        {
            "RenderPipeline" = "UniversalRenderPipeline"
            "RenderType" = "Opaque"
        }

        HLSLINCLUDE
        #pragma shader_feature_local _SCREEN_SPACE_SHADOW
        #pragma shader_feature_local _MATCAP_ON
        #pragma shader_feature_local _SCREEN_SAPCE_RIM
        #pragma shader_feature_local _SRP_DEFAULT_PASS
        #pragma shader_feature_local _Lazer_ON
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
        #include "./OBJ_LIGHT.hlsl"

        CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float4 _Color;
            float4 _ShadowColor;
            float3 _OutlineColor;
            float _BumpScale;
            float _AlbedoSmoothness;
            float _AlphaClip;
            float _ZWrite;
            float _Cull;
            float _ScreenSpaceRimWidth;
            float _OutlineWidth;
            float _OutlineZOffset;
            float _NoseLineKDnDisp;
            float _NoseLineHoriDisp;
            float _EmissionIntensity;

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
            float _MatCapColorBrust1;
            float _MatCapAlphaBrust1;
            float _MatCapRefract1;
            float _MatCapDepth1;
            int _MatCapBlendMode1;
            float4 _MatCapParam1;

            //pbr
            float _MetalIntensity;
            float _SpecularRange;
            float _Glossiness;
            float _ToonSpecular;
            float _ModelSize;
            float _SpecularIntensity;
            float3 _SpecularColor;

            //SH
            float _AmbientColorIntensity;

            //蒙版测试
            int _StencilRef;
        CBUFFER_END

        Texture2D _MainTex;
        sampler sampler_MainTex;
        Texture2D _FaceLightTex;
        sampler sampler_FaceLightTex;
        Texture2D _OtherDataTex1;
        sampler sampler_OtherDataTex1;
        Texture2D _OtherDataTex2;
        sampler sampler_OtherDataTex2;
        Texture2D _Normal;
        sampler sampler_Normal;
        Texture2D _SDFTex;
        sampler sampler_SDFTex;
        Texture2D _MatCapTex1;
        sampler sampler_MatCapTex1;
        Texture2D _Emission;
        sampler sampler_Emission;
        TEXTURE2D(_AO);
        sampler sampler_AO;

        TEXTURE2D(_Metal);
        SAMPLER(sampler_Metal);
        TEXTURE2D(_Roughness);
        SAMPLER(sampler_Roughness);
        TEXTURE2D(_SpecularMask);
        SAMPLER(sampler_SpecularMask);
        TEXTURE2D(_matcapMask);
        SAMPLER(sampler_matcapMask);

        struct UniversalAttributes
        {
            float4 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            float2 uv : TEXCOORD0;
            float2 staticLightmapUV : TEXCOORD1;
            float2 dynamicLightmapUV : TEXCOORD2;
        };

        struct UniversalVaryings
        {
            float4 positionCS : SV_POSITION;
            float4 positionWSAndFogFactor : TEXCOORD0;
            float3 normalWS : TEXCOORD1;
            float4 tangentWS : TEXCOORD2;
            float3 viewDirWS : TEXCOORD3;
            float2 texcoord : TEXCOORD4;

            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
            float4 shadowCoord              : TEXCOORD6;
            #endif
            DECLARE_LIGHTMAP_OR_SH(staticLightmapUV, vertexSH, 7);
            #ifdef DYNAMICLIGHTMAP_ON
            float2  dynamicLightmapUV : TEXCOORD8; // Dynamic lightmap UVs
            #endif
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

        BRDFData G_InitialBRDFData(float3 BaseColor, float Smoothness, float Metallic, float Specular)
        {
            float OutAlpha = 1.0f;
            BRDFData G_BRDFData;
            InitializeBRDFData(BaseColor, Metallic, Specular, Smoothness, OutAlpha, G_BRDFData);
            return G_BRDFData;
        }

        half3 LightingPhysicallyChanged(BRDFData brdfData,
                                        half3 lightColor, half3 lightDirectionWS, half lightAttenuation,
                                        half3 normalWS, half3 viewDirectionWS, bool specularHighlightsOff)
        {
            half NdotL = saturate(dot(normalWS, lightDirectionWS));
            half3 radiance = lightColor * (lightAttenuation * NdotL);

            half3 brdf = brdfData.diffuse;
            #ifndef _SPECULARHIGHLIGHTS_OFF
            [branch] if (!specularHighlightsOff)
            {
                brdf += brdfData.specular * DirectBRDFSpecular(brdfData, normalWS, lightDirectionWS, viewDirectionWS);
            }
            #endif // _SPECULARHIGHLIGHTS_OFF

            return radiance;
            return brdf * radiance;
        }


        half3 LightingPhysicallyChanged(BRDFData brdfData, Light light, half3 normalWS, half3 viewDirectionWS,
                                        bool specularHighlightsOff)
        {
            return LightingPhysicallyChanged(brdfData, light.color, light.direction,
                                             light.distanceAttenuation * light.
                                             shadowAttenuation, normalWS, viewDirectionWS,
                                             specularHighlightsOff);
        }

        half3 CalculateIrradianceFromReflectionProbes1(half3 reflectVector, float3 positionWS, half perceptualRoughness)
        {
            half probe0Volume = CalculateProbeVolumeSqrMagnitude(unity_SpecCube0_BoxMin, unity_SpecCube0_BoxMax);
            half probe1Volume = CalculateProbeVolumeSqrMagnitude(unity_SpecCube1_BoxMin, unity_SpecCube1_BoxMax);

            half volumeDiff = probe0Volume - probe1Volume;
            float importanceSign = unity_SpecCube1_BoxMin.w;

            // A probe is dominant if its importance is higher
            // Or have equal importance but smaller volume
            bool probe0Dominant = importanceSign > 0.0f || (importanceSign == 0.0f && volumeDiff < -0.0001h);
            bool probe1Dominant = importanceSign < 0.0f || (importanceSign == 0.0f && volumeDiff > 0.0001h);

            float desiredWeightProbe0 =
                CalculateProbeWeight(positionWS, unity_SpecCube0_BoxMin, unity_SpecCube0_BoxMax);
            float desiredWeightProbe1 =
                CalculateProbeWeight(positionWS, unity_SpecCube1_BoxMin, unity_SpecCube1_BoxMax);

            // Subject the probes weight if the other probe is dominant
            float weightProbe0 = probe1Dominant
                                     ? min(desiredWeightProbe0,
                                           1.0f - desiredWeightProbe1)
                                     : desiredWeightProbe0;
            float weightProbe1 = probe0Dominant
                                     ? min(desiredWeightProbe1, 1.0f - desiredWeightProbe0)
                                     : desiredWeightProbe1;

            float totalWeight = weightProbe0 + weightProbe1;

            // If either probe 0 or probe 1 is dominant the sum of weights is guaranteed to be 1.
            // If neither is dominant this is not guaranteed - only normalize weights if totalweight exceeds 1.
            weightProbe0 /= max(totalWeight, 1.0f);
            weightProbe1 /= max(totalWeight, 1.0f);

            half3 irradiance = half3(0.0h, 0.0h, 0.0h);
            half3 originalReflectVector = reflectVector;
            half mip = PerceptualRoughnessToMipmapLevel(perceptualRoughness);

            // Sample the first reflection probe
            if (weightProbe0 > 0.01f)
            {
                #ifdef _REFLECTION_PROBE_BOX_PROJECTION
        reflectVector = BoxProjectedCubemapDirection(originalReflectVector, positionWS, unity_SpecCube0_ProbePosition, unity_SpecCube0_BoxMin, unity_SpecCube0_BoxMax);
                #endif // _REFLECTION_PROBE_BOX_PROJECTION

                half4 encodedIrradiance = half4(
                    SAMPLE_TEXTURECUBE_LOD(unity_SpecCube0, samplerunity_SpecCube0, reflectVector, mip));

                #if defined(UNITY_USE_NATIVE_HDR)
        irradiance += weightProbe0 * encodedIrradiance.rbg;
                #else
                irradiance += weightProbe0 * DecodeHDREnvironment(encodedIrradiance, unity_SpecCube0_HDR);
                #endif // UNITY_USE_NATIVE_HDR
            }

            // Sample the second reflection probe
            if (weightProbe1 > 0.01f)
            {
                #ifdef _REFLECTION_PROBE_BOX_PROJECTION
        reflectVector = BoxProjectedCubemapDirection(originalReflectVector, positionWS, unity_SpecCube1_ProbePosition, unity_SpecCube1_BoxMin, unity_SpecCube1_BoxMax);
                #endif // _REFLECTION_PROBE_BOX_PROJECTION
                half4 encodedIrradiance = half4(
                    SAMPLE_TEXTURECUBE_LOD(unity_SpecCube1, samplerunity_SpecCube1, reflectVector, mip));

                #if defined(UNITY_USE_NATIVE_HDR) || defined(UNITY_DOTS_INSTANCING_ENABLED)
        irradiance += weightProbe1 * encodedIrradiance.rbg;
                #else
                irradiance += weightProbe1 * DecodeHDREnvironment(encodedIrradiance, unity_SpecCube1_HDR);
                #endif // UNITY_USE_NATIVE_HDR || UNITY_DOTS_INSTANCING_ENABLED
            }

            // Use any remaining weight to blend to environment reflection cube map
            if (totalWeight < 0.99f)
            {
                half4 encodedIrradiance = half4(SAMPLE_TEXTURECUBE_LOD(_GlossyEnvironmentCubeMap,
                    sampler_GlossyEnvironmentCubeMap,
                    originalReflectVector,
                    mip));

                #if defined(UNITY_USE_NATIVE_HDR) || defined(UNITY_DOTS_INSTANCING_ENABLED)
        irradiance += (1.0f - totalWeight) * encodedIrradiance.rbg;
                #else
                irradiance += (1.0f - totalWeight) * DecodeHDREnvironment(
                    encodedIrradiance, _GlossyEnvironmentCubeMap_HDR);
                #endif // UNITY_USE_NATIVE_HDR || UNITY_DOTS_INSTANCING_ENABLED
            }

            return irradiance;
        }


        float3 TriShadow(float baseAttenuation, float shadowAttenuation)
        {
            //级联阴影
            float albedoSmoothness = max(1e-5, _AlbedoSmoothness);
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
                //将阴影明暗分成六个部分，每0.5一段，1.5~-1
                float aRamp[6] = {
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


            float3 SSSColor = 1.0; //中间过渡部分较浅阴影向上偏移出的次表面部分
            float3 FrontColor = 1.0; //最亮区域，接近没有衰减的部分
            float3 ForwardColor = 1.0; //最强反射部分
            float3 shadowColor = float3(0, 0, 0);
            float3 shadowFadeColor = float3(0, 0, 0);
            float3 ShallowFadeColor = 1.0; //中间过渡部分较深阴影
            float3 ShallowColor = 1.0; //中间过渡部分较浅阴影
            // float zFade = saturate(positionCS.w * 0.43725);
            shadowColor = _ShadowColor;
            // shadowColor = lerp(normalizeColorByAverageColor(shadowColor), shadowColor, zFade);
            shadowFadeColor = shadowColor * _PostShadowFadeTint;
            shadowColor = shadowColor * _PostShadowTint;
            ShallowFadeColor = shadowColor * _PostShallowFadeTint;
            ShallowColor = shadowColor * _PostShallowTint;
            SSSColor = _PostSSSTint;
            FrontColor = _PostFrontTint;
            ForwardColor = 1.0;


            float3 albedo = (albedoForward * ForwardColor + albedoFront * FrontColor + albedoSSS * SSSColor); //亮面颜色
            albedo += (albedoShadowFade * shadowFadeColor + albedoShadow * shadowColor + (albedoShallowFade) *
                ShallowFadeColor + albedoShallow * ShallowColor); //暗面颜色


            return albedo;
        }

        half3 GlossyEnvironmentReflection1(half3 reflectVector, float3 positionWS, half perceptualRoughness,
                                                             half occlusion)
        {
            #if !defined(_ENVIRONMENTREFLECTIONS_OFF)
            half3 irradiance;

            #ifdef _REFLECTION_PROBE_BLENDING
            irradiance = CalculateIrradianceFromReflectionProbes1(reflectVector, positionWS, perceptualRoughness);
            #else
            #ifdef _REFLECTION_PROBE_BOX_PROJECTION
    reflectVector = BoxProjectedCubemapDirection(reflectVector, positionWS, unity_SpecCube0_ProbePosition, unity_SpecCube0_BoxMin, unity_SpecCube0_BoxMax);
            #endif // _REFLECTION_PROBE_BOX_PROJECTION
            half mip = PerceptualRoughnessToMipmapLevel(perceptualRoughness);
            half4 encodedIrradiance = half4(
                SAMPLE_TEXTURECUBE_LOD(unity_SpecCube0, samplerunity_SpecCube0, reflectVector, mip));

            #if defined(UNITY_USE_NATIVE_HDR)
    irradiance = encodedIrradiance.rgb;
            #else
            irradiance = DecodeHDREnvironment(encodedIrradiance, unity_SpecCube0_HDR);
            #endif // UNITY_USE_NATIVE_HDR
            #endif // _REFLECTION_PROBE_BLENDING
            return irradiance * occlusion;
            #else
    return _GlossyEnvironmentColor.rgb * occlusion;
            #endif // _ENVIRONMENTREFLECTIONS_OFF
        }

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

        #if defined(SHADOWS_SHADOWMASK) && defined(LIGHTMAP_ON)
        #define SAMPLE_SHADOWMASK(uv) SAMPLE_TEXTURE2D_LIGHTMAP(SHADOWMASK_NAME, SHADOWMASK_SAMPLER_NAME, uv SHADOWMASK_SAMPLE_EXTRA_ARGS);
        #elif !defined (LIGHTMAP_ON)
        #define SAMPLE_SHADOWMASK(uv) unity_ProbesOcclusion;
        #else
        #define SAMPLE_SHADOWMASK(uv) half4(1, 1, 1, 1);
        #endif

        // 顶点着色器函数
        UniversalVaryings MainVS(UniversalAttributes input)
        {
            UniversalVaryings output = (UniversalVaryings)0;
            //获取世界空间下法线和位置等信息
            VertexPositionInputs positionInputs = GetVertexPositionInputs(input.positionOS.xyz);
            VertexNormalInputs normalInputs = GetVertexNormalInputs(input.normalOS, input.tangentOS);

            output.positionCS = positionInputs.positionCS;
            output.positionWSAndFogFactor = float4(positionInputs.positionWS,
               ComputeFogFactor(positionInputs.positionCS.z));
            output.normalWS = normalInputs.normalWS;

            output.tangentWS.xyz = normalInputs.tangentWS;
            output.tangentWS.w = input.tangentOS.w * GetOddNegativeScale();
            output.viewDirWS = unity_OrthoParams.w == 0
                    ? GetCameraPositionWS() - positionInputs.
                    positionWS
                    : GetWorldToViewMatrix()[2].xyz;

            output.texcoord = input.uv;

            OUTPUT_LIGHTMAP_UV(input.staticLightmapUV, unity_LightmapST, output.staticLightmapUV);
            #ifdef DYNAMICLIGHTMAP_ON
                output.dynamicLightmapUV = input.dynamicLightmapUV.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
            #endif
                OUTPUT_SH(output.normalWS.xyz, output.vertexSH);

            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
                output.shadowCoord = GetShadowCoord(positionInputs);
            #endif

            return output;
        }

        // 片元着色器函数
        float4 MainPS(UniversalVaryings input, bool isFrontFace : SV_IsFrontFace):SV_TARGET
        {
            float3 normalWS = normalize(input.normalWS);
            float3 positionWS = input.positionWSAndFogFactor.xyz;
            float3 viewDirWS = normalize(input.viewDirWS);
            float4 shadowCoord = TransformWorldToShadowCoord(positionWS);
            float2 normalizedScreenSpaceUV = input.positionCS.xy * rcp(GetScaledScreenParams().xy);
            TransformNormalizedScreenUV(normalizedScreenSpaceUV);

            Light mainLight = GetMainLight(shadowCoord);
            float3 lightColor = mainLight.color;
            float3 lightDirectionWS = normalize(mainLight.direction);

            //MainTex 
            float4 var_MainTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.texcoord);
            var_MainTex *= _Color;
            float3 baseCol = var_MainTex.rgb * _Color.xyz;
            float baseAlpha = 1.0;
            baseAlpha = var_MainTex.a;

            float ao = SAMPLE_TEXTURE2D(_AO, sampler_AO, input.texcoord).r;
            float3 emission = SAMPLE_TEXTURE2D(_Emission, sampler_Emission, input.texcoord) * _EmissionIntensity;

            float matcapMask = 0;
            float metallic = 0;
            float specularMask = 0;
            float smoothness = 0.58;

            matcapMask = SAMPLE_TEXTURE2D(_matcapMask, sampler_matcapMask, input.texcoord);
            metallic = SAMPLE_TEXTURE2D(_Metal, sampler_Metal, input.texcoord).b;
            metallic = pow(metallic, _MetalIntensity);
            smoothness = _Glossiness * (1 - SAMPLE_TEXTURE2D(_Roughness, sampler_Roughness, input.texcoord).g);
            specularMask = SAMPLE_TEXTURE2D(_SpecularMask, sampler_SpecularMask, input.texcoord);
            //TBN
            float sign = input.tangentWS.w;
            float3 tangentWS = normalize(input.tangentWS.xyz);
            float3 bitangentWS = sign * normalize(cross(normalWS, tangentWS));
            float3 pixelNormalWS = normalWS;
            float4 var_Normal = SAMPLE_TEXTURE2D(_Normal, sampler_Normal, input.texcoord);
            var_Normal = var_Normal * 2.0 - 1.0;
            float diffuseBais = 0;
            diffuseBais = specularMask * 2.0;
            float3 pixelNormalTS = float3(var_Normal.xy, 0.0);
            pixelNormalTS *= _BumpScale;
            pixelNormalTS.z = sqrt(1.0 - min(0.0, dot(pixelNormalTS.xy, pixelNormalTS.xy)));
            pixelNormalWS = TransformTangentToWorld(pixelNormalTS, float3x3(tangentWS, bitangentWS, normalWS));
            pixelNormalWS = normalize(pixelNormalWS);
            normalWS *= isFrontFace ? 1.0 : -1.0;
            pixelNormalWS *= isFrontFace ? 1.0 : -1.0;

            pixelNormalWS = normalWS;


            // return float4(pixelNormalWS.xyz,1);

            // return float4(pixelNormalWS,1);
            float range = _SpecularRange;
            float specular = 0;
            half3 reflectVector = reflect(-viewDirWS, pixelNormalWS);
            half NoV = saturate(dot(normalWS, viewDirWS));
            half fresnelTerm = Pow4(1.0 - NoV);

            //Shadow  
            float3 albedo = 0;
            float shadowAttenuation = 1;
            float3 additionalLightsColor = 0;

            // //CreateAmbientOcclusionFactor
            half indirectAmbientOcclusion;
            half directAmbientOcclusion;
            #if defined(_SCREEN_SPACE_OCCLUSION) && !defined(_SURFACE_TYPE_TRANSPARENT)
            float2 uv = UnityStereoTransformScreenSpaceTex(normalizedScreenSpaceUV);
            float ssao = SampleAmbientOcclusion(normalizedScreenSpaceUV);
            indirectAmbientOcclusion = ssao;
            directAmbientOcclusion = lerp(half(1.0), ssao, _AmbientOcclusionParam.w);
            #else
            directAmbientOcclusion = 1;
            indirectAmbientOcclusion = 1;
            #endif


            // albedo = (albedo * 0.5 + 0.5) * baseCol;
            // return float4(albedo, 1);
            //---------------------------------------------------------------------------------------------------------

            //MatCap
            float3 MatCapColor = baseCol;
            #if _MATCAP_ON
            {
                float mask = matcapMask;
                float3 normalVS = TransformWorldToViewNormal(pixelNormalWS);
                float2 matcapUV = normalVS.xy * 0.5 + 0.5;
                float refract = _MatCapRefract1;
                if (refract > 0.5)
                {
                    float4 param = _MatCapParam1;
                    float depth = _MatCapDepth1;
                    matcapUV = matcapUV * depth + param.xy * input.texcoord + param.zw;
                    MatCapColor = SAMPLE_TEXTURE2D(_MatCapTex1, sampler_MatCapTex1, matcapUV).rgb;
                    float3 tintColor = _MatCapTintColor1;
                    float alphaBrust = _MatCapAlphaBrust1;
                    float colorBrust = _MatCapColorBrust1;
                    int blendMode = _MatCapBlendMode1;
                    if (blendMode == 0)
                    {
                        float alpha = saturate(alphaBrust * mask);
                        float3 blendColor = tintColor * MatCapColor * colorBrust;
                        MatCapColor = lerp(baseCol, blendColor, alpha);
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
                        float3 blendColor = saturate(
                            (MatCapColor * tintColor - 0.5) * colorBrust + MatCapColor * tintColor);
                        blendColor = lerp(0.5, blendColor, alpha);
                        MatCapColor = lerp(blendColor * baseCol * 2, 1 - 2 * (1 - baseCol) * (1 - blendColor),
                            baseCol >= 0.5);
                    }
                }
            }
            #endif
            // return float4(MatCapColor,1);
            //-------------------------------------------------------------------------------------------------------

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

                // return float4(gammaColor, 1);
            }
            //--------------------------------------------------------------------------------------------
            //PBR
            BRDFData brdfData;
            InitializeBRDFData(gammaColor, metallic, 0, smoothness, baseAlpha, brdfData);
            float3 pbrDiffuseColor = lerp(0.96 * gammaColor, 0, metallic);
            float3 pbrSpecularColor = lerp(0.04, gammaColor, metallic);

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


            // return float4(pbrSpecularColor,1);
            float3 specularColor = 0;
            // Additional Highlight
            float perceptualRoughness = 1 - smoothness;
            float roughness = perceptualRoughness * perceptualRoughness;
            float normalizationTerm = roughness * 4 + 2;
            float roughness2 = roughness * roughness;
            float roughness2Minus1 = roughness2 - 1;

            #if defined(_ADDITIONAL_LIGHTS)
            InputData inputData = (InputData)0;
            inputData.positionWS = positionWS;
            inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);
            uint pixelLightCount = GetAdditionalLightsCount();
                LIGHT_LOOP_BEGIN(pixelLightCount)
                Light light = GetAdditionalLight(lightIndex, positionWS, shadowMask);
                float AdditionalShadow = dot(pixelNormalWS, light.direction);
                // albedo += light.color * light.distanceAttenuation * shadowAttenuation;
                float3 UnityLight = light.shadowAttenuation * light.distanceAttenuation * light.color;
                albedo += TriShadow(AdditionalShadow, shadowAttenuation) * UnityLight;

            
                float NoL = dot(pixelNormalWS, light.direction);
                float rangeNoL = saturate(range * NoL * 0.75 + 0.25);
                float halfWS = normalize(viewDirWS + light.direction);
                float LoH = dot(lightDirectionWS, halfWS);
                float rangeLoH = saturate(range * LoH * 0.75 + 0.25);
                float rangeLoH2 = max(0.1, rangeLoH * rangeLoH);
                float NdotH = dot(pixelNormalWS, halfWS);
                float rangeNdotH = saturate(range * NdotH * 0.75 + 0.25);
                float d = rangeNdotH * rangeNdotH * roughness2Minus1 + 1.0;
                float ggx = roughness2 / ((d * d) * rangeLoH2 * normalizationTerm);
                specular = saturate((ggx - smoothness) * rangeNoL);
                specular = specular / max(1e-5, roughness);
                float toon = _ToonSpecular;
                float size = _ModelSize;
                specular *= toon * size * specularMask;
                specular *= 10;
                specular = saturate(specular);
                specular *= _SpecularIntensity;
                float3 tintColor = _SpecularColor;
                specularColor += specular * tintColor;
            
            LIGHT_LOOP_END
            #endif

            float mainShadow = dot(pixelNormalWS, mainLight.direction) *Luminance(mainLight.color);
            float3 mainLightColor = mainLight.shadowAttenuation * mainLight.distanceAttenuation * (mainLight.color);
            albedo += TriShadow(mainShadow, shadowAttenuation) * mainLightColor;

            float NoL = dot(pixelNormalWS, mainLight.direction);
            float rangeNoL = saturate(range * NoL * 0.75 + 0.25);
            float halfWS = normalize(viewDirWS + mainLight.direction);
            float LoH = dot(lightDirectionWS, halfWS);
            float rangeLoH = saturate(range * LoH * 0.75 + 0.25);
            float rangeLoH2 = max(0.1, rangeLoH * rangeLoH);
            float NdotH = dot(pixelNormalWS, halfWS);
            float rangeNdotH = saturate(range * NdotH * 0.75 + 0.25);
            float d = rangeNdotH * rangeNdotH * roughness2Minus1 + 1.0;
            float ggx = roughness2 / ((d * d) * rangeLoH2 * normalizationTerm);
            specular = saturate((ggx - smoothness) * rangeNoL);
            specular = specular / max(1e-5, roughness);
            float toon = _ToonSpecular;
            float size = _ModelSize;
            specular *= toon * size * specularMask;
            specular *= 10;
            specular = saturate(specular);
            specular *= _SpecularIntensity;
            float3 tintColor = _SpecularColor;
            specularColor += specular * tintColor;


            // return float4(specularColor, 1);


            // return float4(specular.xxx, 1);


            float3 rimDiffuse = pow(max(1e-5, pbrDiffuseColor), 0.2);
            rimDiffuse = normalize(rimDiffuse);


            // float3 ambientColor = gammaColor * _AmbientColorIntensity;
            float3 finalColor = 0;
            finalColor += pbrDiffuseColor * albedo + pbrSpecularColor * albedo * specularColor;
            finalColor += max(0, pbrSpecularColor * specularColor * albedo - 1);
            finalColor += emission;

            // return float4(finalColor, 1);
            //Unity Lit


            //LightMap
            half3 bakedGI = 0;
            #if defined(DYNAMICLIGHTMAP_ON)
            bakedGI = SAMPLE_GI(input.staticLightmapUV, input.dynamicLightmapUV, input.vertexSH, pixelNormalWS);
            #else
            bakedGI = SAMPLE_GI(input.staticLightmapUV, input.vertexSH, normalWS);
            #endif

            indirectAmbientOcclusion = min(indirectAmbientOcclusion, ao);
            lightColor *= directAmbientOcclusion;
            // return float4(indirectAmbientOcclusion.xxx,1);

            //MixRealtimeAndBakedGI
            #if defined(LIGHTMAP_ON) && defined(_MIXED_LIGHTING_SUBTRACTIVE)
                bakedGI = SubtractDirectMainLightFromLightmap(mainLight, normalWS, bakedGI);
            #endif
            half3 giColor = bakedGI;

            //GlobalIllumination
            half3 indirectDiffuse = bakedGI;
            half3 indirectSpecular =
                GlossyEnvironmentReflection1(reflectVector, positionWS, half(1 - smoothness), indirectAmbientOcclusion);
            half3 color = EnvironmentBRDF(brdfData, indirectDiffuse, indirectSpecular, fresnelTerm);
            if (IsOnlyAOLightingFeatureEnabled())
            {
                color = half3(1, 1, 1); // "Base white" for AO debug lighting mode
            }
            giColor = color * indirectAmbientOcclusion;

            finalColor += giColor;
            finalColor = MixFog(finalColor, input.positionWSAndFogFactor.w);

            return float4(finalColor, 1);
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
                float3 normal = NormalInputs.normalWS;

                positionWS += normal * outlineWidth;

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
                outlineColor = _OutlineColor.rgb;
                float4 color = float4(outlineColor, 1);
                color.rgb = MixFog(color.rgb, IN.FogFactor);
                return color;
            }
            ENDHLSL
        }
        UsePass "Universal Render Pipeline/Lit/DEPTHNORMALS"
        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
        UsePass "Universal Render Pipeline/Lit/GBUFFER"
        UsePass "Universal Render Pipeline/Lit/META"
    }
}