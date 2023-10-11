using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEx;

/// <summary>
/// ダメージを受けた時の演出システム
/// </summary>
[Serializable]
public class DamegeColorSystem
{
	[SerializeField] protected int m_State = 0;
	[SerializeField] protected SpriteRenderer m_Sprite;
	[SerializeField] protected Timer m_Timer;
	[SerializeField] protected float m_Duration;

	public DamegeColorSystem(SpriteRenderer _sprite, EachTime _parentTime, float _duration = 1)
	{
		m_Sprite = _sprite;
		m_Timer = new Timer(_parentTime);
		m_Duration = _duration;
	}
	public void Update()
	{
		switch (m_State) {
			case 1: {
				m_Timer.Start(m_Duration);
				m_State++;
			} break;
			case 2: {
				bool end = m_Timer.Update();
				float rate = m_Timer.progress;
				m_Sprite.color = Color.HSVToRGB(1, rate, 1);
				if (end) {
					m_State++;
				}
			} break;
			case 3: {
				m_Sprite.color = Color.HSVToRGB(1, 0, 1);
				m_State++;
			} break;

			default: break;
		}
	}
	public void Destroy()
	{
		m_Sprite = null;
		m_Timer.Destroy();
		m_Timer = null;
	}

	public void Start()
	{
		m_State = 1;
	}

}
