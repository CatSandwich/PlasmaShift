Shader "Unlit/Alien"
{
    Properties
    {
        _A ("A", Color) = (0.5, 0.5, 0.5)
        _B ("B", Color) = (0.5, 0.5, 0.5)
        _C ("C", Color) = (2.0, 1.0, 0.0)
        _D ("D", Color) = (0.5, 0.2, 0.25)
        _Thickness ("Thickness", Float) = 7
        _Scale ("Scale", Float) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Colors.cginc"
            #include "Noise.cginc"

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
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float3 _A;
            float3 _B;
            float3 _C;
            float3 _D;
            float _Thickness;
            float _Scale;

            fixed4 frag (v2f i) : SV_Target
            {
                float f = pow(voronoi(i.uv * _Scale + _Time.y * 0.2).x, _Thickness);
                float t = (i.uv.x + i.uv.y * 0.3) * 0.4 + _Time.y * 0.2;
                float3 col = palette(t, _A, _B, _C, _D) * f * 4;
                col += pow(max(max(i.uv.y, 1 - i.uv.y), max(i.uv.x, 1 - i.uv.x)), 20) * 0.75;
                return float4(col, 1);
            }
            ENDCG
        }
    }
}
