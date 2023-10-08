using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

using UnityEx;

/// <summary>
/// シーンマネージャー
/// <para> ・シーンの遷移を行う </para>
/// </summary>
public class SceneManagerEx : SystemSingleton<SceneManagerEx>
{
	public EScene currentActivveSceneLabel { get; protected set; } = EScene.Boot;

	/// <summary>
	/// 初期化
	/// </summary>
	protected override void Initialize()
	{
		base.Initialize();

	}
	protected override void SelfUpdate()
	{
		base.SelfUpdate();

	}
	protected override void SelfDestroy()
	{
		base.SelfDestroy();
	}

	
	/// <summary>
	/// シーンをロードします
	/// </summary>
	public async UniTask LoadSceneAsync(string _scene, UnityAction<SceneInstance> _callback = null, LoadSceneMode _mode = LoadSceneMode.Single, bool _activeOnLoad = true, int _prioprity = 100)
	{
		var handle = Addressables.LoadSceneAsync(_scene, _mode, _activeOnLoad, _prioprity);
		await handle;

		SceneInstance si = handle.Result;
		if (_activeOnLoad) { //ロードしたシーンをアクティブシーンにする
			await si.ActivateAsync();
		}

		_callback?.Invoke(si);
	}
	/// <summary>
	/// シーンをロードします
	/// <para> ※ ロードしたシーンへの遷移はコールバックから自前で行う必要があります </para>
	/// </summary>
	public async UniTask LoadSceneAsync(EScene _label, UnityAction<SceneInstance> _callback = null, LoadSceneMode _mode = LoadSceneMode.Single, bool _activeOnLoad = true, int _prioprity = 100)
	{
		string name = SceneLibrary.GetSceneLoadPath(_label);
		await LoadSceneAsync(name, _callback, _mode, _activeOnLoad, _prioprity);
	}
	/// <summary>
	/// ロードシーン完了後、即アクティブ化します
	/// </summary>
	/// <param name="_label"></param>
	/// <param name="_callback"></param>
	/// <param name="_mode"></param>
	/// <param name="_activeOnLoad"></param>
	/// <param name="_prioprity"></param>
	public async UniTask LoadActiveSceneAsync(EScene _label, UnityAction<SceneInstance> _callback = null, LoadSceneMode _mode = LoadSceneMode.Single, int _prioprity = 100)
	{
		_callback += SetActiveOnLoad;
		await LoadSceneAsync(_label, _callback, _mode, true, _prioprity);
	}

	/// <summary>
	/// シーンをアンロードする
	/// </summary>
	public async void UnLoadSceneAsync(SceneInstance _unLoadSceneInstance)
	{
		var handle = Addressables.UnloadSceneAsync(_unLoadSceneInstance);
		await handle;
	}

	/// <summary>
	/// シーンインスタンスからカレントシーンにアクティブ化する
	/// </summary>
	protected void SetActiveOnLoad(SceneInstance _sceneInstance)
	{
		SceneManager.SetActiveScene(_sceneInstance.Scene);
	}

}