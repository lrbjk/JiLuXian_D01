// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hovl/Opaque/ParallaxIce"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "black" {}
		_Metallic("Metallic", 2D) = "black" {}
		_Normal("Normal", 2D) = "bump" {}
		_AmbientOcclusion("Ambient Occlusion", 2D) = "white" {}
		_Emission("Emission", 2D) = "white" {}
		_Desaturation("Desaturation", Float) = 0
		_Emissionmult("Emission mult", Float) = 342
		_Emissionpower("Emission power", Float) = 7
		[HDR]_Fresnelcolor("Fresnel color", Color) = (1,1,1,1)
		_Fresnelpower("Fresnel power", Float) = 6
		_Noiseflow("Noise flow", 2D) = "white" {}
		_FlowSpeedXYPowerZHeightW("Flow Speed XY Power Z Height W", Vector) = (0.2,0.1,1,0)
		_Mask("Mask", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldPos;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform sampler2D _Noiseflow;
		uniform float4 _FlowSpeedXYPowerZHeightW;
		uniform float4 _Noiseflow_ST;
		uniform float _Emissionpower;
		uniform float _Emissionmult;
		uniform float _Desaturation;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float _Fresnelpower;
		uniform float4 _Fresnelcolor;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;
		uniform sampler2D _AmbientOcclusion;
		uniform float4 _AmbientOcclusion_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = tex2D( _Albedo, uv_Albedo ).rgb;
			float2 uv0_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			float2 appendResult37 = (float2(_FlowSpeedXYPowerZHeightW.x , _FlowSpeedXYPowerZHeightW.y));
			float2 uv0_Noiseflow = i.uv_texcoord * _Noiseflow_ST.xy + _Noiseflow_ST.zw;
			float2 panner30 = ( 1.0 * _Time.y * appendResult37 + uv0_Noiseflow);
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = Unity_SafeNormalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldTangent = WorldNormalVector( i, float3( 1, 0, 0 ) );
			float3 ase_worldBitangent = WorldNormalVector( i, float3( 0, 1, 0 ) );
			float3x3 ase_worldToTangent = float3x3( ase_worldTangent, ase_worldBitangent, ase_worldNormal );
			float3 ase_tanViewDir = mul( ase_worldToTangent, ase_worldViewDir );
			float2 Offset24 = ( ( ( tex2D( _Noiseflow, panner30 ).r * _FlowSpeedXYPowerZHeightW.z ) - 1 ) * ase_tanViewDir.xy * _FlowSpeedXYPowerZHeightW.w ) + uv0_Emission;
			float4 temp_cast_1 = (_Emissionpower).xxxx;
			float3 desaturateInitialColor9 = ( pow( tex2D( _Emission, Offset24 ) , temp_cast_1 ) * _Emissionmult ).rgb;
			float desaturateDot9 = dot( desaturateInitialColor9, float3( 0.299, 0.587, 0.114 ));
			float3 desaturateVar9 = lerp( desaturateInitialColor9, desaturateDot9.xxx, _Desaturation );
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode40 = tex2D( _Mask, uv_Mask );
			float fresnelNdotV15 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode15 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV15, _Fresnelpower ) );
			o.Emission = ( float4( ( desaturateVar9 * tex2DNode40.r ) , 0.0 ) + ( tex2DNode40.g * fresnelNode15 * _Fresnelcolor ) ).rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			float4 tex2DNode2 = tex2D( _Metallic, uv_Metallic );
			o.Metallic = tex2DNode2.r;
			o.Smoothness = tex2DNode2.a;
			float2 uv_AmbientOcclusion = i.uv_texcoord * _AmbientOcclusion_ST.xy + _AmbientOcclusion_ST.zw;
			o.Occlusion = tex2D( _AmbientOcclusion, uv_AmbientOcclusion ).r;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

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
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
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
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
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
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=17000
509;29;1017;1004;3896.382;1574.483;4.377848;True;False
Node;AmplifyShaderEditor.Vector4Node;36;-3678.619,261.0264;Float;False;Property;_FlowSpeedXYPowerZHeightW;Flow Speed XY Power Z Height W;11;0;Create;True;0;0;False;0;0.2,0.1,1,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;32;-3418.881,86.10722;Float;False;0;29;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;37;-3324.297,206.1625;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;30;-3121.715,92.33292;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.2,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;29;-2920.7,64.04264;Float;True;Property;_Noiseflow;Noise flow;10;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;27;-2527.007,352.9198;Float;False;Tangent;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;25;-2568.831,66.81953;Float;False;0;5;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;38;-2399.92,288.0091;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-2481.632,186.1994;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ParallaxMappingNode;24;-2267.461,157.7188;Float;False;Normal;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT3;1,1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;5;-2018.777,135.4167;Float;True;Property;_Emission;Emission;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-1972.922,324.0551;Float;False;Property;_Emissionpower;Emission power;7;0;Create;True;0;0;False;0;7;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;6;-1674.602,140.071;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1678.921,249.0551;Float;False;Property;_Emissionmult;Emission mult;6;0;Create;True;0;0;False;0;342;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-1740.006,545.8162;Float;False;Property;_Fresnelpower;Fresnel power;9;0;Create;True;0;0;False;0;6;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1427.602,141.371;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1432.921,244.0551;Float;False;Property;_Desaturation;Desaturation;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;40;-1202.395,238.3926;Float;True;Property;_Mask;Mask;12;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DesaturateOpNode;9;-1185.8,142.6711;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;18;-1437.517,619.5192;Float;False;Property;_Fresnelcolor;Fresnel color;8;1;[HDR];Create;True;0;0;False;0;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;15;-1537.379,409.7132;Float;True;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-860.7493,144.5632;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-855.9183,386.8914;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-642.7169,143.1861;Float;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-487.2345,-248.9334;Float;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;None;None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;43;-275.1476,162.1632;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;3;-487.2343,-57.91666;Float;True;Property;_Normal;Normal;2;0;Create;True;0;0;False;0;None;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-494.1053,449.1703;Float;True;Property;_AmbientOcclusion;Ambient Occlusion;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-495.4796,240.289;Float;True;Property;_Metallic;Metallic;1;0;Create;True;0;0;False;0;None;None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;;0;0;Standard;Hovl/Opaque/ParallaxIce;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;37;0;36;1
WireConnection;37;1;36;2
WireConnection;30;0;32;0
WireConnection;30;2;37;0
WireConnection;29;1;30;0
WireConnection;38;0;36;4
WireConnection;33;0;29;1
WireConnection;33;1;36;3
WireConnection;24;0;25;0
WireConnection;24;1;33;0
WireConnection;24;2;38;0
WireConnection;24;3;27;0
WireConnection;5;1;24;0
WireConnection;6;0;5;0
WireConnection;6;1;10;0
WireConnection;7;0;6;0
WireConnection;7;1;11;0
WireConnection;9;0;7;0
WireConnection;9;1;12;0
WireConnection;15;3;19;0
WireConnection;42;0;9;0
WireConnection;42;1;40;1
WireConnection;14;0;40;2
WireConnection;14;1;15;0
WireConnection;14;2;18;0
WireConnection;13;0;42;0
WireConnection;13;1;14;0
WireConnection;43;0;13;0
WireConnection;0;0;1;0
WireConnection;0;1;3;0
WireConnection;0;2;43;0
WireConnection;0;3;2;0
WireConnection;0;4;2;4
WireConnection;0;5;4;1
ASEEND*/
//CHKSM=61D7E00F9F8880FE3FD2A5B5880EB6A63F960DED