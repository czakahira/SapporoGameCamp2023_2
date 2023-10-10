using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �A�N�V�����X�e�[�g�̊��
/// </summary>
public class State_Base
{
	// -----------------------
	// Member
	// -----------------------
	public event UnityAction onEnter;
	public event UnityAction onUpdate;
	public event UnityAction onExit;

	// -----------------------
	// Method
	// -----------------------
	public virtual void SelfAwake(){ }
	public virtual void SelfDestroy(){ }

	/// <summary>
	/// �X�e�[�g�̊J�n
	/// </summary>
	public virtual void OnEnter()
	{
		onEnter?.Invoke();
	}
	/// <summary>
	/// �X�e�[�g�̍X�V
	/// </summary>
	public virtual void OnUpdate()
	{
		onUpdate?.Invoke();
	}
	/// <summary>
	/// �X�e�[�g�̔j��
	/// </summary>
	public virtual void OnExit()
	{
		onExit?.Invoke();
	}
}
