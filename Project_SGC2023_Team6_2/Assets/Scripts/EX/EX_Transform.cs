using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityEx
{
	public static class EX_Transform
	{
		/// <summary>
		/// 同名オブジェクトを全子オブジェクトから検索する
		/// </summary>
		/// <param name="name"></param>
		public static Transform FindDeep(this Transform self, string name)
		{
			Transform[] children = self.GetComponentsInChildren<Transform>();
			if(children.Length > 0) {
				return children.First(child => child.name.Equals(name));
			}
			return null;
		}
		/// <summary>
		/// 親を設定し、ローカル座標も初期化する
		/// </summary>
		public static void SetParentZero(this Transform _self, Transform _parent)
		{
			_self.SetParent(_parent);
			_self.localPosition = Vector3.zero;
			_self.localRotation = Quaternion.identity;
		}

		//▼ 移動系
		//---------------------------------------------
		//　速度(velocity)	= 加速度(accelaration) * 方向(direcion)
		//　移動			= 位置(position) + 速度
		//　秒速(m/s)		= １Fの移動距離 ( pos - prepos ) / delta
		//---------------------------------------------

		/// <summary>
		/// Transformによる移動
		/// </summary>
		/// <param name="self"></param>
		/// <param name="direction"> 方向 </param>
		/// <param name="accelaration"> 加速（m * dt） </param>
		public static void Move(this Transform self, Vector3 direction, float accelaration)
		{
			self.position += direction.normalized * accelaration;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="self"></param>
		/// <param name="velocity">速度/方向ベクトル</param>
		public static void Move(this Transform self, Vector3 velocity)
		{
			self.position += velocity;
		}

		/// <summary> 滑らかに回転(RotateTowards) </summary>
		/// <param name="direction"> 向く方向 </param>
		/// <param name="maxDegreeDelta"> 回転速度 </param>
		public static void SmoothRotateR(this Transform self, Vector3 direction, float maxDegreeDelta)
		{
			Quaternion targetQuat = Quaternion.LookRotation(direction.normalized);
			self.rotation = Quaternion.RotateTowards(self.rotation, targetQuat, maxDegreeDelta);
		}
		/// <summary> 滑らかに回転(Lerp) </summary>
		/// <param name="direction">向く方向</param>
		/// <param name="step"> 補間速度 </param>
		public static void SmoothRotateL(this Transform self, Vector3 direction, float step)
		{
			Quaternion targetQuat = Quaternion.LookRotation(direction.normalized);
			self.rotation = Quaternion.Lerp(self.rotation, targetQuat, step);
		}
		/// <summary> 滑らかに回転(Slerp) </summary>
		/// <param name="direction">向く方向</param>
		/// <param name="step">回転速度</param>
		public static void SmoothRotateS(this Transform self, Vector3 direction, float step)
		{
			Quaternion targetQuat = Quaternion.LookRotation(direction.normalized);
			self.rotation = Quaternion.Slerp(self.rotation, targetQuat, step);
		}

		/// <summary>
		/// 素早く回転
		/// ※引数方向に向けて瞬時に向き直り
		/// </summary>
		/// <param name="direction"> 方向ベクトル </param>
		public static void LookDir(this Transform self, Vector3 direction)
		{
			self.rotation = Quaternion.LookRotation(direction);
		}
	}
}
