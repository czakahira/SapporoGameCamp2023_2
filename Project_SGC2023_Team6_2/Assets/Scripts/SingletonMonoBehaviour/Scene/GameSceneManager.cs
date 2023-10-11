using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

/// <summary>
/// フェードしながらシーンを切り替えるマネージャー
/// </summary>
public class GameSceneManager : SystemSingleton<GameSceneManager>
{
	/// <summary>
	/// フェードイン・アウトに使用するイメージ
	/// </summary>
	[SerializeField] protected Image m_FadeImage;
	/// <summary>
	/// フェードカラー
	/// </summary>
	[SerializeField] protected Color m_FadeColor = Color.white;
	/// <summary>
	/// フェードカーブ
	/// </summary>
	[SerializeField] protected AnimationCurve m_FadeCurve;
	/// <summary>
	/// キャンバスグループ
	/// </summary>
	[SerializeField] protected CanvasGroup m_Group;
	/// <summary>
	/// シーン遷移中
	/// </summary>
	public bool isLoading { get; protected set; }

	protected override void Initialize()
	{
		base.Initialize();
	}

	/// <summary>
	/// 暗転
	/// </summary>
	public async UniTask FadeIn()
	{
		float t = 0;
		float duration = 1; //フェードを終える期間
		while (t < 1) {
			t += TimeManager.rootDeltaTime;
			float rate = (t/duration);
			m_FadeColor.a = m_FadeCurve.Evaluate(rate);
			m_FadeImage.color = m_FadeColor;
			await UniTask.Yield(PlayerLoopTiming.Update);
		}
		m_FadeColor.a = 1;
		m_FadeImage.color = m_FadeColor;
	}
	/// <summary>
	/// 明転
	/// </summary>
	public async UniTask FadeOut()
	{
		float t = 1;
		float duration = 1; //フェードを終える期間
		while (t > 0) {
			t -= TimeManager.rootDeltaTime;
			float rate = (t / duration);
			m_FadeColor.a = m_FadeCurve.Evaluate(rate);
			m_FadeImage.color = m_FadeColor;
			await UniTask.Yield(PlayerLoopTiming.Update);
		}
		m_FadeColor.a = 0;
		m_FadeImage.color = m_FadeColor;
	}

	/// <summary>
	/// 次のシーンへ
	/// </summary>
	public async void NextScene(EScene _scene, UnityAction _onLoaded = null)
	{
		isLoading = true;

		await FadeIn();
		await SceneManagerEx.instance.LoadActiveSceneAsync(_scene);
		await FadeOut();

		isLoading = false;

		_onLoaded?.Invoke();
	}

}
