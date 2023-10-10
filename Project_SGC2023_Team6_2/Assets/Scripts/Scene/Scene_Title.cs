using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// タイトルシーン処理用
/// </summary>
public class Scene_Title : MonoBehaviour
{
	protected const int STEP_INIT = 0;
	protected const int STEP_READY_MOVIE = 1;
	protected const int STEP_PLAY_MOVIE = 2;
	protected const int STEP_EXIT_MOVIE = 3;
	protected const int STEP_OPEN_TITLE = 4;
	protected const int STEP_WAIT_COMMAMD = 5;
	protected const int STEP_NEXT_SCENE = 6;

	/// <summary>
	/// ステート番号
	/// </summary>
	protected int m_SubState = 0;
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
	protected Timer m_Timer;
	/// <summary>
	/// BGMのプレイバック
	/// </summary>
	protected AudioPlayer_BGM m_AudioPkayBack_BGM;
	/// <summary>
	/// タイトル動画グループ
	/// </summary>
	[SerializeField] protected CanvasGroup m_CanvasGroup_TitleMovie;
	/// <summary>
	/// タイトル動画の透明度カーブ
	/// </summary>
	[SerializeField] protected AnimationCurve m_AlphaCurve_TitleMovie;
	/// <summary>
	/// タイトルUIのグループ
	/// </summary>
	[SerializeField] protected CanvasGroup m_CanvasGroup_Title;
	/// <summary>
	/// ゲーム起動UI
	/// </summary>
	[SerializeField] protected Image m_PushCommamdUI;

	// Start is called before the first frame update
	void Awake()
	{
		m_SubState = 0;
	}

	// Update is called once per frame
	void Update()
	{
		switch (m_SubState) {
			// ▼ 初期化
			case STEP_INIT: {
				m_Timer = new Timer();

				SetAlpha(m_CanvasGroup_TitleMovie, 0); 
				SetAlpha(m_CanvasGroup_Title, 0);
				SetVisible(m_PushCommamdUI, false);

				m_Timer.Start(1);
				NextState();
			} break;
			// ▼ タイトルムービーの準備
			case STEP_READY_MOVIE: {
				if (m_Timer.Update() == false) { break; }

				SetAlpha(m_CanvasGroup_TitleMovie, 1);
				m_VideoPlayer.Play();
				NextState();
			} break;
			// ▼ タイトルムービーを再生する
			case STEP_PLAY_MOVIE: {
				var rate = (m_VideoPlayer.time / m_VideoPlayer.clip.length);
				if (rate >= 0.8f) { //再生完了したら次へ
					m_Timer.Start(0.5f);
					NextState();
				}
			} break;
			// ▼ タイトルムービーの退室
			case STEP_EXIT_MOVIE: {
				bool end = m_Timer.Update();
				float rate = m_Timer.progress;
				SetAlpha(m_CanvasGroup_TitleMovie, m_AlphaCurve_TitleMovie.Evaluate(1 - rate));
				if (end) {
					m_Timer.Start(1);
					NextState();
				}
			} break;
			// ▼ タイトル画面の表示
			case STEP_OPEN_TITLE: {
				bool end = m_Timer.Update();
				float rate = m_Timer.progress;
				SetAlpha(m_CanvasGroup_Title, rate);
				if (end) {
					m_AudioPkayBack_BGM = AudioManager.instance.PlayBGM(EBGM.Title2);
					SetAlpha(m_CanvasGroup_Title, 1);
					SetVisible(m_PushCommamdUI, true);
					NextState();
				}
			} break;
			// ▼ 開始コマンドを待つ
			case STEP_WAIT_COMMAMD: {
				//もし、スペースキーが押されたらなら
				if (InputManager.instance.IsGameStart()) {
					AudioManager.instance.StopBGM(m_AudioPkayBack_BGM); //BGMを停止
					AudioManager.instance.PlaySE(ESe.OpenLetter);		//SEを再生
					m_Letteranim.SetBool("LetterBool", true);			//手紙を開く
					m_Timer.Start(1.5f);								//シーンの遷移まで少し待つ
					NextState();
				}
			} break;
			//次のシーンへ
			case STEP_NEXT_SCENE: {
				if (m_Timer.Update() == false) { break; }

				GameSceneManager.instance.NextScene(EScene.GameMain);
				EndState();
			} break;
		}
	}

	/// <summary>
	/// 次のステートへ
	/// </summary>
	protected void NextState() { m_SubState++; }
	/// <summary>
	/// ステート終了
	/// </summary>
	protected void EndState() { m_SubState = -1; }
	/// <summary>
	/// 指定キャンバスの透明度を設定する
	/// </summary>
	protected void SetAlpha(CanvasGroup _cg, float _alpha) { _cg.alpha = _alpha; }

	protected void SetVisible(Graphic _graphic, bool _visible) { _graphic.gameObject.SetActive(_visible); }

}