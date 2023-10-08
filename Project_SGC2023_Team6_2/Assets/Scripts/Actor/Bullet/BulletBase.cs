using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃オブジェクトの基底クラス
/// </summary>
public class BulletBase : MonoBehaviour
{
	/// <summary>
	/// 接触判定タイプ
	/// </summary>
	[SerializeField] protected Collision2DDetector m_Detector;
	/// <summary>
	/// コライダー
	/// </summary>
	[SerializeField] protected Collider2D m_Collider;
	/// <summary>
	/// 専用の剛体
	/// </summary>
	[SerializeField] protected Rigidbody2D m_Rb;
	/// <summary>
	/// 攻撃データ
	/// </summary>
	[SerializeField] protected AttackData m_AttackData;
	/// <summary>
	/// 射出方向
	/// </summary>
	[SerializeField] protected Vector2 m_Direction;
	 
	/// <summary>
	/// 現在使用中
	/// </summary>
	[field: SerializeField] public bool isUsing { get; protected set; }

	
	public virtual void Awake()
	{
		isUsing = false;
		m_Detector.onTrigger2D += OnTrigger2D;
	}
	public virtual void Update()
	{

	}

	/// <summary>
	/// 発射
	/// </summary>
	public virtual void Shoot(Vector2 _direction)
	{
		isUsing = true;
		m_Direction = _direction;
	}
	/// <summary>
	/// 当たった時
	/// </summary>
	public virtual void Hit()
	{
		isUsing = false;

		Destroy(this);
	}

	/// <summary>
	/// 接触イベント
	/// </summary>
	public virtual void OnTrigger2D(EContactType type, Collider2D collider2D)
	{
		if (type == EContactType.Enter) {
			Character character = collider2D.GetComponent<Character>();;
			if (character == null) { return; }

			switch (character.label) {
				case CharacterLabel.Enemy: { Hit(); } break;
			}
		}
	}

	/// <summary>
	/// 攻撃データを取得する
	/// <para> 攻撃を受けた敵が取得する </para>
	/// </summary>
	public AttackData GetAttackData() { return m_AttackData; }
	

}
