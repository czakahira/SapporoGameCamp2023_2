using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class SingletonLibrary
{
	/// <summary>
	/// Keyに登録したシングルトンを、Valueのオブジェクトにアタッチします
	/// </summary>
	private static readonly Dictionary<Type, string> s_SingletonAttachObjectData = new Dictionary<Type, string>()
	{
		{ typeof(UpdateManager),	"GameMainManager" },
		{ typeof(TimeManager),		"GameMainManager" },
	};

	/// <summary>
	/// シングルトンのアタッチ先となるオブジェクトの名前を取得する
	/// </summary>
	public static string GetAttachObjectName(Type type)
	{
		//辞書に有ればそれを使う
		if (s_SingletonAttachObjectData.ContainsKey(type)) { return s_SingletonAttachObjectData[type]; }

		//シングルトンと同名
		return type.Name;
	}
}
