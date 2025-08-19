// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Triplebrick/DoubleLayer"
{
	Properties
	{
		_BasecolorTint("Basecolor Tint", Color) = (1,1,1,0)
		_Basecolor("Basecolor", 2D) = "white" {}
		_BaseNormal("Base Normal", 2D) = "bump" {}
		_CoatNormal("Coat Normal", 2D) = "bump" {}
		_BaseNormalStrength("Base Normal Strength", Range( 0 , 1)) = 0
		_CoatNormalStrength("Coat Normal Strength", Range( 0 , 1)) = 0
		_CoatAmount("Coat Amount", Range( 0 , 1)) = 0
		_CoatSmoothness("Coat Smoothness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityStandardUtils.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldNormal;
			INTERNAL_DATA
			float2 uv_texcoord;
			float3 worldPos;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			fixed3 Albedo;
			fixed3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			fixed Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform sampler2D _Basecolor;
		uniform float4 _Basecolor_ST;
		uniform float4 _BasecolorTint;
		uniform float _BaseNormalStrength;
		uniform sampler2D _BaseNormal;
		uniform float4 _BaseNormal_ST;
		uniform float _CoatNormalStrength;
		uniform sampler2D _CoatNormal;
		uniform float4 _CoatNormal_ST;
		uniform float _CoatSmoothness;
		uniform float _CoatAmount;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			SurfaceOutputStandard s1 = (SurfaceOutputStandard ) 0;
			float2 uv_Basecolor = i.uv_texcoord * _Basecolor_ST.xy + _Basecolor_ST.zw;
			float4 tex2DNode321 = tex2D( _Basecolor, uv_Basecolor );
			s1.Albedo = ( tex2DNode321 * _BasecolorTint ).rgb;
			float2 uv_BaseNormal = i.uv_texcoord * _BaseNormal_ST.xy + _BaseNormal_ST.zw;
			s1.Normal = WorldNormalVector( i, UnpackScaleNormal( tex2D( _BaseNormal, uv_BaseNormal ) ,_BaseNormalStrength ));
			s1.Emission = float3( 0,0,0 );
			s1.Metallic = 0.0;
			s1.Smoothness = tex2DNode321.a;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float fresnelNDotV279 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode279 = ( 0.05 + 1.0 * pow( 1.0 - fresnelNDotV279, 5.0 ) );
			s1.Occlusion = saturate( ( 1.0 - fresnelNode279 ) );

			data.light = gi.light;

			UnityGI gi1 = gi;
			#ifdef UNITY_PASS_FORWARDBASE
			Unity_GlossyEnvironmentData g1 = UnityGlossyEnvironmentSetup( s1.Smoothness, data.worldViewDir, s1.Normal, float3(0,0,0));
			gi1 = UnityGlobalIllumination( data, s1.Occlusion, s1.Normal, g1 );
			#endif

			float3 surfResult1 = LightingStandard ( s1, viewDir, gi1 ).rgb;
			surfResult1 += s1.Emission;

			SurfaceOutputStandardSpecular s334 = (SurfaceOutputStandardSpecular ) 0;
			s334.Albedo = float3( 0,0,0 );
			float2 uv_CoatNormal = i.uv_texcoord * _CoatNormal_ST.xy + _CoatNormal_ST.zw;
			s334.Normal = WorldNormalVector( i, UnpackScaleNormal( tex2D( _CoatNormal, uv_CoatNormal ) ,_CoatNormalStrength ));
			s334.Emission = float3( 0,0,0 );
			float3 temp_cast_1 = (1.0).xxx;
			s334.Specular = temp_cast_1;
			s334.Smoothness = _CoatSmoothness;
			s334.Occlusion = 1.0;

			data.light = gi.light;

			UnityGI gi334 = gi;
			#ifdef UNITY_PASS_FORWARDBASE
			Unity_GlossyEnvironmentData g334 = UnityGlossyEnvironmentSetup( s334.Smoothness, data.worldViewDir, s334.Normal, float3(0,0,0));
			gi334 = UnityGlobalIllumination( data, s334.Occlusion, s334.Normal, g334 );
			#endif

			float3 surfResult334 = LightingStandardSpecular ( s334, viewDir, gi334 ).rgb;
			surfResult334 += s334.Emission;

			float3 lerpResult208 = lerp( surfResult1 , surfResult334 , ( fresnelNode279 * _CoatAmount ));
			c.rgb = lerpResult208;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			o.Normal = float3(0,0,1);
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting keepalpha fullforwardshadows 

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
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				fixed3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
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
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
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
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14201
8;100;1154;686;2344.017;1182.345;4.197654;True;False
Node;AmplifyShaderEditor.CommentaryNode;280;-654.1785,1302.273;Float;False;1024.147;552.1847;Simple fresnel blend;5;298;296;237;47;279;Blend Factor;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;329;937.4981,773.4247;Float;False;1016.546;470.1129;This mirror layer that to mimc a coating layer;5;334;333;332;331;330;Coating Layer (Specular);1,1,1,1;0;0
Node;AmplifyShaderEditor.FresnelNode;279;-604.8812,1353.103;Float;True;World;4;0;FLOAT3;0,0,0;False;1;FLOAT;0.05;False;2;FLOAT;1.0;False;3;FLOAT;5.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;330;987.498,878.8257;Float;False;Property;_CoatNormalStrength;Coat Normal Strength;5;0;Create;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-561.7686,220.575;Float;False;Property;_BaseNormalStrength;Base Normal Strength;4;0;Create;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;296;-296.7101,1352.286;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;321;-38.01158,-426.5747;Float;True;Property;_Basecolor;Basecolor;1;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;338;575.0084,-254.4015;Float;False;Property;_BasecolorTint;Basecolor Tint;0;0;Create;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;331;1325.517,1128.538;Float;False;Property;_CoatSmoothness;Coat Smoothness;7;0;Create;0;0.95;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;339;1447.955,412.7881;Float;False;Constant;_Float0;Float 0;8;0;Create;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;-131.0492,178.2628;Float;True;Property;_BaseNormal;Base Normal;2;0;Create;None;a268ab862991c4743a9281c69bb2c36a;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;333;1444.252,1040.989;Float;False;Constant;_Float2;Float 2;18;0;Create;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;298;-58.64547,1357.051;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-632.2784,1604.784;Float;False;Property;_CoatAmount;Coat Amount;6;0;Create;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;336;795.6553,-353.6602;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;332;1341.971,823.4247;Float;True;Property;_CoatNormal;Coat Normal;3;0;Create;None;b3d940e75e1f5d24684cd93a2758e1bf;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.5;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;237;-283.431,1438.729;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CustomStandardSurface;1;1730.49,170.9699;Float;False;Metallic;6;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,1;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;1.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CustomStandardSurface;334;1699.044,907.5637;Float;False;Specular;6;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,1;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0.0,0,0;False;4;FLOAT;0.0;False;5;FLOAT;1.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FresnelNode;201;354.4107,392.0276;Float;True;World;4;0;FLOAT3;0,0,0;False;1;FLOAT;0.5;False;2;FLOAT;0.87;False;3;FLOAT;5.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;208;2395.602,1132.678;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0.0,0,0;False;2;FLOAT;0.0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;335;1414.005,657.204;Float;False;Constant;_Vector0;Vector 0;8;0;Create;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2753.206,1085.791;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;Triplebrick/DoubleLayer;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0.0,0,0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.CommentaryNode;292;51.41161,-1153.981;Float;False;348.1;312.8;Comment;0;Dual Toning Factor;1,1,1,1;0;0
WireConnection;296;0;279;0
WireConnection;7;5;11;0
WireConnection;298;0;296;0
WireConnection;336;0;321;0
WireConnection;336;1;338;0
WireConnection;332;5;330;0
WireConnection;237;0;279;0
WireConnection;237;1;47;0
WireConnection;1;0;336;0
WireConnection;1;1;7;0
WireConnection;1;3;339;0
WireConnection;1;4;321;4
WireConnection;1;5;298;0
WireConnection;334;1;332;0
WireConnection;334;3;333;0
WireConnection;334;4;331;0
WireConnection;208;0;1;0
WireConnection;208;1;334;0
WireConnection;208;2;237;0
WireConnection;0;13;208;0
ASEEND*/
//CHKSM=2EDC19A6CE90A288608046C7CD9EBDD9440FDFBB