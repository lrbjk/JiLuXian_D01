Shader "Custom/MGCA/OBJ"
{
    Properties
    {
        //测试
        [Header(Test)]
        [Toggle(_Lazer_ON)]_Lazer_ON("_Lazer_ON",Float) = 0
        _CubeMap("Env",Cube) = "CUBE"{}

        //主贴图
        [Space(10)]
        [Hearder(Main Maps)]
        _Color("主颜色",Color) = (1,1,1,1)
        [NoScaleOffset]_MainTex("Albedo",2D) = "white"{}
        [NoScaleOffset]_Normal("Normal",2D) = "Bump"{}
        _BumpScale("法线缩放",Float) = 1
        [NoScaleOffset]_Metal("Metal",2D) = "Black"{}
        _MetalIntensity("Metallic",Range(0.1,20)) = 0
        [NoScaleOffset]_Roughness("Smoothness",2D) = "Black"{}
        _Glossiness("Smoothness",Range(0.1,20)) = 0
        [NoScaleOffset]_SpecularMask("SpecularMask",2D) = "white"{}
        [NoScaleOffset]_Emission("Emission",2D)="black"{}
        _EmissionIntensity("EmissionIntensity",Range(0.1,20)) = 1


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

        //depthShadow
        [Space(10)]
        [Header(ScreenSpaceRim)]
        [Toggle(_SCREEN_SPACE_SHADOW)]_ScreenSpaceShadow("Screen Space Shadow",Float) = 1
        _ScreenSpaceShadowWidth("Screen Space Shadow Width",Range(0,1)) = 0.2
        _ScreenSpaceShadowThreshold("Screen Space Shadow Threshold",Range(0,1)) = 0.015
        _ScreenSpaceShadowFadeout("Screen Space Shadow Fadeout",Range(0,10)) = 0.2

        //shadow
        [Space(10)]
        [Header(Shadow)]
        _AlbedoSmoothness("AlbedoSmoothness",Float) = 1.0
        _ShadowColor("ShadowColor",Color) = (1,1,1,1)
        [Header(Shadow_Fade)]
        _PostShadowFadeTint("PostShadowFadeTint",Color) = (1,1,1,1)
        _PostShadowTint("PostShadowTint",Color) = (1,1,1,1)
        _PostShallowFadeTint("PostShallowFadeTint",Color) = (1,1,1,1)
        _PostShallowTint("PostShallowTint",Color) = (1,1,1,1)
        _PostSSSTint("PostSSSTint",Color) = (1,1,1,1)
        _PostFrontTint("PostFrontTint",Color) = (1,1,1,1)

        //pbr
        [Space(10)]
        [Header(Addotional Specular)]
        [Toggle]_HightlightShape("HightlightShape",Float) = 0
        [HDR]_SpecularColor("SpecularColor",Color) = (1,1,1,1)
        _ShapeSoftness("ShapeSoftness",Range(0,1)) = 1
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

        //RimColor
        [Space(10)]
        [Header(RimColor)]
        [Toggle(_SCREEN_SAPCE_RIM)]_ScreenSpaceRim("ScreenSpace Rim",Float) = 1
        [HDR]_UISunColor("_UISunColor",Color) = (1,1,1,1)
        [HDR]_RimGlowLightColor1("RimGlowLightColor1",Color) = (1,1,1,1)
        _ScreenSpaceRimWidth("ScreenSpaceRimWidth",Range(0,5)) = 1
        _ScreenSpaceRimThreshold("ScreenSpaceRimThreshold",Range(0,1)) = 0.01
        _ScreenSpaceRimFadeout("ScreenSpaceRimFadeout",Range(0,10)) = 0.5
        _ScreenSpaceRimBrightness("ScreenSpaceRimBrightness",Range(0,10)) = 1

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
            float _ScreenSpaceShadow;
            float _ScreenSpaceShadowWidth;
            float _ScreenSpaceShadowThreshold;
            float _ScreenSpaceShadowFadeout;
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
            float _HightlightShape;
            float _ShapeSoftness;
            float _SpecularRange;
            float _Glossiness;
            float _ToonSpecular;
            float _ModelSize;
            float _SpecularIntensity;
            float3 _SpecularColor;

            //SH
            float _AmbientColorIntensity;

            //rim
            float _SkinMatID;
            float3 _UISunColor;
            float3 _RimGlowLightColor1;
            float ScreenSpaceRimWidth;
            float _ScreenSpaceRimThreshold;
            float _ScreenSpaceRimFadeout;
            float _ScreenSpaceRimBrightness;

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
        TextureCube _CubeMap;
        sampler sampler_CubeMap;
        Texture2D _Emission;
        sampler sampler_Emission;

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

        BRDFData G_InitialBRDFData(float3 BaseColor, float Smoothness, float Metallic, float Specular)
        {
            float OutAlpha = 1.0f;
            BRDFData G_BRDFData;
            InitializeBRDFData(BaseColor, Metallic, Specular, Smoothness, OutAlpha, G_BRDFData);
            return G_BRDFData;
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

        // 顶点着色器函数
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
            //PrerpareData
            float4 var_MainTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.texcoord);
            var_MainTex *= _Color;
            float3 baseCol = var_MainTex.rgb * _Color.xyz;
            float baseAlpha = 1.0;
            baseAlpha = var_MainTex.a;
            float3 normalWS = normalize(input.normalWS);
            float3 positionWS = input.positionWSAndFogFactor.xyz;
            float3 viewDirWS = normalize(input.viewDirWS);
            float4 shadowCoord = TransformWorldToShadowCoord(positionWS);
            Light mainLight = GetMainLight(shadowCoord);
            float3 lightColor = mainLight.color;
            float shadow = mainLight.shadowAttenuation;

            #ifdef _ADDITIONAL_LIGHTS
            #if USE_FORWARD_PLUS
            // 修正距离剔除
            InputData inputData = (InputData)0;
            inputData.positionWS = positionWS;
            inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);

            int additionalLightsCount = GetAdditionalLightsCount();
            LIGHT_LOOP_BEGIN(additionalLightsCount)
                {
                    Light AdditionalLight = GetAdditionalLight(lightIndex, positionWS);

                    // float Radiance = max(dot(normalWS, AdditionalLight.direction), 0);
                    // Radiance = (Radiance * 0.5f + 0.5f) * 2.356194f;
                    // Radiance = smoothstep(0.3 - 0.000488f, 0.3 + 0.001464f, Radiance);
                    // Radiance = saturate(Radiance + 0);
                    // Radiance *= AdditionalLight.distanceAttenuation * 0.1;

                    float3 lightColor1 = AdditionalLight.distanceAttenuation * AdditionalLight.color;
                    lightColor += lightColor1;
                    shadow = (shadow + AdditionalLight.distanceAttenuation);
                }
            LIGHT_LOOP_END
            // return float4(lightColor,1);
            #else
            uint lightCount = GetAdditionalLightsCount();
            for (uint lightIndex = 0; lightIndex < lightCount; lightIndex++)
            {
                Light AdditionalLight = GetAdditionalLight(lightIndex, positionWS, 1);
            
                float Radiance = max(dot(normalWS, AdditionalLight.direction), 0);
                Radiance = (Radiance * 0.5f + 0.5f) * 2.356194f;
                Radiance = smoothstep(0.3 - 0.000488f, 0.3 + 0.001464f, Radiance);
                Radiance = saturate(Radiance + 0);
                Radiance *= AdditionalLight.distanceAttenuation;
            
                float3 lightColor1 = Radiance * AdditionalLight.color;
                lightColor += lightColor1;
            }
            #endif
            #endif

            // return float4(lightColor, 1);

            float3 lightDirectionWS = normalize(mainLight.direction);
            float matcapMask = 0;
            float diffuseBais = 0;
            float metallic = 0;
            float specularMask = 0;
            float smoothness = 0.58;
            matcapMask = SAMPLE_TEXTURE2D(_matcapMask, sampler_matcapMask, input.texcoord);
            // metallic = _MetalIntensity * var_OtherDataTex1.g;
            metallic = SAMPLE_TEXTURE2D(_Metal, sampler_Metal, input.texcoord);
            metallic = pow(metallic, _MetalIntensity);
            // smoothness = _Glossiness * var_OtherDataTex2.g;
            smoothness = _Glossiness * SAMPLE_TEXTURE2D(_Roughness, sampler_Roughness, input.texcoord);
            // specularMask = var_OtherDataTex1.b;
            specularMask = SAMPLE_TEXTURE2D(_SpecularMask, sampler_SpecularMask, input.texcoord);

            //TBN
            float sign = input.tangentWS.w;
            float3 tangentWS = normalize(input.tangentWS.xyz);
            float3 bitangentWS = sign * cross(normalWS, tangentWS);
            float3 pixelNormalWS = normalWS;


            float4 var_Normal = SAMPLE_TEXTURE2D(_Normal, sampler_Normal, input.texcoord);
            var_Normal = var_Normal * 2.0 - 1.0;
            diffuseBais = specularMask * 2.0;
            float3 pixelNormalTS = float3(var_Normal.xy, 0.0);
            pixelNormalTS *= _BumpScale;
            pixelNormalTS.z = sqrt(1.0 - min(0.0, dot(pixelNormalTS.xy, pixelNormalTS.xy)));
            pixelNormalWS = TransformTangentToWorld(pixelNormalTS, float3x3(tangentWS, bitangentWS, normalWS));
            pixelNormalWS = normalize(pixelNormalWS);

            normalWS *= isFrontFace ? 1.0 : -1.0;
            pixelNormalWS *= isFrontFace ? 1.0 : -1.0;

            // return float4(pixelNormalWS,1);

            //MatCap
            float3 MatCapColor = var_MainTex;
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

            //Shadow
            //基础阴影衰减
            float baseAttenuation = 1.0;
            {
                float NoL = dot(pixelNormalWS, lightDirectionWS);
                baseAttenuation = NoL + diffuseBais;
                // return float4(baseAttenuation.xxx,1);
            }
            float shadowAttenuation = 1.0;

            //屏幕空间(深度)阴影
            #if _SCREEN_SPACE_SHADOW
            {
                float lineEyeDepth = input.positionCS.w;
                float perspective = 1.0 / lineEyeDepth;
                float offsetMul = _ScreenSpaceShadowWidth * 5.0 * perspective / 100.0;

                float3 lightDirectionVS = TransformWorldToViewDir(lightDirectionWS);
                float2 offset = lightDirectionVS.xy * offsetMul;
                int2 coord = input.positionCS.xy + offset * _ScaledScreenParams.xy;

                coord = min(max(0, coord), _ScaledScreenParams.xy - 1);
                float offsetSceneDepth = LoadSceneDepth(coord);
                float offsetSceneLinearEyeDepth = LinearEyeDepth(offsetSceneDepth, _ZBufferParams);

                float fadeout = max(1e-5, _ScreenSpaceShadowFadeout);
                shadowAttenuation = saturate(
                    (offsetSceneLinearEyeDepth - (lineEyeDepth - _ScreenSpaceShadowThreshold)) * 50 / fadeout);
            }
            #endif


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
            float zFade = saturate(input.positionCS.w * 0.43725);


            shadowColor = _ShadowColor;
            shadowColor = lerp(normalizeColorByAverageColor(shadowColor), shadowColor, zFade);

            shadowFadeColor = shadowColor * _PostShadowFadeTint;
            shadowColor = shadowColor * _PostShadowTint;

            ShallowFadeColor = shadowColor * _PostShallowFadeTint;
            ShallowColor = shadowColor * _PostShallowTint;

            SSSColor = _PostSSSTint;
            FrontColor = _PostFrontTint;

            ForwardColor = 1.0;
            float3 lightColorScaledByMax = ScaleColorByMax(lightColor);
            float3 albedo = (albedoForward * ForwardColor + albedoFront * FrontColor + albedoSSS * SSSColor) *
                lightColor; //亮面颜色

            albedo += (albedoShadowFade * shadowFadeColor + albedoShadow * shadowColor + albedoShallowFade *
                ShallowFadeColor + albedoShallow * ShallowColor) * lightColorScaledByMax; //暗面颜色


            // return float4(albedo,1);


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

            float3 pbrDiffuseColor = lerp(0.96 * gammaColor, 0, metallic);
            float3 pbrSpecularColor = lerp(0.04, gammaColor, metallic);
            // return float4(pbrDiffuseColor,1);
            float3 specularColor = 0;
            float shape = _HightlightShape;
            float range = _SpecularRange;
            float halfWS = normalize(viewDirWS + lightDirectionWS);
            float LoH = dot(lightDirectionWS, halfWS);
            float rangeLoH = saturate(range * LoH * 0.75 + 0.25);
            float rangeLoH2 = max(0.1, rangeLoH * rangeLoH);
            float NoL = dot(pixelNormalWS, lightDirectionWS);
            float rangeNoL = saturate(range * NoL * 0.75 + 0.25);
            float specular = 0;
            // Additional Highlight
            float perceptualRoughness = 1 - smoothness;
            float roughness = perceptualRoughness * perceptualRoughness;

            float normalizationTerm = roughness * 4 + 2;
            float roughness2 = roughness * roughness;

            float roughness2Minus1 = roughness2 - 1;
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
            specularColor = specular * tintColor;

            // return float4(specularColor, 1);


            float3 rimGlowColor = 0;
            //背光衰减
            float LoV = dot(lightDirectionWS, viewDirWS);
            float viewAttenuation = -LoV * 0.5 + 0.5;
            viewAttenuation = pow2(viewAttenuation);
            viewAttenuation = viewAttenuation * 0.5 + 0.5;

            //法线垂直方向衰减
            float verticalAttenuation = pixelNormalWS.y * 0.5 + 0.5;
            verticalAttenuation = pow2(viewAttenuation);
            verticalAttenuation = smoothstep(0, 1, verticalAttenuation);
            float lightAtteuation = saturate(dot(pixelNormalWS, lightDirectionWS)) * shadowAttenuation;

            //菲涅尔
            float cameraDistance = length(input.viewDirWS);
            float NoV = dot(pixelNormalWS, viewDirWS);
            float fresnelDistanceFade = (0.65) - 0.45 * min(1, cameraDistance / 12.0);
            float fresnelAttenuation = 1 - NoV - fresnelDistanceFade;
            float fresnelSoftness = 0.3;
            fresnelAttenuation = smoothstep(0, fresnelSoftness, fresnelAttenuation);
            float distanceAttenuation = 1 - 0.7 * saturate(cameraDistance * 0.2 - 1);
            float edgeAttenuation = 1 - pow4(pow5(viewAttenuation));
            float3 sunColor = _UISunColor;

            //缩放系数
            float sunColorScaled = pow2(pow4(sunColor));
            sunColorScaled /= max(1e-5, dot(sunColorScaled, 0.7));
            // 缩放控制
            sunColor = averageColor(sunColor) * sunColorScaled;
            sunColor = lerp(albedo, sunColor, shadowAttenuation);
            sunColor = lerp(albedo, sunColor, edgeAttenuation);

            // return float4(sunColor,1);


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
            float3 glowColor = _RimGlowLightColor1;
            rimColor *= glowColor;
            float rimColorBrightness = averageColor(rimColor);
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
            // return float4(rimGlowColor,1);

            // float3 ambientColor = SampleSH(pixelNormalWS) * gammaColor * _AmbientColorIntensity;
            float3 ambientColor = gammaColor * _AmbientColorIntensity;
            float3 color = ambientColor;
            color += pbrDiffuseColor * albedo + pbrSpecularColor * specularColor * albedo;
            color += max(0, pbrSpecularColor * specularColor * albedo - 1);
            color += rimGlowColor;
            color += SAMPLE_TEXTURE2D(_Emission, sampler_Emission, input.texcoord) * _EmissionIntensity;
            color = MixFog(color, input.positionWSAndFogFactor.w);
            // shadow = 
            color *= (shadow + 0.6);

            // color *= shadow;

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
                // #if !_OUTLINE_PASS
                // return (Varyings)0;
                // #endif
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
                // #if !_OUTLINE_PASS
                // clip(-1);
                // #endif

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