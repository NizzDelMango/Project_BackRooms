Shader "Hidden/VHSPostProcessEffect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_VHSTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
					
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _VHSTex;
			
			float _yScanline;
			float _xScanline;
			float rand(float3 co){
			     return frac(sin( dot(co.xyz ,float3(12.9898,78.233,45.5432) )) * 43758.5453);
			}
 
			fixed4 frag (v2f_img i) : COLOR{
				fixed4 vhs = tex2D (_VHSTex, i.uv);
				
				float dx = 1-abs(distance(i.uv.y, _xScanline));
				float dy = 1-abs(distance(i.uv.y, _yScanline));
				
				i.uv.x = i.uv.x % 1;
				i.uv.y = i.uv.y % 1;
				
				fixed4 c = tex2D (_MainTex, i.uv);
				
				float bleed = tex2D(_MainTex, i.uv + float2(0.01, 0)).r;
				bleed /= 10;
				
				if(bleed > 0.1){
					vhs += fixed4(bleed * _xScanline, 0, 0, 0);
					
				}
				return c + vhs;
			}
			ENDCG
		}
	}
Fallback off
}