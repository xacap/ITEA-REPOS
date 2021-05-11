Shader "Custom/MummySkin"
{
    Properties
    {
         _MainTex("Base layer (RGB)", 2D) = "white" {}
        _ScrollX("Base layer Scroll speed X", Float) = 1.0
        _ScrollY("Base layer Scroll speed Y", Float) = 0.0
        _Intensity("Intensity", Float) = 0.5
        _Transparency("Transparency", Range(0.0, 1.0)) = 1.0
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

        Lighting Off
        Fog { Mode Off }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        LOD 100

        CGINCLUDE
        #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
        #include "UnityCG.cginc"
        sampler2D _MainTex;

        float4 _MainTex_ST;

         float _ScrollX;
         float _ScrollY;
         float _Intensity;
         float _Transparency;

         struct v2f
         {
             float4 pos : SV_POSITION;
             float2 uv : TEXCOORD0;
             fixed4 color : TEXCOORD1;
         };


         v2f vert(appdata_full v)
         {
             v2f o;
             o.pos = UnityObjectToClipPos(v.vertex);
             o.uv = TRANSFORM_TEX(v.texcoord.xy,_MainTex) + frac(float2(_ScrollX, _ScrollY) * _Time);
             o.color = fixed4(_Intensity, _Intensity, _Intensity, _Transparency);

             return o;
         }
         ENDCG

         Pass 
         {
             CGPROGRAM
             #pragma vertex vert Standard fullforwardshadows
             #pragma fragment frag
             #pragma fragmentoption ARB_precision_hint_fastest    
             #pragma target 3.0

             fixed4 frag(v2f i) : COLOR
             {
                 fixed4 o;
                 fixed4 tex = tex2D(_MainTex, i.uv);
                 o = tex * i.color;

                 return o;
             }
             ENDCG
         }
    }
}

