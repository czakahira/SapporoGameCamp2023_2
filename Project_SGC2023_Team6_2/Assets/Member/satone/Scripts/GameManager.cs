//======================================================
//
// ゲームマネージャー処理 [GameManager.cs]
// Author : shion satone
//
//======================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//マップデータ
	[SerializeField]
	private MapData m_mapData;

	//マップ名
	[SerializeField]
	private string m_mapName;

	[SerializeField]
	[Header("プレイヤーの位置")] Vector3 posCenter;   //プレイヤーの位置

	[SerializeField]
	[Header("一つの円の半径(X, Y)")] Vector2 fRadius;  //一つの円の半径

	[SerializeField]
	[Header("プレイヤーから敵の円の距離")] float fLength;  //プレイヤーから敵の円の距離

	[SerializeField]
	[Header("敵が一度に一つの円に増える数")] int nNumGenerate;   //敵が増える数

	[SerializeField]
	[Header("敵のプレハブ")] GameObject m_enemyPrefab;      //敵のプレハブ

	[SerializeField]
	[Header("敵スポーン時間(秒)")] private int m_DestFrame;     //目標のフレームカウント

	//現在のフレームカウント
	private float m_frame = 0;

	//宝箱の情報
	private TreasureData[] m_treasureData;

	int nNumTreasure = 0;         //宝物数
	int nNumSaveTreasure = 0;     //宝物数保存用

	// Start is called before the first frame update
	void Start()
	{
		m_treasureData = m_mapData.m_treasures;     //宝箱の情報代入

		//======================================================
		//宝箱の生成
		//======================================================
		for (int nCnt = 0; nCnt < m_mapData.m_treasures.Length; nCnt++)
		{
			//宝箱のゲームオブジェクトを生成する
			GameObject obj = Instantiate(m_treasureData[nCnt].m_prefab, m_treasureData[nCnt].m_position, Quaternion.identity) as GameObject;

			nNumTreasure++;     //宝物数加算
		}

		nNumSaveTreasure = nNumTreasure;        //総数保存
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F8) &&
			nNumTreasure > 0)
		{
			nNumTreasure--;     //宝物消す
		}

		//敵の生成
		EnemyGenerate();

	}

	//======================================================
	//敵の生成処理
	//======================================================
	private void EnemyGenerate()
	{
		if (m_DestFrame <= m_frame)
		{//一定時間経過したら
			if (m_enemyPrefab == null) { return; }

			//長さ
			float fLengthDest = fLength * nNumTreasure / nNumSaveTreasure + ((nNumSaveTreasure - nNumTreasure) * 0.5f);

			//敵の数
			int nNum = nNumGenerate + (nNumSaveTreasure - nNumTreasure);

			Debug.Log(fLengthDest);

			Vector3 randPos;        //ランダムな位置

			for (int nCntEnemy = 0; nCntEnemy < nNum; nCntEnemy++)
			{
				//円の範囲内のランダムな位置代入(右)
				randPos.x = fRadius.x * Random.insideUnitCircle.x + posCenter.x + fLengthDest;
				randPos.y = fRadius.y * Random.insideUnitCircle.y + posCenter.y;
				randPos.z = posCenter.z;

				//敵の生成
				Instantiate(m_enemyPrefab, randPos, Quaternion.identity);

				//円の範囲内のランダムな位置代入(左)
				randPos.x = fRadius.x * Random.insideUnitCircle.x + posCenter.x - fLengthDest;
				randPos.y = fRadius.y * Random.insideUnitCircle.y + posCenter.y;
				randPos.z = posCenter.z;

				//敵の生成
				Instantiate(m_enemyPrefab, randPos, Quaternion.identity);

				//円の範囲内のランダムな位置代入(上)
				randPos.x = fRadius.x * Random.insideUnitCircle.x + posCenter.x;
				randPos.y = fRadius.y * Random.insideUnitCircle.y + posCenter.y + fLengthDest;
				randPos.z = posCenter.z;

				//敵の生成
				Instantiate(m_enemyPrefab, randPos, Quaternion.identity);

				//円の範囲内のランダムな位置代入(下)
				randPos.x = fRadius.x * Random.insideUnitCircle.x + posCenter.x;
				randPos.y = fRadius.y * Random.insideUnitCircle.y + posCenter.y - fLengthDest;
				randPos.z = posCenter.z;

				//Debug.Log(fInsideRadius.x + fRadius.x);
				//Debug.Log("X:" + randPos.x + " Y:" + randPos.y);

				//敵の生成
				Instantiate(m_enemyPrefab, randPos, Quaternion.identity);
			}

			m_frame = 0;        //フレーム数リセット
		}
		else
		{//時間経過してなかったら

			m_frame += Time.deltaTime;
		}
	}

	///======================================================
	///円周上のランダムな位置を求める処理
	///======================================================
	private Vector3 CircleHorizon(float radius)
	{
		var angle = Random.Range(0, 360);       //ランダムな角度
		var rad = angle * Mathf.Deg2Rad;        //角度の変換

		//範囲内の座標求める
		var px = Mathf.Cos(rad) * radius;
		var py = Mathf.Sin(rad) * radius;

		return new Vector3(px, py, 0);      //範囲内のランダムな値を返す
	}

	//======================================================
	//マップデータのプロパティ
	//======================================================
	public MapData mapData
	{
		get { return this.m_mapData; }
		set { this.m_mapData = value; }
	}

	//======================================================
	//マップ名のプロパティ
	//======================================================
	public string mapName
	{
		get { return this.m_mapName; }
		set { this.m_mapName = value; }
	}
}
