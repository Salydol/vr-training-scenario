// контур для подсветки объектов
// рисуем только задние грани и раздуваем меш от центра, получается обводка вокруг объекта
// раздуваю от центра а не по нормалям, потому что у куба по нормалям контур рвется на углах
Shader "VRTraining/Outline"
{
    Properties
    {
        _OutlineColor ("Color", Color) = (1, 0.8, 0.1, 1)
        _OutlineWidth ("Width", Range(0, 0.1)) = 0.015
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" "Queue" = "Geometry+1" }

        Pass
        {
            Name "Outline"
            Tags { "LightMode" = "SRPDefaultUnlit" }

            Cull Front
            ZWrite On

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                // это нужно чтобы в шлеме рисовалось в оба глаза
                UNITY_VERTEX_OUTPUT_STEREO
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _OutlineColor;
                float _OutlineWidth;
            CBUFFER_END

            Varyings vert(Attributes input)
            {
                Varyings output;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                // направление от центра объекта к вершине
                float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
                float3 centerWS = TransformObjectToWorld(float3(0, 0, 0));
                float3 direction = normalize(positionWS - centerWS);

                // толщина в метрах, поэтому от масштаба объекта не зависит
                positionWS += direction * _OutlineWidth;

                output.positionCS = TransformWorldToHClip(positionWS);
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                return _OutlineColor;
            }
            ENDHLSL
        }
    }
}
