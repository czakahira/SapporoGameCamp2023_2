using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ゲームのマネージャー
/// <para> ゲーム上に存在する全てのMonoBehaviourの内、最初に呼ばれる </para>
/// </summary>
public class GameMainManager : SingletonBehaviour<GameMainManager>
{
	/// <summary>
	/// フレームレート
	/// </summary>
	public static float fps { get; protected set; } = 0;

	/// <summary>
	/// 破棄イベント
	/// </summary>
	protected UnityAction m_Destoryer;

	/// <summary>
	/// １フレームにおいて最初に呼ばれる更新
	/// <para> 順番がおかしい場合はバグなので修正してください </para>
	/// </summary>
	private void Update()
	{
		//フレームレートを更新します
		fps = (1 / Time.deltaTime);


	}
	/// <summary>
	/// モノビヘイビアのデストロイはココだけ
	/// </summary>
	private void OnDestroy()
	{
		//登録されている全ての破棄イベントを呼び出す
		m_Destoryer?.Invoke();

		//最後にこのインスタンスを削除する
		Destroy();
	}

	/// <summary>
	/// 破棄イベントを登録する
	/// </summary>
	public void AddDestroy(UnityAction _destroy)
	{
		m_Destoryer += _destroy;
	}


}
