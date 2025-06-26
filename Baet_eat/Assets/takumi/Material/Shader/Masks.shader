Shader "Custom/StencilMask"
{
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }
        }
    }
}
