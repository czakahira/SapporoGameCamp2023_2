using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List 拡張メソッド置き場
/// </summary>
static public class EX_List
{
	/// <summary>
	/// リストに新規のオブジェクトを追加します
	/// </summary>
	/// <returns>
	/// <see langword="true"/> List に既存
	/// <see langword="false"/> List に不在
	/// </returns>
	public static bool AddUncontains<T>(this List<T> list, T item)
	{
		if (list.Contains(item)) { return false; }
		
		list.Add(item);
		return true;
	}

	/// <summary>
	/// リストに既存のオブジェクトを削除します
	/// </summary>
	/// <returns> List に存在すれば TRUE を返します </returns>
	public static bool RemoveContains<T>(this List<T> list, T item)
	{
		if (!list.Contains(item)) { return false; }
		
		list.Remove(item);
		return true;
	}

}
