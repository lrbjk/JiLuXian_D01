// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Triplebrick/Base_Normal_Replace"
{
	Properties
	{
		_BaseNormal("Base Normal", 2D) = "white" {}
		_DetailNormal("Detail Normal", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_BaseColor("Base Color", Color) = (0.6544118,0.6544118,0.6544118,0)
		_BaseColorOverlay("Base Color Overlay", Color) = (0.6544118,0.6544118,0.6544118,0)
		_BaseDirtColor("Base Dirt Color", Color) = (0,0,0,0)
		_BaseNormalStrength("Base Normal Strength", Range( 0 , 1)) = 0
		_BaseSmoothness("Base Smoothness", Range( 0 , 1)) = 0.5
		_BaseDirtStrength("Base Dirt Strength", Range( 0.001 , 3)) = 0
		_BaseMetallic("Base Metallic", Range( 0 , 1)) = 0
		_DetailDirtStrength("Detail Dirt Strength", Range( 0 , 1)) = 0
		_DetailEdgeWear("Detail Edge Wear", Range( 0 , 1)) = 0
		_TrimSmoothness("Trim Smoothness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _texcoord4( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
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
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float _BaseDirtStrength;
		uniform float4 _BaseColor;
		uniform float4 _BaseColorOverlay;
		uniform float _DetailDirtStrength;
		uniform float _DetailEdgeWear;
		uniform float _BaseMetallic;
		uniform float _TrimSmoothness;
		uniform float _BaseSmoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_DetailNormal = i.uv_texcoord * _DetailNormal_ST.xy + _DetailNormal_ST.zw;
			float3 tex2DNode3 = UnpackNormal( tex2D( _DetailNormal, uv_DetailNormal ) );
			float2 uv4_BaseNormal = i.uv4_texcoord4 * _BaseNormal_ST.xy + _BaseNormal_ST.zw;
			float3 lerpResult11 = lerp( float3(0,0,1) , UnpackNormal( tex2D( _BaseNormal, uv4_BaseNormal ) ) , _BaseNormalStrength);
			float2 uv_TexCoord104 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float clampResult111 = clamp( ( uv_TexCoord104.x + -0.8 ) , 0.0 , 1.0 );
			float clampResult112 = clamp( ( uv_TexCoord104.y + -0.8 ) , 0.0 , 1.0 );
			float temp_output_109_0 = ( ceil( clampResult111 ) * ceil( clampResult112 ) );
			float3 lerpResult115 = lerp( tex2DNode3 , lerpResult11 , temp_output_109_0);
			o.Normal = lerpResult115;
			float2 uv4_Mask = i.uv4_texcoord4 * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode35 = tex2D( _Mask, uv4_Mask );
			float clampResult77 = clamp( pow( tex2DNode35.g , _BaseDirtStrength ) , 0.0 , 1.0 );
			float4 lerpResult76 = lerp( _BaseDirtColor , float4( 1,1,1,0 ) , clampResult77);
			float2 uv4_TexCoord70 = i.uv4_texcoord4 * float2( 2,2 ) + float2( 0,0 );
			float4 lerpResult71 = lerp( _BaseColor , _BaseColorOverlay , tex2D( _Mask, uv4_TexCoord70 ).g);
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode36 = tex2D( _Mask, uv_Mask );
			float temp_output_120_0 = ceil( ( tex2DNode36.b + -0.8 ) );
			float4 lerpResult123 = lerp( lerpResult71 , float4( float3(1,0.95,0.9) , 0.0 ) , temp_output_120_0);
			float clampResult49 = clamp( ( tex2DNode36.r + 0.55 ) , 0.0 , 1.0 );
			float4 lerpResult92 = lerp( lerpResult123 , ( lerpResult123 * pow( clampResult49 , 6.0 ) ) , _DetailDirtStrength);
			float clampResult62 = clamp( ( ( tex2DNode36.r + -0.55 ) * 5.0 ) , 0.0 , 1.0 );
			float4 clampResult101 = clamp( ( clampResult62 + lerpResult92 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 lerpResult32 = lerp( lerpResult92 , clampResult101 , _DetailEdgeWear);
			o.Albedo = ( lerpResult76 * lerpResult32 ).rgb;
			float lerpResult94 = lerp( 0.0 , clampResult62 , _DetailEdgeWear);
			float clampResult97 = clamp( ( lerpResult94 + ( temp_output_120_0 + _BaseMetallic ) ) , 0.0 , 1.0 );
			o.Metallic = clampResult97;
			float4 temp_cast_2 = (_TrimSmoothness).xxxx;
			float4 temp_output_65_0 = ( ( tex2DNode35.a * lerpResult76 ) * _BaseSmoothness );
			float4 lerpResult125 = lerp( temp_cast_2 , temp_output_65_0 , temp_output_109_0);
			o.Smoothness = lerpResult125.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13901
8;100;1349;639;1794.852;274.544;1.538592;True;False
Node;AmplifyShaderEditor.CommentaryNode;18;-2981.029,-1036.012;Float;False;537.735;983.6135;R = Cavitiy, G = Dirtmap, B = Color Mask Details, A = Roughness;3;69;27;26;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;37;-3304.656,-807.4417;Float;True;Property;_Mask;Mask;2;0;Assets/Scifi/Textures/TrimSheet_basecolor.tga;False;white;Auto;0;1;SAMPLER2D
Node;AmplifyShaderEditor.CommentaryNode;27;-2912.815,-673.2319;Float;False;371;280;uv4;1;36;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;36;-2881.25,-629.6738;Float;True;Property;_TextureSample1;Texture Sample 1;7;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;70;-3362.161,-248.7759;Float;False;3;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2,2;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;50;-2226.053,-493.5641;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.55;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;69;-2861.956,-341.9267;Float;True;Property;_TextureSample2;Texture Sample 2;7;0;None;True;3;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;118;-1882.571,513.1802;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;-0.8;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;72;-3003.494,464.0777;Float;False;Property;_BaseColorOverlay;Base Color Overlay;4;0;0.6544118,0.6544118,0.6544118,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;8;-2993.153,254.2653;Float;False;Property;_BaseColor;Base Color;3;0;0.6544118,0.6544118,0.6544118,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;49;-2072.189,-487.7048;Float;False;3;0;FLOAT;0,0,0,0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;71;-2458.467,275.4841;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.CeilOpNode;120;-1731.585,497.4177;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;26;-2910.605,-966.7463;Float;False;371;280;uv0;1;35;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector3Node;124;-2253.979,-96.64314;Float;False;Constant;_MetalTrimColo3;MetalTrimColo3;15;0;1,0.95,0.9;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;51;-1899.193,-566.8974;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;-0.55;False;1;FLOAT
Node;AmplifyShaderEditor.PowerNode;103;-1884.359,-337.9826;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;6.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;123;-2111.642,149.8729;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SamplerNode;35;-2867.715,-898.7737;Float;True;Property;_TextureSample0;Texture Sample 0;7;0;None;True;3;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;21;-1825.482,-1086.847;Float;False;Property;_BaseDirtStrength;Base Dirt Strength;8;0;0;0.001;3;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;99;-1647.851,-539.8504;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;5.0;False;1;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;104;-2233.729,1844.064;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;93;-1709.371,39.12919;Float;False;Property;_DetailDirtStrength;Detail Dirt Strength;10;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-1681.178,-329.5637;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.PowerNode;74;-1533.299,-804.511;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;92;-1550.481,-187;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleAddOpNode;108;-1918.21,1844.82;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;-0.8;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;107;-1925.507,1957.204;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;-0.8;False;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;62;-1504.473,-486.1902;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;77;-1339.607,-746.6707;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;75;-1364.064,-1160.842;Float;False;Property;_BaseDirtColor;Base Dirt Color;5;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;78;-2223.215,711.7867;Float;False;Property;_BaseMetallic;Base Metallic;9;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;112;-1772.688,1948.212;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;33;-1382.548,84.26886;Float;False;Property;_DetailEdgeWear;Detail Edge Wear;12;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;111;-1762.038,1704.703;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;46;-1299.281,-406.7545;Float;False;2;2;0;FLOAT;0,0,0,0;False;1;COLOR;0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;76;-1209.608,-885.7709;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;1,1,1,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.CeilOpNode;114;-1622.688,1908.212;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;101;-1131.909,-418.7224;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR
Node;AmplifyShaderEditor.Vector3Node;10;-2528.757,1410.333;Float;False;Constant;_Vector0;Vector 0;3;0;0,0,1;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.CeilOpNode;113;-1580.688,1754.212;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;12;-2556.354,1703.328;Float;False;Property;_BaseNormalStrength;Base Normal Strength;6;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;117;-1589.989,602.8667;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;2;-3041.785,1516.055;Float;True;Property;_BaseNormal;Base Normal;0;0;None;True;3;True;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-880.2324,-37.65833;Float;False;2;2;0;FLOAT;0.0;False;1;COLOR;0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;40;-1540.82,284.4577;Float;False;Property;_BaseSmoothness;Base Smoothness;7;0;0.5;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;94;-1065.312,347.0742;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;32;-843.7327,-307.658;Float;False;3;0;COLOR;0.0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;-1495.678,1879.227;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;96;-843.4309,470.1993;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;3;-3014.076,1273.918;Float;True;Property;_DetailNormal;Detail Normal;1;0;None;True;0;True;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-652.3083,-38.56518;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;11;-2260.987,1516.026;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.RangedFloatNode;126;-956.3717,1319.927;Float;False;Property;_TrimSmoothness;Trim Smoothness;15;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;38;521.5351,-714.0347;Float;False;Constant;_DetailRoughness;Detail Roughness;8;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.BlendNormalsNode;6;-1409.035,1314.675;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-507.6571,-343.7149;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;121;-475.8222,94.96035;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;125;-124.7592,-29.54561;Float;False;3;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.ClampOpNode;97;-654.1694,456.7981;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;122;-667.9778,-185.7681;Float;False;Property;_MetalTrimSmoothness;Metal Trim Smoothness;14;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;39;208.0109,-674.2496;Float;False;Property;_DetailRoughnessContrast;Detail Roughness Contrast;13;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;115;-1403.074,1076.824;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.RangedFloatNode;95;-1064.986,687.4885;Float;False;Property;_DetailEdgeMetallic;Detail Edge Metallic;11;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;237.0005,-147.1033;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Triplebrick/Base_Normal_Replace;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;36;0;37;0
WireConnection;50;0;36;1
WireConnection;69;0;37;0
WireConnection;69;1;70;0
WireConnection;118;0;36;3
WireConnection;49;0;50;0
WireConnection;71;0;8;0
WireConnection;71;1;72;0
WireConnection;71;2;69;2
WireConnection;120;0;118;0
WireConnection;51;0;36;1
WireConnection;103;0;49;0
WireConnection;123;0;71;0
WireConnection;123;1;124;0
WireConnection;123;2;120;0
WireConnection;35;0;37;0
WireConnection;99;0;51;0
WireConnection;45;0;123;0
WireConnection;45;1;103;0
WireConnection;74;0;35;2
WireConnection;74;1;21;0
WireConnection;92;0;123;0
WireConnection;92;1;45;0
WireConnection;92;2;93;0
WireConnection;108;0;104;1
WireConnection;107;0;104;2
WireConnection;62;0;99;0
WireConnection;77;0;74;0
WireConnection;112;0;107;0
WireConnection;111;0;108;0
WireConnection;46;0;62;0
WireConnection;46;1;92;0
WireConnection;76;0;75;0
WireConnection;76;2;77;0
WireConnection;114;0;112;0
WireConnection;101;0;46;0
WireConnection;113;0;111;0
WireConnection;117;0;120;0
WireConnection;117;1;78;0
WireConnection;66;0;35;4
WireConnection;66;1;76;0
WireConnection;94;1;62;0
WireConnection;94;2;33;0
WireConnection;32;0;92;0
WireConnection;32;1;101;0
WireConnection;32;2;33;0
WireConnection;109;0;113;0
WireConnection;109;1;114;0
WireConnection;96;0;94;0
WireConnection;96;1;117;0
WireConnection;65;0;66;0
WireConnection;65;1;40;0
WireConnection;11;0;10;0
WireConnection;11;1;2;0
WireConnection;11;2;12;0
WireConnection;6;0;11;0
WireConnection;6;1;3;0
WireConnection;52;0;76;0
WireConnection;52;1;32;0
WireConnection;121;0;65;0
WireConnection;121;1;122;0
WireConnection;121;2;120;0
WireConnection;125;0;126;0
WireConnection;125;1;65;0
WireConnection;125;2;109;0
WireConnection;97;0;96;0
WireConnection;115;0;3;0
WireConnection;115;1;11;0
WireConnection;115;2;109;0
WireConnection;0;0;52;0
WireConnection;0;1;115;0
WireConnection;0;3;97;0
WireConnection;0;4;125;0
ASEEND*/
//CHKSM=0DF2C2261602BD17B7A0D4C4101DFDA036EBE5B5