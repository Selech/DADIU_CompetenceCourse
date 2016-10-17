Shader "DADIU_Competence/GrayScaleFX"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Amount("Amount",  Range(0.0,1.0)) = 0.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _Amount;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				
				float grayCol = (col.r + col.b + col.g) / 3.0;

				fixed4 endCol = (col*(1- _Amount) + (grayCol*_Amount));

				return endCol;
			}
			ENDCG
		}
	}
}
