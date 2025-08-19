// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Triplebrick/Base_Normal_Blend"
{
	Properties
	{
		_BaseNormal("Base Normal", 2D) = "white" {}
		_DirtRoughness("Dirt Roughness", 2D) = "white" {}
		_DetailNormal("Detail Normal", 2D) = "white" {}
		_DetailMask("Detail Mask", 2D) = "white" {}
		_BaseColor("Base Color", Color) = (0.6544118,0.6544118,0.6544118,0)
		_BaseColorOverlay("Base Color Overlay", Color) = (0.6544118,0.6544118,0.6544118,0)
		_BaseDirtColor("Base Dirt Color", Color) = (0,0,0,0)
		_DetailColor("Detail Color", Color) = (0,0,0,0)
		_BaseNormalStrength("Base Normal Strength", Range( 0 , 1)) = 0
		_BaseSmoothness("Base Smoothness", Range( 0 , 1)) = 0.5
		_BaseDirtStrength("Base Dirt Strength", Range( 0.001 , 3)) = 0
		_BaseMetallic("Base Metallic", Range( 0 , 1)) = 0
		_DetailEdgeWear("Detail Edge Wear", Range( 0 , 1)) = 0
		_DetailEdgeSmoothness("Detail Edge Smoothness", Range( 0 , 1)) = 0
		_DetailDirtStrength("Detail Dirt Strength", Range( 0 , 1)) = 0
		_DetailOcclusionStrength("Detail Occlusion Strength", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord4( "", 2D ) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float2 uv4_texcoord4;
		};

		uniform sampler2D _DetailNormal;
		uniform float4 _DetailNormal_ST;
		uniform sampler2D _BaseNormal;
		uniform float4 _BaseNormal_ST;
		uniform float _BaseNormalStrength;
		uniform float4 _BaseDirtColor;
		uniform sampler2D _DirtRoughness;
		uniform float4 _DirtRoughness_ST;
		uniform float _BaseDirtStrength;
		uniform float4 _BaseColor;
		uniform float4 _BaseColorOverlay;
		uniform float4 _DetailColor;
		uniform sampler2D _DetailMask;
		uniform float4 _DetailMask_ST;
		uniform float _DetailDirtStrength;
		uniform float _DetailEdgeWear;
		uniform float _BaseMetallic;
		uniform float _BaseSmoothness;
		uniform float _DetailEdgeSmoothness;
		uniform float _DetailOcclusionStrength;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_DetailNormal = i.uv_texcoord * _DetailNormal_ST.xy + _DetailNormal_ST.zw;
			float3 tex2DNode3 = UnpackNormal( tex2D( _DetailNormal, uv_DetailNormal ) );
			float2 uv4_BaseNormal = i.uv4_texcoord4 * _BaseNormal_ST.xy + _BaseNormal_ST.zw;
			float3 lerpResult11 = lerp( float3(0,0,1) , UnpackNormal( tex2D( _BaseNormal, uv4_BaseNormal ) ) , _BaseNormalStrength);
			o.Normal = BlendNormals( tex2DNode3 , lerpResult11 );
			float2 uv4_DirtRoughness = i.uv4_texcoord4 * _DirtRoughness_ST.xy + _DirtRoughness_ST.zw;
			float4 tex2DNode143 = tex2D( _DirtRoughness, uv4_DirtRoughness );
			float clampResult77 = clamp( pow( tex2DNode143.g , _BaseDirtStrength ) , 0.0 , 1.0 );
			float4 lerpResult76 = lerp( _BaseDirtColor , float4( 1,1,1,0 ) , clampResult77);
			float4 lerpResult71 = lerp( _BaseColor , _BaseColorOverlay , tex2DNode143.r);
			float2 uv_DetailMask = i.uv_texcoord * _DetailMask_ST.xy + _DetailMask_ST.zw;
			float4 tex2DNode36 = tex2D( _DetailMask, uv_DetailMask );
			float4 lerpResult134 = lerp( lerpResult71 , _DetailColor , ceil( ( ( 1.0 - tex2DNode36.b ) + -0.95 ) ));
			float temp_output_120_0 = ceil( ( tex2DNode36.b + -0.8 ) );
			float4 lerpResult123 = lerp( lerpResult134 , float4( float3(1,0.95,0.9) , 0.0 ) , temp_output_120_0);
			float clampResult49 = clamp( pow( ( tex2DNode36.r + 0.55 ) , 6.0 ) , 0.0 , 1.0 );
			float4 lerpResult92 = lerp( lerpResult123 , ( lerpResult123 * clampResult49 ) , _DetailDirtStrength);
			float clampResult62 = clamp( ( ( tex2DNode36.r + -0.55 ) * 2.0 ) , 0.0 , 1.0 );
			float4 clampResult101 = clamp( ( clampResult62 + lerpResult92 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 lerpResult32 = lerp( lerpResult92 , clampResult101 , _DetailEdgeWear);
			o.Albedo = ( lerpResult76 * lerpResult32 ).rgb;
			float lerpResult94 = lerp( 0.0 , clampResult62 , _DetailEdgeWear);
			float clampResult97 = clamp( ( lerpResult94 + temp_output_120_0 ) , 0.0 , 1.0 );
			float clampResult145 = clamp( ( clampResult97 + _BaseMetallic ) , 0.0 , 1.0 );
			o.Metallic = clampResult145;
			float4 temp_cast_2 = (_DetailEdgeSmoothness).xxxx;
			float4 lerpResult121 = lerp( ( ( max( clampResult62 , tex2DNode143.a ) * lerpResult76 ) * _BaseSmoothness ) , temp_cast_2 , clampResult97);
			o.Smoothness = lerpResult121.r;
			float lerpResult138 = lerp( 1.0 , tex2DNode36.g , _DetailOcclusionStrength);
			o.Occlusion = lerpResult138;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14201
6;93;1130;680;1377.501;282.2423;1.578804;False;False
Node;AmplifyShaderEditor.CommentaryNode;18;-3446.678,-1132.657;Float;False;515.9005;765.2666;R = Cavitiy, G = Dirtmap, B = Color Mask Details, A = Roughness;2;26;27;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;27;-3378.464,-769.8761;Float;False;371;280;uv4;1;36;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;36;-3346.899,-727.3181;Float;True;Property;_DetailMask;Detail Mask;3;0;Create;None;4a3848591ce935e4dad9a07fc6696f83;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;130;-2928.12,-250.9236;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;26;-3376.254,-1063.391;Float;False;371;280;uv0;1;143;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;72;-3394.463,442.1131;Float;False;Property;_BaseColorOverlay;Base Color Overlay;5;0;Create;0.6544118,0.6544118,0.6544118,0;0.9294118,0.9411765,0.9490196,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;8;-3384.122,232.3007;Float;False;Property;_BaseColor;Base Color;4;0;Create;0.6544118,0.6544118,0.6544118,0;0.9490196,0.945098,0.9294118,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;131;-2728.081,-271.6658;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;-0.95;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;143;-3339.083,-1003.106;Float;True;Property;_DirtRoughness;Dirt Roughness;1;0;Create;None;9ee49387df9326442af2587f7e7327c3;True;3;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;71;-2757.186,328.1992;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;135;-2727.841,-120.0552;Float;False;Property;_DetailColor;Detail Color;7;0;Create;0,0,0,0;0.3215685,0.3215685,0.3215685,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;50;-2436.12,-512.9489;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.55;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;118;-2195.756,499.6233;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;-0.8;False;1;FLOAT;0
Node;AmplifyShaderEditor.CeilOpNode;133;-2596.711,-254.2214;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;134;-2470.921,75.57092;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;127;-2250.55,-487.9212;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;6.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CeilOpNode;120;-2036.613,500.8069;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;124;-2305.558,-92.16446;Float;False;Constant;_MetalTrimColor;MetalTrimColor;15;0;Create;1,0.95,0.9;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;51;-1899.193,-566.8974;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;-0.55;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;123;-2092.614,134.7373;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;49;-2178.536,-328.6187;Float;True;3;0;FLOAT;0,0,0,0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;99;-1647.851,-541.9295;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;2.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1825.482,-1086.847;Float;False;Property;_BaseDirtStrength;Base Dirt Strength;10;0;Create;0;0.01;0.001;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-1681.178,-329.5637;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;93;-1709.371,39.12919;Float;False;Property;_DetailDirtStrength;Detail Dirt Strength;14;0;Create;0;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;74;-1533.299,-804.511;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;62;-1504.473,-486.1902;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-1382.548,84.26886;Float;False;Property;_DetailEdgeWear;Detail Edge Wear;12;0;Create;0;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;92;-1550.481,-187;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;75;-1364.064,-1160.842;Float;False;Property;_BaseDirtColor;Base Dirt Color;6;0;Create;0,0,0,0;0.2039215,0.1647058,0.07450978,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;94;-1296.066,290.8427;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;77;-1339.607,-746.6707;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;46;-1299.281,-406.7545;Float;False;2;2;0;FLOAT;0,0,0,0;False;1;COLOR;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;76;-1209.608,-885.7709;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;1,1,1,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;129;-1135.764,-102.5898;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;96;-1216.755,522.9928;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;10;-2528.757,1410.333;Float;False;Constant;_Vector0;Vector 0;3;0;Create;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ClampOpNode;101;-1131.909,-418.7224;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-3041.785,1516.055;Float;True;Property;_BaseNormal;Base Normal;0;0;Create;None;7d09e7c7fffa5a94eadbfb28633eaa1e;True;3;True;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-880.2324,-37.65833;Float;False;2;2;0;FLOAT;0.0;False;1;COLOR;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;97;-930.2104,482.7545;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-1045.729,75.21457;Float;False;Property;_BaseSmoothness;Base Smoothness;9;0;Create;0.5;0.75;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-2556.354,1703.328;Float;False;Property;_BaseNormalStrength;Base Normal Strength;8;0;Create;0;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;78;-1418.552,817.4737;Float;False;Property;_BaseMetallic;Base Metallic;11;0;Create;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;122;-667.9778,-185.7681;Float;False;Property;_DetailEdgeSmoothness;Detail Edge Smoothness;13;0;Create;0;0.74;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-652.3083,-38.56518;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;32;-843.7327,-307.658;Float;False;3;0;COLOR;0.0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;11;-2260.987,1516.026;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;137;-361.1563,453.2509;Float;False;Property;_DetailOcclusionStrength;Detail Occlusion Strength;16;0;Create;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;117;-673.8782,477.7081;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-3014.076,1273.918;Float;True;Property;_DetailNormal;Detail Normal;2;0;Create;None;1c1221435ae8dd740802e4127090b7fa;True;0;True;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;141;-2630.118,1079.372;Float;False;Constant;_Color0;Color 0;17;0;Create;0.5036319,0.4980392,1,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CeilOpNode;114;-1622.688,1908.212;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;108;-1918.21,1844.82;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;-0.937;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;111;-1762.038,1704.703;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendNormalsNode;6;-1409.035,1314.675;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;145;-416.4275,105.1458;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;112;-1772.688,1948.212;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;107;-1925.507,1957.204;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;-0.937;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;208.0109,-674.2496;Float;False;Property;_DetailRoughnessContrast;Detail Roughness Contrast;15;0;Create;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;521.5351,-714.0347;Float;False;Constant;_DetailRoughness;Detail Roughness;8;0;Create;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;-1495.678,1879.227;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;121;-428.1258,-35.81998;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CeilOpNode;113;-1580.688,1754.212;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;104;-2233.729,1844.064;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;138;-29.25116,346.7925;Float;False;3;0;FLOAT;1.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-507.6571,-343.7149;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;115;-1674.393,1046.433;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;135.1354,-137.3909;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Triplebrick/Base_Normal_Blend;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;130;0;36;3
WireConnection;131;0;130;0
WireConnection;71;0;8;0
WireConnection;71;1;72;0
WireConnection;71;2;143;1
WireConnection;50;0;36;1
WireConnection;118;0;36;3
WireConnection;133;0;131;0
WireConnection;134;0;71;0
WireConnection;134;1;135;0
WireConnection;134;2;133;0
WireConnection;127;0;50;0
WireConnection;120;0;118;0
WireConnection;51;0;36;1
WireConnection;123;0;134;0
WireConnection;123;1;124;0
WireConnection;123;2;120;0
WireConnection;49;0;127;0
WireConnection;99;0;51;0
WireConnection;45;0;123;0
WireConnection;45;1;49;0
WireConnection;74;0;143;2
WireConnection;74;1;21;0
WireConnection;62;0;99;0
WireConnection;92;0;123;0
WireConnection;92;1;45;0
WireConnection;92;2;93;0
WireConnection;94;1;62;0
WireConnection;94;2;33;0
WireConnection;77;0;74;0
WireConnection;46;0;62;0
WireConnection;46;1;92;0
WireConnection;76;0;75;0
WireConnection;76;2;77;0
WireConnection;129;0;62;0
WireConnection;129;1;143;4
WireConnection;96;0;94;0
WireConnection;96;1;120;0
WireConnection;101;0;46;0
WireConnection;66;0;129;0
WireConnection;66;1;76;0
WireConnection;97;0;96;0
WireConnection;65;0;66;0
WireConnection;65;1;40;0
WireConnection;32;0;92;0
WireConnection;32;1;101;0
WireConnection;32;2;33;0
WireConnection;11;0;10;0
WireConnection;11;1;2;0
WireConnection;11;2;12;0
WireConnection;117;0;97;0
WireConnection;117;1;78;0
WireConnection;114;0;112;0
WireConnection;108;0;104;1
WireConnection;111;0;108;0
WireConnection;6;0;3;0
WireConnection;6;1;11;0
WireConnection;145;0;117;0
WireConnection;112;0;107;0
WireConnection;107;0;104;2
WireConnection;109;0;113;0
WireConnection;109;1;114;0
WireConnection;121;0;65;0
WireConnection;121;1;122;0
WireConnection;121;2;97;0
WireConnection;113;0;111;0
WireConnection;138;1;36;2
WireConnection;138;2;137;0
WireConnection;52;0;76;0
WireConnection;52;1;32;0
WireConnection;115;0;3;0
WireConnection;115;1;11;0
WireConnection;115;2;109;0
WireConnection;0;0;52;0
WireConnection;0;1;6;0
WireConnection;0;3;145;0
WireConnection;0;4;121;0
WireConnection;0;5;138;0
ASEEND*/
//CHKSM=FEB37468967B9B0532A992336085F187502A6D35