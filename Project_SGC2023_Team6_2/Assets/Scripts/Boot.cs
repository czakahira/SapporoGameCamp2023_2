using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
	/// <summary>
	/// 初期化完了しゲームが起動できる準備が整ったフラグ
	/// </summary>
	[SerializeField] protected bool m_Complete = false;

	void Awake()
	{
		Awakening();
	}

	/// <summary>
	/// ゲーム起動
	/// </summary>
	protected void Awakening()
	{
		m_Complete = false;

		//最初にゲームのマネージャーを作成
		if (!GameMainManager.activated) { return; }

		//各シングルトンマネージャーを順番に作成、初期化していく
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

		//初期化完了
		m_Complete = true;

		//タイトルへ
		GameSceneManager.instance.NextScene(EScene.Title);
	}

}