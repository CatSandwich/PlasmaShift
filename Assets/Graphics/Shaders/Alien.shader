Shader "Unlit/Alien"
{
    Properties
    {
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

            static fixed3 a = 0.5;
            static fixed3 b = 0.5;
            static fixed3 c = float3(2, 1, 0);
            static fixed3 d = float3(0.5, 0.2, 0.25);

            fixed4 frag (v2f i) : SV_Target
            {
                float f = pow(voronoi(i.uv * 2 + _Time.y * 0.2).x, 7);
                float t = (i.uv.x + i.uv.y * 0.3) * 0.4 + _Time.y * 0.2;
                float3 col = palette(t, a, b, c, d) * f * 4;
                col += pow(max(max(i.uv.y, 1 - i.uv.y), max(i.uv.x, 1 - i.uv.x)), 20) * 0.75;
                return float4(col, 1);
            }
            ENDCG
        }
    }
}
