using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEx
{
	/// <summary>
	/// 主要Collider拡張(Extend)メソッド
	/// </summary>
	public static class EX_Collider
	{
		/// <summary>
		/// 全Collider共通
		/// </summary>
		/// <param name="mat"></param>
		/// <param name="trigger"></param>
		//PhysicMaterialの付与
		public static void SetPhysicMat(this Collider col, PhysicMaterial mat)
		{
			col.material = mat;
		}
		//ColliderのisTrigger(物理判定)の有効化/無効化
		public static void IsTrigger(this Collider col, bool trigger)
		{
			col.isTrigger = trigger;
		}


		/// <summary>
		/// CapsuleCollider
		/// </summary>
		/// <param name="center"></param>
		/// <param name="size"></param>
		/// <param name="dir"></param>
		public static void SetCenter(this CapsuleCollider col, Vector3 center)
		{
			col.center = center;
		}
		public static void SetSize(this CapsuleCollider col, float radius, float height)
		{
			col.radius = radius;
			col.height = height;
		}
		public static void SetDirection(this CapsuleCollider col, Vector3 dir)
		{
			col.direction = dir == Vector3.right ? 0 :	//X軸
							dir == Vector3.up ? 1 :		//Y軸
							dir == Vector3.forward ? 2 ://Z軸
													 1; //それ以外はY軸
		}

		/// <summary>
		/// BoxCollider
		/// </summary>
		/// <param name="center"></param>
		/// <param name="size"></param>
		public static void SetCenter(this BoxCollider col, Vector3 center)
		{
			col.center = center;
		}
		public static void SetSize(this BoxCollider col, Vector3 size)
		{
			col.size = size;
		}

		/// <summary>
		/// SphereCollider
		/// </summary>
		/// <param name="center"></param>
		/// <param name="radius"></param>
		public static void SetCenter(this SphereCollider col, Vector3 center)
		{
			col.center = center;
		}
		public static void SetRadius(this SphereCollider col, float radius)
		{
			col.radius = radius;
		}

		//設置地点を基準にコライダーを延長
		public static void ExtendLength(this BoxCollider self, float lengthSpeed, float maxLength) 
		{
			bool IsHit = false;
			Vector3 pre_size = self.size;

		}
	}
}
