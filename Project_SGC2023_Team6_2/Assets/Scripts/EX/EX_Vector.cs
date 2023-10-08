using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEx
{
	public static class VectorEX
	{
		/// <summary>Shorthand for writing Vector3(1, 0, 1)</summary>
		public static Vector3 Horizontal => new Vector3(1,0,1);
		/// <summary>Shorthand for writing Vector3(1, 1, 0)</summary>
		public static Vector3 Dimention2 => new Vector3(1,1,0);


		/* ※角度関係で覚えておくべきこと
		 * ---------------------------------------------
		 * 
		 *　Mathf.PI		 円周率		= 3.14159265358979... 
		 *　Mathf.Deg2Rad	 ラジアン	= (2 * Mathf.PI) / 360 = π/180
		 *　Mathf.Rad2Deg	 角度		= 360 / (2 * Mathf.PI) = 180/π
		 *　360°			 円周		= 2 * Mathf.PI
		 *　degree(deg)		 度			= rad * Mathf.Rad2Deg
		 *　radian(rad)		 円弧の長さ	= deg * Mathf.Deg2Rad
		 *　
		 * ---------------------------------------------
		 * 
		 * ・Quaternion.AngleAxis(angle, Vector3.up);
		 * ・Quaternion.Euler(0,angle,0);
		 * 
		 *  上記２パターンで得られる回転は同じになります
		 * 
		 * 
		 * transform.forward の内部計算は以下になります
		 * 
		 * return trandform.rotation * Vector3.forward;
		 * 
		 */

#if false // Unity Internal
		public static void Normalize(this Vector3 v)
		{
			float mag = v.Magnitude();
			v = v / mag;
		}
		public static float Distance(Vector3 a, Vector3 b)
		{
			return (a - b).Magnitude();
		}
		public static float Dot(Vector3 from, Vector3 to)
		{
			float dot = (from.x * to.x) + (from.y * to.y) + (from.z * to.z);
			return dot;
		}
		public static float Dot_Normalize(Vector3 from, Vector3 to)
		{
			float dot = Dot(from.normalized, to.normalized);
			return dot;
		}

		/// <summary> ２つのベクトルの外積を取得 </summary>
		public static Vector3 Cross(Vector3 from, Vector3 to)
		{
			float x = (from.y * to.z) - (from.z * to.y);
			float y = (from.z * to.x) - (from.x * to.z);
			float z = (from.x * to.y) - (from.y * to.x);
			return new Vector3(x,y,z);
		}
		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			float mag = Dot(onNormal, onNormal);
			if(mag >= Mathf.Epsilon)
			{
				float dot = Dot(vector, onNormal);
				return onNormal * dot / mag;
			}
			return Vector3.zero;
		}
		public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
		{
			float mag = Dot(planeNormal, planeNormal);
			if (mag >= Mathf.Epsilon)
			{
				float dot = Dot(vector, planeNormal);
				#region formula
				//                return new Vector3(vector.x - planeNormal.x * dot / mag,
				//                                   vector.y - planeNormal.y * dot / mag,
				//                                   vector.z - planeNormal.z * dot / mag);
				#endregion
				return vector - planeNormal * dot / mag;
			}
			return Vector3.zero;
		}

#region Get Vector (sqr)magnitude
		public static float SqrMagnitude(this Vector2 v)
		{
			float v1 = (v.x * v.x);
			float v2 = (v.y * v.y);
			float a = v1 + v2;
			return a;
		}
		public static float SqrMagnitude(this Vector3 v)
		{
			float v1 = (v.x * v.x);
			float v2 = (v.y * v.y);
			float v3 = (v.z * v.z);
			float a = v1 + v2 + v3;
			return a;
		}
		public static float Magnitude(this Vector2 v)
		{
			float root = (float)Math.Sqrt(SqrMagnitude(v));
			return root;
		}
		public static float Magnitude(this Vector3 v)
		{
			float root = (float)Math.Sqrt(SqrMagnitude(v));
			return root;
		}
#endregion

		/// <summary>
		/// 反射ベクトルを求める
		/// </summary>
		public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
		{
			float dot = Dot(inNormal, inDirection);
			float rate = -2f;
			float factor = rate * dot;
			return factor * inNormal + inDirection;
		}

		public static float Angle(Vector3 from, Vector3 to)
		{
			float numerator = Dot(from, to);
			float denominator = (float)Math.Sqrt(from.SqrMagnitude() * to.SqrMagnitude());
			float adjust = Mathf.Clamp((numerator / denominator), -1, 1);
			float arccos = (float)Math.Acos(adjust);
			float angle = arccos * Mathf.Rad2Deg;
			
			return angle;
		} 
		public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
		{
			float angle = Angle(from, to);
			Vector3 cross = Cross(from, to);
			float sign = Math.Sign((axis.x * cross.x) + (axis.y * cross.y) + (axis.z * cross.z));
			float result = angle * sign;

			return result;
		}
#endif

		/// <summary> 速度 = 距離 / 時間 </summary>
		public static float GetSpeed(float distance, float time)
		{
			return distance / time;
		}
		/// <summary> 時間 = 距離 / 速度 </summary>
		public static float GetTime(float distance, float speed)
		{
			return distance / speed;
		}
		/// <summary> 距離 = 速度 * 時間 </summary>
		public static float GetDistance(float speed, float time)
		{
			return speed * time;
		}


		/// <summary>
		/// 二次ベジェ曲線による座標取得
		/// </summary>
		/// <param name="p0">起点</param>
		/// <param name="p1">中点</param>
		/// <param name="p2">終点</param>
		/// <param name="t">(0～1)</param>
		/// <returns></returns>
		public static Vector3 GetPoint_QuadraticBezierCurves(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			Vector3 point = Vector3.zero;

			float u = 1 - t;
			float uu = u * u;
			float tt = t * t;

			point = (uu * p0) + (2 * u * t * p1) + (tt * p2);

			return point;
		}
	}

   
}
