Shader "Hovl/Particles/Blend_CenterGlow"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_Noise("Noise", 2D) = "white" {}
		_Flow("Flow", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_SpeedMainTexUVNoiseZW("Speed MainTex U/V + Noise Z/W", Vector) = (0,0,0,0)
		_DistortionSpeedXYPowerZ("Distortion Speed XY Power Z", Vector) = (0,0,0,0)
		_Emission("Emission", Float) = 2
		_Color("Color", Color) = (0.5,0.5,0.5,1)
		_Opacity("Opacity", Float) = 1
		[Toggle]_Usecenterglow("Use center glow?", Float) = 0
		[Toggle]_Usealphacenterglow("Use alpha center glow?", Float) = 0
		[MaterialToggle] _Usedepth ("Use depth?", Float ) = 0
        _Depthpower ("Depth power", Float ) = 1
		[Enum(Both,0,Back,1,Front,2)]_Cull("Render Face", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	Category 
	{
		SubShader
		{
		LOD 0

			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull [_Cull]
			Lighting Off 
			ZWrite Off
			ZTest LEqual
			
			Pass {
			
				CGPROGRAM
				
				#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
				#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
				#endif
				
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				#include "UnityShaderVariables.cginc"
				#define ASE_NEEDS_FRAG_COLOR


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					
				};

				struct v2f 
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
					
				};
				
				
				#if UNITY_VERSION >= 560
				UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
				#else
				uniform sampler2D_float _CameraDepthTexture;
				#endif

				//Don't delete this comment
				// uniform sampler2D_float _CameraDepthTexture;

				uniform sampler2D _MainTex;
				uniform float4 _MainTex_ST;
				uniform float _InvFade;
				uniform float _CullMode;
				uniform float _Usecenterglow;
				uniform float4 _SpeedMainTexUVNoiseZW;
				uniform sampler2D _Flow;
				uniform float4 _DistortionSpeedXYPowerZ;
				uniform float4 _Flow_ST;
				uniform sampler2D _Mask;
				uniform float4 _Mask_ST;
				uniform sampler2D _Noise;
				uniform float4 _Noise_ST;
				uniform float4 _Color;
				uniform float _Emission;
				uniform float _Opacity;
				uniform float _Usealphacenterglow;
				uniform fixed _Usedepth;
				uniform float _Depthpower;


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					

					v.vertex.xyz +=  float3( 0, 0, 0 ) ;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos (o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = v.texcoord;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag ( v2f i  ) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID( i );
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( i );

					float lp = 1;
					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate ((sceneZ-partZ) / _Depthpower);
						lp *= lerp(1, fade, _Usedepth);
						i.color.a *= lp;
					#endif

					float2 appendResult21 = (float2(_SpeedMainTexUVNoiseZW.x , _SpeedMainTexUVNoiseZW.y));
					float2 uv_MainTex = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					float2 panner107 = ( 1.0 * _Time.y * appendResult21 + uv_MainTex);
					float2 appendResult100 = (float2(_DistortionSpeedXYPowerZ.x , _DistortionSpeedXYPowerZ.y));
					float4 uvs4_Flow = i.texcoord;
					uvs4_Flow.xy = i.texcoord.xy * _Flow_ST.xy + _Flow_ST.zw;
					float2 panner110 = ( 1.0 * _Time.y * appendResult100 + (uvs4_Flow).xy);
					float2 uv_Mask = i.texcoord.xy * _Mask_ST.xy + _Mask_ST.zw;
					float4 tex2DNode33 = tex2D( _Mask, uv_Mask );
					float Flowpower102 = _DistortionSpeedXYPowerZ.z;
					float2 temp_cast_0 = (( ( tex2D( _Flow, panner110 ).a * tex2DNode33.a ) * Flowpower102 )).xx;
					float4 tex2DNode13 = tex2D( _MainTex, ( panner107 - temp_cast_0 ) );
					float2 appendResult22 = (float2(_SpeedMainTexUVNoiseZW.z , _SpeedMainTexUVNoiseZW.w));
					float2 uv_Noise = i.texcoord.xy * _Noise_ST.xy + _Noise_ST.zw;
					float2 panner108 = ( 1.0 * _Time.y * appendResult22 + uv_Noise);
					float4 tex2DNode14 = tex2D( _Noise, panner108 );
					float3 temp_output_78_0 = (( tex2DNode13 * tex2DNode14 * _Color * i.color )).rgb;
					float clampResult40 = clamp( ( tex2DNode33.a * ( tex2DNode33.a - (1.0 + (uvs4_Flow.z - 0.0) * (0.0 - 1.0) / (1.0 - 0.0)) ) ) , 0.0 , 1.0 );
					float4 appendResult87 = (float4(( (( _Usecenterglow )?( ( temp_output_78_0 * clampResult40 ) ):( temp_output_78_0 )) * _Emission ) , saturate( ( tex2DNode13.a * tex2DNode14.a * _Color.a * i.color.a * _Opacity * (( _Usealphacenterglow )?( clampResult40 ):( 1.0 )) ) )));
					

					fixed4 col = appendResult87;
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	
	
	Fallback Off
}
/*ASEBEGIN
Version=19701
Node;AmplifyShaderEditor.CommentaryNode;104;-4130.993,490.5418;Inherit;False;1910.996;537.6462;Texture distortion;13;91;33;100;102;99;94;95;103;92;59;98;110;158;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;99;-3968.293,619.481;Float;False;Property;_DistortionSpeedXYPowerZ;Distortion Speed XY Power Z;5;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;98;-3920.299,848.9976;Inherit;False;0;91;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;100;-3535.482,654.5021;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;59;-3583.603,566.496;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;110;-3339.196,596.5295;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;91;-3152.937,567.9764;Inherit;True;Property;_Flow;Flow;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.CommentaryNode;109;-3401.27,-330.4436;Inherit;False;1037.896;533.6285;Textures movement;7;107;108;29;21;89;22;15;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;33;-3146.373,763.0061;Inherit;True;Property;_Mask;Mask;3;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.Vector4Node;15;-3351.27,-101.4007;Float;False;Property;_SpeedMainTexUVNoiseZW;Speed MainTex U/V + Noise Z/W;4;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;102;-3556.945,748.0421;Float;False;Flowpower;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;-2762.212,550.0183;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;21;-2778.501,-153.1786;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;94;-2609.926,543.6367;Inherit;False;True;True;False;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;103;-2605.07,630.9626;Inherit;False;102;Flowpower;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-2856.788,-280.4436;Inherit;False;0;13;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;107;-2570.374,-239.5098;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;22;-2766.722,70.18491;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;-2388.997,542.6455;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;89;-2861.858,-55.04038;Inherit;False;0;14;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;36;-2404.11,1277.984;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;108;-2577.237,-21.63752;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;96;-1989.684,-41.77601;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;37;-2163.727,1203.653;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-1937.593,1156.593;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-1804.579,119.2214;Inherit;True;Property;_Noise;Noise;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode;13;-1803.192,-66.2159;Inherit;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;31;-1728.612,316.0578;Float;False;Property;_Color;Color;7;0;Create;True;0;0;0;False;0;False;0.5,0.5,0.5,1;0.5,0.5,0.5,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.VertexColorNode;32;-1680,512;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;40;-1764.275,1143.857;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-1135.791,-2.490838;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;78;-945.7914,41.06877;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;72;-1580.242,1135.946;Inherit;False;True;True;True;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;163;-1585.376,1208.858;Inherit;False;True;False;False;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;161;-907.298,700.0748;Inherit;False;Constant;_Float0;Float 0;13;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-714.9078,127.0253;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ToggleSwitchNode;160;-743.4769,702.1085;Inherit;False;Property;_Usealphacenterglow;Use alpha center glow?;10;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-754.0106,521.2757;Float;False;Property;_Opacity;Opacity;8;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;52;-446.0907,153.7209;Float;False;Property;_Emission;Emission;6;0;Create;True;0;0;0;False;0;False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;90;-536.6786,48.89112;Float;False;Property;_Usecenterglow;Use center glow?;9;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;-460.9,315.4933;Inherit;False;6;6;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-285.8404,56.42584;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;164;-306.2898,314.3496;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;87;-123.9274,58.99411;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;158;-3553.936,926.9137;Inherit;False;Random;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;134;-1506.595,1939.054;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;133;-1306.595,1812.654;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;154;-913.3213,2069.178;Inherit;True;Simplex2D;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;155;-902.7032,2286.093;Inherit;True;Simplex2D;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;152;-610.3025,1876.045;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;159;-1437.039,2177.329;Inherit;False;158;Random;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;157;-1070.889,2273.451;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT3;3,7,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;156;-1070.643,2039.402;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT3;10,10,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;117;-413.9872,1950.647;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;122;-1475.073,1816.901;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;132;-1062.338,1798.794;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;113;-911.2253,1854.281;Inherit;True;Simplex2D;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;151;-1382.471,1685.094;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;123;-95.69926,1030.128;Inherit;False;2;2;0;FLOAT;1;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;112;-3967.198,-207.5443;Inherit;False;Property;_CullMode;Culling;11;1;[Enum];Create;False;0;3;Cull Off;0;Cull Front;1;Cull Back;2;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;120;-1805.198,1973.661;Inherit;False;Property;_XOffsetYScaleZWSpeed;X Offset Y Scale ZW Speed;12;0;Create;True;0;0;0;False;0;False;0,2,0,0;0,2,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;68;48.80347,59.22049;Float;False;True;-1;2;;0;11;Hovl/Particles/Blend_CenterGlow;0b6a9f8b4f707c74ca64c0be8e590de0;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;False;True;2;5;False;;10;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;True;True;2;True;_CullMode;False;True;True;True;True;False;0;False;;False;False;False;False;False;False;False;False;False;True;2;False;;True;3;False;;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;100;0;99;1
WireConnection;100;1;99;2
WireConnection;59;0;98;0
WireConnection;110;0;59;0
WireConnection;110;2;100;0
WireConnection;91;1;110;0
WireConnection;102;0;99;3
WireConnection;92;0;91;4
WireConnection;92;1;33;4
WireConnection;21;0;15;1
WireConnection;21;1;15;2
WireConnection;94;0;92;0
WireConnection;107;0;29;0
WireConnection;107;2;21;0
WireConnection;22;0;15;3
WireConnection;22;1;15;4
WireConnection;95;0;94;0
WireConnection;95;1;103;0
WireConnection;36;0;98;3
WireConnection;108;0;89;0
WireConnection;108;2;22;0
WireConnection;96;0;107;0
WireConnection;96;1;95;0
WireConnection;37;0;33;4
WireConnection;37;1;36;0
WireConnection;39;0;33;4
WireConnection;39;1;37;0
WireConnection;14;1;108;0
WireConnection;13;1;96;0
WireConnection;40;0;39;0
WireConnection;30;0;13;0
WireConnection;30;1;14;0
WireConnection;30;2;31;0
WireConnection;30;3;32;0
WireConnection;78;0;30;0
WireConnection;72;0;40;0
WireConnection;163;0;40;0
WireConnection;41;0;78;0
WireConnection;41;1;72;0
WireConnection;160;0;161;0
WireConnection;160;1;163;0
WireConnection;90;0;78;0
WireConnection;90;1;41;0
WireConnection;88;0;13;4
WireConnection;88;1;14;4
WireConnection;88;2;31;4
WireConnection;88;3;32;4
WireConnection;88;4;62;0
WireConnection;88;5;160;0
WireConnection;51;0;90;0
WireConnection;51;1;52;0
WireConnection;164;0;88;0
WireConnection;87;0;51;0
WireConnection;87;3;164;0
WireConnection;158;0;98;4
WireConnection;133;0;122;0
WireConnection;133;1;134;0
WireConnection;154;0;156;0
WireConnection;154;1;120;2
WireConnection;155;0;157;0
WireConnection;155;1;120;2
WireConnection;152;0;154;0
WireConnection;152;1;113;0
WireConnection;152;2;155;0
WireConnection;157;0;151;2
WireConnection;157;2;133;0
WireConnection;157;3;159;0
WireConnection;156;0;151;2
WireConnection;156;2;133;0
WireConnection;156;3;159;0
WireConnection;117;0;152;0
WireConnection;117;1;120;1
WireConnection;122;0;120;3
WireConnection;122;1;120;4
WireConnection;122;2;120;3
WireConnection;132;0;151;2
WireConnection;132;1;133;0
WireConnection;132;2;159;0
WireConnection;113;0;132;0
WireConnection;113;1;120;2
WireConnection;123;0;33;4
WireConnection;123;1;117;0
WireConnection;68;0;87;0
ASEEND*/
//CHKSM=16EC9ED650C302F50188A187A7CC0DE92B001599