using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// セーブ（ロード）ライブラリ <br/>
/// <br/>
/// 相対パスは エディタ上ではプロジェクトフォルダ（Assetsフォルダの1つ上）が、 <br/>
/// Windowsビルドでは出力されたexeの在る所が、カレントディレクトリになります
/// </summary>
public static class SaveLoadLibrary
{
	// -------------------------
	// セーブ(ロード)の標準設定
	// -------------------------
	/// <summary>
	/// セーブデータの文字列形式
	/// </summary>
	static readonly public EConvertTextType SAVEDATA_CONVERT_TYPE = EConvertTextType.JSON;
	/// <summary>
	/// セーブデータを暗号化（復号化）するフラグ
	/// </summary>
	static readonly public bool SAVEDATA_SECURITY_FLAG = true;
	/// <summary>
	/// セーブデータのスロット数
	/// </summary>
	static readonly public int SAVEDATA_SLOT_MAX = 3; // Total 4 Slot
	/// <summary>
	/// セーブデータのルートディレクトリ名
	/// </summary>
	static readonly private string SAVEDATA_ROOT_DIRECTORY_NAME = "SaveData";
	/// <summary>
	/// セーブデータのファイル名
	/// </summary>
	/* ※補足
	 * 
	 *  sdf {スロット番号} slt {拡張子}
	 *		↓↓↓
	 *  sdf = Save Data File
	 *  slt = Slot
	 *  
	 */
	static readonly private string SAVEDATA_FILE_NAME = "sdf{0}slt{1}";
	/// <summary>
	/// セーブデータのファイル拡張子
	/// </summary>
	static public string SAVEDATA_FILE_EXTENSIONS 
	{ 
		get{
			//暗号化が有効な時はバイナリ
			if (SAVEDATA_SECURITY_FLAG) { return ".bin"; }
			else
			//JSON文字列の生データ
			if (SAVEDATA_CONVERT_TYPE == EConvertTextType.JSON) { return ".json"; }
			else
			//XML文字列の生データ
			if (SAVEDATA_CONVERT_TYPE == EConvertTextType.XML) { return ".xml"; }

			//どれにも当てはまらない
			return ".unknown";
		}
	}
	/// <summary>
	/// 各スロットの相対パス
	/// </summary>
	static readonly public string[] SAVESLOT_PATHS;

	// -------------------------
	// 静的コンストラクタ
	// -------------------------
	static SaveLoadLibrary()
	{
		//拡張子
		string ext = SAVEDATA_FILE_EXTENSIONS;

		//全セーブスロットの相対パスを作成する
		SAVESLOT_PATHS = new string[SAVEDATA_SLOT_MAX];
		for (int i = 0; i < SAVEDATA_SLOT_MAX; i++) {
			string fileName = string.Format(SAVEDATA_FILE_NAME, i, ext);

			SAVESLOT_PATHS[i] = $@"{SAVEDATA_ROOT_DIRECTORY_NAME}\{fileName}";
		}
	}

}
