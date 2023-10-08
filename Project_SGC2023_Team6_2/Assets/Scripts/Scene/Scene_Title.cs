using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// タイトルシーン処理用
/// </summary>
public class Scene_Title : MonoBehaviour
{
	protected int m_Step = 0;

	/// <summary>
	/// Animatorをanimという変数で定義する
	/// </summary>
	[SerializeField] protected Animator m_Letteranim;
	/// <summary>
	/// タイトル動画用
	/// </summary>
	[SerializeField] protected VideoPlayer m_VideoPlayer;
	/// <summary>
	/// タイマー
	/// </summary>
	protected Timer m_Timer = new Timer();
	/// <summary>
	/// 
	/// </summary>
	protected AudioPlayer_BGM m_AudioPlayer;
	/// <summary>
	/// 
	/// </summary>
	[SerializeField] protected CanvasGroup m_CanvasGroup;

	[SerializeField] protected AnimationCurve m_Curve;


	// Start is called before the first frame update
	void Start()
	{
		m_Step = 0;
	}

	// Update is called once per frame
	void Update()
	{
		

		switch (m_Step) {
			case 0: {
				m_VideoPlayer.Play();
				m_Step++;
			} break;

			case 1: {
				float rate = ((float)m_VideoPlayer.time / (float)m_VideoPlayer.clip.length);
				if (rate >= 1) {
					m_AudioPlayer = AudioManager.instance.PlayBGM(EBGM.Title2);
					m_Timer.Start(1);
					m_Step++;
				}
			} break;

			case 2: {
				bool end = m_Timer.Update();
				float rate = m_Timer.progress;
				m_CanvasGroup.alpha = m_Curve.Evaluate(rate);
				if (end) {
					m_Step++;
				}
			} break;

			case 3: {
				//もし、スペースキーが押されたらなら
				if (Input.GetKey(KeyCode.Space))
				{
					AudioManager.instance.StopBGM(m_AudioPlayer);
					AudioManager.instance.PlaySE(ESe.OpenLetter);
					//Bool型のパラメーターであるblRotをTrueにする
					m_Letteranim.SetBool("LetterBool", true);
					m_Timer.Start(1);
					m_Step++;
				}
			} break;

			case 4: {
				if (!m_Timer.Update()) {  break;}

				GameSceneManager.instance.NextScene(EScene.GameMain);
			} break;
		}
	}
}