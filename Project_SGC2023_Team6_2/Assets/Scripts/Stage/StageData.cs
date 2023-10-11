using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージデータ
/// </summary>
[Serializable]
public class StageData
{
	/// <summary>
	/// ステージのID
	/// </summary>
	public int id = 0;
	/// <summary>
	/// ゲームの期間
	/// </summary>
	public float timer = 60;
	/// <summary>
	/// プレイヤーのスポーン位置
	/// </summary>
	public Vector3 position_PlayerSpawn = Vector3.zero;



}
