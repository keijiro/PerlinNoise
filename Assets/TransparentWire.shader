Shader "Custom/TransparentWire" {
	Properties {
		_Color ("Color", Color) = (1, 1, 1, 0.5)
	}
	SubShader {
		Tags { "RenderType"="Transparent" }

		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			half4 _Color;

			struct appdata {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 pos : SV_POSITION;
			};

			v2f vert(appdata v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}

			half4 frag(v2f i) : COLOR {
				return _Color;
			}

			ENDCG
		}
	} 
}
