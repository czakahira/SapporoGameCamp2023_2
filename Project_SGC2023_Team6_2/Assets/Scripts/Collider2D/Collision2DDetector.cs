using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 2D�R���C�_�[�ڐG�n���h��
/// </summary>
public class Collision2DDetector : MonoBehaviour
{
	/// <summary>
	/// �g���K�[�ڐG�C�x���g
	/// </summary>
	public event UnityAction<EContactType, Collider2D> onTrigger2D;

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		onTrigger2D?.Invoke(EContactType.Enter, collision);
	}
	protected virtual void OnTriggerStay2D(Collider2D collision)
	{
		onTrigger2D?.Invoke(EContactType.Stay, collision);
	}
	protected virtual void OnTriggerExit2D(Collider2D collision)
	{
		onTrigger2D?.Invoke(EContactType.Exit, collision);
	}

	/// <summary>
	/// �����ڐG�C�x���g
	/// </summary>
	public event UnityAction<EContactType, Collision2D> onCollision2D;

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		onCollision2D?.Invoke(EContactType.Enter, collision);
	}
	protected virtual void OnCollisionStay2D(Collision2D collision)
	{
		onCollision2D?.Invoke(EContactType.Stay, collision);
	}
	protected virtual void OnCollisionExit2D(Collision2D collision)
	{
		onCollision2D?.Invoke(EContactType.Exit, collision);
	}
}
