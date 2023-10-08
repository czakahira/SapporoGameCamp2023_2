using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using Cysharp.Threading.Tasks;

/// <summary>
/// Addressable用クラス
/// <param>
/// このクラス使用にはPaccageManagerより「Addressable」をインポートする必要があります。
/// 
/// </param>
/// </summary>
public class AddresableManager : SystemSingleton<AddresableManager>
{
	/// <summary>
	/// キャンセルトークン
	/// </summary>
	public CancellationToken cancellation { get; protected set; }

	/// <summary>
	/// 初期化
	/// </summary>
	protected override void Initialize()
	{
		base.Initialize();

		//キャンセルトークン作成
		cancellation = this.GetCancellationTokenOnDestroy();
	}

#if false

	#region Member
	/// <summary>
	/// 標準で用意されているローダー数
	/// </summary>
	protected static uint g_DefaultLaderCount = 10;

	/// <summary>
	/// 管理しているローダー
	/// </summary>
	protected List<AddressableLoader> m_EnqueueLoader;

	/// <summary>
	/// ロード済み（解放待ち）のアセット情報
	/// </summary>
	protected List<AsyncLoadedInfo> m_LoadedInfos;

	#endregion

	/// <summary>
	/// ローダーを取得する
	/// <para>
	/// ※取得したローダーは使用完了後に解放する必要があります
	/// </para>
	/// </summary>
	public AddressableLoader GetLoader()
	{
		//まだアクティブになっていないローダーを取得します
		foreach(var loader in m_EnqueueLoader){
			if(!loader.active){
				loader.Access(); //アクセスします
				return loader;
			}
		}
		//管理済みの中に使えるローダーが無ければ、新しく作りそれを返します
		var newLoader = AddLoader();
		newLoader.Access();
		return newLoader;
	}
	/// <summary>
	/// 指定ローダーを解放する
	/// <para>
	/// ・使用完了したローダーはこの関数を使用して解放する必要があります
	/// </para>
	/// </summary>
	/// <param name="_loader"> 解放するローダー </param>
	public void ReleaseLoader(ref AddressableLoader _loader)
	{
		_loader.Release();	//ローダー側の解放処理を呼ぶ
		_loader = null;		//nullを渡す
	}

	/// <summary>
	/// 初期化
	/// </summary>
	protected override void Initialize()
	{
		m_EnqueueLoader = new List<AddressableLoader>();
		m_LoadedInfos = new List<AsyncLoadedInfo>();

		for (int i = 0; i < g_DefaultLaderCount; i++) {
			AddLoader(); //ローダーを作成
		}

		base.Initialize();
	}
	/// <summary>
	/// ローダーを追加する
	/// </summary>
	/// <param name="_loader"> 追加され出力されたローダー </param>
	protected AddressableLoader AddLoader()
	{
		AddressableLoader loader = g_Object.AddComponent<AddressableLoader>();
		loader.Init();
		m_EnqueueLoader.Add(loader);
		return loader;
	}

	/// <summary>
	/// ロードが完了したアセット情報を登録する
	/// </summary>
	/// <param name="_key"> アセットのキー </param>
	/// <param name="_handle"> アセットのハンドル </param>
	public void AddInfo(object _key, AsyncOperationHandle _handle)
	{
		//登録済みのアセットは追加しない
		foreach(var info in m_LoadedInfos){
			if(info.key.GetType() == _key.GetType()) {
				if (info.key == _key) {
					return;
				}
			}
		}
		//未追加だったので追加する
		m_LoadedInfos.Add(new AsyncLoadedInfo(_key, _handle));
	}

	/// <summary>
	/// 指定キーのアセットがすでにロード済みか
	/// </summary>
	/// <param name="_key"></param>
	/// <param name="_output"> ロード情報の返り値 </param>
	/// <returns> TURE = アセットが見つかった </returns>
	public bool LoadedAssets(object _key, out AsyncOperationHandle _output)
	{	
		_output = new AsyncOperationHandle();           //出力先を初期化
		foreach (var info in m_LoadedInfos) {
			if (info.key.GetType() == _key.GetType()) { //型が同一である
				if (info.key == _key) {					//同じキーである
					_output = info.handle;				//出力する
					return true;
				}
			}
		}
		return false;
	}

#endif

}
