using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// アクションステートの基底
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
	/// ステートの開始
	/// </summary>
	public virtual void OnEnter()
	{
		onEnter?.Invoke();
	}
	/// <summary>
	/// ステートの更新
	/// </summary>
	public virtual void OnUpdate()
	{
		onUpdate?.Invoke();
	}
	/// <summary>
	/// ステートの破棄
	/// </summary>
	public virtual void OnExit()
	{
		onExit?.Invoke();
	}
}
