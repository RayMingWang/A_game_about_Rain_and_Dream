
Shader "Shaders101/Texture"
{
	Properties{
		_MainTex("Texture", 2D) = "white"{}
		_SecondTex("Second Texture",2D) = "white"{}
		_Tween("Tween",Range(0,1)) = 0
		_Tile("Tile",Range(1,10))=1
		[MaterialToggle] _Greyout("Grey out",float) = 0
		_Color("Color",Color) = (1,1,1,1)
		
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			//Blend One One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _SecondTex;
			float _Tween;
			int _Tile;
			float _Greyout;
			float4 _Color;



			float4 frag(v2f i) : SV_Target
			{

				
				
				float4 color = ((tex2D(_MainTex,i.uv *_Tile)*(_Tween-1)
									+tex2D(_SecondTex,i.uv *_Tile)*(_Tween)
									)* _Color);
				if(_Greyout!=0){
					float luminance = 0.3*color.r+0.59*color.g+0.11*color.b;
					color = (luminance,luminance,luminance,color.a)* _Color;
				}
				return color;
			}
			ENDCG
		}
	}
}