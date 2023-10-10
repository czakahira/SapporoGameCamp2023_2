using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 汎用ステートのコントローラー
/// </summary>
public class StateController<T>  where T : Enum
{
	// -----------------------
	// Member
	// -----------------------
	/// <summary>
	/// 登録済みのステート
	/// </summary>
	protected Dictionary<T, State_Base> m_States;
	/// <summary>
	/// 登録済みステート数
	/// </summary>
	protected int m_StateCount;

	public T previousStateName { get; protected set; }
	public T currentStateName { get; protected set; }

	/// <summary>
	/// 前回実行していたにステート
	/// </summary>
	protected State_Base m_PreviousState;
	/// <summary>
	/// 現在実行中にステート
	/// </summary>
	protected State_Base m_CurrentState;

	// -----------------------
	// Method
	// -----------------------
	/// <summary>
	///	自前で呼ぶ初期化
	/// </summary>
	public virtual void SelfAwake()
	{
		m_States = new Dictionary<T, State_Base>();
		m_StateCount = 0;

		m_PreviousState = null;
		m_CurrentState = null;
	}
	/// <summary>
	///	自前で呼ぶ更新
	/// </summary>
	public virtual void SelfUpdate()
	{
		m_CurrentState.OnUpdate();
	}
	/// <summary>
	///	自前で呼ぶ破棄
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
	/// 指定スートを所持しているか
	/// </summary>
	public bool HasState(T _name) { return m_States.ContainsKey(_name); }
	/// <summary>
	/// 指定ステートを取得する
	/// </summary>
	public virtual State_Base GetState(T _name)
	{
		if (HasState(_name)) { return m_States[_name]; }
		return null;
	}
	/// <summary>
	/// ステートを追加する
	/// </summary>
	public virtual void AddState(T _name, State_Base _state)
	{
		if (HasState(_name)) {
			Debug.LogError($"{_name} は登録済みです。");
			return;
		}
		m_States.Add(_name, _state);
		m_StateCount++;
	}
	/// <summary>
	/// ステートを破棄する
	/// </summary>
	public virtual void RemoveState(T _name, State_Base _state)
	{
		if (HasState(_name) == false) {
			Debug.LogError($"{_name} は未登録です。");
			return;
		}
		m_States.Remove(_name);
		m_StateCount--;
	}
	/// <summary>
	/// 指定ステートに切り替える
	/// </summary>
	public virtual void ChangeState(T _name)
	{
		if (HasState(_name) == false) { 
			Debug.LogError($"{_name} 存在しないステートに切り替えることはできません");
			return;
		}

		previousStateName = currentStateName;
		m_PreviousState = m_CurrentState;
		currentStateName = _name;
		m_CurrentState = GetState(_name);

		m_PreviousState?.OnExit(); //前のステートから抜ける
		m_CurrentState?.OnEnter(); //次のステートに入る

		Debug.Log($"ステート変更: {previousStateName} -> {currentStateName}");
	}

}
