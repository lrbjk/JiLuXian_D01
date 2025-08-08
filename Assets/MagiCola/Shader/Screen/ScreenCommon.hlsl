#ifndef _ScreenCommon
#define _ScreenCommon

float ScaledGaussianRandom(float3 seed, float mean, float standardDeviation)
{
    seed = frac(seed * float3(12.9898, 78.233, 37.719));
    float u1 = frac(sin(dot(seed, float3(1.0, 57.0, 113.0))) * 43758.5453);
    float u2 = frac(sin(dot(seed, float3(31.0, 97.0, 23.0))) * 43758.5453);

    float r = sqrt(-2.0 * log(u1));
    float theta = 6.28318530718 * u2;
    float gaussianRandom = r * cos(theta);


    gaussianRandom = gaussianRandom * standardDeviation + mean;

    return saturate(gaussianRandom);
}

float4 Vec2ToVec4(float2 a, float2 b)
{
    return float4(a.x, a.y, b.x, b.y);
}

float LevelLow(float origin, float _Level)
{
    return round(origin / _Level) * _Level;
}

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float4 posCS : SV_POSITION;
    float2 uv : TEXCOORD0;
    float4 posSS :TEXCOORD1;
};



float3 Convolute(float2 uv, float2 _TexDeltaSize, TEXTURE2D (_MainTex), sampler sampler_MainTex,
                 float4x4 _ConvoluteCore)
{
    half3 result = 0;
    half2 offsets[9] =
    {
        float2(-_TexDeltaSize.x, -_TexDeltaSize.y),
        float2(0.0, -_TexDeltaSize.y),
        float2(_TexDeltaSize.x, -_TexDeltaSize.y),
        float2(-_TexDeltaSize.x, 0.0),
        float2(0.0, 0.0),
        float2(_TexDeltaSize.x, 0.0),
        float2(-_TexDeltaSize.x, _TexDeltaSize.y),
        float2(0.0, _TexDeltaSize.y),
        float2(_TexDeltaSize.x, _TexDeltaSize.y)
    };

    int index = 0;
    for (int y = 0; y < 3; y++)
    {
        for (int x = 0; x < 3; x++)
        {
            float2 sampleUV = uv + offsets[index];
            float3 sampleColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, sampleUV).rgb;
            result += sampleColor * _ConvoluteCore[y][x];
            index++;
        }
    }
    return result;
}

float3 WaterMark(float3 Color, float2 uv, float4 _MarkTextureRect, TEXTURE2D (_MarkTexture),
                 sampler sampler_MarkTexture,
                 float _MarkTextureAlpha)
{
    float2 newUV = (uv - _MarkTextureRect.xy) / _MarkTextureRect.zw;
    float3 colBase = Color;
    float4 colAdd = SAMPLE_TEXTURE2D(_MarkTexture, sampler_MarkTexture, newUV);
    float blendAlpha = colAdd.a * _MarkTextureAlpha * step(newUV.x, 1) * step(0, newUV.x) * step(newUV.y, 1)
        * step(0, newUV.y);
    return colBase * (1 - blendAlpha) + float4(colAdd.xyz, 1) * blendAlpha;
}

float SnowEffect(float3 seed, float mean, float standardDeviation)
{
    seed = frac(seed * float3(12.9898, 78.233, 37.719));
    float u1 = frac(sin(dot(seed, float3(1.0, 57.0, 113.0))) * 43758.5453);
    float u2 = frac(sin(dot(seed, float3(31.0, 97.0, 23.0))) * 43758.5453);
    float r = sqrt(-2.0 * log(u1));
    float theta = 6.28318530718 * u2;
    float gaussianRandom = r * cos(theta);
    gaussianRandom = gaussianRandom * standardDeviation + mean;
    return saturate(gaussianRandom);
}

void UVshifting(inout float3 yuv)
{
    yuv.x = ((yuv.x - 128) * _Contrast + 128);
    yuv.x *= _Light;
    yuv.x = max(yuv.x, _DarkFade);
    yuv.x = min(yuv.x, 255 - _BrightFade);
    yuv.y = LevelLow(min(255, (yuv.y - 128) * _VividU + _ShiftU + 128), _Level);
    yuv.z = LevelLow(min(255, (yuv.z - 128) * _VividV + _ShiftV + 128), _Level);
}

float3 rgb2yuv(float3 rgb)
{
    float y = rgb.r * 0.299 + rgb.g * 0.587 + rgb.b * 0.114;
    float u = rgb.r * -0.168736 + rgb.g * -0.331264 + rgb.b * 0.5 + 128.0;
    float v = rgb.r * 0.5 + rgb.g * -0.418688 + rgb.b * -0.081312 + 128.0;


    y = floor(y);
    u = floor(u);
    v = floor(v);

    return float3(y, u, v);
}

float3 InvertLight(float3 color)
{
    return 1 - color;
}

float3 FishEye(TEXTURE2D (_MainTex), sampler sampler_MainTex, float _FishEyePow, float _FishEyeIntensity,
               float2 uv)
{
    float2 newUV = (uv - 0.5) * 2; // -1 --- 1
    float2 resultUV = (1 - pow(clamp(length(newUV), 0, 1), _FishEyePow)) * _FishEyeIntensity * newUV;
    resultUV = uv - resultUV;
    float3 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, resultUV).xyz;
    return col;
}


float3 GraphNoise(float3 color, float2 uv, float _LightNoise, float _DrakNoise)
{
    float light = (ScaledGaussianRandom(float3(uv * 10, _Time.x), 0.5, 0.4) * _LightNoise - _LightNoise /
        2);
    color = color + float3(light, light, light);
    float dark = (ScaledGaussianRandom(float3(uv * 5, _Time.x * 2), 0.5, 0.4) * _DrakNoise - _DrakNoise /
        2) * (1 - (color.x + color.y + color.z) / 3);
    color = color + float3(dark, dark, dark);
    return float4(color, 0);
}

inline float3 Convolute(float2 uv)
{
    #if _CONVOLUTEEFFECT_ON
                return Convolute(uv, _MainTex_TexelSize.xy, _MainTex, sampler_MainTex,
                  GetConvolutionKernel(_KernelType));
    #else
    return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
    #endif
}

inline float3 WaterMarkEffect(float2 posSS, float2 uv)
{
    #if _WATERMARKEFFECT_ON
            float2 size = _MarkTexture_TexelSize.zw / _ScreenParams.xy;
            float4 rect = _MarkTextureRect;
            size = rect.zw * size;
            return  WaterMark(Convolute(uv), posSS, Vec2ToVec4(rect.xy - size / 2.0f, size), _MarkTexture, sampler_MarkTexture,
                            _MarkTextureAlpha);
    #else
    return Convolute(uv);
    #endif
}

//色彩调整
inline float3 YUVHandleEffect(float2 posSS, float2 uv)
{
    #if _YUVHANDLEEFFECT_ON
            _ShiftX = _ShiftX / _MainTex_TexelSize.x;
            _ShiftY = _ShiftY / _MainTex_TexelSize.y;   
            float3 yuv = float3(WaterMarkEffect(posSS, uv).x, WaterMarkEffect(posSS, uv + _ShiftX).y,
            WaterMarkEffect(posSS, uv - _ShiftY).z) * 255;
            UVshifting(yuv);
            return yuv / 255;
    #else
    return WaterMarkEffect(posSS, uv);
    #endif
}

//反转
inline float3 InvertLight(float2 posSS, float2 uv)
{
    #if _INVERTLIGHTEFFECT_ON
            return InvertLight(YUVHandleEffect( posSS, uv));
     #else
     return YUVHandleEffect(posSS, uv);
     #endif
}

//鱼眼
inline float3 FishEyeEffect(float2 posSS, float2 uv)
{
    #if _FISHEYEEFFECT_ON
            float4 _FishEyeIntensity = float4(_FishEyeIntensity_X, _FishEyeIntensity_Y, 0, 0);
            float2 newUV = (uv - 0.5) * 2; // -1 --- 1
            float2 resultUV = (1 - pow(clamp(length(newUV), 0, 1), _FishEyePow)) * _FishEyeIntensity * newUV;
            resultUV = uv - resultUV;
            float3 col = InvertLight(posSS, resultUV);
            return col;
    #else
    return InvertLight(posSS, uv);
    #endif
}

//扫描线
inline float3 Interlaced(float2 posSS, float2 uv)
{
    #if _INTERLACEDEFFECT_ON
            float2 _TexSize = _MainTex_TexelSize.zw;
            float2 scenePos = floor(uv * _TexSize);
            float interlacePeriod = (scenePos.y % _InterlacedLine) / _InterlacedLine;
            float interlacePixel = abs(interlacePeriod - 0.5) * 2;
            float wLeft = ceil(_Interlaced * interlacePixel) * 4 / _TexSize.x;
            float interlaceLightPeriod = (interlacePixel - 0.5) * 2;
            
            float3 col = FishEyeEffect(posSS,float2(uv.x + wLeft, uv.y));
            return col * (1 + _InterlacedLight * interlaceLightPeriod);
    #else
    return FishEyeEffect(posSS, uv);
    #endif
}

inline float3 TransposeX(float2 posSS, float2 uv)
{
    #if _TRANSPOSEXEFFECT_ON
            float2 _TexSize = _MainTex_TexelSize.zw;
            float wLeft = floor(
                _TexSize.x * _TransposeX * pow((1 - uv.y), _TransposePow) * (1 + _TransposeNoise * (
                    ScaledGaussianRandom(float3(uv, _Time.y), 0.5, 0.2) - 0.5))) * 4;
            float3 col = Interlaced(posSS, float2(uv.x + (wLeft / _TexSize.x), uv.y));
            return col;
    #else
    return Interlaced(posSS, uv);
    #endif
}

inline float3 GraphNoise(float2 posSS, float2 uv)
{
    #if _GRAPHNOISEEFFECT_ON
            return GraphNoise(TransposeX(posSS, uv),uv,_LightNoise / 255,_DrakNoise / 255);
    #else
    return TransposeX(posSS, uv);
    #endif
}

inline float3 LEDEffect(float2 posSS, float2 uv, float4 posCS)
{
    #if _LEDEFFECT_ON
            int group = 4 * max(_LEDResolutionLevel, 1);
            half2 index = float2(posCS.x % group, posCS.y % group);
            float2 onePiece = float2(group / _ScreenParams.x, group / _ScreenParams.y);
            float flag = step(_LEDResolutionLevel, 0.5);
            float2 newUV = lerp(
                float2(round(uv.x / onePiece.x) * onePiece.x, round(uv.y / onePiece.y) * onePiece.y), uv, flag);
            float3 col = GraphNoise(posSS, newUV);
            half2 disTo = float2(index.x / group, index.y / group);
            float3 col_temp = col * float4(step(disTo.x, 0.25), step(disTo.x, 0.5) * step(0.25, disTo.x),
        step(0.5, disTo.x) * step(disTo.x, 0.75), 1) * step(
                0.25, disTo.y);
            col = lerp(col_temp, col, flag);
            return col;
    #else
    return GraphNoise(posSS, uv);
    #endif
}

inline float3 CutOff(float2 posSS, float2 uv, float4 posCS)
{
    #if _CUTOFFEFFECT_ON
    return SnowEffect(float3(uv * 10,unity_DeltaTime.x),0.5,0.4);
    #else
        return LEDEffect( posSS,  uv,  posCS);
    #endif
}


v2f vert(appdata v)
{
    v2f o;
    VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
    o.posCS = vertexInput.positionCS;
    o.posSS = vertexInput.positionNDC;
    o.uv = v.uv;
    return o;
}

float4 frag(v2f input) : SV_Target
{
    float2 posSS = input.posSS.xy / input.posSS.w;
    float4 posCS = input.posCS;
    float2 uv = input.uv;
    
    return float4(CutOff(posSS, uv, posCS), 1);
}

#endif