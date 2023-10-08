using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーンライブラリ
/// </summary>
public static class SceneLibrary
{
	/// <summary>
	/// シーンの存在するルートフォルダパス
	/// </summary>
	public static readonly string SCENEASSETS_ROOT_PATH = "Assets/Scenes/Main";

	/// <summary>
	/// シーンのラベルからロード用パスを取得します
	/// </summary>
	public static string GetSceneLoadPath(EScene _label)
	{
		return $"{SCENEASSETS_ROOT_PATH}/{GetSceneName(_label)}";
	}
	/// <summary>
	/// ラベルからシーンの名前を取得する
	/// </summary>
	public static string GetSceneName(EScene _label)
	{
		return $"{_label.ToString()}.unity";
	}

}

/// <summary>
/// シーンラベル
/// </summary>
public enum EScene
{
	None,

	Boot,

	Title,

	GameMain,
}
