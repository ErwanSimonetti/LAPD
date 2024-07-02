Shader "Custom/ColoredOutline"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range (0.01, 0.1)) = 0.03
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        // Deuxième passe pour dessiner l'outline
        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }
            Cull Front
            ZWrite On
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha
            Offset 0, 0

            CGPROGRAM
            #pragma vertex vertOutline
            #pragma fragment fragOutline
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            fixed4 _OutlineColor;
            float _OutlineThickness;

            v2f vertOutline(appdata_t v)
            {
                // Calculer les normales en utilisant la matrice de l'objet pour le monde
                float3 norm = mul((float3x3) unity_WorldToObject, v.normal);
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex + _OutlineThickness * float4(norm, 0));
                o.color = _OutlineColor;
                return o;
            }

            fixed4 fragOutline(v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
