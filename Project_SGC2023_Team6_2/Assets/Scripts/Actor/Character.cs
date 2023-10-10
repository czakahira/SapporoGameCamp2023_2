using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	[SerializeField] protected StateController<ECharacterState> m_ActionController;
	[Tooltip("�X�v���C�g")]
	[SerializeField] protected SpriteRenderer m_SpriteRenderer;
	[Tooltip("�ړ����x")]
	[SerializeField] protected float m_MoveSpeed = 10;

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

	public virtual void ChangeState(ECharacterState _state)
	{
		m_ActionController?.ChangeState(_state);
	}
	#endregion
}

/// <summary>
/// �L�����N�^�[�̃X�e�[�g�ꗗ
/// </summary>
public enum ECharacterState
{
	None = 0,

	Idle,
	//Walk,
	Run,
}
