﻿Shader "DADIU_Competence/GlowShaderCenter" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_GlowPower ("Glow Power", Range(0,100.0)) = 1.0
		_GlowColor ("Glow Color", Color) = (1,1,1,1)
		_GlowAmount("Glow Amount", Range(0.0,1.0)) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float4 color : Color;
			float3 viewDir;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float4 _GlowColor;
		float _GlowPower;
		float _GlowAmount;

		float _Bool = 1.0;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb * IN.color;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;

			half rim = saturate(dot(normalize(IN.viewDir), o.Normal)) * _GlowAmount;
			o.Emission = _GlowColor.rgb * pow(rim, _GlowPower);

		}
		ENDCG
	}
	FallBack "Diffuse"
}
