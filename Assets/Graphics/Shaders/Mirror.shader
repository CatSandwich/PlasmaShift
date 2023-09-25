Shader "Unlit/Mirror"
{
    Properties
    {
        _Hit ("Hit", Range(0, 1)) = 0
        _Opacity ("Opacity", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        Blend SrcAlpha One
        ZTest Always
        ZWrite Off
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert alpha
            #pragma fragment frag alpha

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
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =  v.uv;
                return o;
            }

            float _Hit;
            float _Opacity;

            float4 frag (v2f i) : SV_Target
            {
                float dist = ((1 - abs(0.5 - i.uv.y)) - 0.5) * 2;

                float3 ambientCol = float3(pow(dist, 3), 0, 0) * 0.25;

                float flicker = sin((_Time.y * 50) % 2);
                float3 hitCol = float3(pow(dist, 1.7), 0, 0) * flicker;
                hitCol += flicker * pow(dist, 6);
                // hitCol *= 2;

                float3 col = lerp(ambientCol, hitCol, _Hit) * _Opacity;

                return float4(col, 1);
            }
            ENDCG
        }
    }
}
