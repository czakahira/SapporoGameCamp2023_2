using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイマー
/// </summary>
public class Timer
{
	public enum eCheckType
	{
		None = 0,
		/// <summary>
		/// 時刻になったら一度だけ返す
		/// </summary>
		Just,
		/// <summary>
		/// 時刻後も継続して返す
		/// </summary>
		Cont,
		/// <summary>
		/// 時刻になったら経過をリセットし再開する
		/// </summary>
		Loop,
	}

	/// <summary>
	/// タイマーに使用するタイム
	/// </summary>
	protected EachTime m_Time;
	/// <summary>
	/// チェックタイプ
	/// </summary>
	[SerializeField] protected eCheckType m_Type;
	/// <summary>
	/// デルタのスケールを適用する
	/// </summary>
	[SerializeField] protected bool m_Scaled;
	/// <summary>
	/// 終了秒数
	/// </summary>
	[SerializeField] protected float m_Duration;
	/// <summary>
	/// 経過秒数
	/// </summary>
	[SerializeField] protected float m_Elapsed;
	/// <summary>
	/// 一時停止
	/// </summary>
	[field: SerializeField] public bool pause { get; set; } = false;
	/// <summary>
	/// 進捗率（0～1）
	/// </summary>
	public float progress => (m_Elapsed / m_Duration);

	public Timer(EachTime _base = null)
	{
		if (_base == null) { //始祖のタイマーを使う
			_base = TimeManager.rootTime;
		}
		m_Time = _base.CreateChild();
	}

	public void Start(float _duration, eCheckType _type = eCheckType.Just, bool _isScaled = true)
	{
		m_Type = _type;
		m_Duration = _duration;
		m_Scaled = _isScaled;
		m_Elapsed = 0;
	}
	public bool Update()
	{
		if (pause) { return false; }

		m_Elapsed += (m_Scaled ? m_Time.deltaTime : m_Time.unscaledDeltaTime);
		switch (m_Type) {
			case eCheckType.Just: {
				if (IsElapsed_Duration()) {
					m_Type = eCheckType.None;
					return true;
				}
			} break;

			case eCheckType.Cont: {
				return IsElapsed_Duration();
			} break;

			case eCheckType.Loop: {
				if (IsElapsed_Duration()) {
					m_Elapsed = 0.0f;
					return true;
				}
			} break;
		}

		return false;
	}
	public void Destroy()
	{
		if (m_Time != null) {
			m_Time.SelfRelease();
			m_Time = null;
		}

		m_Elapsed = m_Duration = 0.0f;
		m_Type = eCheckType.None;
		pause = false;
	}

	protected bool IsElapsed_Duration() { return IsElapsed(m_Duration);}
	protected bool IsElapsed(float _value) { return m_Elapsed >= _value; }

}
