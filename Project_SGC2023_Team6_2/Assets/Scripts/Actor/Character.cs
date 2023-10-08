using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// \�L�����N�^�[���N���X
/// </summary>
public class Character : MonoBehaviour
{
	[Tooltip("�A�j���[�^�[")]
	[SerializeField] protected Animator m_Animator;
	[Tooltip("��p����")]
	[SerializeField] protected Rigidbody2D m_Rb;
	[Tooltip("�R���C�_�[")]
	[SerializeField] protected CapsuleCollider2D m_Collider;
	[Tooltip("�X�e�[�g�Ǘ�")]
	[SerializeField] protected ActionController m_ActionController;
	[Tooltip("�X�v���C�g")]
	[SerializeField] protected SpriteRenderer m_SpriteRenderer;
	[Tooltip("�ړ����x")]
	[SerializeField] protected float m_MoveSpeed = 10;

	[Header("DEBUG")]
	[SerializeField] protected bool m_IsAutoAwake = false;


	public virtual CharacterLabel label => CharacterLabel.None;
	/// <summary>
	/// �������Ă������
	/// </summary>
	public Vector3 currentDirection { get; protected set; }

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

}
