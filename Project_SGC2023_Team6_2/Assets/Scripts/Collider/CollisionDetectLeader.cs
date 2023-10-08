using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEx;

public class CollisionDetectLeader : IColliderEvent
{
	protected List<CollisionDetector> m_Detectors;

	/// <summary> 
	/// 接触中のCollisionリスト
	/// </summary>
	public List<Collision> contactCollisions { get; protected set; }
	/// <summary> 
	/// 接触中のColliderリスト
	/// </summary>
	public List<Collider> contactColliders { get; protected set; }


	public virtual void Initialize() 
	{
		m_Detectors = new List<CollisionDetector>();

		contactCollisions = new List<Collision>();
		contactColliders = new List<Collider>();
	}
	public virtual void Destroy()
	{
		m_Detectors.Clear();
		m_Detectors = null;

		contactColliders.Clear();
		contactCollisions = null;

		contactColliders.Clear();
		contactCollisions = null;
	}

	public virtual bool AddDetector(CollisionDetector _detector)
	{
		if (m_Detectors.AddUncontains(_detector)) {
			_detector.SetCollisionEvent(OnCollision, OnCollisionEnter, OnCollisionStay, OnCollisionExit);
			_detector.SetTriggerEvent(OnTrigger, OnTriggerEnter, OnTriggerStay, OnTriggerExit);
			return true;
		}
		return false;
	}
	public virtual bool RemoveDetector(CollisionDetector _detector)
	{
		if (m_Detectors.RemoveContains(_detector)) {
			_detector.RemoveCollisionEvent(OnCollision, OnCollisionEnter, OnCollisionStay, OnCollisionExit);
			_detector.RemoveTriggerEvent(OnTrigger, OnTriggerEnter, OnTriggerStay, OnTriggerExit);
			return true;
		}
		return false;
	}

	/// <summary> 
	/// 接触したCollisionをリストに追加する
	/// </summary>
	protected virtual void AddCollision(Collision collision)
	{
		contactCollisions.AddUncontains(collision);
	}
	/// <summary> 
	/// 離れたCollisionをリストから削除する
	/// </summary>
	protected virtual void RemoveCollision(Collision collision)
	{
		contactCollisions.RemoveContains(collision);
	}
	/// <summary> 
	/// 接触したColliderをリストに追加する
	/// </summary>
	protected virtual void AddCollider(Collider collider)
	{
		contactColliders.AddUncontains(collider);
	}
	/// <summary> 
	/// 離れたColliderをリストから削除する
	/// </summary>
	protected virtual void RemoveCollider(Collider collider)
	{
		contactColliders.RemoveContains(collider);
	}

	// ----------------------------------------
	// 接触イベント
	// ----------------------------------------
	public virtual void OnCollision(EContactType _type, Collision collision)
	{

	}
	public virtual void OnCollisionEnter(Collision collision)
	{
		AddCollision(collision);
	}
	public virtual void OnCollisionStay(Collision collision)
	{

	}
	public virtual void OnCollisionExit(Collision collision)
	{
		RemoveCollision(collision);
	}

	public virtual void OnTrigger(EContactType _type, Collider collider)
	{

	}
	public virtual void OnTriggerEnter(Collider collider)
	{
		AddCollider(collider);
	}
	public virtual void OnTriggerStay(Collider collider)
	{

	}
	public virtual void OnTriggerExit(Collider collider)
	{
		RemoveCollider(collider);
	}

}
