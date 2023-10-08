using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インターフェース：ステータス
/// </summary>
public interface IStatus
{
	/// <summary>
	/// 攻撃力
	/// </summary>
	public float GetAttackPower();
	/// <summary>
	/// 体力
	/// </summary>
	public float GetHealthPoint();
}
