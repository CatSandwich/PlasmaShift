Shader "Unlit/Veins"
{
    Properties
    {
        _Near ("Near", Float) = 0
        _Far ("Far", Float) = 0
        _NearCol ("Near Color", Color) = (1,1,1)
        _FarCol ("Far Color", Color) = (1,1,1)
    }
    SubShader
    {
        Tags { 
            "Queue"="Overlay" 
            "LightMode"="ForwardBase" 
            "RenderType"="Transparent" 
        }
        Blend SrcAlpha OneMinusSrcAlpha

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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float3 pos : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };

            float _Near;
            float _Far;
            float3 _NearCol;
            float3 _FarCol;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.pos = mul(unity_ObjectToWorld, v.vertex);
                o.normal = normalize(mul(unity_ObjectToWorld, v.normal));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float diff = _Far - _Near;
                float fraction = (i.pos.z - _Near) / diff;
                
                float3 col = lerp(_NearCol, _FarCol, fraction);

                float sides = 1 - pow(dot(i.normal, float3(0,0,1)) * 0.5 + 0.5, 1);

                return float4(col, saturate(i.pos.y * 0.7) * sides);
            }
            ENDCG
        }
    }
}
