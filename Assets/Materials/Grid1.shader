// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33486,y:32661,varname:node_3138,prsc:2|emission-6492-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32740,y:33191,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9686275,c2:0.1411765,c3:0.4509804,c4:1;n:type:ShaderForge.SFN_TexCoord,id:7863,x:31524,y:32745,varname:node_7863,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:7430,x:32043,y:32675,varname:node_7430,prsc:2|A-7863-U,B-3129-OUT,C-6299-Z;n:type:ShaderForge.SFN_Multiply,id:2647,x:32010,y:32853,varname:node_2647,prsc:2|A-7863-V,B-3129-OUT,C-6299-X;n:type:ShaderForge.SFN_Vector1,id:3129,x:31490,y:32657,varname:node_3129,prsc:2,v1:10;n:type:ShaderForge.SFN_Sin,id:3306,x:32251,y:32730,varname:node_3306,prsc:2|IN-7430-OUT;n:type:ShaderForge.SFN_Sin,id:1260,x:32228,y:32945,varname:node_1260,prsc:2|IN-2647-OUT;n:type:ShaderForge.SFN_Abs,id:2481,x:32445,y:32730,varname:node_2481,prsc:2|IN-3306-OUT;n:type:ShaderForge.SFN_Abs,id:1976,x:32445,y:32945,varname:node_1976,prsc:2|IN-1260-OUT;n:type:ShaderForge.SFN_Subtract,id:3050,x:32633,y:32700,varname:node_3050,prsc:2|A-9554-OUT,B-2481-OUT;n:type:ShaderForge.SFN_Subtract,id:3292,x:32633,y:32925,varname:node_3292,prsc:2|A-9554-OUT,B-1976-OUT;n:type:ShaderForge.SFN_Vector1,id:1031,x:32587,y:32846,varname:node_1031,prsc:2,v1:10;n:type:ShaderForge.SFN_Vector1,id:9554,x:32407,y:32868,varname:node_9554,prsc:2,v1:1;n:type:ShaderForge.SFN_Power,id:397,x:32812,y:32700,varname:node_397,prsc:2|VAL-3050-OUT,EXP-1031-OUT;n:type:ShaderForge.SFN_Power,id:4588,x:32809,y:32908,varname:node_4588,prsc:2|VAL-3292-OUT,EXP-1031-OUT;n:type:ShaderForge.SFN_Max,id:5599,x:33056,y:32748,varname:node_5599,prsc:2|A-397-OUT,B-4588-OUT;n:type:ShaderForge.SFN_Lerp,id:6492,x:33263,y:32716,varname:node_6492,prsc:2|A-9603-RGB,B-7241-RGB,T-5599-OUT;n:type:ShaderForge.SFN_Tex2d,id:8093,x:32508,y:33144,ptovrint:False,ptlb:node_8093,ptin:_node_8093,varname:node_8093,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2e03de08f5ceb5d48b6e70666aebceeb,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:9603,x:32924,y:33264,ptovrint:False,ptlb:node_9603,ptin:_node_9603,varname:node_9603,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.03137255,c2:0.04705883,c3:0.1921569,c4:1;n:type:ShaderForge.SFN_Color,id:2150,x:33223,y:32863,ptovrint:False,ptlb:node_2150,ptin:_node_2150,varname:node_2150,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ObjectScale,id:6299,x:31707,y:32545,varname:node_6299,prsc:2,rcp:False;proporder:7241-9603-2150;pass:END;sub:END;*/

Shader "Shader Forge/Grid1" {
    Properties {
        _Color ("Color", Color) = (0.9686275,0.1411765,0.4509804,1)
        _node_9603 ("node_9603", Color) = (0.03137255,0.04705883,0.1921569,1)
        _node_2150 ("node_2150", Color) = (0.5,0.5,0.5,1)
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
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float4, _node_9603)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
////// Lighting:
////// Emissive:
                float4 _node_9603_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_9603 );
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float node_9554 = 1.0;
                float node_3129 = 10.0;
                float node_1031 = 10.0;
                float3 emissive = lerp(_node_9603_var.rgb,_Color_var.rgb,max(pow((node_9554-abs(sin((i.uv0.r*node_3129*objScale.b)))),node_1031),pow((node_9554-abs(sin((i.uv0.g*node_3129*objScale.r)))),node_1031)));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
