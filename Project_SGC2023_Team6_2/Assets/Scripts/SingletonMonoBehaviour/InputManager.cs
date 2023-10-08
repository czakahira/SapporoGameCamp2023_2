using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// ���͒l��Q�Ƃ���}�l�[�W���[
/// </summary>
public class InputManager : SingletonBehaviour<InputManager>
{
	/// <summary>
	/// �ړ����͒l��擾
	/// </summary>
	public Vector2 GetMove()
	{
		return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	/// <summary>
	/// ���B�R�}���h
	/// </summary>
	public bool IsMultiply()
	{
		//�L�[�{�[�h�̓��͒l
		bool keyboard = (Keyboard.current.leftCtrlKey.isPressed && Keyboard.current.cKey.wasPressedThisFrame);
		bool pad = false;


		return keyboard || pad;
	}
}
