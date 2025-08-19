#ifndef OBJ_LIGHT
#define OBJ_LIGHT

#include "./NiloOutlineUtil.hlsl"

#if defined(LIGHTMAP_ON)
#define DECLARE_LIGHTMAP_OR_SH(lmName, shName, index) float2 lmName : TEXCOORD##index
#define OUTPUT_LIGHTMAP_UV(lightmapUV, lightmapScaleOffset, OUT) OUT.xy = lightmapUV.xy * lightmapScaleOffset.xy + lightmapScaleOffset.zw;
#define OUTPUT_SH(normalWS, OUT)
#else
#define DECLARE_LIGHTMAP_OR_SH(lmName, shName, index) half3 shName : TEXCOORD##index
#define OUTPUT_LIGHTMAP_UV(lightmapUV, lightmapScaleOffset, OUT)
#define OUTPUT_SH(normalWS, OUT) OUT.xyz = SampleSHVertex(normalWS)
#endif

float NormalDistributionFunc_GGX(float NdotH, float roughness)
{
    //迪士尼原则中的 a
    float a = roughness * roughness;
    float a2 = a * a;
    float NdotH2 = NdotH * NdotH;
    float nom = a2;
    float denom = NdotH2 * (a2 - 1.0) + 1.0;
    denom = PI * denom * denom;
    return nom / max(denom, 0.001);
}

float GeometryFunc_SchlickGGX(float NdotV, float NdotL, float roughness)
{
    float a = roughness * roughness;
    float r = a + 1.0;
    float k = (r * r) / 8.0;
    float GV = NdotV / (NdotV * (1.0 - k) + k); //视线方向
    float GL = NdotL / (NdotL * (1.0 - k) + k); //光线方向
    return GV * GL;
}


float3 Fresnel_Schlick(float VdotH, float3 F0)
{
    return F0 + (1 - F0) * pow(1 - VdotH, 5);
}

float3 CalculatePBRSpecular(float3 viewDirWS, float3 lightDir, float3 normal, float smoothness, float3 F0)
{
    float3 halfWS = normalize(viewDirWS + lightDir);
    float NoH = max(dot(normal, halfWS), 0);
    float VoH = max(dot(viewDirWS, halfWS), 0);
    float NoL = max(0, dot(normal, lightDir));
    float NoV = max(dot(normal, viewDirWS), 0);
    float dTerm = NormalDistributionFunc_GGX(NoH, 1 - smoothness);
    float3 fTerm = Fresnel_Schlick(VoH, F0);
    float gTerm = GeometryFunc_SchlickGGX(NoV, NoL, 1 - smoothness);
    float3 directBRDFSpecFactor = dTerm * fTerm * gTerm / max((4.0 * NoV * NoL), 0.01);
    return directBRDFSpecFactor;
}

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


float3 OctouniVector(float2 oct)
{
    float3 N = float3(oct, 1 - dot(1, abs(oct)));
    float t = max(-N.z, 0);
    N.x += N.x >= 0 ? (-t) : t;
    N.y += N.y >= 0 ? (-t) : t;
    return normalize(N);
}

#endif
