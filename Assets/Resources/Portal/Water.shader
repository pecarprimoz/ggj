#warning Upgrade NOTE: unity_Scale shader variable was removed; replaced 'unity_Scale.w' with '1.0'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/HawkWater_Tesselated" {
    Properties{
        _Tess("Tessellation", Range(1,32)) = 4
        _WaveScale("Wave scale", Range(0.02,0.15)) = 0.063
        _ReflDistort("Reflection distort", Range(0,1.5)) = 0.44
        _RefrDistort("Refraction distort", Range(0,1.5)) = 0.40
        _RefrColor("Refraction color", COLOR) = (.34, .85, .92, 1)
        _Fresnel("Fresnel (A) ", 2D) = "gray" {}
        _BumpMap("Normalmap ", 2D) = "bump" {}
        WaveSpeed("Wave speed (map1 x,y; map2 x,y)", Vector) = (19,9,-16,-7)
        _ReflectiveColor("Reflective color (RGB) fresnel (A) ", 2D) = "" {}
        _ReflectiveColorCube("Reflective color cube (RGB) fresnel (A)", Cube) = "" { TexGen CubeReflect }
        _HorizonColor("Simple water horizon color", COLOR) = (.172, .463, .435, 1)
        _MainTex("Fallback texture", 2D) = "" {}
        _ReflectionTex("Internal Reflection", 2D) = "" {}
        _RefractionTex("Internal Refraction", 2D) = "" {}
    }
        SubShader{

            Tags { "RenderType" = "Opaque" }
            LOD 300


            CGPROGRAM
        // Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it does not contain a surface program or both vertex and fragment programs.
        #pragma exclude_renderers gles

                 #pragma surface surf BlinnPhong addshadow fullforwardshadows vertex:disp tessellate:tessDistance nolightmap

                #pragma target 5.0
                #include "Tessellation.cginc"

                sampler2D _MainTex;
                sampler2D _ReflectionTex;
                sampler2D _ReflectiveColor;
                sampler2D _BumpMap;

                float _Tess;
                float4 _WaveScale4;
                float4 _WaveOffset;
                float T;
                float _ReflDistort;



                //////////////////////////////////////
                //float4
                //////////////////////////////////////

                struct Input {
                    float2 uv_MainTex;
                    float3 worldPos;
                    float3 viewDir;
                    float4 screenPos;
                    INTERNAL_DATA
                };

                struct appdata {
                    float4 vertex : POSITION;
                    float4 tangent : TANGENT;
                    float3 normal : NORMAL;
                    float2 texcoord : TEXCOORD0;
                };

                float4 tessDistance(appdata v0, appdata v1, appdata v2) {
                    float minDist = 0.0;
                    float maxDist = 100.0;
                    return UnityDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, minDist, maxDist, _Tess);
                }

                void disp(inout appdata v)
                {
                    // Just do a really simple Sine wave
                    v.vertex.y += sin((T / 10) * v.vertex.z);
                }

                void surf(Input IN, inout SurfaceOutput o) {
                    // scroll bump waves

                    // The vertex location (This needs to be the actual vertex location)
                    float4 vertL = float4(IN.worldPos,1);
                    // Manually mulled the vert location with the world view matrix
                    float4 secondaryLocation = UnityObjectToClipPos(vertL);

                    // Scroll the bump waves
                    float4 temp;
                    temp.xyzw = vertL.xzxz * _WaveScale4 / 1.0 + _WaveOffset;
                    float2 bumpuv0 = temp.xy;
                    float2 bumpuv1 = temp.wz;

                    // Compute the screen positino
                    float4 ref = ComputeScreenPos(secondaryLocation);

                    // Normalize and calculate the view direction
                    float3 viewDir = ObjSpaceViewDir(vertL);
                    viewDir = normalize(viewDir);


                    // combine two scrolling bumpmaps into one
                    half3 bump1 = UnpackNormal(tex2D(_BumpMap, bumpuv0)).rgb;
                    half3 bump2 = UnpackNormal(tex2D(_BumpMap, bumpuv1)).rgb;
                    half3 bump = (bump1 + bump2) * 0.5;

                    // fresnel factor
                    half fresnelFac = dot(viewDir, bump);

                    // Take all the info and actually generate the reflective surface

                    float4 uv1 = ref; uv1.xy += bump * _ReflDistort;
                    half4 refl = tex2Dproj(_ReflectionTex, UNITY_PROJ_COORD(uv1));

                    float4 c_holder;

                    half4 water = tex2D(_ReflectiveColor, float2(fresnelFac,fresnelFac));
                    c_holder.rgb = lerp(water.rgb, refl.rgb, water.a);
                    c_holder.a = refl.a * water.a;

                    // Set the computed information
                    o.Albedo = c_holder.rgb;
                    o.Alpha = c_holder.a;
                }


                ENDCG


    }
        FallBack "Diffuse"
}