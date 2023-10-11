using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EState =CharacterLibrary.EState;

/// <summary>
/// \�L�����N�^�[���N���X
/// </summary>
public class Character : MonoBehaviour
{
	// --------------------
	// Member
	// --------------------
	#region open
	[Tooltip("�A�j���[�^�[")]
	[SerializeField] protected Animator m_Animator;
	[Tooltip("��p����")]
	[SerializeField] protected Rigidbody2D m_Rb;
	[Tooltip("�R���C�_�[")]
	[SerializeField] protected CapsuleCollider2D m_Collider;
	[Tooltip("�X�e�[�g�Ǘ�")]
	[SerializeField] protected StateController<EState> m_ActionController;
	[Tooltip("�L�����X�v���C�g")]
	[SerializeField] protected SpriteRenderer m_CharacterRenderer;
	[Tooltip("�ړ����x")]
	[SerializeField] protected float m_MoveSpeed = 10;
	[Tooltip("��p�̃^�C��")]
	[SerializeField] protected EachTime m_SelfTime;

	[Header("DEBUG")]
	[SerializeField] protected bool m_IsAutoAwake = false;
	#endregion

	// ---------------------
	// Property
	// ---------------------
	#region open
	public virtual CharacterLabel label => CharacterLabel.None;
	/// <summary>
	/// �������Ă������
	/// </summary>
	public Vector3 currentDirection { get; protected set; }
	#endregion

	// --------------------
	// Methos
	// --------------------
	#region open
	// Start is called before the first frame update
	void Awake()
	{
		//if (m_IsAutoAwake == false) { return; }
		SelfAwake();
	}
	// Update is called once per frame
	void Update()
	{
		SelfUpdate();
	}

	public virtual void SelfAwake() { }
	public virtual void SelfUpdate() { }
	public virtual void SeldDestory() { }

	/// <summary>
	/// �X�e�[�g�؂�ւ�
	/// </summary>
	public virtual void ChangeState(EState _state)
	{
		m_ActionController?.ChangeState(_state);
	}

	/// <summary>
	/// �L�����̌������X�V����
	/// </summary>
	protected virtual void UpdateFlip() { }

	#endregion
}


