using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インゲームの処理
/// </summary>
public class Scene_GameMain : MonoBehaviour
{
	public enum EState
	{
		None,

		Init,

		Ready,
		Main,
		TimeOut,
		Clear,
	}
	/// <summary>
	/// インゲームの状況
	/// </summary>
	protected EState m_State = EState.None;
	/// <summary>
	/// インゲームのステート管理
	/// </summary>
	protected StateController<EState> m_States;
	/// <summary>
	/// 制限時間のタイマー
	/// </summary>
	protected Timer m_Timer;


	// Start is called before the first frame update
	void Awake()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		switch (m_State) {
			case EState.None: {

			} break;
			default: break;
		}
	}

	/// <summary>
	/// チェック：クリア
	/// </summary>
	protected bool Check_Clear()
	{
		return false;
	}
	/// <summary>
	/// チェック：制限時間
	/// </summary>
	/// <returns></returns>
	protected bool Check_GameFail()
	{

		return false;
	}

}
