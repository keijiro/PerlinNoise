Shader "Custom/Untextured" {
	Properties {
        _Color ("Color", Color) = (1, 1, 1, 0)
        _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
        _Specular ("Specular", Float) = 0.0
        _Gloss ("Gloss", Float) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf BlinnPhong
        
        float4 _Color;
        float _Specular;
        float _Gloss;

		struct Input {
            float noInput;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color.rgb;
            o.Alpha = _Color.a;
            o.Specular = _Specular;
            o.Gloss = _Gloss;
		}
		ENDCG
	} 
	FallBack "Specular"
}