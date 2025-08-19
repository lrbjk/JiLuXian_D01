// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Triplebrick/Fluid"
{
	Properties
	{
		[Header(Refraction)]
		_Normal("Normal", 2D) = "bump" {}
		_ChromaticAberration("Chromatic Aberration", Range( 0 , 0.3)) = 0.1
		_Texture0("Texture 0", 2D) = "white" {}
		_EmissionStrength("Emission Strength", Float) = 5
		_Opacity("Opacity", Range( 0 , 1)) = 0.5
		_Color("Color", Color) = (1,1,1,0.003921569)
		_BaseEmission("Base Emission", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile _ALPHAPREMULTIPLY_ON
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
			float3 worldPos;
		};

		uniform sampler2D _Normal;
		uniform float _BaseEmission;
		uniform sampler2D _Texture0;
		uniform float4 _Color;
		uniform float _EmissionStrength;
		uniform float _Opacity;
		uniform sampler2D _GrabTexture;
		uniform float _ChromaticAberration;

		inline float4 Refraction( Input i, SurfaceOutputStandard o, float indexOfRefraction, float chomaticAberration ) {
			float3 worldNormal = o.Normal;
			float4 screenPos = i.screenPos;
			#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
			#else
				float scale = 1.0;
			#endif
			float halfPosW = screenPos.w * 0.5;
			screenPos.y = ( screenPos.y - halfPosW ) * _ProjectionParams.x * scale + halfPosW;
			#if SHADER_API_D3D9 || SHADER_API_D3D11
				screenPos.w += 0.00000000001;
			#endif
			float2 projScreenPos = ( screenPos / screenPos.w ).xy;
			float3 worldViewDir = normalize( UnityWorldSpaceViewDir( i.worldPos ) );
			float3 refractionOffset = ( ( ( ( indexOfRefraction - 1.0 ) * mul( UNITY_MATRIX_V, float4( worldNormal, 0.0 ) ) ) * ( 1.0 / ( screenPos.z + 1.0 ) ) ) * ( 1.0 - dot( worldNormal, worldViewDir ) ) );
			float2 cameraRefraction = float2( refractionOffset.x, -( refractionOffset.y * _ProjectionParams.x ) );
			float4 redAlpha = tex2D( _GrabTexture, ( projScreenPos + cameraRefraction ) );
			float green = tex2D( _GrabTexture, ( projScreenPos + ( cameraRefraction * ( 1.0 - chomaticAberration ) ) ) ).g;
			float blue = tex2D( _GrabTexture, ( projScreenPos + ( cameraRefraction * ( 1.0 + chomaticAberration ) ) ) ).b;
			return float4( redAlpha.r, green, blue, redAlpha.a );
		}

		void RefractionF( Input i, SurfaceOutputStandard o, inout fixed4 color )
		{
			#ifdef UNITY_PASS_FORWARDBASE
			float2 uv_TexCoord12 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 panner14 = ( uv_TexCoord12 + 0.01 * _Time.y * float2( 1,1 ));
			float4 tex2DNode15 = tex2D( _Normal, panner14 );
			float blendOpSrc16 = 0;
			float blendOpDest16 = tex2DNode15.g;
			color.rgb = color.rgb + Refraction( i, o, ( saturate( ( 1.0 - ( 1.0 - blendOpSrc16 ) * ( 1.0 - blendOpDest16 ) ) )), _ChromaticAberration ) * ( 1 - color.a );
			color.a = 1;
			#endif
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = float3(0,0,1);
			float2 uv_TexCoord12 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 panner13 = ( uv_TexCoord12 + 0.01 * _Time.y * float2( 2,-1 ));
			float2 panner14 = ( uv_TexCoord12 + 0.01 * _Time.y * float2( 1,1 ));
			float4 tex2DNode15 = tex2D( _Normal, panner14 );
			o.Normal = BlendNormals( UnpackNormal( tex2D( _Normal, panner13 ) ) , tex2DNode15.rgb );
			float2 temp_cast_1 = (pow( tex2DNode15.g , 0.2 )).xx;
			float2 uv_TexCoord7 = i.uv_texcoord * float2( 1,1 ) + temp_cast_1;
			float2 panner8 = ( uv_TexCoord7 + -0.02 * _Time.y * float2( 1,1 ));
			float2 temp_cast_2 = (tex2DNode15.g).xx;
			float2 uv_TexCoord24 = i.uv_texcoord * float2( 3,3 ) + temp_cast_2;
			float2 panner6 = ( uv_TexCoord24 + 0.02 * _Time.y * float2( 2,-1 ));
			float4 blendOpSrc9 = tex2D( _Texture0, panner8 );
			float4 blendOpDest9 = tex2D( _Texture0, panner6 );
			float4 temp_output_27_0 = ( ( _BaseEmission + ( saturate( ( blendOpSrc9 + blendOpDest9 ) )) ) * _Color );
			o.Albedo = temp_output_27_0.rgb;
			o.Emission = ( pow( temp_output_27_0 , 2.0 ) * _EmissionStrength ).rgb;
			o.Smoothness = 0.0;
			o.Alpha = _Opacity;
			o.Normal = o.Normal + 0.00001 * i.screenPos * i.worldPos;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha finalcolor:RefractionF fullforwardshadows exclude_path:deferred 

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
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				float4 tSpace0 : TEXCOORD4;
				float4 tSpace1 : TEXCOORD5;
				float4 tSpace2 : TEXCOORD6;
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
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
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
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.screenPos = IN.screenPos;
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
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14201
8;100;1107;686;189.1927;49.7236;1;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-2915.819,1098.609;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;10;-2719.791,801.5858;Float;True;Property;_Normal;Normal;1;0;Create;68bd474dc8be89a43a60df151f0314e9;68bd474dc8be89a43a60df151f0314e9;True;bump;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;14;-2513.045,1235.63;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;0.01;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;15;-2216.752,1065.826;Float;True;Property;_TextureSample3;Texture Sample 3;1;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;26;-2276.984,-239.0443;Float;True;2;0;FLOAT;0.0;False;1;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;24;-1917.368,3.900693;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;3,3;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-1913.609,-218.2902;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;2;-1444.066,-485.0562;Float;True;Property;_Texture0;Texture 0;2;0;Create;00cc3a2bfa769fa439233d3edcd59043;00cc3a2bfa769fa439233d3edcd59043;False;white;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;8;-1411.578,-184.405;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;-0.02;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;6;-1427.138,-5.899508;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;2,-1;False;1;FLOAT;0.02;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;3;-925.8606,-469.2544;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-942.7132,-178.177;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendOpsNode;9;-383.6963,29.10207;Float;True;LinearDodge;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-229.7415,-185.8093;Float;False;Property;_BaseEmission;Base Emission;6;0;Create;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;28;55.78804,-290.9044;Float;False;Property;_Color;Color;5;0;Create;1,1,1,0.003921569;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-41.77734,-73.91608;Float;False;2;2;0;FLOAT;0,0,0,0;False;1;COLOR;0.0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;233.7378,-142.6206;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;13;-2531.347,1065.061;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;2,-1;False;1;FLOAT;0.01;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;11;-2226.952,774.9641;Float;True;Property;_TextureSample2;Texture Sample 2;1;0;Create;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;23;280.0362,170.783;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;2.0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-124.8162,334.1982;Float;False;Property;_EmissionStrength;Emission Strength;3;0;Create;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;16;-232.9622,891.3948;Float;False;Screen;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;204.5893,279.4518;Float;False;Property;_Opacity;Opacity;4;0;Create;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;454.0493,166.3161;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendNormalsNode;17;-359.393,560.172;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;29;278.0966,448.6599;Float;False;Constant;_Smoothness;Smoothness;7;0;Create;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;792.3746,52.83894;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Triplebrick/Fluid;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;0;False;0;0;Transparent;0.5;True;True;0;False;Transparent;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;2;SrcAlpha;OneMinusSrcAlpha;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;0;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;0;12;0
WireConnection;15;0;10;0
WireConnection;15;1;14;0
WireConnection;26;0;15;2
WireConnection;24;1;15;2
WireConnection;7;1;26;0
WireConnection;8;0;7;0
WireConnection;6;0;24;0
WireConnection;3;0;2;0
WireConnection;3;1;8;0
WireConnection;5;0;2;0
WireConnection;5;1;6;0
WireConnection;9;0;3;0
WireConnection;9;1;5;0
WireConnection;30;0;31;0
WireConnection;30;1;9;0
WireConnection;27;0;30;0
WireConnection;27;1;28;0
WireConnection;13;0;12;0
WireConnection;11;0;10;0
WireConnection;11;1;13;0
WireConnection;23;0;27;0
WireConnection;16;1;15;2
WireConnection;21;0;23;0
WireConnection;21;1;22;0
WireConnection;17;0;11;0
WireConnection;17;1;15;0
WireConnection;0;0;27;0
WireConnection;0;1;17;0
WireConnection;0;2;21;0
WireConnection;0;4;29;0
WireConnection;0;8;16;0
WireConnection;0;9;20;0
ASEEND*/
//CHKSM=F27E5DF1FE5A909AB0C1B340068B3551FC4CC65B