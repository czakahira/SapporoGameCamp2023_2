using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEx;

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
	protected List<UnityAction> m_DestoryEvent;

	protected override void Initialize()
	{
		base.Initialize();

		m_DestoryEvent = new List<UnityAction>();
	}

	/// <summary>
	/// １フレームにおいて最初に呼ばれる更新
	/// <para> 順番がおかしい場合はバグなので修正してください </para>
	/// </summary>
	private void Update()
	{
		//フレームレートを更新します
		fps = (1 / Time.deltaTime);

		//ゲームを終了する
		if (Boot.gameAwakened) {
			if (InputManager.instance.IsQuitGame()) {
				Quit();
			}
		}
	}
	/// <summary>
	/// モノビヘイビアのデストロイはココだけ
	/// </summary>
	private void OnDestroy()
	{
		//登録されている全ての破棄イベントを呼び出す
		foreach (var destroy in m_DestoryEvent) { destroy?.Invoke(); }
		m_DestoryEvent.Clear();
		m_DestoryEvent = null;

		//最後にこのインスタンスを削除する
		Destroy();

		Debug.Log($"<color={ColorEX.blue_aqua.ToCode()}> Successfully </color>: Game Shutdown");
	}

	/// <summary>
	/// 破棄イベントを登録する
	/// </summary>
	public void AddDestroy(UnityAction _destroy)
	{
		m_DestoryEvent.Insert(0, _destroy);
	}

	/// <summary>
	/// ゲームを終了する
	/// </summary>
	public static void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

}
