using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイムマネージャー
/// </summary>
public class TimeManager : SystemSingleton<TimeManager>
{
	/// <summary>
	/// 始祖タイムが持つ子タイムのキャパシティ
	/// </summary>
	protected const int DEFAULT_ROOT_CHILD_CAPACITY = 100;
	/// <summary>
	/// 動的オブジェクトタイムのキャパシティ
	/// </summary>
	protected const int DEFAULT_OBJECT_CHILD_CAPACITY = 10000;
	/// <summary>
	/// 始祖のタイム
	/// </summary>
	protected EachTime m_RootTime;
	protected EachTime m_ObjectTime;

	// --------------------
	// Property
	// --------------------
	#region Open
	/// <summary>
	/// 始祖のタイム
	/// <para> ゲーム中に存在する他のタイムは、全てこれから派生していく </para>
	/// </summary>
	public static EachTime rootTime => instance.m_RootTime;
	/// <summary>
	/// シーン上に存在するキャラクター等のオブジェクトを司るタイム
	/// </summary>
	public static EachTime objectTime => instance.m_ObjectTime;
	/// <summary>
	/// 始祖のタイムスケール
	/// </summary>
	public static float rootTimeScale {
		get { return rootTime.timeScale; }
		set { rootTime.timeScale = value; }
	}
	/// <summary>
	/// 始祖のデルタ
	/// </summary>
	public static float rootDeltaTime { get { return rootTime.deltaTime; } }
	/// <summary>
	/// 始祖の未スケーリングデルタ
	/// </summary>
	public static float rootUnscaledDeltaTime { get { return rootTime.unscaledDeltaTime; } }
	#endregion

	// --------------------
	// Method
	// --------------------
	#region Open
	/// <summary>
	/// 初期化
	/// </summary>
	protected override void Initialize()
	{
		base.Initialize();

		//始祖の誕生
		m_RootTime = new EachTime(Time.timeScale, DEFAULT_ROOT_CHILD_CAPACITY);

		//派生していく
		m_ObjectTime = m_RootTime.CreateChild(DEFAULT_OBJECT_CHILD_CAPACITY);
	}

	protected override void SelfUpdate()
	{
		base.SelfUpdate();

		rootTime.Update(Time.deltaTime);
	}
	protected override void SelfFixedUpdate()
	{
		base.SelfFixedUpdate();

		rootTime.FixedUpdate(Time.fixedDeltaTime);
	}

	protected override void SelfDestroy()
	{
		base.SelfDestroy();
		m_RootTime.ReleaseChild(m_ObjectTime);
		m_ObjectTime = null;

		rootTime.Destroy();
	}
	#endregion

}