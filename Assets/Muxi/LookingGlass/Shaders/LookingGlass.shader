Shader "Custom/ScreenDisplay"
{
    Properties
    {
        // Main Textures
        [NoScaleOffset] _ScreenTexture("Screen Texture", 2D) = "white" {}
        [NoScaleOffset] _TransitionMap("Transition Map", 2D) = "white" {}
        
        // Animation & Effects
        _TransitionProgress("Transition Progress", Range(0, 1)) = 0
        [HDR] _TransitionEdgeColor("Transition Edge Color", Color) = (2, 1.686275, 0, 0)
        
        // Pixelation Settings
        _PixelationAmount("Pixelation Amount", Range(0, 1)) = 1
        _MaxPixelationLevel("Max Pixelation Level", Float) = 3
        
        // Color Adjustments
        _Exposure("Exposure", Float) = 1
        _ColorFilter("Color Filter", Color) = (1, 1, 1, 0)
        _MinimumBlackLevel("Minimum Black Level", Range(0, 1)) = 0
        
        // Screen Properties
        [Toggle(_ENABLE_BOX_PROJECTION)] _EnableBoxProjection("Enable Box Projection", Float) = 1
        _BaseColor("Base Color", Color) = (0, 0, 0, 0)
        _LEDPatternResolution("LED Pattern Resolution", Float) = 2000
        
        // LED Pattern Textures
       [NoScaleOffset] _LEDPatternTexture("LED Pattern Texture", 2D) = "white" {}
        [NonModifiableTextureData][NoScaleOffset] _TriplanarTexture("Triplanar Texture", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
            "UniversalMaterialType" = "Unlit"
            "Queue" = "Geometry"
            "DisableBatching" = "False"
            "ShaderGraphShader" = "true"
            "ShaderGraphTargetId" = "UniversalUnlitSubTarget"
        }
        
        Pass
        {
            Name "Universal Forward"
            
            // Render State
            Cull Back
            Blend One Zero
            ZTest LEqual
            ZWrite On
            
            HLSLPROGRAM
            
            // Pragmas
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_fog
            #pragma instancing_options renderinglayer
            #pragma vertex vert
            #pragma fragment frag
            
            // Keywords
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma shader_feature _ _SAMPLE_GI
            #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
            #pragma multi_compile_fragment _ DEBUG_DISPLAY
            #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
            #pragma shader_feature_local _ _ENABLE_BOX_PROJECTION
            #pragma multi_compile _ _FORWARD_PLUS
            
            // Defines
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD2
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD1
            #define VARYINGS_NEED_TEXCOORD2
            #define FEATURES_GRAPH_VERTEX
            #define SHADERPASS SHADERPASS_UNLIT
            #define _FOG_FRAGMENT 1
            
            // Includes
           
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
            
            // Structs
            struct Attributes
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float4 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float4 uv2 : TEXCOORD2;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD3;
                float3 normalWS : TEXCOORD4;
                float4 texCoord0 : TEXCOORD0;
                float4 texCoord1 : TEXCOORD1;
                float4 texCoord2 : TEXCOORD2;
                
            };
            
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
                float4 _LEDPatternTexture_TexelSize;
                float4 _TriplanarTexture_TexelSize;
                float4 _ScreenTexture_TexelSize;
                float4 _TransitionMap_TexelSize;
                float _TransitionProgress;
                float4 _TransitionEdgeColor;
                float _PixelationAmount;
                float _MaxPixelationLevel;
                float _Exposure;
                float4 _ColorFilter;
                float4 _BaseColor;
                float _MinimumBlackLevel;
                float _LEDPatternResolution;
            CBUFFER_END
            
            // Textures and Samplers
            SAMPLER(SamplerState_Linear_Repeat);
            TEXTURE2D(_LEDPatternTexture);
            SAMPLER(sampler_LEDPatternTexture);
            TEXTURE2D(_TriplanarTexture);
            SAMPLER(sampler_TriplanarTexture);
            TEXTURE2D(_ScreenTexture);
            SAMPLER(sampler_ScreenTexture);
            TEXTURE2D(_TransitionMap);
            SAMPLER(sampler_TransitionMap);
            float _TransitionAmount;
            
            // Graph Functions
            void Multiply_float4_float4(float4 A, float4 B, out float4 Out)
            {
                Out = A * B;
            }
            
            void Multiply_float_float(float A, float B, out float Out)
            {
                Out = A * B;
            }
            
            void Sine_float(float In, out float Out)
            {
                Out = sin(In);
            }
            
            void Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
            {
                Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
            }
            
            // Screen Scan Effect
            struct Bindings_ScreenScan
            {
                half4 uv0;
                float3 TimeParameters;
            };
            
            void ScreenScan(float Speed, float Frequency, float Gradient, float Width, Bindings_ScreenScan IN, out float Mask)
            {
                float timeOffset;
                Multiply_float_float(IN.TimeParameters.x, Speed, timeOffset);
                
                float4 uv = IN.uv0;
                float uvFrequency;
                Multiply_float_float(uv[0], Frequency, uvFrequency);
                
                float scanPosition = timeOffset + uvFrequency;
                float sineWave;
                Sine_float(scanPosition, sineWave);
                
                float absoluteSine = abs(sineWave);
                float2 widthRange = float2(0, Width);
                float remappedSine;
                Remap_float(absoluteSine, widthRange, float2(0, 1), remappedSine);
                
                Mask = saturate(remappedSine);
            }
            
            void Floor_float(float In, out float Out)
            {
                Out = floor(In);
            }
            
            void Multiply_float3_float3(float3 A, float3 B, out float3 Out)
            {
                Out = A * B;
            }
            
            void Floor_float3(float3 In, out float3 Out)
            {
                Out = floor(In);
            }
            
            void WorldToHClip_float(float3 WorldPos, out float4 Out)
            {
                Out = TransformWorldToHClip(WorldPos);
            }
            
            void Remap_float2(float2 In, float2 InMinMax, float2 OutMinMax, out float2 Out)
            {
                Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
            }
            
            // Pixelate Effect
            struct Bindings_Pixelate
            {
                float3 ObjectSpaceNormal;
                float3 ObjectSpacePosition;
            };
            
            void Pixelate(float Resolution, Bindings_Pixelate IN, out float2 PixelatedUV, out float4 Pixels)
            {
                float3 scaledPosition;
                Multiply_float3_float3((Resolution.xxx), IN.ObjectSpacePosition, scaledPosition);
                
                float3 flooredPosition;
                Floor_float3(scaledPosition, flooredPosition);
                
                float3 pixelPosition = flooredPosition / (Resolution.xxx);
                float3 worldPixelPos = TransformObjectToWorld(pixelPosition.xyz);
                
                float4 clipPos;
                WorldToHClip_float(worldPixelPos, clipPos);
                
                float4 ndcPos = clipPos / clipPos.w;
                float2 screenUV;
                Remap_float2(ndcPos.xy, float2(-1, 1), float2(0, 1), screenUV);
                
                PixelatedUV = float2(screenUV.x, 1.0 - screenUV.y);
                
                // Triplanar sampling
                float3 triplanarUV = scaledPosition;
                float3 triplanarBlend = SafePositivePow_float(IN.ObjectSpaceNormal, min(float(1), floor(log2(Min_float())/log2(1/sqrt(3)))));
                triplanarBlend /= dot(triplanarBlend, 1.0);
                
                float4 triplanarX = SAMPLE_TEXTURE2D(_TriplanarTexture, sampler_TriplanarTexture, triplanarUV.zy);
                float4 triplanarY = SAMPLE_TEXTURE2D(_TriplanarTexture, sampler_TriplanarTexture, triplanarUV.xz);
                float4 triplanarZ = SAMPLE_TEXTURE2D(_TriplanarTexture, sampler_TriplanarTexture, triplanarUV.xy);
                
                Pixels = triplanarX * triplanarBlend.x + triplanarY * triplanarBlend.y + triplanarZ * triplanarBlend.z;
            }
            
            void Remap_float4(float4 In, float2 InMinMax, float2 OutMinMax, out float4 Out)
            {
                Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
            }
            
            void Distance_float(float A, float B, out float Out)
            {
                Out = distance(A, B);
            }
            
            void Reciprocal_float(float In, out float Out)
            {
                Out = 1.0/In;}
            
            // Transition Effect
            struct Bindings_Transition
            {
            };
            
            void TransitionEffect(float TransitionSample, float EdgeFadeTime, float t, Bindings_Transition IN, out float Fade, out float Mask)
            {
                float edgeStart = 1.0 - EdgeFadeTime;
                float2 remapRange = float2(0.01, edgeStart);
                
                float remappedSample;
                Remap_float(TransitionSample, float2(0, 1), remapRange, remappedSample);
                
                float stepResult = step(remappedSample, t);
                float saturatedStep = saturate(stepResult);
                
                float distance;
                Distance_float(remappedSample, t, distance);
                
                float reciprocalFade;
                Reciprocal_float(EdgeFadeTime, reciprocalFade);
                
                float fadeDistance;
                Multiply_float_float(distance, reciprocalFade, fadeDistance);
                
                float subtractResult = saturatedStep - fadeDistance;
                Fade = saturate(subtractResult);
                Mask = saturatedStep;
            }
            
            void Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
            {
                Out = lerp(A, B, T);
            }
            
            void Distance_float3(float3 A, float3 B, out float Out)
            {
                Out = distance(A, B);
            }
            
            void URPReflections(float3 positionWS, float3 viewDirectionWS, float3 normalWS, float3 screenspaceUV, float Roughness, out float3 Reflection)
            {
                
                half3 reflectVector = reflect(-viewDirectionWS, normalWS);
                Reflection = GlossyEnvironmentReflection(reflectVector, positionWS, Roughness, 1.0h, screenspaceUV);
            }
            
            void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
            {
                Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
            }
            
            // Screen Lighting
            struct Bindings_ScreenLighting
            {
                float3 WorldSpaceNormal;
                float3 WorldSpaceViewDirection;
                float3 WorldSpacePosition;
                float2 NDCPosition;
                float2 PixelPosition;
                half4 uv1;
                half4 uv2;
            };
            
            void ScreenLighting(float4 Color, Bindings_ScreenLighting IN, out float3 Lighting)
            {
                float4 screenPos = float4(IN.NDCPosition.xy, 0, 0);
                
                float3 reflections;
                URPReflections(IN.WorldSpacePosition, IN.WorldSpaceViewDirection, IN.WorldSpaceNormal, screenPos.xyz, 0.05, reflections);
                
                float fresnel;
                Unity_FresnelEffect_float(IN.WorldSpaceNormal, IN.WorldSpaceViewDirection, 1.0, fresnel);
                
                float fresnelPower = pow(fresnel, 3.0);
                
                float remappedFresnel;
                Remap_float(fresnelPower, float2(0, 1), float2(0.02, 0.6), remappedFresnel);
                
                float3 reflectionContribution;
                Multiply_float3_float3(reflections, (remappedFresnel.xxx), reflectionContribution);
                
                float fresnelAdd = fresnel + 0.05;
                float saturatedFresnel = saturate(fresnelAdd);
                
                float3 bakedGI = SHADERGRAPH_BAKED_GI(IN.WorldSpacePosition, IN.WorldSpaceNormal, IN.PixelPosition.xy, IN.uv1.xy, IN.uv2.xy, true);
                
                float3 scaledGI;
                Multiply_float3_float3(bakedGI, float3(0.015, 0.015, 0.015), scaledGI);
                
                float3 giContribution;
                Multiply_float3_float3((saturatedFresnel.xxx), scaledGI, giContribution);
                
                float3 colorWithGI = giContribution + Color.xyz;
                Lighting = reflectionContribution + colorWithGI;
            }
            
            void Unity_Fog_float(out float4 Color, out float Density, float3 Position)
            {
                SHADERGRAPH_FOG(Position, Color, Density);
            }
            
            // Vertex Shader
            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
                
                output.positionCS = TransformObjectToHClip(input.positionOS);
                output.positionWS = TransformObjectToWorld(input.positionOS);
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.texCoord0 = input.uv0;
                output.texCoord1 = input.uv1;
                output.texCoord2 = input.uv2;
                
                return output;
            }
            
            // Fragment Shader
            half4 frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                
                // Calculate NDC position
                float2 ndcPosition = input.positionCS.xy / _ScreenParams.xy;
                ndcPosition.y = 1.0f - ndcPosition.y;
                
                // LED Pattern
                float4 ledUV = input.texCoord0;
                float4 ledScaledUV;
                Multiply_float4_float4((_LEDPatternResolution.xxxx), ledUV, ledScaledUV);
                
                float4 ledPattern = SAMPLE_TEXTURE2D(_LEDPatternTexture, sampler_LEDPatternTexture, ledScaledUV.xy);
                
                // Screen Scan Effect
                Bindings_ScreenScan screenScanBindings;
                screenScanBindings.uv0 = input.texCoord0;
                screenScanBindings.TimeParameters = _TimeParameters.xyz;
                
                float scanMask;
                ScreenScan(0.6, 0.7, ledUV[0], 0.4, screenScanBindings, scanMask);
                
                // Calculate pixelation level
                float2 pixelRange = float2(4.0, _MaxPixelationLevel);
                float remappedScan;
                Remap_float(scanMask, float2(0, 1), pixelRange, remappedScan);
                
                float pixelPower = pow(2.0, remappedScan);
                float pixelSize;
                Floor_float(pixelPower, pixelSize);
                
                // Pixelate effect
                Bindings_Pixelate pixelateBindings;
                pixelateBindings.ObjectSpaceNormal = TransformWorldToObjectNormal(input.normalWS);
                pixelateBindings.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
                
                float2 pixelatedUV;
                float4 pixelData;
                Pixelate(pixelSize, pixelateBindings, pixelatedUV, pixelData);
                // Blend between pixelated and normal UV
                float4 screenPosition = float4(ndcPosition.xy, 0, 0);
                float2 finalUV = lerp(pixelatedUV, screenPosition.xy, (_TransitionAmount.xx));
                
                // Sample screen texture
                float4 screenColor = SAMPLE_TEXTURE2D(_ScreenTexture, sampler_ScreenTexture, finalUV);
               
                // Apply exposure
                float4 exposedColor;
                Multiply_float4_float4(screenColor, (_Exposure.xxxx), exposedColor);
                
                // Apply color filter
                float4 filteredColor;
                Multiply_float4_float4(_ColorFilter, exposedColor, filteredColor);
                
                // Remap black levels
                float blackLevelOffset;
                Multiply_float_float(_MinimumBlackLevel, 0.01, blackLevelOffset);
                float2 blackLevelRange = float2(blackLevelOffset, 1.0);
                
                float4 remappedColor;
                Remap_float4(filteredColor, float2(0, 1), blackLevelRange, remappedColor);
                
                // Transition effect
                float4 transitionSample = SAMPLE_TEXTURE2D(_TransitionMap, sampler_TransitionMap, input.texCoord0.xy);
                
                Bindings_Transition transitionBindings;
                float transitionFade;
                float transitionMask;
                TransitionEffect(transitionSample.r, 0.1, _TransitionProgress, transitionBindings, transitionFade, transitionMask);
                
                // Blend with edge color
                float4 edgeBlend;
                Lerp_float4(remappedColor, IsGammaSpace() ? LinearToSRGB(_TransitionEdgeColor) : _TransitionEdgeColor, 
                                  (transitionFade.xxxx), edgeBlend);
                
                // Apply transition mask
                float4 maskedColor;
                Multiply_float4_float4(edgeBlend, (transitionMask.xxxx), maskedColor);
                 // return  maskedColor;

                // Apply LED pattern
                float4 ledModulated;
                Multiply_float4_float4(ledPattern, maskedColor, ledModulated);
                
                // Enhance LED effect
                float4 enhancedLED;
                Multiply_float4_float4(ledModulated, (5.0), enhancedLED);
               
                // Distance fade
                float distanceToCamera;
                Distance_float3(input.positionWS, _WorldSpaceCameraPos, distanceToCamera);
                
                float distanceRemap;
                Remap_float(distanceToCamera, float2(0, 3), float2(0, 1), distanceRemap);
                
                float distanceFade = saturate(distanceRemap);
                float4 distanceFaded = lerp(enhancedLED, maskedColor, (distanceFade.xxxx));
                return  distanceFaded;
                
                // Screen lighting 还有点问题 先pass吧
                Bindings_ScreenLighting lightingBindings;
                lightingBindings.WorldSpaceNormal = input.normalWS;
                lightingBindings.WorldSpaceViewDirection = GetWorldSpaceNormalizeViewDir(input.positionWS);
                lightingBindings.WorldSpacePosition = input.positionWS;
                lightingBindings.NDCPosition = ndcPosition;
                lightingBindings.PixelPosition = input.positionCS.xy;
                lightingBindings.uv1 = input.texCoord1;
                lightingBindings.uv2 = input.texCoord2;
                
                float3 screenLighting;
                ScreenLighting(_BaseColor, lightingBindings, screenLighting);
                
                // Combine with lighting
                float3 finalColor = distanceFaded.xyz + screenLighting;
                
                // Sample original screen for transition
                float4 originalScreen = SAMPLE_TEXTURE2D(_ScreenTexture, sampler_ScreenTexture, screenPosition.xy);
                
                // Final transition blend
                float3 transitionedColor = lerp(finalColor, originalScreen.xyz, (_TransitionAmount.xxx));
                
                // Apply fog
                float4 fogColor;
                float fogDensity;
                Unity_Fog_float(fogColor, fogDensity, TransformWorldToObject(input.positionWS));
                
                float3 foggedColor = lerp(transitionedColor, fogColor.xyz, (fogDensity.xxx));
                
                return half4(foggedColor, 1.0);
            }
            
            ENDHLSL
        }
    }
    
    FallBack "Hidden/Shader Graph/FallbackError"
}