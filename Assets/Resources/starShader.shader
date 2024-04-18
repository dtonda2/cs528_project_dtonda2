Shader "Custom/starShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // Add instancing support for this shader.
            // You need to check for availability at compile time.
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                // Calculate distance from center
                float2 center = float2(0.5, 0.5);
                float distance = length(i.uv - center);
                // Adjust alpha based on distance from center
                col.a *= smoothstep(0.5, 0.55, distance); // Adjust the 0.5 and 0.55 values for the size of the circle
                return col;
            }
            ENDCG
        }
    }
}