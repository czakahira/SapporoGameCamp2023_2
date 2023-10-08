using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�N�V�����X�e�[�g�̃R���g���[���[
/// </summary>
public class ActionController
{
	// -----------------------
	// Member
	// -----------------------
	/// <summary>
	/// �o�^�ς݂̃X�e�[�g
	/// </summary>
	protected Dictionary<Enum, ActionStateBase> m_States;
	/// <summary>
	/// �o�^�ς݃X�e�[�g��
	/// </summary>
	protected int m_StateCount;

	public Enum previousStateName { get; protected set; }
	public Enum currentStateName { get; protected set; }

	/// <summary>
	/// �O����s���Ă����ɃX�e�[�g
	/// </summary>
	protected ActionStateBase m_PreviousState;
	/// <summary>
	/// ���ݎ��s���ɃX�e�[�g
	/// </summary>
	protected ActionStateBase m_CurrentState;

	// -----------------------
	// Method
	// -----------------------
	/// <summary>
	///	���O�ŌĂԏ�����
	/// </summary>
	public virtual void SelfAwake()
	{
		m_States = new Dictionary<Enum, ActionStateBase>();
		m_StateCount = 0;

		m_PreviousState = null;
		m_CurrentState = null;
	}
	/// <summary>
	///	���O�ŌĂԍX�V
	/// </summary>
	public virtual void SelfUpdate()
	{
		m_CurrentState.OnUpdate();
	}
	/// <summary>
	///	���O�ŌĂԔj��
	/// </summary>
	public virtual void SelfDestory()
	{
		foreach (var state in m_States.Values) {
			state.SelfDestroy();
		}
		m_States.Clear();
		m_StateCount = 0;

		m_CurrentState = null;
	}

	/// <summary>
	/// �w��X�[�g���������Ă��邩
	/// </summary>
	public bool HasState(Enum _name) { return m_States.ContainsKey(_name); }
	/// <summary>
	/// �w��X�e�[�g���擾����
	/// </summary>
	public virtual ActionStateBase GetState(Enum _name)
	{
		if (HasState(_name)) { return m_States[_name]; }
		return null;
	}
	/// <summary>
	/// �X�e�[�g��ǉ�����
	/// </summary>
	public virtual void AddState(Enum _name, ActionStateBase _state)
	{
		if (HasState(_name)) {
			Debug.LogError($"{_name} �͓o�^�ς݂ł��B");
			return;
		}
		m_States.Add(_name, _state);
		m_StateCount++;
	}
	/// <summary>
	/// �X�e�[�g��j������
	/// </summary>
	public virtual void RemoveState(Enum _name, ActionStateBase _state)
	{
		if (HasState(_name) == false) {
			Debug.LogError($"{_name} �͖��o�^�ł��B");
			return;
		}
		m_States.Remove(_name);
		m_StateCount--;
	}
	/// <summary>
	/// �w��X�e�[�g�ɐ؂�ւ���
	/// </summary>
	public virtual void ChangeState(Enum _name)
	{
		if (HasState(_name) == false) { 
			Debug.LogError($"{_name} ���݂��Ȃ��X�e�[�g�ɐ؂�ւ��邱�Ƃ͂ł��܂���");
			return;
		}

		previousStateName = currentStateName;
		m_PreviousState = m_CurrentState;
		currentStateName = _name;
		m_CurrentState = GetState(_name);

		m_PreviousState?.OnExit(); //�O�̃X�e�[�g���甲����
		m_CurrentState?.OnEnter(); //���̃X�e�[�g�ɓ���

		Debug.Log($"�X�e�[�g�ύX: {previousStateName} -> {currentStateName}");
	}

}
