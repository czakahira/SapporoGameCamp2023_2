using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターステータス
/// </summary>
public class CharacterStatus : IStatus
{
	/// <summary>
	/// 体力
	/// </summary>
	[SerializeField] protected float m_HealthPoint;
	/// <summary>
	/// 攻撃力
	/// </summary>
	[SerializeField] protected float m_AttackPower;

	public float GetAttackPower() { return m_AttackPower; }
	public float GetHealthPoint() { return m_HealthPoint; }

}
