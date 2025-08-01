#ifndef OBJ_LIGHT
#define OBJ_LIGHT


#if defined(LIGHTMAP_ON)
#define DECLARE_LIGHTMAP_OR_SH(lmName, shName, index) float2 lmName : TEXCOORD##index
#define OUTPUT_LIGHTMAP_UV(lightmapUV, lightmapScaleOffset, OUT) OUT.xy = lightmapUV.xy * lightmapScaleOffset.xy + lightmapScaleOffset.zw;
#define OUTPUT_SH(normalWS, OUT)
#else
#define DECLARE_LIGHTMAP_OR_SH(lmName, shName, index) half3 shName : TEXCOORD##index
#define OUTPUT_LIGHTMAP_UV(lightmapUV, lightmapScaleOffset, OUT)
#define OUTPUT_SH(normalWS, OUT) OUT.xyz = SampleSHVertex(normalWS)
#endif

#endif
