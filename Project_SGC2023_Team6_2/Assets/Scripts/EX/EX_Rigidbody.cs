using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEx
{
	/// <summary>
	/// Rigidbody拡張メソッド
	/// </summary>
	public static class EX_Rigidbody
	{
		/// <summary> 標準重力加速度 </summary>
		public readonly static float g_Geavity = -9.81f;

		/// <summary>
		/// オブジェクトの重量・体重(kg)設定
		/// </summary>
		/// <param name="rb"></param>
		/// <param name="mass"> 重量(kg) </param>
		public static void SetMass(this Rigidbody rb, float mass) { rb.mass = mass; }

		/// <summary>
		/// Unity重力影響の受け付け
		/// </summary>
		/// <param name="rb"></param>
		/// <param name="gravity"></param>
		public static void SetGravity(this Rigidbody rb, bool gravity = true) { rb.useGravity = gravity; }
		/// <summary>
		/// Rigidbodyの移動の補間方法設定
		/// </summary>
		public static void SetInterpolate(this Rigidbody rb, RigidbodyInterpolation interpolate)
		{
			// None			- 補間を適用しない
			// Interpolate	- 前フレームの Transform にもとづいて Transform をスムージング
			// Extrapolate	- 次フレームの Transform を予測して Transform をスムージング
			rb.interpolation = interpolate;
		}
		/// <summary>
		/// Rigidbodyの衝突検知の制度を設定する
		/// <see href="https://ekulabo.com/collision-detection-performance"> どれが何のモードなのかは、説明がムズいので上記リンクを要参照 </see>
		/// </summary>
		public static void SetColDetection(this Rigidbody rb, CollisionDetectionMode mode)
		{
			rb.collisionDetectionMode = mode;
		}
		/// <summary>
		/// 衝突や自身のvelocityにおける自然な 移動・回転する軸制限
		/// </summary>
		public static void SetConstraints(this Rigidbody rb, Vector3 pos, Vector3 rot)
		{
			//呼ばれたらリセット
			RigidbodyConstraints constraints = RigidbodyConstraints.None;

			//移動
			if (pos.x != 0) constraints |= RigidbodyConstraints.FreezePositionX;
			if (pos.y != 0) constraints |= RigidbodyConstraints.FreezePositionY;
			if (pos.z != 0) constraints |= RigidbodyConstraints.FreezePositionZ;
			//回転
			if (rot.x != 0) constraints |= RigidbodyConstraints.FreezeRotationX;
			if (rot.y != 0) constraints |= RigidbodyConstraints.FreezeRotationY;
			if (rot.z != 0) constraints |= RigidbodyConstraints.FreezeRotationZ;

			rb.constraints = constraints;
		}

		/// <summary>
		/// ブレーキ X.Z軸
		/// </summary>
		/// <param name="rb"></param>
		/// <param name="multiplier"></param>
		/// <returns></returns>
		public static Vector3 Brake_XZ(this Rigidbody rb, float multiplier = 0)
		{
			Vector3 velociy = new Vector3(rb.velocity.x, 0, rb.velocity.z) * multiplier + new Vector3(0, rb.velocity.y, 0);
			return velociy;
		}
		/// <summary>
		/// ブレーキ Y軸
		/// </summary>
		/// <param name="rb"></param>
		/// <param name="multiplier"></param>
		/// <returns></returns>
		public static Vector3 Brake_Jump(this Rigidbody rb, float multiplier = 1)
		{
			Vector3 velociy = new Vector3(rb.velocity.x, 0, rb.velocity.z) * multiplier;
			return velociy;
		}

		/// <summary>
		/// 加速による移動
		/// </summary>
		/// <param name="rb"></param>
		/// <param name="direction">移動方向</param>
		/// <param name="speed">移動速度</param>
		/// <param name="useMass">質量を考慮するか</param>
		public static void MoveAddForce(this Rigidbody rb, Vector3 direction, float speed, bool useMass = false)
		{
			if (useMass) {
				rb.AddForce(direction * speed, ForceMode.Force);
			} else {
				rb.AddForce(direction * speed, ForceMode.Acceleration);
			}
		}

		/// <summary>
		/// 速度変更による移動
		/// ※Unity公式では物理挙動が狂うとして推奨されていない
		/// </summary>
		/// <param name="rb">  </param>
		/// <param name="direction"> 移動方向 </param>
		/// <param name="speed"> 移動速度 </param>
		/// <param name="consier"> 考慮されるべき速度 </param>
		public static void MoveVelocity(this Rigidbody rb, Vector3 direction, float speed, Vector3 consier, float delta)
		{
			float Speed = speed * delta;

			Vector3 considering = new Vector3(
				consier.x != 0 ? rb.velocity.x : 0,
				consier.y != 0 ? rb.velocity.y : 0,
				consier.z != 0 ? rb.velocity.z : 0 );

			rb.velocity = (direction.normalized * Speed) + considering;
		}

		/// <summary>
		/// from から To にかけて、補間をかけて回転します
		/// </summary>
		public static void MoveRotateTowards(this Rigidbody rigidbody, Quaternion from, Quaternion to, float maxDegreesDelta)
		{
			rigidbody.MoveRotation(Quaternion.RotateTowards(from, to, maxDegreesDelta));
		}
		/// <summary>
		/// 指定方向へ、指定度毎秒で回転します
		/// </summary>
		public static void MoveLookRotateTowards(this Rigidbody rigidbody, Vector3 direction, float maxDegreesDelta, Vector3 upward)
		{
			rigidbody.MoveRotation(Quaternion.RotateTowards(rigidbody.rotation, Quaternion.LookRotation(direction, upward), maxDegreesDelta)); ;
		}

	}
}
