using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 2Dコライダー接触ハンドル
/// </summary>
public class Collision2DDetector : MonoBehaviour
{
	/// <summary>
	/// トリガー接触イベント
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
	/// 物理接触イベント
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
