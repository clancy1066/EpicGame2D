shader "Custom/FogOfWar"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _FogMask ("Fog Mask", 2D) = "black" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            sampler2D _FogMask;
            float4 _MainTex_ST; // Automatically added by Unity for scaling and offset

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Explicit UV transformation
                o.uv = v.texcoord * _MainTex_ST.xy + _MainTex_ST.zw;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the main texture and fog mask
                fixed4 mainColor = tex2D(_MainTex, i.uv);
                fixed4 fogColor = tex2D(_FogMask, i.uv);

                // Blend fog and main texture (black hides, white reveals)
                return lerp(mainColor, fixed4(0, 0, 0, 1), 1.0 - fogColor.r);
            }
            ENDCG
        }
    }
}