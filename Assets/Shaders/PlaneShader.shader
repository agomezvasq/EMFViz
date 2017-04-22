// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/NewUnlitShader"
{
	Properties
	{
		_Position1 ("Position of Sphere 1", Vector) = (2.04, -0.12, -10, 0.0)
		_Charge1 ("Charge of Sphere 1", Float) = 1.0
		_Position2 ("Position of Sphere 2", Vector) = (-4.01, -0.71, -10, 0.0)
		_Charge2 ("Charge of Sphere 2", Float) = -1.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 world_space_pos : TEXCOORD1;
			};

			float4 _MainTex_ST;
			float4 _Position1;
			float _Charge1;
			float4 _Position2;
			float _Charge2;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.world_space_pos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}

			float hyp(float x, float y) {
				return sqrt(x * x + y * y);
			}

			float mag(float2 f2) {
				return hyp(f2.x, f2.y);
			}

			float angle(float2 f2) {
				return atan2(f2.y, f2.x);
			}

			float inverse_square(float x) {
				return 1.0 / (x * x);
			}

			float g(float x) {
				return 2.0 * x / (x + 1.0);
			}

			float t(float x) {
				return log(x) / (log(x) + 1);
			}

			float3 col(float x) {
				if (x >= 0.0) {
					return float3(x, 0.0, 0.0);
				} else {
					return float3(0.0, -x, -x);
				}
			}

			float3 rgb_to_hsv(float r, float g, float b) {
				float max_value = max(r, max(g, b));
				float min_value = min(r, min(g, b));
				float delta = max_value - min_value;

				float h = 0.0;
				float s = 0.0;
				float v = 0.0;
				if (max_value != 0) {
					s = delta / max_value;
				} else {
					return float3(0.0, 0.0, 0.0);
				}

				if (max_value == r) {
					h = (g - b) / delta;
				} 
				else if (max_value == g) {
					h = 2.0 + (b - r) / delta;
				}
				else {
					h = 4.0 + (r - g) / delta;
				} 
				h *= 60.0;
				if (h < 0.0) {
					h += 360.0;
				}
				v = max_value;

				return float3(h, s, v);
			} 

			float3 hsv_to_rgb(float h, float s, float v) {
				int i = 0;
				float f = 0.0;
				float p = 0.0;
				float q = 0.0;
				float t = 0.0;

				float r = 0.0;
				float g = 0.0; 
				float b = 0.0;
				if (s == 0) {
					return float3(v, v, v);
				}
				h /= 60.0;
				i = floor(h);
				f = h - i;
				p = v * (1 - s);
				q = v * (1 - s * f);
				t = v * (1 - s * (1 - f));

				switch(i) {
					case 0:
						r = v;
						g = t;
						b = p;
						break;
					case 1:
						r = q;
						g = v;
						b = p;
						break;
					case 2:
						r = p;
						g = v;
						b = t;
						break;
					case 3:
						r = p;
						g = q;
						b = v;
						break;
					case 4:
						r = t;
						g = p;
						b = v;
						break;
					default:
						r = v;
						g = p;
						b = q;
						break;
				}

				return float3(r, g, b);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 pixel_to_sphere1 = i.world_space_pos - _Position1;
				float2 pixel_to_sphere2 = i.world_space_pos - _Position2;
				float mag1 = mag(pixel_to_sphere1);
				float mag2 = mag(pixel_to_sphere2);
				pixel_to_sphere1 = pixel_to_sphere1 / mag1 * 1.0 / (mag1 * mag1);
				pixel_to_sphere2 = pixel_to_sphere2 / mag2 * -1.0 / (mag2 * mag2);
				float2 f2 = pixel_to_sphere1 + pixel_to_sphere2;
				float mag3 = mag(f2);
				float theta = angle(f2) + 2.0 * 3.14159;
				float3 color1 = col(cos(theta));
				float3 color1_hsv = rgb_to_hsv(color1.x, color1.y, color1.z);
				color1_hsv.z = g(mag3);
				color1 = hsv_to_rgb(color1_hsv.x, color1_hsv.y, color1_hsv.z);
				float4 color1_rgba = float4(color1, 1.0);
				return color1_rgba;
			}

			//fixed4 add_colors(fixed4 x1, fixed4 x2) {
			//	fixed4 result =
			//}
			ENDCG
		}
	}
}
