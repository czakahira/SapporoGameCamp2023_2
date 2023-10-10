using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// \キャラクター基底クラス
/// </summary>
public class Character : MonoBehaviour
{
	// --------------------
	// Member
	// --------------------
	#region open
	[Tooltip("アニメーター")]
	[SerializeField] protected Animator m_Animator;
	[Tooltip("専用剛体")]
	[SerializeField] protected Rigidbody2D m_Rb;
	[Tooltip("コライダー")]
	[SerializeField] protected CapsuleCollider2D m_Collider;
	[Tooltip("ステート管理")]
	[SerializeField] protected StateController<ECharacterState> m_ActionController;
	[Tooltip("スプライト")]
	[SerializeField] protected SpriteRenderer m_SpriteRenderer;
	[Tooltip("移動速度")]
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
	/// 今向いている方向
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
/// キャラクターのステート一覧
/// </summary>
public enum ECharacterState
{
	None = 0,

	Idle,
	//Walk,
	Run,
}
