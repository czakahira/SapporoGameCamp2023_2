using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEx
{
	/// <summary>
	/// Animator拡張メソッド
	/// </summary>
	public static class EX_Animator
	{
		/// <summary>
		/// ハッシュより指定ステート中か取得します
		/// </summary>
		/// <param name="_info"></param>
		/// <param name="_fullPathHash"></param>
		/// <returns></returns>
		public static bool IsState(this AnimatorStateInfo _info, int _fullPathHash)
		{
			return (_info.fullPathHash.Equals(_fullPathHash));
		}

		/// <summary>
		/// ApplyRootMotionのON/OFF
		/// </summary>
		public static void SetRootMotion(this Animator anim, bool _value)
		{
			anim.applyRootMotion = _value;
		}

		/// <summary>
		/// トリガーの有効/無効を切り替えます
		/// </summary>
		public static void SetTrigger(this Animator anim, int id, bool value)
		{
			if (value) anim.SetTrigger(id);
			else anim.ResetTrigger(id);
		}
		public static void SetTrigger(this Animator anim, string name, bool value)
		{
			if (value) anim.SetTrigger(name);
			else anim.ResetTrigger(name);
		}

		/// <summary>
		/// 動的にアニメーションを遷移させる
		/// ※PlayのようにStateを直接再生する訳ではない
		/// </summary>
		/// <param name="anim">Animator</param>
		/// <param name="name">再生するAnimationStateName</param>
		/// <param name="duration">ブレンド時間</param>
		/// <param name="layer">指定AnimationStateのあるレイヤー</param>
		/// <param name="offset">指定アニメをいつから始めるか</param>
		/// <param name="normalizedTime"></param>
		public static void SetTransion(this Animator anim, string name, float duration, int layer, float offset = 0, float normalizedTime = 0)
		{
			anim.CrossFadeInFixedTime(name, duration, layer, offset, normalizedTime);
		}

	}
}