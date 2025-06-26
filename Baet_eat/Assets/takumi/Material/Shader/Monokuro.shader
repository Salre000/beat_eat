Shader "Unlit/Monokuro"
{
Properties
    {
        [KeywordEnum(Simple, NTSC)] _GrayType("GrayType", Int) = 1 //�O���[�X�P�[��������ۂ̌v�Z�^�C�v
        _BlendRate ("BlendRate", Range(0.0, 1.0)) = 1 //���S�ɃO���[�X�P�[��������܂ł̊���
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
            //�v���p�e�B�ɑΉ�����ϐ��̐錾
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
                //�P�����ϖ@�Ŋ��S�ȃO���[�X�P�[���������l���擾
                fixed gray = (col.r + col.g + col.b) / 3;
                grayCol = fixed3(gray, gray, gray);
#elif _GRAYTYPE_NTSC
                //NTSC���d���ϖ@�Ŋ��S�ȃO���[�X�P�[���������l���擾
                fixed gray = dot(col.rgb, fixed3(0.299, 0.587, 0.114));
                grayCol = fixed3(gray, gray, gray);
#endif
                //_BlendRate���Q�Ƃ��Alerp�ŃI���W�i���F�Ɗ��S�ȃO���[�X�P�[�����̊Ԃ̒l��ݒ�
                col.rgb = lerp(col.rgb, grayCol, _BlendRate);

                return col;
            }
            ENDCG
        }
    }}
