using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

using UnityEngine;
using UnityEngine.InputSystem;

using Cysharp.Threading.Tasks;

using UnityEx;

/// <summary>
/// セーブ・ロードマネージャー
/// </summary>
public class SaveLoadManager : SingletonBehaviour<SaveLoadManager>
{
	/// <summary>
	/// 現在のセーブ先スロット番号
	/// </summary>
	public int currentSaveSlotIndex { get; protected set; } = -1;
	/// <summary>
	/// 現在のセーブ先のファイル相対パス
	/// </summary>
	public string currentDataFilePath { get; protected set; } = null;
	/// <summary>
	/// セーブ中もしくはロード中フラグ
	/// </summary>
	public bool nowSavingOrLoading { get; protected set; } = false;


	#region Method
	/// <summary>
	/// 次回セーブ・ロードするスロットを指定する
	/// </summary>
	public void SetNextDataSlot(int _slotIndex)
	{
		if(!MathEX.Until(_slotIndex, -1, SaveLoadLibrary.SAVEDATA_SLOT_MAX)) {
			DebugEX.LogError($"SlotIndex {_slotIndex} Invalid");
			return;
		}

		currentSaveSlotIndex = _slotIndex;
		currentDataFilePath = SaveLoadLibrary.SAVESLOT_PATHS[_slotIndex];
	}

	/// <summary>
	/// 非同期セーブを行う
	/// <para> セーブの完了はコールバックから確認できます </para>
	/// </summary>
	public async void DoSave<T>(T _saveData, Action<EGeneralResult> _callback = null)
	{
		if (nowSavingOrLoading) { return; }

		//ファイルに書き込み出力する
		nowSavingOrLoading = true;
		using (FileStreamer stream = new FileStreamer()) {
			await stream.WriteAsync(_saveData, currentDataFilePath, _callback, SaveLoadLibrary.SAVEDATA_CONVERT_TYPE, SaveLoadLibrary.SAVEDATA_SECURITY_FLAG);
			nowSavingOrLoading = false;
		}
	}

	/// <summary>
	/// 非同期ロードを行う
	/// <para> ロード成功後の返り値Tはコールバックから参照できます </para>
	/// </summary>
	public async void DoLoad<T>(Action<EGeneralResult, T> _callback = null)
	{
		if (nowSavingOrLoading) { return; }

		//ファイルを読み込む
		nowSavingOrLoading = true;
		using (FileStreamer stream = new FileStreamer()) {
			await stream.ReadAsync(currentDataFilePath, _callback, SaveLoadLibrary.SAVEDATA_CONVERT_TYPE, SaveLoadLibrary.SAVEDATA_SECURITY_FLAG);
			nowSavingOrLoading = false;
		}
	}

	#endregion

}


