Shader "Custom/CircleMask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Center ("Center", Vector) = (0.5, 0.5, 0, 0)
        _Radius ("Radius", Range(0, 1.5)) = 1.5
        _Softness ("Softness", Range(0, 0.1)) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float2 _Center;
            float _Radius;
            float _Softness;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Ajustamos el aspecto para que el círculo no sea un óvalo
                float aspect = _ScreenParams.x / _ScreenParams.y;
                float2 uv = i.uv;
                uv.x *= aspect;
                float2 center = _Center;
                center.x *= aspect;

                // Calculamos la distancia del píxel actual al centro
                float dist = distance(uv, center);
                
                // Creamos el efecto de recorte circular
                float mask = smoothstep(_Radius, _Radius + _Softness, dist);
                
                return fixed4(0, 0, 0, mask); // Retorna negro con la opacidad de la máscara
            }
            ENDCG
        }
    }
}