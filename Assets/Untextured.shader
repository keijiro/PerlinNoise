Shader "Custom/Untextured" {
	Properties {
        _Color ("Color", Color) = (1, 1, 1, 0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert
        
        float4 _Color;

		struct Input {
            float noInput;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color.rgb;
		}
		ENDCG
	} 
	FallBack Off
}