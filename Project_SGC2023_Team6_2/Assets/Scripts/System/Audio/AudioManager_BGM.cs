using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGM関係の処理置き場
/// </summary>
public partial class AudioManager
{
	protected void Initialize_BGM()
	{
		int def = 5; //初期数

		m_BgmPlayers = new List<AudioPlayer_BGM>(def);
		m_BgmPlayerCount = 0;

		for (int i = 0; i < def; i++) {
			AddBgmPlayer();
		}
	}
	protected void Update_BGM()
	{
		if (m_BgmPlayerCount > 0) {
			foreach (var ply in m_BgmPlayers) {
				ply.SelfUpdate();
			}
		}
	}
	protected void Destroy_BGM()
	{
		if (m_BgmPlayerCount > 0) {
			foreach (var ply in m_BgmPlayers) {
				ply.SelfDestroy();
			}
		}
		m_BgmPlayers.Clear();
		m_BgmPlayers = null;
		m_BgmPlayerCount--;
	}

	/// <summary>
	/// BGM再生者を新規作成
	/// </summary>
	public AudioPlayer_BGM CreateBgmPlayer()
	{
		var player = CreatePlayer<AudioPlayer_BGM>(m_Parent_BGM);
		return player;
	}
	/// <summary>
	/// BGM再生者を追加
	/// </summary>
	protected AudioPlayer_BGM AddBgmPlayer()
	{
		var player = CreateBgmPlayer();
		m_BgmPlayers.Add(player);
		m_BgmPlayerCount++;

		return player;
	}
	/// <summary>
	/// 指定BGMのデータを取得する
	/// </summary>
	public BGMData GetBgmData(EBGM _bgm)
	{
		foreach (var data in m_BgmDats.dats) {
			if (data.label == _bgm) {
				return data;
			}
		}
		return default;
	}
	/// <summary>
	/// BGMを再生する
	/// </summary>
	public AudioPlayer_BGM PlayBGM_GetPlayback(EBGM _bgm)
	{
		AudioPlayer_BGM player = null;
		foreach (var ply in m_BgmPlayers) {
			if (ply.isUsing == false) {
				player = ply;
				break;
			}
		}
		if (player == null) {
			player = AddBgmPlayer();
		}

		player.Play(_bgm);

		return player;
	}
	public static AudioPlayer_BGM PlayBGM(EBGM _bgm)
	{
		return instance.PlayBGM_GetPlayback(_bgm);
	}
	/// <summary>
	/// 指定プレイヤーのBGMを止める
	/// </summary>
	public bool StopBGM(AudioPlayer_BGM _playBack)
	{
		if (m_BgmPlayers.Contains(_playBack)) {
			_playBack.Stop();
		}
		return false;
	}

}

[Serializable]
public class AudioPlayer_BGM : AudioPlayer_Base
{
	public override EAudioType type => EAudioType.BGM;

	public BGMData data { get; protected set; }

	public void Play(EBGM _bgm)
	{
		data = AudioManager.instance.GetBgmData(_bgm);
		m_Source.clip = data.clip;
		volume = data.volume;
		m_Source.loop = data.loop;
		Play();

		isPlaying = true;
		isUsing = true;
	}

	public override void Stop()
	{
		base.Stop();

		isPlaying = false;
		isUsing = false;
	}

}
