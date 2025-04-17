Shader "Custom/StencilReader" {
    SubShader {
        Tags {"Queue"="Geometry+1"}
        Stencil {
            Ref 1
            Comp Equal
        }
        Pass {
            // Your regular water rendering
        }
    }
}
