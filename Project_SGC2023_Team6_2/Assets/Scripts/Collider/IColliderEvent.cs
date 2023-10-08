using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コライダーイベント
/// </summary>
public interface IColliderEvent
{
	public void OnCollision(EContactType _type, Collision collision);
	public void OnCollisionEnter(Collision collision);
	public void OnCollisionStay(Collision collision);
	public void OnCollisionExit(Collision collision);
	public void OnTrigger(EContactType _type, Collider collider);
	public void OnTriggerEnter(Collider collider);
	public void OnTriggerStay(Collider collider);
	public void OnTriggerExit(Collider collider);
}
