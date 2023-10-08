using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EX_Tag_Layer { }

public static class EX_Tag
{
	/// <summary>
	/// シーン上で動くキャラは全てこのタグで統一する
	/// 敵・味方判別はスクリプト側で区別できるようにする
	/// </summary>
	public readonly static string Character = "Character";

	/// <summary>
	/// シーン上にあるアイテム系オブジェクトはこれで統一
	/// </summary>
	public readonly static string Item = "Item";

	/// <summary>
	/// 攻撃の当たり判定はこれで統一
	/// ヒット条件はスクリプト側で持つ
	/// </summary>
	public readonly static string Attack = "Attack";
	
	
}

public static class EX_Layer
{
	[Flags]
	public enum Name
	{
		MapFloor = 1 << 8,
		MapWall = 1 << 9,
		MapAll = MapFloor | MapWall,

		Character = 1 << 12,
	}
}
