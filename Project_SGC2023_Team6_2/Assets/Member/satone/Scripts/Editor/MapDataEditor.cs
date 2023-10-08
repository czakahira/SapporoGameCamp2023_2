//======================================================
//
// マップデータの情報処理 [MapDataEditor.cs]
// Author : shion satone
//
//======================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(GameManager))]     //GameManagerクラスのエディタ拡張を作るよう指定

//======================================================
//マップのセーブロード処理
//======================================================
public class MapDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameManager manager = target as GameManager;    //asはキャスト

        if(GUILayout.Button("Save"))
        {//セーブボタン

            //プレハブは、プレハブ名をJSONに出力する

            //宝箱のファイル名代入
            foreach (TreasureData data in manager.mapData.m_treasures)
            {
                data.m_name = data.m_prefab.name;
            }

            //jsonファイルの出力
            string dataPath = Application.dataPath + "/Member/satone/Resources/" + manager.mapName + ".json";

            var json = JsonUtility.ToJson(manager.mapData);
            StreamWriter writer = new StreamWriter(dataPath, false);        //jsonデータに変換
            writer.WriteLine(json);     //JSONデータ書き込み
            writer.Flush();             //バッファをクリアする
            writer.Close();             //ファイルをクローズする

        }
        else if (GUILayout.Button("Load"))
        {//ロードボタン

            //JSONファイルの読み込み
            string jsonString = Resources.Load<TextAsset>(manager.mapName).ToString();

            manager.mapData = JsonUtility.FromJson<MapData>(jsonString);

            //Resourceからプレハブを読み込む
            foreach (TreasureData data in manager.mapData.m_treasures)
            {
                data.m_prefab = (GameObject)Resources.Load(data.m_name);
            }
        }

        DrawDefaultInspector();

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
