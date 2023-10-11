using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEx;

/// <summary>
/// ゲーム起動処理
/// </summary>
public class Boot : MonoBehaviour
{
	/// <summary>
	/// 初期化完了しゲームが起動できる準備が整ったフラグ
	/// </summary>
	protected static bool s_GameAwakened = false;
	public static bool gameAwakened {  get { return s_GameAwakened; } }


	private void Awake() { Awakening(); }

	/// <summary>
	/// ゲーム起動
	/// </summary>
	protected void Awakening()
	{
		s_GameAwakened = false;

		//最初にゲームのマネージャーを起動
		if (GameMainManager.activated == false) {
			IsFailedBoot();
			return;
		}
		//必要なシングルトンをイベント実行順に起動
#if false
		if (!UpdateManager.activated
		 || !AddresableManager.activated
		 || !TimeManager.activated
		 || !SceneManagerEx.activated
		 || !GameSceneManager.activated
		 || !AudioManager.activated
		 || !InputManager.activated) {
			return;
		}

		//登録順とは逆順で破棄イベントを呼ぶようにする
		GameMainManager.instance.AddDestroy(InputManager.instance.Destroy);
		GameMainManager.instance.AddDestroy(AudioManager.instance.Destroy);
		GameMainManager.instance.AddDestroy(GameSceneManager.instance.Destroy);
		GameMainManager.instance.AddDestroy(SceneManagerEx.instance.Destroy);
		GameMainManager.instance.AddDestroy(TimeManager.instance.Destroy);
		GameMainManager.instance.AddDestroy(AddresableManager.instance.Destroy);
		GameMainManager.instance.AddDestroy(UpdateManager.instance.Destroy);
#else		
		if (SingletonActivateCheck<UpdateManager>()		== false
		 || SingletonActivateCheck<AddresableManager>() == false
		 || SingletonActivateCheck<TimeManager>()		== false
		 || SingletonActivateCheck<SceneManagerEx>()	== false
		 || SingletonActivateCheck<GameSceneManager>()	== false
		 || SingletonActivateCheck<AudioManager>()		== false
		 || SingletonActivateCheck<InputManager>()		== false ) {
			IsFailedBoot();
			return;
		}
#endif
		//初期化完了
		s_GameAwakened = true;

		Debug.Log($"<color={ColorEX.green_lime.ToCode()}> Successfully </color>: Game Boot");

		//タイトルへ
		GameSceneManager.instance.NextScene(EScene.Title);
	}
	/// <summary>
	/// シングルトンの起動チェック
	/// </summary>
	protected bool SingletonActivateCheck<T>() where T : SingletonBehaviour<T>
	{
		if (SingletonBehaviour<T>.activated == true) {
			//ゲーム終了時にシングルトンの破棄を呼んでもらう
			GameMainManager.instance.AddDestroy(SingletonBehaviour<T>.instance.Destroy);
			return true;
		}
		return false;
	}

	/// <summary>
	/// ゲームの起動に失敗しました
	/// </summary>
	protected void IsFailedBoot()
	{
		Debug.LogError($"Application Quit: Boot Failed");
		GameMainManager.Quit();
	}

}