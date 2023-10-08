//======================================================
//
// マップデータの情報保存用 [MapData.cs]
// Author : shion satone
//
//======================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//======================================================
//マップの情報まとめ
//======================================================
[System.Serializable]
public class MapData
{
    //生成情報配列
    public TreasureData[] m_treasures;       //宝箱の情報
}

//======================================================
//宝箱の情報
//======================================================
[System.Serializable]
public class TreasureData
{
    //プレハブの名前保存用
    [HideInInspector] public string m_name;

    //生成するプレハブ
    public GameObject m_prefab;

    //出現フレーム数
    public int m_frame;

    //出現位置
    public Vector3 m_position;
}