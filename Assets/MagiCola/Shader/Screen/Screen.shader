Shader "Custom/Screen"
{
    Properties
    {
        _MainTex("主帖图",2D) = "white"{}
        [Foldout]
        _BaseSetting("基 础 设 置_Foldout",Float) = 1
        [Toggle(_YUVHANDLEEFFECT_ON)]_YUVHandleEffect("启 用 色 彩 处 理_Foldout",Float) = 0
        _Light("画 面 亮 度",Range(0,4)) = 0
        _Contrast("画 面 反 差",Range(0,4)) = 1
        _DarkFade("暗 部 褪 色",Range(0,128)) = 0
        _BrightFade("亮 部 褪 色",Range(0,128)) = 0
        [Foldout_Out]
        _BaseSetting_Out("离开基础设置_Foldout",Float) = 1


        [Foldout]
        _ColorSetting("颜 色 设 置_Foldout",Float) = 1
        _VividU("紫 鲜 艳 度",Range(0.1,4)) = 1.18
        _VividV("青 鲜 艳 度",Range(0.1,4)) = 0.93
        _ShiftU("紫 色 偏 移",Range(-200,200)) = 0
        _ShiftV("青 色 偏 移",Range(-200,200)) = 0
        _Level("Level",Range(1,255)) = 4
        _ShiftX("横 向 偏 移",Range(0,1000)) = 6
        _ShiftY("纵 向 偏 移",Range(0,1000)) = 4
        [Foldout_Out]
        _ColorSetting_Out("离开颜色设置_Foldout",Float) = 1


        [Foldout]
        _EffectSetting("效 果 设 置_Foldout",Float) = 1
        [Foldout(2,2,1,0)]
        _InterlacedEffect("启 用 隔 行 扫 描_Foldout",Float) = 1
        _Interlaced("隔 行 扫 描",Range(0,8)) = 1
        _InterlacedLine("隔 行 扫 描 周 期",Range(2,8)) = 4
        _InterlacedLight("隔 行 扫 描 亮 度 差",Range(0,1)) = 0.2
        [Foldout_Out(2)]
        _InterlacedEffect_Out("离开扫描设置_Foldout",Float) = 1
        [Foldout(2,2,1,0)]
        _TransposeXEffect("启 用 漂 移_Foldout",Float) = 1
        _TransposeX("横 向 漂 移",Range(0,10)) = 0.04
        _TransposePow("横 向 漂 移 抛 物 线",Range(1,8)) = 6.7
        _TransposeNoise("横 向 漂 移 杂 质",Range(0,2)) = 0.616
        [Foldout_Out]
        _EffectSetting_Out("离开效果设置_Foldout",Float) = 1


        [Foldout]
        _QualitySetting("质 量 设 置_Foldout",Float) = 1
        [Foldout(2,2,1,0)]
        _GRAPHNOISEEFFECT("启 用 质 量 设 置_Foldout",Float) = 1
        _LightNoise("信 号 噪 声",Range(0,500)) = 0
        _DrakNoise("胶 片 颗 粒",Range(0,500)) = 5
        [Foldout_Out]
        _QualitySetting_Out("离开质量设置_Foldout",Float) = 1

        [Foldout]
        _LEDSetting("LED 设 置_Foldout",Float) = 1
        [Foldout(2,2,1,0)]
        _LEDEffect("LED 启 用_Foldout",Float) = 1
        _LEDResolutionLevel("LED 分 辨 率 缩 减",Range(0,10)) = 1
        [Foldout_Out]
        _Quality_Out("离开LED设置_Foldout",Float) = 1


        [Foldout]
        _FishEyeSetting("鱼 眼 设 置_Foldout",Float) = 1
        [Foldout(2,2,1,0)]
        _FishEyeEffect("启 用 鱼 眼_Foldout",Float) = 1
        _FishEyeIntensity_X("鱼 眼 横 向 强 度",Range(0,1)) = 0.1
        _FishEyeIntensity_Y("鱼 眼 纵 向 强 度",Range(0,1)) = 0.1
        _FishEyePow("鱼 眼 曲 率",Range(0,10)) = 0.5
        [Foldout_Out]
        _FishEyeSetting_Out("离开鱼眼设置_Foldout",Float) = 1

        [Foldout]
        _WaterEffectSetting("水 印 设 置_Foldout",Float) = 1
        [Foldout(2,2,1,0)]
        _WaterMarkEffect("启 用 水 印_Foldout",Float) = 1
        _MarkTextureRect("水 印 偏 移/缩 放",Vector) = (0.5,0.21,1,1)
        _MarkTextureAlpha("水 印 透 明 度",Range(0,1)) = 1
        _MarkTexture("水 印 贴 图",2D) = "white"{}
        [Foldout_Out]
        _WaterEffectSetting_Out("离开水印设置_Foldout",Float) = 1

        [Foldout]
        _OthertSetting("其 他 设 置_Foldout",Float) = 1
        [Foldout(2,2,1,0)]
        _ConvoluteEffect("强 调 线 条_Foldout",Float) = 1
        [KeywordEnum(RightTilt,LeftTilt,Sauna,Emboss)]_KernelType("滤镜类型",Float) = 0
        [Foldout_Out(2)]
        _ConvoluteEffect_Out("离开强调线条_Foldout",Float) = 1
        [Toggle(_CUTOFFEFFECT_ON)]_GraphNoiseEffect("切 断 信 号",Float) = 0
        [Toggle(_INVERTLIGHTEFFECT_ON)]_InvertLightEffect("颠 倒 黑 白",Float) = 0
        [Foldout_Out]
        _OthertSetting_Out("离开其他设置_Foldout",Float) = 1


    }
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalRenderPipeline"
            "RenderType" = "Opaque"
        }

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_TexelSize;
            float4 _MainTex_ST;
            float4 _MarkTexture_TexelSize;
            float4 _MarkTextureRect;

            float _MarkTextureAlpha;
            float _ShiftX;
            float _ShiftY;
            float _Contrast;
            float _Light;
            float _DarkFade;
            float _BrightFade;
            float _VividU;
            float _VividV;
            float _ShiftU;
            float _ShiftV;
            float _Level;
            float _FishEyeIntensity_X;
            float _FishEyeIntensity_Y;
            float _FishEyePow;
            float _InterlacedLight;
            float _TransposeX;
            float _TransposePow;
            float _TransposeNoise;
            float _DrakNoise;
            float _LightNoise;

            int _KernelType;
            int _InterlacedLine;
            int _Interlaced;
            int _LEDResolutionLevel;
        CBUFFER_END

        inline float4x4 GetConvolutionKernel(int kernelType)
        {
            switch (kernelType)
            {
            case 0: return float4x4(
                    float4(0, -1, 0, 0),
                    float4(-1, 2, 2, 0),
                    float4(0, -1, 0, 0),
                    float4(0, 0, 0, 0)
                );;
            case 1: return float4x4(
                    float4(0, -1, 0, 0),
                    float4(3, 2, -2, 0),
                    float4(0, -1, 0, 0),
                    float4(0, 0, 0, 0)
                );;
            case 2: return float4x4(
                    float4(1.0f / 9, 1.0f / 9, 1.0f / 9, 0),
                    float4(1.0f / 9, 1.0f / 9, 1.0f / 9, 0),
                    float4(1.0f / 9, 1.0f / 9, 1.0f / 9, 0),
                    float4(0, 0, 0, 0)
                );;
            case 3: return float4x4(
                    float4(1, 1, 1, 0),
                    float4(1, 1, -1, 0),
                    float4(-1, -1, -1, 0),
                    float4(0, 0, 0, 0)
                );;
            default: return (float4x4)1;
            }
        }


        TEXTURE2D(_MainTex);
        sampler sampler_MainTex;
        TEXTURE2D(_MarkTexture);
        sampler sampler_MarkTexture;

        #include "./ScreenCommon.hlsl"
        ENDHLSL

        Pass
        {
            Name "MainPass"

            Tags
            {
                "LightMode" = "UniversalForward"
            }

            HLSLPROGRAM
            #pragma shader_feature_local _CONVOLUTEEFFECT_ON
            #pragma shader_feature_local _WATERMARKEFFECT_ON
            #pragma shader_feature_local _YUVHANDLEEFFECT_ON
            #pragma shader_feature_local _INVERTLIGHTEFFECT_ON
            #pragma shader_feature_local _FISHEYEEFFECT_ON
            #pragma shader_feature_local _INTERLACEDEFFECT_ON
            #pragma shader_feature_local _TRANSPOSEXEFFECT_ON
            #pragma shader_feature_local _GRAPHNOISEEFFECT_ON
            #pragma shader_feature_local _LEDEFFECT_ON
            #pragma shader_feature_local _CUTOFFEFFECT_ON

            #pragma vertex vert
            #pragma fragment frag
            ENDHLSL
        }

        UsePass "Universal Render Pipeline/Lit/DEPTHNORMALS"
        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
        UsePass "Universal Render Pipeline/Lit/GBUFFER"
        UsePass "Universal Render Pipeline/Lit/META"
    }
    CustomEditor "Scarecrow.SimpleShaderGUI"
}