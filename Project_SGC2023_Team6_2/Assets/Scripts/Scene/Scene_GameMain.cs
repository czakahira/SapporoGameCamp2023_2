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
		Timeout,
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
	/// インゲーム用タイム
	/// </summary>
	protected EachTime m_GameTime;
	/// <summary>
	/// 制限時間のタイマー
	/// </summary>
	protected Timer m_StageTimer;
	/// <summary>
	/// ステージのフェーズタイマー
	/// </summary>
	protected Timer m_PhaseTimer;
	/// <summary>
	/// プレイヤー
	/// </summary>
	protected PlayerCharacter m_Player;


	void Awake()
	{
		
	}
	void Update()
	{
		switch (m_State) {
			case EState.Init: {
				m_GameTime = TimeManager.rootTime.CreateChild(3);

				m_StageTimer = new Timer(m_GameTime);
				m_PhaseTimer = new Timer(m_GameTime);
			} break;
			case EState.Ready: {
				
			} break;
			case EState.Main: {
				if (Check_Gameover() == true) {

				}
			} break;

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
	protected bool Check_Timeout()
	{
		return m_StageTimer.Update();
	}
	/// <summary>
	/// ゲームの終了チェック
	/// </summary>
	protected bool Check_Gameover()
	{
		//クリア
		if (Check_Clear()) {
			return true;
		}
		//時間切れ
		else if (Check_Timeout()) {
		
			return true;
		}
		return false;
	}

	/// <summary>
	/// ゲームの準備
	/// </summary>
	protected void Setup()
	{
		Setup_Player();
	}
	protected void Setup_Player()
	{
		m_Player.ChangeState(CharacterLibrary.EState.None);
	}

}
