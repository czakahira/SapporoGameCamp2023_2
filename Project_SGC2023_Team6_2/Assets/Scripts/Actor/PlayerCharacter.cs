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
	/// �ړ������̖��
	/// </summary>
	public Transform m_Arrow;

	protected int ANIM_PARAM_HASH_RUN = Animator.StringToHash("Run");
	
	public override void SelfAwake()
	{
		base.SelfAwake();

		//�A�N�V�����֌W
		m_ActionController = new ActionController();
		m_ActionController.SelfAwake();
		m_ActionController.AddState(EState.Idle, new ActionStateBase());
		m_ActionController.AddState(EState.Run, new ActionStateBase());

		var state_Idle = m_ActionController.GetState(EState.Idle);
		state_Idle.onUpdate += State_Idle;
		var state_Run = m_ActionController.GetState(EState.Run);
		state_Run.onUpdate += State_Run;

		m_ActionController.ChangeState(EState.Run);

	}
	public override void SelfUpdate()
	{
		m_ActionController.SelfUpdate();

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
		var stick = InputManager.instance.GetMove();
		//�X�e�B�b�N�������ȏ�|���ꂽ��ړ�
		if (stick.sqrMagnitude >= Mathf.Pow(0.5f, 2)) {
			m_ActionController.ChangeState(EState.Run);
		}
	}
	/// <summary>
	/// �ړ����
	/// </summary>
	protected void State_Run()
	{
		//�X�e�B�b�N�̓��͒l����
		var stick = InputManager.instance.GetMove();

		//�X�e�B�b�N�����������ɂȂ�����ҋ@
		if (stick.sqrMagnitude < Mathf.Pow(0.5f, 2)) {
			m_ActionController.ChangeState(EState.Idle);
			m_Rb.velocity = Vector2.zero; //������؂�
		} else {
			m_Rb.velocity = stick.normalized * (m_MoveSpeed);
			currentDirection = stick;						
			m_SpriteRenderer.flipX = (stick.x > 0);
			
			//移動方向に矢印を向ける
			m_Arrow.transform.localEulerAngles = new Vector3(0, 0, MathEX.RoundAngleRepeat(currentDirection.x, -currentDirection.y));
		}
	}

}
