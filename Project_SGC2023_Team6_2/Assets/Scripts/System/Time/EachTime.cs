using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各オブジェクトが持つことを想定したタイムクラス
/// <para>
/// １）そのオブジェクトだけ時間を早めたいor遅らせたい時に使用
/// </para>
/// <para>
/// ２）これは自由にインスタンスしてはいけません。インスタンスが欲しい場合はTimeManagerを介して　CreateChild() から取得してください。
/// </para>
/// </summary>
[Serializable]
public class EachTime
{
	// -------------------------
	// Member
	// -------------------------
	#region Open
	/// <summary>
	/// 自前のスケール（この値は基本弄らない。弄るのはプロパティ
	/// </summary>
	[NonSerialized] protected float m_SelfTimeScale;
	/// <summary>
	/// 管理下にある子タイム
	/// </summary>
	[NonSerialized] protected List<EachTime> m_ChildTimes;
	/// <summary>
	/// 管理下にある子タイムの総数
	/// </summary>
	protected int m_ChildTimesCount;
	/// <summary>
	/// 親タイム
	/// </summary>
	protected EachTime m_Parent;
	#endregion

	// -------------------------
	// Property
	// -------------------------
	#region Open
	public float timeScale
	{
		get { return m_SelfTimeScale; }
		set {
			m_SelfTimeScale = value;
			//子にも反映する
			if (m_ChildTimesCount > 0) {
				for (int i = 0; i < m_ChildTimesCount; i++) {
					m_ChildTimes[i].timeScale = m_SelfTimeScale;
				}
			}
		}
	}
	public float unscaledElapsedTime { get; protected set; }
	public float unscaledDeltaTime { get; protected set; }
	public float unscaledFixedDeltaTime { get; protected set; }
	public float elapsedTime { get { return (unscaledElapsedTime * m_SelfTimeScale); } }
	public float deltaTime { get { return unscaledDeltaTime * m_SelfTimeScale; } }
	public float fixedDeltaTime { get { return ((unscaledFixedDeltaTime * m_SelfTimeScale)); } }
	#endregion

	// ------------------------------
	// Constructor & Destructor
	// ------------------------------
	/// <summary>
	/// 
	/// </summary>
	/// <param name="_initialTimeScale"> 初回スケール </param>
	/// <param name="_childCapacity"> 子の要領 </param>
	/// <param name="_parent"> 紐づく親 </param>
	public EachTime(float _initialTimeScale = 1.0f, int _childCapacity = 1, EachTime _parent = null)
	{
		m_SelfTimeScale = _initialTimeScale;
		
		unscaledElapsedTime = 0;
		unscaledDeltaTime = 0;
		unscaledFixedDeltaTime = 0;

		m_ChildTimes = new List<EachTime>(_childCapacity);
		m_ChildTimesCount = 0;

		m_Parent = _parent;
	}
	~EachTime() { }

	// -------------------------
	// Method
	// -------------------------
	#region Open
	/// <summary>
	/// 
	/// </summary>
	/// <param name="_deltaTime">
	/// Time.deltaTime が渡される想定だが、絶対ではない <br/>
	/// Time.deltaTime を基準に扱うことで、Time.timeScale を全てのインスタンスに反映することができる
	/// </param>
	public void Update(float _deltaTime)
	{
		//デルタを更新
		unscaledDeltaTime = _deltaTime;
		unscaledElapsedTime += unscaledDeltaTime;

		//子には親のスケールが乗ったデルタを渡す
		if (m_ChildTimesCount > 0) {
			for (int i = 0; i < m_ChildTimesCount; i++) {
				m_ChildTimes[i].Update(deltaTime);
			}
		}
	}
	public void FixedUpdate(float _fixedDeltaTime)
	{
		unscaledFixedDeltaTime = _fixedDeltaTime;

		if (m_ChildTimesCount > 0) {
			for (int i = 0; i < m_ChildTimesCount; i++) {
				m_ChildTimes[i].FixedUpdate(fixedDeltaTime);
			}
		}
	}
	/// <summary>
	/// 破棄
	/// <para> ・親が破棄されたら、その子も芋づる式に解放・破棄されていく </para>
	/// <para> ・始祖のタイムが破棄されれば、全てのタイムが破棄される感じ </para>
	/// </summary>
	public void Destroy()
	{
		if (m_Parent != null) {
			m_Parent = null;
		}

		if (m_ChildTimesCount > 0) {
			foreach (var cld in m_ChildTimes) {
				cld.Destroy();		//破棄する
			}
			m_ChildTimes.Clear();
			m_ChildTimes = null;
		}
	}

	/// <summary>
	/// 子タイムを取得します <br/>
	/// ※取得後、不要になったら必ず解放してください
	/// </summary>
	public EachTime CreateChild(int _childCapacity = 1)
	{
		EachTime child = new EachTime(m_SelfTimeScale, _childCapacity, this);
		m_ChildTimes.Add(child);
		m_ChildTimesCount++;
		return child;
	}
	/// <summary>
	/// 指定した子タイムを解放します
	/// </summary>
	public void ReleaseChild(EachTime _child)
	{
		if (m_ChildTimes.RemoveContains(_child)) {
			m_ChildTimesCount--;
		}
	}
	/// <summary>
	/// このタイムが子タイムである時、キャッシュした親タイムから解放します
	/// </summary>
	public void SelfRelease()
	{
		m_Parent?.ReleaseChild(this);
	}

	public float GetSpeed(float _value) { return _value * deltaTime; }
	public float GetStep(float _value) { return (1 - Mathf.Exp(-_value * deltaTime)); }
	#endregion

}
