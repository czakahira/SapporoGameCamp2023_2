using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// �^�C�g���V�[�������p
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
	/// �X�e�[�g�ԍ�
	/// </summary>
	protected int m_SubState = 0;
	/// <summary>
	/// Animator��anim�Ƃ����ϐ��Œ�`����
	/// </summary>
	[SerializeField] protected Animator m_Letteranim;
	/// <summary>
	/// �^�C�g������p
	/// </summary>
	[SerializeField] protected VideoPlayer m_VideoPlayer;
	/// <summary>
	/// �^�C�}�[
	/// </summary>
	protected Timer m_Timer;
	/// <summary>
	/// BGM�̃v���C�o�b�N
	/// </summary>
	protected AudioPlayer_BGM m_AudioPkayBack_BGM;
	/// <summary>
	/// �^�C�g������O���[�v
	/// </summary>
	[SerializeField] protected CanvasGroup m_CanvasGroup_TitleMovie;
	/// <summary>
	/// �^�C�g������̓����x�J�[�u
	/// </summary>
	[SerializeField] protected AnimationCurve m_AlphaCurve_TitleMovie;
	/// <summary>
	/// �^�C�g��UI�̃O���[�v
	/// </summary>
	[SerializeField] protected CanvasGroup m_CanvasGroup_Title;
	/// <summary>
	/// �Q�[���N��UI
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
			// �� ������
			case STEP_INIT: {
				m_Timer = new Timer();

				SetAlpha(m_CanvasGroup_TitleMovie, 0); 
				SetAlpha(m_CanvasGroup_Title, 0);
				SetVisible(m_PushCommamdUI, false);

				m_Timer.Start(1);
				NextState();
			} break;
			// �� �^�C�g�����[�r�[�̏���
			case STEP_READY_MOVIE: {
				if (m_Timer.Update() == false) { break; }

				SetAlpha(m_CanvasGroup_TitleMovie, 1);
				m_VideoPlayer.Play();
				NextState();
			} break;
			// �� �^�C�g�����[�r�[���Đ�����
			case STEP_PLAY_MOVIE: {
				var rate = (m_VideoPlayer.time / m_VideoPlayer.clip.length);
				if (rate >= 0.8f) { //�Đ����������玟��
					m_Timer.Start(0.5f);
					NextState();
				}
			} break;
			// �� �^�C�g�����[�r�[�̑ގ�
			case STEP_EXIT_MOVIE: {
				bool end = m_Timer.Update();
				float rate = m_Timer.progress;
				SetAlpha(m_CanvasGroup_TitleMovie, m_AlphaCurve_TitleMovie.Evaluate(1 - rate));
				if (end) {
					m_Timer.Start(1);
					NextState();
				}
			} break;
			// �� �^�C�g����ʂ̕\��
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
			// �� �J�n�R�}���h��҂�
			case STEP_WAIT_COMMAMD: {
				//�����A�X�y�[�X�L�[�������ꂽ��Ȃ�
				if (InputManager.instance.IsGameStart()) {
					AudioManager.instance.StopBGM(m_AudioPkayBack_BGM); //BGM���~
					AudioManager.instance.PlaySE(ESe.OpenLetter);		//SE���Đ�
					m_Letteranim.SetBool("LetterBool", true);			//�莆���J��
					m_Timer.Start(1.5f);								//�V�[���̑J�ڂ܂ŏ����҂�
					NextState();
				}
			} break;
			//���̃V�[����
			case STEP_NEXT_SCENE: {
				if (m_Timer.Update() == false) { break; }

				GameSceneManager.instance.NextScene(EScene.GameMain);
				EndState();
			} break;
		}
	}

	/// <summary>
	/// ���̃X�e�[�g��
	/// </summary>
	protected void NextState() { m_SubState++; }
	/// <summary>
	/// �X�e�[�g�I��
	/// </summary>
	protected void EndState() { m_SubState = -1; }
	/// <summary>
	/// �w��L�����o�X�̓����x��ݒ肷��
	/// </summary>
	protected void SetAlpha(CanvasGroup _cg, float _alpha) { _cg.alpha = _alpha; }

	protected void SetVisible(Graphic _graphic, bool _visible) { _graphic.gameObject.SetActive(_visible); }

}