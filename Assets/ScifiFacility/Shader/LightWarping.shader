// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LightWrapping"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Color("Color", Color) = (0,0,0,0)
		_Specular("Specular", Color) = (0.1544118,0.1544118,0.1544118,0)
		_Smoothness("Smoothness", Range( 0 , 1)) = 0.5
		_LightWarp("LightWarp", Range( 0 , 2)) = 1
		_LightWarpColor("LightWarpColor", Color) = (1,1,1,0)
		_UseColorRamp("UseColorRamp", Range( 0 , 1)) = 0
		_ColorRump("ColorRump", 2D) = "white" {}
		_Flakers("Flakers", 2D) = "white" {}
		_AddFlakers("AddFlakers", Range( 0 , 1)) = 0.2
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		ZTest LEqual
		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma exclude_renderers xbox360 xboxone ps4 psp2 n3ds wiiu 
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldPos;
			float2 uv_Flakers;
		};

		uniform fixed4 _Color;
		uniform fixed _LightWarp;
		uniform sampler2D _ColorRump;
		uniform sampler2D _Flakers;
		uniform fixed _AddFlakers;
		uniform fixed _UseColorRamp;
		uniform fixed4 _LightWarpColor;
		uniform fixed4 _Specular;
		uniform fixed _Smoothness;

		void surf( Input input , inout SurfaceOutputStandardSpecular output )
		{
			output.Albedo = _Color.rgb;
			float temp_output_74_0 = ( _LightWarp * 0.5 );
			float temp_output_79_0 = max( 0.0 , ( ( dot( WorldNormalVector( input , output.Normal ) , WorldSpaceLightDir( fixed4( input.worldPos, 0) ) ) * ( 1.0 - temp_output_74_0 ) ) + temp_output_74_0 ) );
			fixed4 temp_cast_2 = temp_output_79_0;
			float2 temp_output_127_0 = float2( temp_output_79_0 , 0 );
			output.Emission = max( float4( 0,0,0,0 ) , ( lerp( temp_cast_2 , tex2D( _ColorRump,lerp( fixed4( temp_output_127_0, 0.0 , 0.0 ) , ( saturate( ( fixed4( ( saturate( ( tex2D( _Flakers,( input.uv_Flakers * float2( 10,10 ) )) > 0.5 ? ( 1.0 - ( 1.0 - 2.0 * ( tex2D( _Flakers,( input.uv_Flakers * float2( 10,10 ) )) - 0.5 ) ) * ( 1.0 - tex2D( _Flakers,input.uv_Flakers) ) ) : ( 2.0 * tex2D( _Flakers,( input.uv_Flakers * float2( 10,10 ) )) * tex2D( _Flakers,input.uv_Flakers) ) ) )).xy, 0.0 , 0.0 ) > 0.5 ? ( 1.0 - ( 1.0 - 2.0 * ( fixed4( ( saturate( ( tex2D( _Flakers,( input.uv_Flakers * float2( 10,10 ) )) > 0.5 ? ( 1.0 - ( 1.0 - 2.0 * ( tex2D( _Flakers,( input.uv_Flakers * float2( 10,10 ) )) - 0.5 ) ) * ( 1.0 - tex2D( _Flakers,input.uv_Flakers) ) ) : ( 2.0 * tex2D( _Flakers,( input.uv_Flakers * float2( 10,10 ) )) * tex2D( _Flakers,input.uv_Flakers) ) ) )).xy, 0.0 , 0.0 ) - 0.5 ) ) * ( 1.0 - fixed4( temp_output_127_0, 0.0 , 0.0 ) ) ) : ( 2.0 * fixed4( ( saturate( ( tex2D( _Flakers,( input.uv_Flakers * float2( 10,10 ) )) > 0.5 ? ( 1.0 - ( 1.0 - 2.0 * ( tex2D( _Flakers,( input.uv_Flakers * float2( 10,10 ) )) - 0.5 ) ) * ( 1.0 - tex2D( _Flakers,input.uv_Flakers) ) ) : ( 2.0 * tex2D( _Flakers,( input.uv_Flakers * float2( 10,10 ) )) * tex2D( _Flakers,input.uv_Flakers) ) ) )).xy, 0.0 , 0.0 ) * fixed4( temp_output_127_0, 0.0 , 0.0 ) ) ) )) , ( _AddFlakers * 1.5 ) ).rg) , _UseColorRamp ) * _LightWarpColor ) ).rgb;
			output.Specular = _Specular.rgb;
			fixed4 temp_cast_4 = _Smoothness;
			fixed4 temp_cast_5 = _Smoothness;
			float2 In0 = ( saturate( ( tex2D( _Flakers,( input.uv_Flakers * float2( 10,10 ) )) > 0.5 ? ( 1.0 - ( 1.0 - 2.0 * ( tex2D( _Flakers,( input.uv_Flakers * float2( 10,10 ) )) - 0.5 ) ) * ( 1.0 - tex2D( _Flakers,input.uv_Flakers) ) ) : ( 2.0 * tex2D( _Flakers,( input.uv_Flakers * float2( 10,10 ) )) * tex2D( _Flakers,input.uv_Flakers) ) ) )).xy;
			fixed4 temp_cast_7 = max(In0.r, In0.g);
			output.Smoothness = lerp( temp_cast_4 , ( saturate( ( temp_cast_5 * temp_cast_7 ) )) , _AddFlakers ).r;
			output.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=3001
-1913;57;1906;986;450.2972;1253.392;1.6;True;True
Node;AmplifyShaderEditor.CommentaryNode;157;1839.203,-388.89;Float;624.8001;258.7201;Add Flakers to color ramp;3;146;130;132
Node;AmplifyShaderEditor.CommentaryNode;155;1897.102,-872.7896;Float;843.6005;356.88;Add Flakers to Smoothness;5;145;144;153;152;140
Node;AmplifyShaderEditor.CommentaryNode;151;2524.002,-56.28766;Float;1007.53;459.9699;Use Color Ramp;6;149;124;123;121;148;72
Node;AmplifyShaderEditor.CommentaryNode;150;337.0042,-1138.883;Float;1392.85;500.5298;Flakers;7;143;137;134;135;136;133;129
Node;AmplifyShaderEditor.CommentaryNode;80;53.30788,38.01118;Float;558;373;w;2;75;74
Node;AmplifyShaderEditor.CommentaryNode;78;1174.305,-62.68805;Float;627;253;LightWarpping;3;127;79;81
Node;AmplifyShaderEditor.CommentaryNode;77;645.9077,-67.98801;Float;414;237;NdotLWrap;2;73;71
Node;AmplifyShaderEditor.CommentaryNode;76;58.40852,-384.888;Float;533;404;NdotL;3;70;68;69
Node;AmplifyShaderEditor.DotProductOpNode;69;436.5132,-264.2878;Float;FLOAT3;0.0,0,0;FLOAT3;0,0,0
Node;AmplifyShaderEditor.WorldNormalVector;68;93.40879,-332.8882;Float;FLOAT3;0,0,0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;471.9108,166.0102;Float;FLOAT;0.0;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;70;79.4087,-154.8874;Float;FLOAT4;0.0,0,0,0
Node;AmplifyShaderEditor.OneMinusNode;73;697.5071,80.81058;Float;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOp;79;1468.704,42.1124;Float;FLOAT;0.0;FLOAT;0.0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;71;916.905,-15.98804;Float;FLOAT;0.0;FLOAT;0.0
Node;AmplifyShaderEditor.AppendNode;127;1643.604,-21.18222;Float;FLOAT2;0;0;0;0;FLOAT;0.0;FLOAT;0.0;FLOAT;0.0;FLOAT;0.0
Node;AmplifyShaderEditor.ColorNode;139;3105.099,-741.5793;Float;Property;_Specular;Specular;1;0.1544118,0.1544118,0.1544118,0
Node;AmplifyShaderEditor.RangedFloatNode;128;-370.1921,74.81761;Float;Property;_LightWarp;LightWarp;3;1;0;2
Node;AmplifyShaderEditor.RangedFloatNode;75;94.00852,239.8102;Float;Constant;_Float1;Float 1;8;0.5;0;2
Node;AmplifyShaderEditor.SimpleAddOpNode;81;1241.503,53.01157;Float;FLOAT;0.0;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;133;760.2086,-863.2816;Float;Property;_TextureSample1;Texture Sample 1;2;None;True;0;False;white;Auto;False;Instance;129;Auto;SAMPLER2D;;FLOAT2;0,0;FLOAT;1.0;FLOAT2;0,0;FLOAT2;0,0;FLOAT;1.0
Node;AmplifyShaderEditor.BlendOpsNode;136;1094.208,-921.2819;Float;Overlay;True;COLOR;0,0,0,0;COLOR;0,0,0,0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;135;596.2081,-836.2819;Float;FLOAT2;0.0,0;FLOAT2;10,10
Node;AmplifyShaderEditor.TextureCoordinatesNode;134;381.8082,-840.0826;Float;0;129;FLOAT2;1,1;FLOAT2;0,0
Node;AmplifyShaderEditor.ComponentMaskNode;137;1285.306,-921.5806;Float;True;True;False;False;COLOR;0,0,0,0
Node;AmplifyShaderEditor.CustomExpressionNode;143;1500.106,-921.5801;Float;max(In0.r, In0.g);1;1;True;In0;FLOAT2;0.0,0;FLOAT2;0.0,0
Node;AmplifyShaderEditor.ColorNode;72;2963.903,181.4109;Float;Property;_LightWarpColor;LightWarpColor;4;1,1,1,0
Node;AmplifyShaderEditor.LerpOp;148;2995.705,35.81879;Float;FLOAT;0.0;FLOAT4;0.0,0,0,0;FLOAT;0.0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;121;3194.507,92.6164;Float;FLOAT4;0,0,0,0;COLOR;0.0,0,0,0
Node;AmplifyShaderEditor.SimpleMaxOp;123;3330.906,90.7177;Float;COLOR;0,0,0,0;COLOR;0.0,0,0,0
Node;AmplifyShaderEditor.SamplerNode;124;2608.606,106.6173;Float;Property;_ColorRump;ColorRump;6;None;True;0;False;white;Auto;False;Object;-1;Auto;SAMPLER2D;;FLOAT2;0,0;FLOAT;1.0;FLOAT2;0,0;FLOAT2;0,0;FLOAT;1.0
Node;AmplifyShaderEditor.RangedFloatNode;149;2608.105,281.919;Float;Property;_UseColorRamp;UseColorRamp;5;0;0;1
Node;AmplifyShaderEditor.WireNode;154;2229.505,-489.6894;Float;FLOAT;0.0
Node;AmplifyShaderEditor.RangedFloatNode;140;1998.491,-807.8799;Float;Property;_Smoothness;Smoothness;2;0.5;0;1
Node;AmplifyShaderEditor.WireNode;152;2490.806,-710.6888;Float;FLOAT;0.0
Node;AmplifyShaderEditor.WireNode;153;1959.1,-710.6891;Float;FLOAT;0.0
Node;AmplifyShaderEditor.LerpOp;144;2552.704,-702.1804;Float;FLOAT;0;COLOR;0,0,0,0;FLOAT;0.5
Node;AmplifyShaderEditor.WireNode;156;2159.403,-46.68982;Float;FLOAT2;0.0,0
Node;AmplifyShaderEditor.BlendOpsNode;132;1951.606,-338.982;Float;Overlay;True;COLOR;0,0,0,0;COLOR;0,0,0,0
Node;AmplifyShaderEditor.LerpOp;130;2266.009,-308.8822;Float;FLOAT2;0.0,0;COLOR;0.0,0,0,0;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;146;1954.194,-229.9817;Float;FLOAT;0;FLOAT;1.5
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;3720.897,-633.2859;Fixed;True;2;Fixed;ASEMaterialInspector;StandardSpecular;LightWrapping;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;3;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;False;False;False;False;False;False;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;True;FLOAT3;0,0,0;FLOAT3;0,0,0;FLOAT3;0,0,0;FLOAT3;0,0,0;FLOAT;0.0;FLOAT;0.0;FLOAT3;0,0,0;FLOAT3;0,0,0;FLOAT;0.0;OBJECT;0.0;OBJECT;0.0;OBJECT;0.0;OBJECT;0.0;FLOAT3;0,0,0
Node;AmplifyShaderEditor.WireNode;158;3011.106,-521.0905;Float;COLOR;0.0,0,0,0
Node;AmplifyShaderEditor.ColorNode;104;3152.408,-918.2872;Float;Property;_Color;Color;0;0,0,0,0
Node;AmplifyShaderEditor.BlendOpsNode;145;2321.305,-682.6796;Float;Multiply;True;COLOR;0,0,0,0;COLOR;0,0,0,0
Node;AmplifyShaderEditor.RangedFloatNode;138;1189.295,-501.2824;Float;Property;_AddFlakers;AddFlakers;8;0.2;0;1
Node;AmplifyShaderEditor.SamplerNode;129;764.6074,-1048.281;Float;Property;_Flakers;Flakers;7;None;True;0;False;white;Auto;False;Object;-1;Auto;SAMPLER2D;;FLOAT2;0,0;FLOAT;1.0;FLOAT2;0,0;FLOAT2;0,0;FLOAT;1.0
WireConnection;69;0;68;0
WireConnection;69;1;70;0
WireConnection;74;0;128;0
WireConnection;74;1;75;0
WireConnection;73;0;74;0
WireConnection;79;1;81;0
WireConnection;71;0;69;0
WireConnection;71;1;73;0
WireConnection;127;0;79;0
WireConnection;81;0;71;0
WireConnection;81;1;74;0
WireConnection;133;1;135;0
WireConnection;136;0;129;0
WireConnection;136;1;133;0
WireConnection;135;0;134;0
WireConnection;137;0;136;0
WireConnection;143;0;137;0
WireConnection;148;0;79;0
WireConnection;148;1;124;0
WireConnection;148;2;149;0
WireConnection;121;0;148;0
WireConnection;121;1;72;0
WireConnection;123;1;121;0
WireConnection;124;1;130;0
WireConnection;154;0;138;0
WireConnection;152;0;140;0
WireConnection;153;0;143;0
WireConnection;144;0;152;0
WireConnection;144;1;145;0
WireConnection;144;2;154;0
WireConnection;156;0;127;0
WireConnection;132;0;127;0
WireConnection;132;1;137;0
WireConnection;130;0;156;0
WireConnection;130;1;132;0
WireConnection;130;2;146;0
WireConnection;146;0;138;0
WireConnection;0;0;104;0
WireConnection;0;2;123;0
WireConnection;0;3;139;0
WireConnection;0;4;158;0
WireConnection;158;0;144;0
WireConnection;145;0;140;0
WireConnection;145;1;153;0
ASEEND*/
//CHKSM=E73D79A642B9605D70435300EEE6651D52BD1205