using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �C���Q�[���̏���
/// </summary>
public class Scene_GameMain : MonoBehaviour
{
	public enum EState
	{
		None,

		Init,

		Ready,
		Main,
		TimeOut,
		Clear,
	}
	/// <summary>
	/// �C���Q�[���̏�
	/// </summary>
	protected EState m_State = EState.None;
	/// <summary>
	/// �C���Q�[���̃X�e�[�g�Ǘ�
	/// </summary>
	protected StateController<EState> m_States;
	/// <summary>
	/// �������Ԃ̃^�C�}�[
	/// </summary>
	protected Timer m_Timer;


	// Start is called before the first frame update
	void Awake()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		switch (m_State) {
			case EState.None: {

			} break;
			default: break;
		}
	}

	/// <summary>
	/// �`�F�b�N�F�N���A
	/// </summary>
	protected bool Check_Clear()
	{
		return false;
	}
	/// <summary>
	/// �`�F�b�N�F��������
	/// </summary>
	/// <returns></returns>
	protected bool Check_GameFail()
	{

		return false;
	}

}
