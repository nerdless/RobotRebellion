Shader "Diplomado/Alpha" 
{
	Properties 
	{
		_MainTex ("Base", 2D) = "white" {}
		_TransVal("Transparency", Range(0,1)) = 0.5
	}
	
	SubShader 
	{
		CGPROGRAM
		#pragma surface surf Lambert alpha
			
		sampler2D _MainTex;
		float _TransVal;
		
		struct Input 
		{
			float2 uv_MainTex; 
		};
		
		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex); //obtiene un color de un vertice con respecto a su cordenada uv a una textura.
			o.Albedo = c.rgb; //es el color base del material es igual a la componente RGB de l vertice
			o.Alpha = c.r * _TransVal;    //accede al alpha
		}
		ENDCG
	}
		
	
	
	FallBack "Diffuse"
}
