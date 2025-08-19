// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Triplebrick/Cutout_Single_RGB"
{
	Properties
	{
		_BaseColor_Alpha("BaseColor_Alpha", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_TintColor("Tint Color", Color) = (0.5882353,0.5882353,0.5882353,0)
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_WearAmount("Wear Amount", Range( 0 , 1)) = 0
		_DirtAmount("Dirt Amount", Range( 0 , 1)) = 0
		_ScratchesAmount("Scratches Amount", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" }
		Cull Back
		Blend One Zero , SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _BaseColor_Alpha;
		uniform float4 _BaseColor_Alpha_ST;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float _DirtAmount;
		uniform float4 _TintColor;
		uniform float _ScratchesAmount;
		uniform float _WearAmount;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_BaseColor_Alpha = i.uv_texcoord * _BaseColor_Alpha_ST.xy + _BaseColor_Alpha_ST.zw;
			float4 tex2DNode24 = tex2D( _BaseColor_Alpha, uv_BaseColor_Alpha );
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode2 = tex2D( _Mask, uv_Mask );
			float4 lerpResult13 = lerp( tex2DNode24 , ( tex2DNode24 * tex2DNode2.g ) , _DirtAmount);
			float4 temp_output_12_0 = ( lerpResult13 * _TintColor );
			o.Albedo = temp_output_12_0.rgb;
			o.Metallic = 0.0;
			o.Alpha = 1;
			float lerpResult18 = lerp( 1.0 , tex2DNode2.r , _ScratchesAmount);
			float lerpResult10 = lerp( tex2DNode2.a , tex2DNode2.b , _WearAmount);
			clip( ( lerpResult18 * lerpResult10 ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14201
8;100;1282;639;1518.056;477.4766;1.318698;True;False
Node;AmplifyShaderEditor.SamplerNode;2;-900.6737,-35.1236;Float;True;Property;_Mask;Mask;1;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;24;-1301.261,-725.3196;Float;True;Property;_BaseColor_Alpha;BaseColor_Alpha;0;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-1175.857,-198.9548;Float;False;Property;_DirtAmount;Dirt Amount;5;0;Create;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-747.3376,-390.5056;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-481.6869,343.7408;Float;False;Constant;_Float0;Float 0;6;0;Create;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-568.298,194.0022;Float;False;Property;_WearAmount;Wear Amount;4;0;Create;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-628.5518,452.6429;Float;False;Property;_ScratchesAmount;Scratches Amount;6;0;Create;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;13;-400.9303,-467.0428;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;18;-189.164,392.167;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;10;-180.0892,129.8534;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;4;-364.5142,-827.569;Float;False;Property;_TintColor;Tint Color;2;0;Create;0.5882353,0.5882353,0.5882353,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;21;226.7647,92.36475;Float;False;Constant;_Float1;Float 1;6;0;Create;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-167.2943,-566.1464;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;48.25391,185.9686;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;23;13.74307,-179.6652;Float;False;Overlay;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenColorNode;22;-334.3712,-224.9408;Float;False;Global;_GrabScreen0;Grab Screen 0;6;0;Create;Object;-1;False;True;1;0;FLOAT4;0,0,0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;463.2,-52.67285;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Triplebrick/Cutout_Single_RGB;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;0;False;-6;0;Custom;0.5;True;True;0;True;Opaque;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;One;OneMinusSrcAlpha;2;SrcAlpha;OneMinusSrcAlpha;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;3;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;25;0;24;0
WireConnection;25;1;2;2
WireConnection;13;0;24;0
WireConnection;13;1;25;0
WireConnection;13;2;15;0
WireConnection;18;0;20;0
WireConnection;18;1;2;1
WireConnection;18;2;19;0
WireConnection;10;0;2;4
WireConnection;10;1;2;3
WireConnection;10;2;11;0
WireConnection;12;0;13;0
WireConnection;12;1;4;0
WireConnection;17;0;18;0
WireConnection;17;1;10;0
WireConnection;23;0;22;0
WireConnection;23;1;12;0
WireConnection;0;0;12;0
WireConnection;0;3;21;0
WireConnection;0;10;17;0
ASEEND*/
//CHKSM=AE45B7C5476F7C7E8D56CBD9AC0A7565E828A97E