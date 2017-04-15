Shader "Yogi/Occlusion"  
{  
    Properties  
    {  
        _BumpMap("Normalmap", 2D) = "bump" {}  
        _OccColor("Occlusion Color", Color) = (1, 1, 1, 1)  
        _OccPower("Occlusion Power", Range(0.0, 2.0)) = 0.5  
        _Alpha("Alpha", Range(0, 1)) = 1  
    }  
  
    SubShader  
    {  
        Tags  
        {  
            "Queue" = "Transparent+1"  
            "RenderType" = "Opaque"  
            "IgnoreProjector" = "True"  
        }  
  
        Pass  
        {  
            Name "BASE"  
  
            Blend SrcAlpha OneMinusSrcAlpha  
            Fog { Mode Off }  
            Lighting Off  
            ZWrite Off  
            ZTest Greater  
  
            CGPROGRAM  
            #include "UnityCG.cginc"  
            #pragma vertex vert  
            #pragma fragment frag  
            #pragma fragmentoption ARB_precision_hint_fastest  
  
            sampler2D _BumpMap;  
            fixed4 _OccColor;  
            fixed _OccPower;  
  
            struct a2v  
            {  
                fixed4 vertex : POSITION;  
                fixed3 normal : NORMAL;  
                fixed4 tangent : TANGENT;  
                fixed4 texcoord : TEXCOORD0;  
            };  
  
            struct v2f  
            {  
                fixed4 pos : SV_POSITION;  
                fixed2 uv : TEXCOORD0;  
                fixed3 viewDir : TEXCOORD1;  
            };  
  
            v2f vert(a2v v)  
            {  
                v2f o;  
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);  
                o.uv = v.texcoord.xy;  
  
                TANGENT_SPACE_ROTATION;  
                o.viewDir = normalize(mul(rotation, ObjSpaceViewDir(v.vertex)));  
  
                return o;  
            }  
  
            fixed4 frag(v2f i) : SV_Target  
            {  
                fixed3 n = UnpackNormal(tex2D(_BumpMap, i.uv));  
                fixed o = 1 - saturate(dot(n, i.viewDir));  
                fixed4 c = _OccColor * pow(o, _OccPower);  
  
                return c;  
            }  
  
            ENDCG  
        }  
    }  
  
    FallBack "Mobile/Diffuse"  
}  