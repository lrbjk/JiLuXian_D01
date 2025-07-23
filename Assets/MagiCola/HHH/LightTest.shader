Shader "Unlit/LightTest"
{
    Properties
    {
        _AlbedoSmoothness("_AlbedoSmoothness",Float) = 0.5
    }
    SubShader
    {
        Tags
        {
            "LightMode"="UniversalForward" "RenderType"="Opaque" "Queue"="Geometry"
        }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 positionWS : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            float _AlbedoSmoothness;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.normal = TransformObjectToWorldNormal(v.normal);
                o.positionWS = TransformObjectToWorld(v.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float baseAttenuation = 1.0;
                {
                    float pixelNormalWS = i.normal;

                    float4 shadowCoord = TransformWorldToShadowCoord(i.positionWS);
                    Light light = GetMainLight(shadowCoord);
                    float3 lightDirectionWS = light.direction;
                    
                    
                    float NoL = dot(pixelNormalWS, lightDirectionWS);
                    baseAttenuation = NoL;
                }

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
                return float4(albedoShadowFade.xxx + albedoShadow.xxx + albedoShallowFade + albedoShallow + albedoSSS + albedoFront + albedoForward,1);
            }
            ENDHLSL
        }
    }
}