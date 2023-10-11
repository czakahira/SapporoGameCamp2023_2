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
		Timeout,
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
	/// �C���Q�[���p�^�C��
	/// </summary>
	protected EachTime m_GameTime;
	/// <summary>
	/// �������Ԃ̃^�C�}�[
	/// </summary>
	protected Timer m_StageTimer;
	/// <summary>
	/// �X�e�[�W�̃t�F�[�Y�^�C�}�[
	/// </summary>
	protected Timer m_PhaseTimer;
	/// <summary>
	/// �v���C���[
	/// </summary>
	protected PlayerCharacter m_Player;


	void Awake()
	{
		
	}
	void Update()
	{
		switch (m_State) {
			case EState.Init: {
				m_GameTime = TimeManager.rootTime.CreateChild(3);

				m_StageTimer = new Timer(m_GameTime);
				m_PhaseTimer = new Timer(m_GameTime);
			} break;
			case EState.Ready: {
				
			} break;
			case EState.Main: {
				if (Check_Gameover() == true) {

				}
			} break;

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
	protected bool Check_Timeout()
	{
		return m_StageTimer.Update();
	}
	/// <summary>
	/// �Q�[���̏I���`�F�b�N
	/// </summary>
	protected bool Check_Gameover()
	{
		//�N���A
		if (Check_Clear()) {
			return true;
		}
		//���Ԑ؂�
		else if (Check_Timeout()) {
		
			return true;
		}
		return false;
	}

	/// <summary>
	/// �Q�[���̏���
	/// </summary>
	protected void Setup()
	{
		Setup_Player();
	}
	protected void Setup_Player()
	{
		m_Player.ChangeState(CharacterLibrary.EState.None);
	}

}
