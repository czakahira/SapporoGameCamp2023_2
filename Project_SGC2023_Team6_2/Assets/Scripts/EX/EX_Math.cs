using System;
using UnityEngine;


namespace UnityEx
{
	/// <summary>
	/// Math(f)の便利クラス置き場
	/// </summary>
	public static class MathEX
	{
		/// <summary>
		/// 「最小値 ≦ 指定値 ≦ 最大値」であるか
		/// </summary>
		public static bool Within(float value, float min, float max)
		{
			return (min <= value && value <= max);
		}
		/// <summary>
		/// 「最小値 ＜ 指定値 ＜ 最大値」であるか
		/// </summary>
		public static bool Until(float value, float min, float max)
		{
			return (min < value && value < max);
		}


		/// <summary>
		/// 四捨五入（float）
		/// </summary>
		/// <param name="value"> 化す数値 </param>
		/// <returns> 有理数 </returns>
		public static float Round2Float(float value)
		{
			//返す値
			float result = value;
			//小数点以下の値を出す
			float less = MathF.Abs(value - MathF.Floor(value));

			//切り上げ
			if (less >= 0.5f) {
				result = MathF.Ceiling(value);
			}
			//切り捨て
			else {
				result = MathF.Floor(value);
			}
			return result;
		}

		/// <summary>
		/// 四捨五入（int）
		/// </summary>
		/// <param name="value"> 化す数値 </param>
		/// <returns> 整数 </returns>
		public static int Round2Int(float value)
		{
			//返す値
			int result = 0;
			//小数点以下の値を出す
			float less = MathF.Abs(value - MathF.Floor(value));
			//切り上げor切り捨て
			if (less >= 0.5f) {
				result = Mathf.CeilToInt(value);
			}
			else {
				result = Mathf.FloorToInt(value);
			}
			return result;
		}

		/// <summary>
		/// 四捨五入(float：小数点指定)
		/// </summary>
		/// <param name="value"> 化す数値 </param>
		/// <param name="decimalPoint"> 着目する小数点の位置 </param>
		/// <returns> 有理数 </returns>
		public static float Round2FloatDigit(float value, int decimalPoint = 1)
		{
			//累乗できる値でなければ返す
			if (decimalPoint <= 0) { DebugEX.LogError("小数点第1位未満は指定できません"); return value; }

			//指定位置まで、10で累乗
			float adjuster = (float)Math.Pow((double)10, (double)decimalPoint - 1);
			value *= adjuster;
			float result = Round2Float(value);
			//累乗した値で割る
			result /= adjuster;

			//返す
			return result;
		}

		/// <summary>
		/// ２点間（x を基軸にした y と）のなす角（度数法）を取得します
		/// </summary>
		/// <param name="y"></param>
		/// <param name="x"></param>
		/// <returns> degree（-180～180） </returns>
		public static float RoundAngle(float y, float x)
		{
			return MathF.Atan2(y, x) * Mathf.Rad2Deg;
		}
		/// <summary>
		/// ２点間（x を基軸にした y と）のなす角（度数法）を取得します
		/// </summary>
		/// <param name="y"></param>
		/// <param name="x"></param>
		/// <returns> degree（0～360） </returns>
		public static float RoundAngleRepeat(float y, float x)
		{
			return Mathf.Repeat(RoundAngle(y, x), 360);
		}

		/// <summary>
		/// 指定値を、最小～最大の間でループさせます
		/// </summary>
		public static float Loop(float _value, float _min, float _max)
		{
			float diff = Math.Abs(_max - _min); //最小～最大の差
			while (_value < _min) { _value += diff; }
			while (_value > _max) { _value -= diff; }
			return _value;
		}

		/// <summary>
		/// 平均値を求める
		/// </summary>
		public static float Average(float[] _value)
		{
			float sum = 0;
			int len = _value.Length;
			for (int i = 0; i < len; i++) {
				sum += _value[i];
			}

			return sum / len;
		}
		/// <summary>
		/// 平均座標を求める
		/// </summary>
		public static Vector3 Average(Vector3[] _point)
		{
			Vector3 sum = Vector3.zero;
			int len = _point.Length;
			for (int i = 0; i < len; i++) {
				sum += _point[i];
			}

			return sum / len;
		}

	}
}
