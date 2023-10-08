using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 重み抽選用クラス
/// </summary>
/// <typeparam name="T">抽選する物の型</typeparam>
[System.Serializable]
public class RandomWeight<T>
{
	/// <summary>
	/// 抽選物の辞書
	/// <para> key = 抽選物 value = 重み </para>
	/// </summary>
	[SerializeField] private Dictionary<T, float> m_LotteryContents;

	// -------------------------
	// Constructor & Destructor
	// -------------------------
	public RandomWeight()
	{
		m_LotteryContents = new Dictionary<T, float>();
		Clear();
	}
	~RandomWeight() { }

	public void Destroy()
	{
		Clear();
		m_LotteryContents = null;
	}

	/// <summary>
	/// 抽選するキーの追加
	/// </summary>
	/// <param name="key"> 追加するキー</param>
	/// <param name="weight"> 重み </param>
	public void Add(T key, float weight)
	{
		//既に追加済みのキーなら重み変更
		if (m_LotteryContents.ContainsKey(key)) {
			Edit(key, weight);
			return;
		}
		//無ければ追加
		m_LotteryContents.Add(key, weight);
	}
	/// <summary>
	/// 指定キーの重みを変更する
	/// </summary>
	/// <param name="key"></param>
	/// <param name="weight"> 変更後の重み </param>
	public void Edit(T key, float weight)
	{
		if (m_LotteryContents.ContainsKey(key)) {
			m_LotteryContents[key] = weight;
			return;
		}
		DebugEX.LogError($"Key = {key} が存在しません");
	}
	/// <summary>
	/// 抽選物をクリアする
	/// </summary>
	public void Clear() { m_LotteryContents.Clear(); }
	/// <summary>
	/// 抽選する
	/// </summary>
	/// <param name="result"> 抽選の結果 </param>
	/// <returns> 抽選が正常に成功したか </returns>
	public bool GetRandom(out T result)
	{
		//初期化
		result = default;

		//抽選物が無ければ返す
		if (m_LotteryContents.Count <= 0) { DebugEX.LogWarning("抽選する物がありません"); return false; }

		//重みの合計値算出
		float total = 0.0f;
		foreach (var elem in m_LotteryContents) { total += elem.Value; }

		//乱数取得
		float randomPoint = RandomUtil.instance.RangeF(0.0f, total);
		
		//照会
		foreach (var elem in m_LotteryContents) {
			DebugEX.Log($"合計値 = {total}\n当選値 = {elem.Value}\n獲得乱数 = {randomPoint}");
			//乱数が重み未満なら返す
			if (randomPoint < elem.Value) {
				result = elem.Key;
				return true;
			}
			//重みを削っていく
			randomPoint -= elem.Value;
		}
		return false;
	}
}
