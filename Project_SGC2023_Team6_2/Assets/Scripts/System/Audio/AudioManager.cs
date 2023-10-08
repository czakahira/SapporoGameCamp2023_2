using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音マネージャー
/// </summary>
public partial class AudioManager : SystemSingleton<AudioManager>
{
	// --------------------
	// Member
	// --------------------
	/// <summary>
	/// 始祖のソース
	/// </summary>
	[SerializeField] protected AudioSource m_AudioSourceOrigin;

	[Header("BGM")]
	/// <summary>
	/// BGMプレイヤーの親
	/// </summary>
	[SerializeField] protected Transform m_Parent_BGM;
	/// <summary>
	/// BGMのデータ群
	/// </summary>
	[SerializeField] protected BGMDatas m_BgmDats;
	/// <summary>
	/// BGMの再生者
	/// </summary>
	protected List<AudioPlayer_BGM> m_BgmPlayers;
	/// <summary>
	/// BGM再生者の数
	/// </summary>
	[SerializeField] protected int m_BgmPlayerCount;

	[Header("SE")]
	/// <summary>
	/// SEプレイヤーの親
	/// </summary>
	[SerializeField] protected Transform m_Parent_SE;
	/// <summary>
	/// SEのデータ群
	/// </summary>
	[SerializeField] protected SEDatas m_SeDats;
	/// <summary>
	/// SE再生者
	/// </summary>
	protected List<AudioPlayer_SE> m_SePlayers;
	/// <summary>
	/// SE再生者の数
	/// </summary>
	[SerializeField] protected int m_SePlayerCount;


	// --------------------
	// Method
	// --------------------
	/// <summary>
	/// ソースを作成する
	/// </summary>
	public T CreatePlayer<T>(Transform _parent = null) where T : AudioPlayer_Base, new ()
	{
		T audio = new T();
		var source = AudioSource.Instantiate(m_AudioSourceOrigin);
		if (_parent != null) { source.transform.SetParent(_parent); }
		audio.Initialize(source);

		return audio;
	}

	protected override void Initialize()
	{
		base.Initialize();

		DontDestroyOnLoad(gameObject);

		Initialize_SE();
		Initialize_BGM();
	}
	protected override void SelfUpdate()
	{
		base.SelfUpdate();

		Update_SE();
		Update_BGM();
	}
	protected override void SelfDestroy()
	{
		base.SelfDestroy();

		Destroy_SE();
		Destroy_BGM();
	}

}

/// <summary>
/// 音源プレイヤーの基底クラス
/// </summary>
[Serializable]
public class AudioPlayer_Base
{
	/// <summary>
	/// 音源
	/// </summary>
	[SerializeField] protected AudioSource m_Source;
	/// <summary>
	/// 標準音量
	/// </summary>
	[SerializeField] protected float m_Volume = 1;
	/// <summary>
	/// 音量の倍率
	/// </summary>
	[SerializeField] protected float m_VolumeScale = 1;

	public virtual EAudioType type { get; protected set; } = EAudioType.None;
	/// <summary>
	/// 再生中
	/// </summary>
	public bool isPlaying { get; protected set; } = false;
	/// <summary>
	/// 使用中
	/// </summary>
	public bool isUsing { get; protected set; }
	/// <summary>
	/// 音量
	/// </summary>
	public float volume
	{
		get { return m_Volume; }
		set {
			if (m_Volume == value) { return; }
			m_Volume = value;
			UpdateVolume();
		}
	}
	public float volumeScale 
	{
		get { return m_VolumeScale; }
		set {
			if (m_VolumeScale == value) { return; }
			m_VolumeScale = value;
			UpdateVolume();
		}
	}

	public AudioPlayer_Base()
	{
		
	}
	public virtual void Initialize(AudioSource _source)
	{
		m_Source = _source;

		isPlaying = false;
		isUsing = false;
	}

	public virtual void SelfUpdate() { }
	public virtual void SelfDestroy() 
	{

		if (m_Source != null) {
			AudioSource.Destroy(m_Source);
			m_Source = null;
		}
	}

	public virtual void Play()
	{
		m_Source.Play();
	}
	public virtual void Stop()
	{
		m_Source.Stop();
	}

	protected void UpdateVolume() { m_Source.volume = m_Volume * m_VolumeScale; }

}

