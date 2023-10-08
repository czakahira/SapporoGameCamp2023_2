using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �U���I�u�W�F�N�g�̊��N���X
/// </summary>
public class BulletBase : MonoBehaviour
{
	/// <summary>
	/// �ڐG����^�C�v
	/// </summary>
	[SerializeField] protected Collision2DDetector m_Detector;
	/// <summary>
	/// �R���C�_�[
	/// </summary>
	[SerializeField] protected Collider2D m_Collider;
	/// <summary>
	/// ��p�̍���
	/// </summary>
	[SerializeField] protected Rigidbody2D m_Rb;
	/// <summary>
	/// �U���f�[�^
	/// </summary>
	[SerializeField] protected AttackData m_AttackData;
	/// <summary>
	/// �ˏo����
	/// </summary>
	[SerializeField] protected Vector2 m_Direction;
	 
	/// <summary>
	/// ���ݎg�p��
	/// </summary>
	[field: SerializeField] public bool isUsing { get; protected set; }

	
	public virtual void Awake()
	{
		isUsing = false;
		m_Detector.onTrigger2D += OnTrigger2D;
	}
	public virtual void Update()
	{

	}

	/// <summary>
	/// ����
	/// </summary>
	public virtual void Shoot(Vector2 _direction)
	{
		isUsing = true;
		m_Direction = _direction;
	}
	/// <summary>
	/// ����������
	/// </summary>
	public virtual void Hit()
	{
		isUsing = false;

		Destroy(this);
	}

	/// <summary>
	/// �ڐG�C�x���g
	/// </summary>
	public virtual void OnTrigger2D(EContactType type, Collider2D collider2D)
	{
		if (type == EContactType.Enter) {
			Character character = collider2D.GetComponent<Character>();;
			if (character == null) { return; }

			switch (character.label) {
				case CharacterLabel.Enemy: { Hit(); } break;
			}
		}
	}

	/// <summary>
	/// �U���f�[�^���擾����
	/// <para> �U�����󂯂��G���擾���� </para>
	/// </summary>
	public AttackData GetAttackData() { return m_AttackData; }
	

}
