using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEx
{
	public static class EX_CharacterController
	{
		/// <summary>
		/// Set CharacterControler's Properties
		/// </summary>
		/// <param name="sloplimit"> 移動可能な勾配値 </param>
		/// <param name="stepoffset"> 登れる段差の高さ </param>
		/// <param name="skinwidth"> めり込み防止の余裕(適正値はradiusの10%) </param>
		/// <param name="minmovedistance">最低限移動する距離</param>
		/// <param name="center"> コライダー座標値 </param>
		/// <param name="radius"> コライダー半径 </param>
		/// <param name="height"> コライダーの高さ(最低値はradiusの2倍) </param>
		public static void SetProperties(this CharacterController ctrl,
										Vector3 center,
										float radius,
										float height,
										float slopelimit = 45.0f,
										float stepoffset = 0.3f,
										float skinwidth = -1f, 
										float minmovedistance = 0.0f
										)
		{
			ctrl.center = center;
			ctrl.radius = radius;
			//高さが最低値(radius * 2)未満かチェック
			float hg = (height < (radius * 2)) ? (radius * 2) : height;
			ctrl.height = hg;
			ctrl.slopeLimit = slopelimit;
			//段差高度が高さ(height)を超過するかチェック
			float sto = stepoffset > height ? height : stepoffset;
			ctrl.stepOffset = sto;
			//余裕が未設定ならradiusの10％に設定
			float sw = skinwidth < 0 ? radius % 10 : skinwidth;
			ctrl.skinWidth = sw;
			ctrl.minMoveDistance = minmovedistance;
		}
	}
}
