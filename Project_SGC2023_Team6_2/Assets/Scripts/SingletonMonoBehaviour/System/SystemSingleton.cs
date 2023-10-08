using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEx;

/// <summary>
/// システム関連用のマネージャー
/// <para> ！）生成時、アップデーターに更新処理を自動委託します </para>
/// </summary>
/// <typeparam name="T">  </typeparam>
public abstract class SystemSingleton<T> : SingletonBehaviour<T> where T : MonoBehaviour
{
	protected override void Initialize() 
	{
		base.Initialize();
		UpdateManager.instance.Add(SelfUpdate, SelfFixedUpdate, SelfLateUpdate);
	}
	protected override void SelfDestroy()
	{
		base.SelfDestroy();
		UpdateManager.instance.Remove(SelfUpdate, SelfFixedUpdate, SelfLateUpdate);
	}

	protected virtual void SelfUpdate() { }
	protected virtual void SelfFixedUpdate() { }
	protected virtual void SelfLateUpdate() { }

}
