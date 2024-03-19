// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Blur"
{ 
    Properties
    {
        _MainText ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(1, 10)) = 3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"}

        Pass {
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
                float2 uv : TEXTCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _BlurSize;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = fixed4(0, 0, 0, 0);
                float blurPixels = _BlurSize;
    
                for (int x = -blurPixels; x <= blurPixels; x++)
                {
                    for (int y = -blurPixels; y <= blurPixels; y++)
                    {
            col += tex2D(_MainTex, i.uv + float2(x, y) / _ScreenParams.xy);
        }
                }
    
                col /= pow((2 * blurPixels + 1), 2);
    
                return col;
            }
            ENDCG

        }  
    }
    FallBack "Diffuse"
}