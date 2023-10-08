using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEx;

/// <summary>
/// 接触タイプ
/// </summary>
public enum EContactType
{
	None,

	Enter,      //接触開始
	Stay,       //接触中
	Exit,       //離れた
}

/// <summary>
/// コライダーの衝突検知器
/// </summary>
public class CollisionDetector : MonoBehaviour
{
	// --------------------
	// Property
	// --------------------
	#region Open
	/// <summary>
	/// 固有ID
	/// <para>
	/// 0 は無効な値です。
	/// </para>
	/// </summary>
	public uint uniqueID { get; protected set; } = 0;
	/// <summary>
	/// コリジョンイベント有効フラグ
	/// </summary>
	public bool enableOnCollisionEvent { get; set; } = true;
	/// <summary>
	/// トリガーイベント有効フラグ
	/// </summary>
	public bool enableOnTriggerEvent { get; set; } = true;
	#endregion

	// --------------------
	// Member
	// --------------------
	#region Open

	//▼ 物理衝突イベント
	public event UnityAction<EContactType, Collision> onCollision;
	public event UnityAction<Collision> onCollisionEnter;
	public event UnityAction<Collision> onCollisionStay;
	public event UnityAction<Collision> onCollisionExit;
	// ▼ トリガーイベント
	public event UnityAction<EContactType, Collider> onTrigger;
	public event UnityAction<Collider> onTriggerEnter;
	public event UnityAction<Collider> onTriggerStay;
	public event UnityAction<Collider> onTriggerExit;

	#endregion

	// --------------------
	// Method
	// --------------------
	#region Open

	//■ Event
	#region Open
	public void SetCollisionEvent(
		UnityAction<EContactType, Collision> _onCollision = null,
		UnityAction<Collision> _onCollisionEnter = null,
		UnityAction<Collision> _onCollisionStay = null,
		UnityAction<Collision> _onCollisionExit = null
	) {
		onCollision += _onCollision;
		onCollisionEnter += _onCollisionEnter;
		onCollisionStay += _onCollisionStay;
		onCollisionExit += _onCollisionExit;
	}
	public void RemoveCollisionEvent(
		UnityAction<EContactType, Collision> _onCollision = null,
		UnityAction<Collision> _onCollisionEnter = null,
		UnityAction<Collision> _onCollisionStay = null,
		UnityAction<Collision> _onCollisionExit = null
	) {
		onCollision -= _onCollision;
		onCollisionEnter -= _onCollisionEnter;
		onCollisionStay -= _onCollisionStay;
		onCollisionExit -= _onCollisionExit;
	}

	public void SetTriggerEvent(
		UnityAction<EContactType, Collider> _onTrigger = null,
		UnityAction<Collider> _onTriggerEnter = null,
		UnityAction<Collider> _onTriggerStay = null,
		UnityAction<Collider> _onTriggerExit = null
	) {
		onTrigger += _onTrigger;
		onTriggerEnter += _onTriggerEnter;
		onTriggerStay += _onTriggerStay;
		onTriggerExit += _onTriggerExit;
	}
	public void RemoveTriggerEvent(
		UnityAction<EContactType, Collider> _onTrigger = null,
		UnityAction<Collider> _onTriggerEnter = null,
		UnityAction<Collider> _onTriggerStay = null,
		UnityAction<Collider> _onTriggerExit = null
	) {
		onTrigger -= _onTrigger;
		onTriggerEnter -= _onTriggerEnter;
		onTriggerStay -= _onTriggerStay;
		onTriggerExit -= _onTriggerExit;
	}
	#endregion

	#region ▼ OnCollision
	protected virtual void OnCollision(EContactType _type, Collision collision)
	{
		if (!enableOnCollisionEvent) { return; }

		switch (_type) {
			case EContactType.Enter: {
				onCollision?.Invoke(EContactType.Enter, collision);
				onCollisionEnter?.Invoke(collision);
			} break;

			case EContactType.Stay: {
				onCollision?.Invoke(EContactType.Stay, collision);
				onCollisionStay?.Invoke(collision);
			} break;

			case EContactType.Exit: {
				onCollision?.Invoke(EContactType.Exit, collision);
				onCollisionExit?.Invoke(collision);
			} break;
		}
	}
	protected virtual void OnCollisionEnter(Collision collision)
	{
		OnCollision(EContactType.Enter, collision);
	}
	protected virtual void OnCollisionStay(Collision collision)
	{
		OnCollision(EContactType.Stay, collision);
	}
	protected virtual void OnCollisionExit(Collision collision)
	{
		OnCollision(EContactType.Exit, collision);
	}
	#endregion

	#region ▼ OnTrigger
	protected virtual void OnTrigger(EContactType _type, Collider collider)
	{
		if (!enableOnTriggerEvent) { return; }

		switch (_type) {
			case EContactType.Enter: {
				onTrigger?.Invoke(EContactType.Enter, collider);
				onTriggerEnter?.Invoke(collider);
			} break;

			case EContactType.Stay: {
				onTrigger?.Invoke(EContactType.Stay, collider);
				onTriggerStay?.Invoke(collider);
			} break;

			case EContactType.Exit: {
				onTrigger?.Invoke(EContactType.Exit, collider);
				onTriggerExit?.Invoke(collider);
			} break;
		}
	}
	protected virtual void OnTriggerEnter(Collider collider)
	{
		OnTrigger(EContactType.Enter, collider);
	}
	protected virtual void OnTriggerStay(Collider collider)
	{
		OnTrigger(EContactType.Stay, collider);
	}
	protected virtual void OnTriggerExit(Collider collider)
	{
		OnTrigger(EContactType.Exit, collider);
	}
	#endregion

	#endregion

}
