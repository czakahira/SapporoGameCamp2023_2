using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����N�^�[�X�e�[�^�X
/// </summary>
public class CharacterStatus : IStatus
{
	/// <summary>
	/// �̗�
	/// </summary>
	[SerializeField] protected float m_HealthPoint;
	/// <summary>
	/// �U����
	/// </summary>
	[SerializeField] protected float m_AttackPower;

	public float GetAttackPower() { return m_AttackPower; }
	public float GetHealthPoint() { return m_HealthPoint; }

}
