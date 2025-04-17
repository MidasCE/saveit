Shader "Custom/StencilWriter" {
    SubShader {
        Tags {"Queue"="Geometry"}
        Stencil {
            Ref 1
            Comp Always
            Pass Replace
        }
        ColorMask 0
        ZWrite Off
        Pass {}
    }
}
