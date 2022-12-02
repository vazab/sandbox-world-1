Shader "Custom/Diffuse - TextureTiling"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Scale("Texture Scale", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _Color;
        float _Scale;

        struct Input
        {
            float3 worldNormal;
            float3 worldPos;
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            float2 UV = IN.uv_MainTex;
            float3 scale = float3(
            length(unity_ObjectToWorld._m00_m10_m20),
            length(unity_ObjectToWorld._m01_m11_m21),
            length(unity_ObjectToWorld._m02_m12_m22)
            );

            UV = 
            abs(IN.worldNormal.x) > 0.5 ? float2(UV.x * scale.z, UV.y * scale.y) :
            abs(IN.worldNormal.z) > 0.5 ? float2(UV.x * scale.x, UV.y * scale.y) * -1 :
            float2(UV.x * scale.x, UV.y * scale.z) * -1;

            o.Albedo = tex2D(_MainTex, UV * _Scale) * _Color;
        }
        ENDCG
    }
    Fallback "Diffuse"
}