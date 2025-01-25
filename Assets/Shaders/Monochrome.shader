Shader "Hidden/Monochrome"
{
    Properties
    {
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType"="Transparent"
        }

        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass 
        {
            Name "MONOCHROME"
            
            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareOpaqueTexture.hlsl" // needed to sample scene color/luminance

            #pragma vertex Vert // vertex shader is provided by the Blit.hlsl include
            #pragma fragment frag
            

            half4 frag(Varyings IN) : SV_TARGET
            {
                float2 uv = IN.texcoord;
                // float depth = Linear01Depth(IN.positionCS);
                
                float3 sceneColor = SampleSceneColor(uv);
                float average = (sceneColor.r + sceneColor.g + sceneColor.b) / 3;

                return float4(average, average, average, 1.0);
            }
            ENDHLSL
        }
    }
}