Shader "Custom/DonutRangeShader"
{
    Properties
    {
        _MainColor("Color", Color) = (0, 1, 0, 0.5)
        _MinRadius("Min Radius", Float) = 0.2
        _MaxRadius("Max Radius", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _MainColor;
            float _MinRadius;
            float _MaxRadius;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * 2.0 - 1.0; // -1 Å` +1 Ç…ê≥ãKâª
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float dist = length(i.uv); // íÜêSÇ©ÇÁÇÃãóó£
                if (dist < _MinRadius || dist > _MaxRadius)
                {
                    discard;
                }
                return _MainColor;
            }
            ENDCG
        }
    }
}