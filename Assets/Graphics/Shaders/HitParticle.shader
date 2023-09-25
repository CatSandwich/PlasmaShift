Shader "Unlit/HitParticle"
{
    Properties
    {
        _Periods ("Periods", Float) = 8
        _Whiteness ("Whiteness", Float) = 0
    }
    SubShader
    {
        Tags {"Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent"}
        Blend SrcAlpha One
        ZTest Always
        ZWrite Off
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

            float _Periods;
            float _Whiteness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = (i.uv - 0.5) * 2;
                float angle = atan2(uv.y, uv.x) + _Time.y * 5 + length(uv);

                float f = sin(angle * _Periods) * 0.5 + 0.5;
                float zoom = 1 + sin(_Time.y * 5) * 0.1;
                float len = saturate(1 - pow(length(uv * 1.5 * zoom), 0.3) + f * 0.1);

                float h = len + _Time.y;
                float3 col = lerp(
                    saturate(0.5 + 0.5*cos(6.28318*((1.-h)+float3(0, 0.33, 0.67)))) * len,
                    len,
                    _Whiteness
                );

                col += pow(len, 2.5) * 1;

                return float4(col, 1);
            }
            ENDCG
        }
    }
}
