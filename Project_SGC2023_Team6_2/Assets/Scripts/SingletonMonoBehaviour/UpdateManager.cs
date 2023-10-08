using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public enum UpdateType
{
	Update,
	FixedUpdate,
	LateUpdate,
}

/// <summary>
/// アップデートマネージャー
/// <para> １）シーン上に存在するMonobehaviour毎に、Update系を呼ぶとパフォーマンスが低下するので、マネージャーに肩代わりさせます </para>
/// <para> ２）シングルトン関連はここにコールバックを渡して使います </para>
/// </summary>
public class UpdateManager : SingletonBehaviour<UpdateManager>
{
	#region ▼ Member
	protected event UnityAction m_Update = delegate { };
	protected event UnityAction m_LateUpdate = delegate { };
	protected event UnityAction m_FixedUpdate = delegate { };
	#endregion

	#region ▼ Method
	private void Update()
	{
		m_Update?.Invoke();
	}
	private void LateUpdate()
	{
		m_LateUpdate?.Invoke();
	}
	private void FixedUpdate()
	{
		m_FixedUpdate?.Invoke();
	}
	protected override void SelfDestroy()
	{
		base.SelfDestroy();

		//全て破棄する
		m_Update = null;
		m_FixedUpdate = null;
		m_LateUpdate = null;
	}

	/// <summary>
	/// 登録する
	/// </summary>
	/// <param name="_action"></param>
	/// <param name="_type"></param>
	public void Add(UnityAction _action, UpdateType _type = UpdateType.Update)
	{
		switch (_type) {
			case UpdateType.Update:			m_Update += _action;		break;
			case UpdateType.FixedUpdate:	m_FixedUpdate += _action;	break;
			case UpdateType.LateUpdate:		m_LateUpdate += _action;	break;
		}
	}
	public void Add(UnityAction _update, UnityAction _fixedUpdate, UnityAction _lateUpdate)
	{
		m_Update += _update;
		m_FixedUpdate += _fixedUpdate;
		m_LateUpdate += _lateUpdate;
	}

	/// <summary>
	/// 解除する
	/// <para> ※Addした後にこれを呼ばずとも、このマネージャー終了時に全て自動で破棄されます </para>
	/// </summary>
	/// <param name="_action"></param>
	/// <param name="_type"></param>
	public void Remove(UnityAction _action, UpdateType _type = UpdateType.Update)
	{
		switch(_type) {
			case UpdateType.Update:			m_Update -= _action;		break;
			case UpdateType.FixedUpdate:	m_FixedUpdate -= _action;	break;
			case UpdateType.LateUpdate:		m_LateUpdate -= _action;	break;
		}
	}
	public void Remove(UnityAction _update, UnityAction _fixedUpdate, UnityAction _lateUpdate)
	{
		m_Update -= _update;
		m_FixedUpdate -= _fixedUpdate;
		m_LateUpdate -= _lateUpdate;
	}

	#endregion
}
