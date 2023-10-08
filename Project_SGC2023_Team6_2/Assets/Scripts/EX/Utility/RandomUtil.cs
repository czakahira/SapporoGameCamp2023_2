using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEx;

/// <summary>
/// 乱数の便利クラス
/// </summary>
public class RandomUtil
{
	#region Property
	private static RandomUtil m_Instance = null;
	public static RandomUtil instance
	{
		get {
			if (m_Instance == null) {
				m_Instance = new RandomUtil();
			}
			return m_Instance;
		}
	}
	#endregion

	#region Field
	private System.Random m_Random;
	private int m_Seed;
	#endregion

	#region Constructor
	public RandomUtil()
	{
		Initialize(Time.frameCount);
	}
	public RandomUtil(int seed)
	{
		Initialize(seed);
	}
	#endregion

	#region Public
	/// <summary>
	/// 0.0以上、1.0未満の範囲でランダムなdoubleを返す
	/// </summary>
	public double GetRandomD()
	{
		return m_Random.NextDouble();
	}
	/// <summary>
	/// 0.0以上、1.0未満の範囲でランダムなfloatを返す
	/// </summary>
	public float GetRandomF()
	{
		return (float)m_Random.NextDouble();
	}

	/// <summary>
	/// min ≦ 返り値 ＜ max
	/// </summary>
	public int Range(int min, int max)
	{
		return m_Random.Next(min, max);
	}
	/// <summary>
	/// min ≦ 返り値 ＜ max
	/// </summary>
	public float RangeF(float min, float max)
	{
		return (GetRandomF() * (max - min)) + min;
	}
	/// <summary>
	/// min ≦ 返り値 ≦ max
	/// </summary>
	public int RangeInclude(float min, float max)
	{
		return MathEX.Round2Int(RangeF(min, max));
	}

	/// <summary>
	/// 1/2の確率でtrue/false
	/// </summary>
	public bool HeadOrTail()
	{
		return 0 == Range(0, 2);
	}
	/// <summary>
	/// rate(確率:0.0～1.0)で抽選
	/// </summary>
	public bool Lottery(float rate)
	{
		return GetRandomF() <= rate;
	}

	#endregion

	#region Private
	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="_seed"> シード値 </param>
	private void Initialize(int _seed)
	{
		m_Seed = _seed;
		m_Random = null;
		m_Random = new System.Random(m_Seed);
	}
	#endregion
}

