using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardBullet : BulletBase
{
	/// <summary>
	/// �O�i���x
	/// </summary>
	[SerializeField] public float m_Speed;

	public override void Update()
	{
		if(isUsing) {
			m_Rb.velocity = transform.right * m_Speed;
		}
		
	}

	
}
