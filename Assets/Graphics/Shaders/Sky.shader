Shader "Unlit/Sky"
{
    Properties
    {
        _Size ("Size", Float) = 1
        _Color1 ("Color 1", Color) = (1,1,1)
        _Color2 ("Color 2", Color) = (1,1,1)
        _Color3 ("Color 3", Color) = (1,1,1)
        _Color4 ("Color 4", Color) = (1,1,1)
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

            float _Size;
            float3 _Color1;
            float3 _Color2;
            float3 _Color3;
            float3 _Color4;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float aspect = _ScreenParams.y / _ScreenParams.x;
                fixed2 pos = i.uv * _Size * aspect;
                pos.x += _Time.y * 0.1;

                float v = voronoi(pos).x;
                v = smoothstep(1, 0.2, v);

                fixed3 col = lerp(_Color1, _Color3, gnoise(pos * float2(0.1, 0.2)));
                col = lerp(col, _Color2, v);
                col = lerp(col, _Color4, gnoise(pos * float2(0.4, 0.2)) * 0.5 + 0.5);
                return float4(col, 1);
            }
            ENDCG
        }
    }
}
