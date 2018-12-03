// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|diff-895-OUT,spec-4121-OUT,gloss-8856-OUT,normal-573-RGB,amdfl-6433-OUT;n:type:ShaderForge.SFN_Tex2d,id:3014,x:32133,y:32667,ptovrint:False,ptlb:dif,ptin:_dif,varname:node_3014,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:573,x:31865,y:32777,ptovrint:False,ptlb:norm,ptin:_norm,varname:node_573,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Multiply,id:4121,x:32314,y:33298,varname:node_4121,prsc:2|A-789-RGB,B-6770-OUT;n:type:ShaderForge.SFN_Tex2d,id:6748,x:31866,y:33600,ptovrint:False,ptlb:Local AO,ptin:_LocalAO,varname:node_6748,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:8856,x:32802,y:33256,ptovrint:False,ptlb:node_8856,ptin:_node_8856,varname:node_8856,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Color,id:789,x:31845,y:33379,ptovrint:False,ptlb:sPECcOLOUR,ptin:_sPECcOLOUR,varname:node_789,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4980392,c2:0.4980392,c3:0.4980392,c4:1;n:type:ShaderForge.SFN_Power,id:6770,x:32061,y:33871,varname:node_6770,prsc:2|VAL-6748-RGB,EXP-8897-OUT;n:type:ShaderForge.SFN_Vector1,id:8897,x:31797,y:33921,varname:node_8897,prsc:2,v1:3;n:type:ShaderForge.SFN_Cubemap,id:8979,x:31919,y:32267,ptovrint:False,ptlb:Metal  Cuvemap,ptin:_MetalCuvemap,varname:node_8979,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,pvfc:0|DIR-4641-OUT;n:type:ShaderForge.SFN_ViewReflectionVector,id:4641,x:31708,y:32241,varname:node_4641,prsc:2;n:type:ShaderForge.SFN_Blend,id:895,x:32404,y:32465,varname:node_895,prsc:2,blmd:6,clmp:True|SRC-6433-OUT,DST-3014-RGB;n:type:ShaderForge.SFN_Tex2d,id:4422,x:31897,y:32539,ptovrint:False,ptlb:MetalMAsk,ptin:_MetalMAsk,varname:node_4422,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6433,x:32133,y:32483,varname:node_6433,prsc:2|A-8979-RGB,B-4422-RGB;proporder:3014-573-6748-8856-789-8979-4422;pass:END;sub:END;*/

Shader "Shader Forge/DNS_AO_Cube" {
    Properties {
        _dif ("dif", 2D) = "white" {}
        _norm ("norm", 2D) = "bump" {}
        _LocalAO ("Local AO", 2D) = "white" {}
        _node_8856 ("node_8856", Range(0, 1)) = 0
        _sPECcOLOUR ("sPECcOLOUR", Color) = (0.4980392,0.4980392,0.4980392,1)
        _MetalCuvemap ("Metal  Cuvemap", Cube) = "_Skybox" {}
        _MetalMAsk ("MetalMAsk", 2D) = "white" {}
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
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _dif; uniform float4 _dif_ST;
            uniform sampler2D _norm; uniform float4 _norm_ST;
            uniform sampler2D _LocalAO; uniform float4 _LocalAO_ST;
            uniform float _node_8856;
            uniform float4 _sPECcOLOUR;
            uniform samplerCUBE _MetalCuvemap;
            uniform sampler2D _MetalMAsk; uniform float4 _MetalMAsk_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _norm_var = UnpackNormal(tex2D(_norm,TRANSFORM_TEX(i.uv0, _norm)));
                float3 normalLocal = _norm_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _node_8856;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _LocalAO_var = tex2D(_LocalAO,TRANSFORM_TEX(i.uv0, _LocalAO));
                float3 specularColor = (_sPECcOLOUR.rgb*pow(_LocalAO_var.rgb,3.0));
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _MetalMAsk_var = tex2D(_MetalMAsk,TRANSFORM_TEX(i.uv0, _MetalMAsk));
                float3 node_6433 = (texCUBE(_MetalCuvemap,viewReflectDirection).rgb*_MetalMAsk_var.rgb);
                indirectDiffuse += node_6433; // Diffuse Ambient Light
                float4 _dif_var = tex2D(_dif,TRANSFORM_TEX(i.uv0, _dif));
                float3 diffuseColor = saturate((1.0-(1.0-node_6433)*(1.0-_dif_var.rgb)));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _dif; uniform float4 _dif_ST;
            uniform sampler2D _norm; uniform float4 _norm_ST;
            uniform sampler2D _LocalAO; uniform float4 _LocalAO_ST;
            uniform float _node_8856;
            uniform float4 _sPECcOLOUR;
            uniform samplerCUBE _MetalCuvemap;
            uniform sampler2D _MetalMAsk; uniform float4 _MetalMAsk_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _norm_var = UnpackNormal(tex2D(_norm,TRANSFORM_TEX(i.uv0, _norm)));
                float3 normalLocal = _norm_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _node_8856;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _LocalAO_var = tex2D(_LocalAO,TRANSFORM_TEX(i.uv0, _LocalAO));
                float3 specularColor = (_sPECcOLOUR.rgb*pow(_LocalAO_var.rgb,3.0));
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MetalMAsk_var = tex2D(_MetalMAsk,TRANSFORM_TEX(i.uv0, _MetalMAsk));
                float3 node_6433 = (texCUBE(_MetalCuvemap,viewReflectDirection).rgb*_MetalMAsk_var.rgb);
                float4 _dif_var = tex2D(_dif,TRANSFORM_TEX(i.uv0, _dif));
                float3 diffuseColor = saturate((1.0-(1.0-node_6433)*(1.0-_dif_var.rgb)));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
