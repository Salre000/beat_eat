Shader "Unlit/Monokuro"
{
Properties
    {
        [KeywordEnum(Simple, NTSC)] _GrayType("GrayType", Int) = 1 //グレースケール化する際の計算タイプ
        _BlendRate ("BlendRate", Range(0.0, 1.0)) = 1 //完全にグレースケール化するまでの割合
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" }
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _GRAYTYPE_SIMPLE _GRAYTYPE_NTSC

            struct appdata
            {
                fixed2 uv : TEXCOORD0;
                fixed4 vertex : POSITION;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                fixed2 uv : TEXCOORD0;
                fixed4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            //プロパティに対応する変数の宣言
            fixed _GrayType;
            fixed _BlendRate;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= i.color;

                fixed3 grayCol;
#ifdef _GRAYTYPE_SIMPLE
                //単純平均法で完全なグレースケール化した値を取得
                fixed gray = (col.r + col.g + col.b) / 3;
                grayCol = fixed3(gray, gray, gray);
#elif _GRAYTYPE_NTSC
                //NTSC加重平均法で完全なグレースケール化した値を取得
                fixed gray = dot(col.rgb, fixed3(0.299, 0.587, 0.114));
                grayCol = fixed3(gray, gray, gray);
#endif
                //_BlendRateを参照し、lerpでオリジナル色と完全なグレースケール化の間の値を設定
                col.rgb = lerp(col.rgb, grayCol, _BlendRate);

                return col;
            }
            ENDCG
        }
    }}
