using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEx
{
	public static partial class Util
	{
		private static uint m_Static_UniqueIdCounter;

		/// <summary>
		/// 非nullのActionを実行
		/// </summary>
		public static void SafeInvoke(Action action)
		{
			//if (action != null) { action(); }
			action?.Invoke(); //C# 6.0
		}

		public static uint CreateUniqueID()
		{
			DebugEX.Assert(m_Static_UniqueIdCounter != 0xFFFFFFFF);
			return ++m_Static_UniqueIdCounter;
		}

		#region DateTime
		/// <summary>
		/// DateTimeをUnixTimeへ変換する
		/// </summary>
		/// <param name="_dateTime"></param>
		/// <returns></returns>
		public static long ToUnixTime(DateTime _dateTime)
		{
			DateTimeOffset offset = new DateTimeOffset(_dateTime.ToUniversalTime().Ticks, new TimeSpan(0, 0, 0));
			return offset.ToUnixTimeSeconds();
		}
		/// <summary>
		/// UnixTimeをDateTimeへ変換する
		/// </summary>
		/// <param name="_unixTime"></param>
		/// <returns></returns>
		public static DateTime ToDateTime(long _unixTime)
		{
			return DateTimeOffset.FromUnixTimeSeconds(_unixTime).LocalDateTime;
		}
		/// <summary>
		/// from から to までの差を返す
		/// </summary>
		/// <param name="_from"></param>
		/// <param name="_to"></param>
		/// <returns></returns>
		public static TimeSpan GetTimeSpan(DateTime _from, DateTime _to)
		{
			return (_from - _to);
		}
		/// <summary>
		/// 現地時刻から to までの差を返す
		/// </summary>
		/// <param name="_to"></param>
		/// <returns></returns>
		public static TimeSpan GetTimeSpanNow(DateTime _to)
		{
			return GetTimeSpan(DateTime.Now, _to);
		}
		#endregion

	}
}
