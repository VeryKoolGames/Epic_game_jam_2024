Shader "Custom/OldTVFade"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Fade ("Fade Amount", Range(0, 1)) = 0.0
        _SetBlack ("Set Black", Range(0, 1)) = 0.0
        _Steps ("Fade Steps", Range(1, 100)) = 20
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

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Fade;
            float _SetBlack;
            float _Steps;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 col = tex2D(_MainTex, uv);

                // Calculate the fade effect
                if (_SetBlack > 0.5)
                {
                    col.rgb = 0;
                }
                else
                {
                    float stepFade = floor(_Fade * _Steps) / _Steps;
                    if (uv.y < stepFade)
                    {
                        col.rgb = 0;
                    }
                }

                return col;
            }
            ENDCG
        }
    }
}
