using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// 入力判定マネージャー
/// </summary>
public class InputManager : SingletonBehaviour<InputManager>
{
	/// <summary>
	/// 移動スティック
	/// </summary>
	public Vector2 GetMove()
	{
		return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	/// <summary>
	/// 増殖コマンド
	/// </summary>
	public bool IsMultiply()
	{
		bool keyboard = (Keyboard.current.leftCtrlKey.isPressed && Keyboard.current.cKey.wasPressedThisFrame);
		bool pad = false;


		return keyboard || pad;
	}
	/// <summary>
	/// タイトルで使用するゲーム開始コマンド
	/// </summary>
	public bool IsGameStart()
	{
		return Keyboard.current.spaceKey.wasPressedThisFrame;
	}
	/// <summary>
	/// ゲームを終了する
	/// </summary>
	public bool IsQuitGame()
	{
		return Keyboard.current.escapeKey.wasPressedThisFrame;
	}
}
