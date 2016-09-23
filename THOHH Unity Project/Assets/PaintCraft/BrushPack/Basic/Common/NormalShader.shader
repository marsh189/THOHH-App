Shader "AIO_Tools/Normal" {	
	Properties {		
		_MainTex ("Swatch", 2D) = "white" {}	
	}

   SubShader {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Cull Off
	Lighting Off
	ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha
    Pass {
     CGPROGRAM //Shader Start, Vertex Shader named vert, Fragment shader named frag
     #pragma vertex vert
     #pragma fragment frag
     #include "UnityCG.cginc"
     //Link properties to the shader
     
     sampler2D _MainTex;
     
     
     struct v2f 
     {
	     float4  pos : SV_POSITION;
	     float2  uv : TEXCOORD0;
	     fixed4  color : COLOR;
     };

     float4 _MainTex_ST;
  

     v2f vert (appdata_full v)
     {
	     v2f o;
	     o.pos = mul (UNITY_MATRIX_MVP, v.vertex); 
	     o.uv = TRANSFORM_TEX (v.texcoord, _MainTex); 
	
	     o.color =  v.color;
	     return o;
     }

     half4 frag (v2f i) : COLOR
     {
         fixed4 color = i.color;
	 color.a *= tex2D(_MainTex, i.uv).a;
         return color;
     }

     ENDCG //Shader End
    }
   }
}

