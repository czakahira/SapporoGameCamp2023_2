using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//名前空間(Unity拡張メソッド)
namespace UnityEx
{
	/// <summary>
	/// GameObject拡張(Extend)メソッド　("this Type self"のTypeを拡張する)
	/// </summary>
	public static class EX_GameObject
	{
		//空のオブジェクト作成
		public static GameObject Create(string name)
		{
			GameObject obj = new GameObject(name);
			return obj;
		}

		//オブジェクト名の変更
		/// <summary> re"Name" this gameObject </summary>
		public static string ReName(this GameObject self, string name)
		{
			return self.name = name;
		}
		//オブジェクトのタグ変更
		/// <summary> re"Tag" this gameObject </summary>
		public static string ReTag(this GameObject self, string tag)
		{
			return self.tag = tag;
		}
		//オブジェクトのレイヤー変更
		/// <summary> re"Layer"this gameObject </summary>
		public static LayerMask ReLayer(this GameObject self, string Layer)
		{
			return self.layer = LayerMask.NameToLayer(Layer);
		}
		public static LayerMask ReLayer(this GameObject self, uint LayerNum)
		{
			return self.layer = Convert.ToInt32(LayerNum);
		}
		//オブジェクトのStatic化
		/// <summary> this gameObject change "static"Object </summary>
		public static bool IsStatic(this GameObject self, bool check)
		{
			return self.isStatic = check;
		}
		//オブジェクトの複製
		public static GameObject Clone(this GameObject original)
		{
			GameObject clone = GameObject.Instantiate(original);
			clone.ReName(original.name + " Clone");
			return clone;
		}
		public static GameObject Clone(this GameObject original, Vector3 position, Quaternion rotation, Transform parent)
		{
			GameObject clone = GameObject.Instantiate(original, position, rotation, parent);
			clone.ReName(original.name + " Clone");
			return clone;
		}
		/// <summary>
		/// オブジェクトの破棄
		/// </summary>
		/// <param name="leavechild">子を残す</param>
		public static void Discard(this GameObject self, bool leavechild = false)
		{
			//親だった場合、引数boolによって子を残す
			if (leavechild) { self.transform.DetachChildren(); }
			GameObject.Destroy(self);
		}
		//オブジェクトの親取得
		public static GameObject GetMother(this GameObject self)
		{
			return self.transform.parent.gameObject;
		}
		public static GameObject GetRoot(this GameObject self)
		{
			return self.transform.root.gameObject;
		}

		/// <summary>
		/// 指定名オブジェクトを検索
		/// </summary>
		/// <param name="root">親</param>
		/// <param name="name">探すオブジェクト名</param>
		/// <param name="includeInactive">非アクティブでも検索対象にするか</param>
		/// <returns> name.gameObject </returns>
		public static GameObject FindDeep(this GameObject root, string name, bool includeInactive = false)
		{
			Transform[] children = root?.GetComponentsInChildren<Transform>(includeInactive);
			if(children != null) {
				foreach (var child in children) {
					if (child.name.Equals(name)) {
						return child.gameObject;
					}
				}
			}
			return null;
		}

	}
}
