using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEx
{
	public static class EX_Enum
	{
		/// <summary>
		/// フラグの整合性をチェックする
		/// </summary>
		public static bool CheckType(this Enum _base, Enum _flag)
		{
			return (_base.GetType() == _flag.GetType());
		}
		/// <summary> 
		/// フラグの設定
		/// </summary>
		public static void SetFlag(this Enum _base, Enum _flag)
		{
			if(_base.CheckType(_flag)) { return; }
			_base = _flag;
		}
		/// <summary> 
		/// フラグの追加 
		/// </summary>
		public static void AddFlag(this Enum _base, Enum _flag)
		{
			if (_base.CheckType(_flag)) { return; }

			ulong b = Convert.ToUInt64(_base);
			ulong f = Convert.ToUInt64(_flag);
			b |= f;

			Type t = _base.GetType();
			_base = (Enum)Convert.ChangeType(b, t);
		}
		/// <summary> 
		/// フラグの解除 
		/// </summary>
		public static void DelFlag(this Enum _base, Enum _flag)
		{
			if (_base.CheckType(_flag)) { return; }

			ulong b = Convert.ToUInt64(_base);
			ulong f = Convert.ToUInt64(_flag);

			b &= ~f;

			Type t = _base.GetType();
			_base = (Enum)Convert.ChangeType(b, t);
		}
		/// <summary> 
		/// 所持フラグのチェック 
		/// </summary>
		public static bool HasFlag(this Enum _base, Enum _flag)
		{
			if (_base.CheckType(_flag)) { return false; }

			ulong b = Convert.ToUInt64(_base);
			ulong f = Convert.ToUInt64(_flag);

			return (b & f) == f;
		}
	}

}

