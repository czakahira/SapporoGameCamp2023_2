using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEx;

/// <summary>
/// �v���C���[�L�����N�^�[�̏���
/// </summary>
public class PlayerCharacter : Character
{
	public enum EState
	{
		Idle,
		//Walk,
		Run,
	}

	/// <summary>
	/// 移動可能になるスティックの入力値
	/// </summary>
	[SerializeField] protected float m_Threshold_Run = 0.3f;
	/// <summary>
	/// 現在の移動方向を示す矢印
	/// </summary>
	[SerializeField] protected Transform m_Arrow;

	protected int ANIM_PARAM_HASH_RUN = Animator.StringToHash("Run");

	/// <summary>
	/// 移動入力値
	/// </summary>
	protected Vector2 m_Stick;
	
	public override void SelfAwake()
	{
		base.SelfAwake();

		//アクション関係初期化
		m_ActionController = new ActionController();
		m_ActionController.SelfAwake();
		m_ActionController.AddState(EState.Idle, new ActionStateBase());
		m_ActionController.AddState(EState.Run, new ActionStateBase());

		var state_Idle = m_ActionController.GetState(EState.Idle);
		state_Idle.onUpdate += State_Idle;
		var state_Run = m_ActionController.GetState(EState.Run);
		state_Run.onUpdate += State_Run;

		m_ActionController.ChangeState(EState.Idle);

	}
	public override void SelfUpdate()
	{
		//スティックの入力値を更新
		m_Stick = InputManager.instance.GetMove();

		//アクションを更新
		m_ActionController.SelfUpdate();

		//アニメーターのパラメータを更新する
		m_Animator.SetBool(ANIM_PARAM_HASH_RUN, (EState)m_ActionController.currentStateName == EState.Run);
	}
	public override void SeldDestory()
	{
		m_ActionController.SelfDestory();
	}

	/// <summary>
	/// �ҋ@���
	/// </summary>
	protected void State_Idle()
	{
		if ( CanMove() ) {
			m_ActionController.ChangeState(EState.Run);
		}
	}
	/// <summary>
	/// �ړ����
	/// </summary>
	protected void State_Run()
	{	
		if ( CanMove() == false ) {
			m_Rb.velocity = Vector2.zero; //慣性を抜く
			m_ActionController.ChangeState(EState.Idle);
		} else {
			m_Rb.velocity = m_Stick.normalized * (m_MoveSpeed);
			currentDirection = m_Stick.normalized;
			m_SpriteRenderer.flipX = (m_Stick.x > 0);
			
			//移動方向に矢印を向ける
			m_Arrow.transform.localEulerAngles = new Vector3(0, 0, MathEX.RoundAngleRepeat(currentDirection.x, -currentDirection.y));
		}
	}

	protected bool CanMove() { return (m_Stick.sqrMagnitude >= Mathf.Pow(m_Threshold_Run, 2)); }

}
