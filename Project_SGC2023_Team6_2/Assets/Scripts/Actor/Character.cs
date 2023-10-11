using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EState =CharacterLibrary.EState;

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
	[SerializeField] protected StateController<EState> m_ActionController;
	[Tooltip("キャラスプライト")]
	[SerializeField] protected SpriteRenderer m_CharacterRenderer;
	[Tooltip("専用のタイム")]
	[SerializeField] protected EachTime m_SelfTime;
	[Tooltip("被ダメージ時のカラーチェンジシステム")]
	[SerializeField] protected DamegeColorSystem m_DamegeColorSystem;
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

	public virtual void SelfAwake()
	{
		m_SelfTime = TimeManager.rootTime.CreateChild();

		m_DamegeColorSystem = new DamegeColorSystem(m_CharacterRenderer, m_SelfTime, 0.15f);
	}
	public virtual void SelfUpdate()
	{
		if (Keyboard.current.numpadPlusKey.wasPressedThisFrame) {
			m_DamegeColorSystem.Start();
		}

		m_DamegeColorSystem.Update();
	}
	public virtual void SeldDestory()
	{
		m_SelfTime.Destroy();
		m_SelfTime = null;
	}

	/// <summary>
	/// ステート切り替え
	/// </summary>
	public virtual void ChangeState(EState _state)
	{
		m_ActionController?.ChangeState(_state);
	}

	/// <summary>
	/// キャラの向きを更新する
	/// </summary>
	protected virtual void UpdateFlip() { }

	#endregion
}


