// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-6025-OUT;n:type:ShaderForge.SFN_Multiply,id:3289,x:32298,y:32890,varname:node_3289,prsc:2|A-8129-RGB,B-4962-RGB;n:type:ShaderForge.SFN_TexCoord,id:9292,x:31817,y:32968,varname:node_9292,prsc:2,uv:1;n:type:ShaderForge.SFN_Tex2d,id:4962,x:32043,y:32968,ptovrint:False,ptlb:Ao,ptin:_Ao,varname:node_4962,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9292-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:8129,x:32043,y:32748,ptovrint:False,ptlb:Albedo,ptin:_Albedo,varname:node_8129,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Cubemap,id:7249,x:31748,y:32199,ptovrint:False,ptlb:Metal  Cuvemap,ptin:_MetalCuvemap,varname:node_8979,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,pvfc:0|DIR-8500-OUT;n:type:ShaderForge.SFN_ViewReflectionVector,id:8500,x:31558,y:32241,varname:node_8500,prsc:2;n:type:ShaderForge.SFN_Blend,id:6025,x:32303,y:32491,varname:node_6025,prsc:2,blmd:6,clmp:True|SRC-1884-OUT,DST-3289-OUT;n:type:ShaderForge.SFN_Tex2d,id:2675,x:31648,y:32579,ptovrint:False,ptlb:MetalMAsk,ptin:_MetalMAsk,varname:node_4422,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:217,x:31866,y:32438,varname:node_217,prsc:2|A-7249-RGB,B-2675-RGB;n:type:ShaderForge.SFN_Multiply,id:1884,x:32044,y:32256,varname:node_1884,prsc:2|A-9215-OUT,B-217-OUT;n:type:ShaderForge.SFN_Slider,id:9215,x:32224,y:32332,ptovrint:False,ptlb:node_9215,ptin:_node_9215,varname:node_9215,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:4962-8129-7249-2675-9215;pass:END;sub:END;*/

Shader "Shader Forge/Unlit_AO_cube" {
    Properties {
        _Ao ("Ao", 2D) = "white" {}
        _Albedo ("Albedo", 2D) = "white" {}
        _MetalCuvemap ("Metal  Cuvemap", Cube) = "_Skybox" {}
        _MetalMAsk ("MetalMAsk", 2D) = "white" {}
        _node_9215 ("node_9215", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Ao; uniform float4 _Ao_ST;
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform samplerCUBE _MetalCuvemap;
            uniform sampler2D _MetalMAsk; uniform float4 _MetalMAsk_ST;
            uniform float _node_9215;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 _MetalMAsk_var = tex2D(_MetalMAsk,TRANSFORM_TEX(i.uv0, _MetalMAsk));
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float4 _Ao_var = tex2D(_Ao,TRANSFORM_TEX(i.uv1, _Ao));
                float3 emissive = saturate((1.0-(1.0-(_node_9215*(texCUBE(_MetalCuvemap,viewReflectDirection).rgb*_MetalMAsk_var.rgb)))*(1.0-(_Albedo_var.rgb*_Ao_var.rgb))));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
