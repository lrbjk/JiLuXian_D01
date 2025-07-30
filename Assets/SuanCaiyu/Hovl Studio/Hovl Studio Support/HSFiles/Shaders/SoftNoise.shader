// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hovl/Particles/SoftNoise"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_Noise("Noise", 2D) = "white" {}
		_SpeedMainTexUVNoiseZW("Speed MainTex U/V + Noise Z/W", Vector) = (0,0,0,0)
		_Noisescale("Noise scale", Float) = 1000
		_Noisepower("Noise power", Float) = 1
		_Noiselerp("Noise lerp", Float) = 1
		_Color("Color", Color) = (1,1,1,1)
		_Emissionpower("Emission power", Float) = 1
		_Emission("Emission", Float) = 2
		_OpacityTex("OpacityTex", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_Maskpower("Mask power", Float) = 1
		_Maskmultiplayer("Mask multiplayer", Float) = 3
		[Toggle]_Softedges("Soft edges", Float) = 0
		[Toggle]_Usedepth("Use depth", Float) = 0
		_Depthpower("Depth power", Float) = 1
		_OpacityTexspeedXY("OpacityTex speed XY", Vector) = (0,-0.5,0,0)
		_Sideopacitymult("Side opacity mult", Float) = 1.5
		[Toggle]_Upopacity("Up opacity", Float) = 1
		[Enum(Cull Off,0,Cull Front,1,Cull Back,2)]_CullMode("Culling", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float3 uv_texcoord;
			float4 vertexColor : COLOR;
			float4 screenPos;
			float3 worldNormal;
			float3 viewDir;
		};

		uniform float _CullMode;
		uniform float4 _SpeedMainTexUVNoiseZW;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float _Noisescale;
		uniform float _Noisepower;
		uniform sampler2D _Noise;
		uniform float4 _Noise_ST;
		uniform float _Noiselerp;
		uniform float _Emissionpower;
		uniform float _Emission;
		uniform float4 _Color;
		uniform float _Softedges;
		uniform float _Usedepth;
		uniform sampler2D _OpacityTex;
		uniform float4 _OpacityTexspeedXY;
		uniform float4 _OpacityTex_ST;
		uniform float _Maskpower;
		uniform float _Maskmultiplayer;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Depthpower;
		uniform float _Sideopacitymult;
		uniform float _Upopacity;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 appendResult34 = (float2(_SpeedMainTexUVNoiseZW.x , _SpeedMainTexUVNoiseZW.y));
			float3 uvs_MainTex = i.uv_texcoord;
			uvs_MainTex.xy = i.uv_texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
			float2 panner31 = ( 1.0 * _Time.y * appendResult34 + uvs_MainTex.xy);
			float simplePerlin2D127 = snoise( panner31*_Noisescale );
			simplePerlin2D127 = simplePerlin2D127*0.5 + 0.5;
			float4 temp_cast_1 = (( simplePerlin2D127 * _Noisepower )).xxxx;
			float2 appendResult36 = (float2(_SpeedMainTexUVNoiseZW.z , _SpeedMainTexUVNoiseZW.w));
			float3 uvs_Noise = i.uv_texcoord;
			uvs_Noise.xy = i.uv_texcoord.xy * _Noise_ST.xy + _Noise_ST.zw;
			float2 panner38 = ( 1.0 * _Time.y * appendResult36 + uvs_Noise.xy);
			float4 lerpResult129 = lerp( temp_cast_1 , ( tex2D( _MainTex, panner31 ) * tex2D( _Noise, panner38 ) ) , _Noiselerp);
			float4 temp_cast_2 = (_Emissionpower).xxxx;
			o.Emission = ( pow( lerpResult129 , temp_cast_2 ) * _Emission * _Color * i.vertexColor ).rgb;
			float2 appendResult110 = (float2(_OpacityTexspeedXY.x , _OpacityTexspeedXY.y));
			float3 uvs_OpacityTex = i.uv_texcoord;
			uvs_OpacityTex.xy = i.uv_texcoord.xy * _OpacityTex_ST.xy + _OpacityTex_ST.zw;
			float2 panner94 = ( 1.0 * _Time.y * appendResult110 + uvs_OpacityTex.xy);
			float clampResult97 = clamp( ( pow( tex2D( _OpacityTex, panner94 ).r , ( _Maskpower + uvs_MainTex.z ) ) * _Maskmultiplayer ) , 0.0 , 1.0 );
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float temp_output_62_0 = ( _Color.a * i.vertexColor.a * clampResult97 * tex2D( _Mask, uv_Mask ).a );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth71 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth71 = abs( ( screenDepth71 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _Depthpower ) );
			float clampResult73 = clamp( distanceDepth71 , 0.0 , 1.0 );
			float3 ase_worldNormal = i.worldNormal;
			float dotResult80 = dot( ase_worldNormal , i.viewDir );
			float temp_output_89_0 = ( pow( dotResult80 , 3.0 ) * _Sideopacitymult );
			float clampResult122 = clamp( ( pow( uvs_MainTex.y , 4.0 ) * 3.0 ) , 0.0 , 1.0 );
			float lerpResult84 = lerp( temp_output_89_0 , ( (( _Upopacity )?( clampResult122 ):( 1.0 )) * (0.0 + (temp_output_89_0 - 0.0) * (1.0 - 0.0) / (-1.0 - 0.0)) ) , (1.0 + (sign( dotResult80 ) - -1.0) * (0.0 - 1.0) / (1.0 - -1.0)));
			float clampResult85 = clamp( lerpResult84 , 0.0 , 1.0 );
			float clampResult91 = clamp( clampResult85 , 0.0 , 1.0 );
			o.Alpha = (( _Softedges )?( ( (( _Usedepth )?( ( temp_output_62_0 * clampResult73 ) ):( temp_output_62_0 )) * clampResult91 ) ):( (( _Usedepth )?( ( temp_output_62_0 * clampResult73 ) ):( temp_output_62_0 )) ));
		}

		ENDCG
		CGPROGRAM
		#pragma exclude_renderers xboxseries playstation switch nomrt 
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				float3 worldNormal : TEXCOORD4;
				half4 color : COLOR0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xyz = customInputData.uv_texcoord;
				o.customPack1.xyz = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				o.color = v.color;
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xyz;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = worldViewDir;
				surfIN.worldNormal = IN.worldNormal;
				surfIN.screenPos = IN.screenPos;
				surfIN.vertexColor = IN.color;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=18933
799;73;772;599;1627.66;-1256.859;1;True;False
Node;AmplifyShaderEditor.Vector4Node;109;-2309.97,549.2347;Float;False;Property;_OpacityTexspeedXY;OpacityTex speed XY;16;0;Create;True;0;0;0;False;0;False;0,-0.5,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;105;-1782.93,1133.382;Inherit;False;2035.048;868.9979;Soft edges;17;82;83;84;85;91;86;87;89;81;79;80;119;120;121;122;123;124;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;95;-2067.155,459.6099;Inherit;False;0;63;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldNormalVector;81;-1732.93,1497.234;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;30;-1989.886,-93.2309;Inherit;False;0;29;3;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;79;-1725.894,1644.996;Float;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;110;-1977.97,579.2347;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;120;-1467.62,1179.754;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;4;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;80;-1495.457,1608.056;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;94;-1610.606,503.7028;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-0.5;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-1446.171,638.929;Float;False;Property;_Maskpower;Mask power;11;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;121;-1303.868,1181.107;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;63;-1421.938,444.807;Inherit;True;Property;_OpacityTex;OpacityTex;9;0;Create;True;0;0;0;False;0;False;-1;None;4b0225a5290cbe540bc56e26a8682db2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;119;-1280.085,1401.063;Inherit;False;Property;_Sideopacitymult;Side opacity mult;17;0;Create;True;0;0;0;False;0;False;1.5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;117;-1260.999,641.2726;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;87;-1292.546,1508.552;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;89;-1037.928,1505.78;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;103;-1111.835,633.8851;Float;False;Property;_Maskmultiplayer;Mask multiplayer;12;0;Create;True;0;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;122;-1062.233,1180.714;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;53;-1104.212,537.9693;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;32;-2475.029,113.5536;Float;False;Property;_SpeedMainTexUVNoiseZW;Speed MainTex U/V + Noise Z/W;2;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;34;-1900.836,89.92324;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;86;-797.0067,1585.503;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;-1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;-891.8019,538.4253;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;36;-1893.917,302.0966;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;37;-1989.587,182.8138;Inherit;False;0;35;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;72;-737.9787,989.5668;Float;False;Property;_Depthpower;Depth power;15;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;124;-808.9406,1418.774;Inherit;False;Property;_Upopacity;Up opacity;18;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;1;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SignOpNode;82;-1021.445,1802.546;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;31;-1706.663,22.98934;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;125;-1622.407,-312.0488;Float;False;Property;_Noisescale;Noise scale;3;0;Create;True;0;0;0;False;0;False;1000;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;49;-593.6298,76.03733;Float;False;Property;_Color;Color;6;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;38;-1699.744,235.1628;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;52;-878.2848,656.6249;Inherit;True;Property;_Mask;Mask;10;0;Create;True;0;0;0;False;0;False;-1;None;05ea33e1e37a8c245aca3e86dec28fdf;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;97;-734.5894,538.6395;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;50;-553.3295,263.2375;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;83;-863.4612,1800.38;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;71;-523.9275,970.9049;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;123;-580.5659,1560.618;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;62;-279.5515,638.8843;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;73;-252.2921,972.2811;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;84;-365.8072,1502.777;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;126;-1316.01,-109.4258;Float;False;Property;_Noisepower;Noise power;4;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;127;-1381.652,-333.5779;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1000;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;35;-1419.74,254.6808;Inherit;True;Property;_Noise;Noise;1;0;Create;True;0;0;0;False;0;False;-1;None;74ed93858b3298e4f93e6146b3ef490c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;29;-1422.349,38.68793;Inherit;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;0;False;0;False;-1;None;74ed93858b3298e4f93e6146b3ef490c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-1042.133,164.2799;Inherit;False;2;2;0;COLOR;1,1,1,1;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;130;-1043.582,-60.98273;Float;False;Property;_Noiselerp;Noise lerp;5;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;128;-1032.01,-198.4255;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;85;-179.8115,1502.588;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-84.76637,819.1151;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;74;105.5726,639.8444;Float;False;Property;_Usedepth;Use depth;14;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;91;77.118,1250.014;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;129;-820.3317,-107.9052;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-820.3423,36.43726;Float;False;Property;_Emissionpower;Emission power;7;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;51;-540.7476,-7.942116;Float;False;Property;_Emission;Emission;8;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;373.2736,773.8013;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;47;-586.3438,-109.1628;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;76;533.3589,639.5092;Float;False;Property;_Softedges;Soft edges;13;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;133;-1244.811,-1247.49;Inherit;False;Property;_CullMode;Culling;19;1;[Enum];Create;False;0;3;Cull Off;0;Cull Front;1;Cull Back;2;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-299.0436,-106.5626;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;69;1260.156,160.5588;Float;False;True;-1;2;;0;0;Standard;Hovl/Particles/SoftNoise;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;d3d9;d3d11_9x;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;ps4;psp2;n3ds;wiiu;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;133;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;110;0;109;1
WireConnection;110;1;109;2
WireConnection;120;0;30;2
WireConnection;80;0;81;0
WireConnection;80;1;79;0
WireConnection;94;0;95;0
WireConnection;94;2;110;0
WireConnection;121;0;120;0
WireConnection;63;1;94;0
WireConnection;117;0;54;0
WireConnection;117;1;30;3
WireConnection;87;0;80;0
WireConnection;89;0;87;0
WireConnection;89;1;119;0
WireConnection;122;0;121;0
WireConnection;53;0;63;1
WireConnection;53;1;117;0
WireConnection;34;0;32;1
WireConnection;34;1;32;2
WireConnection;86;0;89;0
WireConnection;96;0;53;0
WireConnection;96;1;103;0
WireConnection;36;0;32;3
WireConnection;36;1;32;4
WireConnection;124;1;122;0
WireConnection;82;0;80;0
WireConnection;31;0;30;0
WireConnection;31;2;34;0
WireConnection;38;0;37;0
WireConnection;38;2;36;0
WireConnection;97;0;96;0
WireConnection;83;0;82;0
WireConnection;71;0;72;0
WireConnection;123;0;124;0
WireConnection;123;1;86;0
WireConnection;62;0;49;4
WireConnection;62;1;50;4
WireConnection;62;2;97;0
WireConnection;62;3;52;4
WireConnection;73;0;71;0
WireConnection;84;0;89;0
WireConnection;84;1;123;0
WireConnection;84;2;83;0
WireConnection;127;0;31;0
WireConnection;127;1;125;0
WireConnection;35;1;38;0
WireConnection;29;1;31;0
WireConnection;39;0;29;0
WireConnection;39;1;35;0
WireConnection;128;0;127;0
WireConnection;128;1;126;0
WireConnection;85;0;84;0
WireConnection;75;0;62;0
WireConnection;75;1;73;0
WireConnection;74;0;62;0
WireConnection;74;1;75;0
WireConnection;91;0;85;0
WireConnection;129;0;128;0
WireConnection;129;1;39;0
WireConnection;129;2;130;0
WireConnection;77;0;74;0
WireConnection;77;1;91;0
WireConnection;47;0;129;0
WireConnection;47;1;46;0
WireConnection;76;0;74;0
WireConnection;76;1;77;0
WireConnection;48;0;47;0
WireConnection;48;1;51;0
WireConnection;48;2;49;0
WireConnection;48;3;50;0
WireConnection;69;2;48;0
WireConnection;69;9;76;0
ASEEND*/
//CHKSM=725716D8681D9529675D9AEB4D40FDEC93C42E36