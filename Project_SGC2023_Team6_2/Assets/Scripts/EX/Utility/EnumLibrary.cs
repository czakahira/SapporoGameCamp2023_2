using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumLibrary
{
	
}

/// <summary>
/// 読み書きの結果
/// </summary>
public enum EGeneralResult
{
	Success, //成功
	Failed,  //失敗
}

/// <summary>
/// 汎用ステート
/// </summary>
public enum EGeneralState
{
	None,	//何もしない
	Start,	//開始
	Update,	//更新
	End,	//終了
	Pause,	//停止
}

/// <summary>
/// 汎用方向
/// </summary>
public enum EGeneralDirection
{
	Neutral = 0,	//ニュートラル

	Up,			//上
	UpRight,	//右上
	Right,		//右
	DownRight,	//右下
	Down,		//下
	DownLeft,	//左下
	Left,		//左
	UpLeft,		//左上
}

/// <summary>
/// ドラッグタイプ
/// </summary>
public enum EDragCallbackType
{
	None,

	BeginDrag,  //OnBeginDrag
	Drag,       //OnDrag
	EndDrag,    //OnEndDrag

	InternalBeginDrag,
	InternalDrag,
	InternalEndDrag,
}

/// <summary>
/// UIボタン入力タイプ
/// </summary>
public enum EButtonCallbackType
{
	None,			//何もしていない

	BiginDrag,		//ドラッグ開始
	Drag,			//ドラッグ中
	EndDrag,		//ドラッグ終了

	Down,			//押した
	Up,				//離した
	Click,			//クリック

	Enter,			//矩形に入った
	Exit,			//矩形から出た

	Tap,			//タップ
	DoubleTap,		//ダブルタップ

	BeginLongPress,	//長押し（開始）
	LongPress,		//長押し（継続）
	ExitLongPress,	//長押し（終了）
}