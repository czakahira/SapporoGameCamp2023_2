using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLibrary
{
	/// <summary>
	/// キャラクターのステート一覧
	/// </summary>
	public enum EState
	{
		None = 0,

		SpawnWait, //出現して待機

		Idle,		//素立ち
		//Walk,
		Run,		//走り
	}
}
