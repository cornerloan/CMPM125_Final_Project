Shader "Custom/OutlineShader"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (1,1,0,1)
        _OutlineWidth ("Outline Width", Range(0.0, 0.1)) = 0.02
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Name "Outline"
            Tags { "LightMode"="Always" }
            
            Cull Front
            ZWrite On
            ZTest LEqual
            
            ColorMask 0
            Blend Zero One

            Stencil
            {
                Ref 2
                Comp Always
                Pass Replace
            }
        }

        Pass
        {
            Name "Outline Fill"
            Tags { "LightMode"="Always" }
            
            Cull Back
            ZWrite Off
            ZTest Always

            Stencil
            {
                Ref 2
                Comp Equal
            }
            
            Color [_OutlineColor]
            Offset 0, [_OutlineWidth]
        }
    }
}
