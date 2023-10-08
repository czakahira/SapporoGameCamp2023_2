using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dictionary の拡張メソッド置き場
/// </summary>
static public class EX_Dictionary
{
	/// <summary>
	/// keyが既に有れば上書き、無ければ追加する
	/// </summary>
	/// <typeparam name="T1"> keyの型 </typeparam>
	/// <typeparam name="T2"> valueの型 </typeparam>
	/// <param name="dictionary">追加先の辞書</param>
	/// <param name="key"> 存在を確認するkey </param>
	/// <param name="value"> 追加するvalue< /param>
	public static void ReplaceByKey<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, T2 value)
	{
		//有る
		if (dictionary.ContainsKey(key)) {
			dictionary[key] = value;
		}
		//無い
		else {
			dictionary.Add(key, value);
		}
	}

	/// <summary>
	/// 新規のキーを追加します
	/// </summary>
	/// <returns> キーが存在せず、追加に成功すれば TRUE を返します </returns>
	public static bool AddUncontainsKey<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, T2 value)
	{
		if (dictionary.ContainsKey(key)) { return false; }

		dictionary.Add(key, value);
		return true;
	}

	/// <summary>
	/// 既存のキーを削除します
	/// </summary>
	/// <returns> キーが存在し、削除に成功すれば TRUE を返します </returns>
	public static bool RemoveContainsKey<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key)
	{
		if (!dictionary.ContainsKey(key)) {
			Debug.LogError($"Requested Remove Key is Not Contains {key}");
			return false;
		}

		dictionary.Remove(key);
		return true;
	}
	/// <summary>
	/// 存在するキーの要素を返します
	/// </summary>
	public static T2 GetConta8insKey<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key)
	{
		if (dictionary.ContainsKey(key)) {
			return dictionary[key];
		}
		return default;
	}
}
