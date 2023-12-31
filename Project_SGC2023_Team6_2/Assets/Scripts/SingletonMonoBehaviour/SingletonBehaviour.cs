﻿using System;
using System.Collections;

using UnityEngine;

using UnityEx;

/// <summary>
/// シーン上に１つ存在するビヘイビア（シングルトン）
/// <para> ※これは継承を前提としたクラスです </para>
/// </summary>
/// <typeparam name="T"> 継承先のクラスタイプ </typeparam>
public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	/// <summary>
	/// 常駐オブジェクト化フラグ
	/// </summary>
	protected static bool s_IsDontDestroyOnloaded = false;
	/// <summary> 
	/// インスタンス(静的メンバー)
	/// </summary>
	protected static T s_Instance = null;
	/// <summary> 
	/// インスタンス(静的プロパティ)
	/// </summary>
	public static T instance {
		get {
			if(s_Instance == null) {
				s_Instance = FindObjectOfType<T>();
				if (s_Instance == null) {
					string name = SingletonLibrary.GetAttachObjectName(typeof(T));
					GameObject obj = GameObject.Find(name);
					if (obj == null) {
						obj = new GameObject(name);
					}
					s_Instance = obj.AddComponent<T>();
				}
				//常駐オブジェクトとする
				if (s_IsDontDestroyOnloaded == false) {
					DontDestroyOnLoad(s_Instance);
					s_IsDontDestroyOnloaded = true;
				}
				//初期化
				(s_Instance as SingletonBehaviour<T>)?.Initialize();
			}
			//初めてプロパティにアクセスしたらインスタンスが作成される
			//NULLが返ってくる場合はバグなので要確認
			return s_Instance;
		}
	}
	/// <summary>
	/// 有効フラグ
	/// </summary>
	public static bool activated { get { return instance != null; } }


	/// <summary>
	/// インスタンスを削除する
	/// </summary>
	private void ClearInstance()
	{
		if (s_Instance != null) {
			s_Instance = null;
		}

		s_IsDontDestroyOnloaded = false;
	}
	/// <summary>
	/// インスタンス作成時に１度呼ぶ初期化処理
	/// </summary>
	protected virtual void Initialize()
	{
#if UNITY_DEBUG
		Debug.Log($"<color={ColorEX.green_spring.ToCode()}> Awakened </color>: {typeof(T).Name}");
#endif
	}
	/// <summary>
	/// インスタンス破棄時に１度だけ呼ぶ処理
	/// </summary>
	protected virtual void SelfDestroy() { }
	public void Destroy()
	{
		SelfDestroy();

#if UNITY_DEBUG
		Debug.Log($"<color={ColorEX.blue.ToCode()}> Destroied </color>: {typeof(T).Name}");
#endif
		//インスタンスを破棄
		ClearInstance();
		//破棄
		Destroy(this);
	}
}
