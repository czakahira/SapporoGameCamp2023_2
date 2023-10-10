using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SEW関係の処理置き場
/// </summary>
public partial class AudioManager
{
	protected void Initialize_SE()
	{
		int def = 10; //初期数

		m_SePlayers = new List<AudioPlayer_SE>(def);
		m_SePlayerCount = 0;

		for (int i = 0; i < def; i++) {
			AddSePlayer();
		}
	}
	protected void Update_SE()
	{
		if (m_SePlayerCount > 0) {
			foreach (var ply in m_SePlayers) {
				ply.SelfUpdate();
			}
		}
	}
	protected void Destroy_SE()
	{
		if (m_SePlayerCount > 0) {
			foreach (var ply in m_SePlayers) {
				if (ply.isPlaying) { ply.Stop(); }
				ply.SelfDestroy();
			}
		}
		m_SePlayers.Clear();
		m_SePlayers = null;
		m_SePlayerCount--;
	}

	/// <summary>
	/// BGM再生者を新規作成
	/// </summary>
	public AudioPlayer_SE CreateSePlayer()
	{
		var player = CreatePlayer<AudioPlayer_SE>(m_Parent_SE);
		return player;
	}
	/// <summary>
	/// BGM再生者を追加
	/// </summary>
	protected AudioPlayer_SE AddSePlayer()
	{
		var player = CreateSePlayer();
		m_SePlayers.Add(player);
		m_SePlayerCount++;

		return player;
	}
	/// <summary>
	/// 指定BGMのデータを取得する
	/// </summary>
	public SEData GetSeData(ESe _se)
	{
		foreach (var data in m_SeDats.dats) {
			if (data.label == _se) {
				return data;
			}
		}
		return default;
	}
	/// <summary>
	/// BGMを再生する
	/// </summary>
	public bool PlaySE(ESe _se)
	{
		AudioPlayer_SE player = null;
		foreach (var ply in m_SePlayers) {
			if (ply.isUsing == false) {
				player = ply;
				break;
			}
		}
		if (player == null) {
			player = AddSePlayer();
		}

		player.Play(GetSeData(_se));

		return true;
	}
}


[Serializable]
public class AudioPlayer_SE : AudioPlayer_Base
{
	public override EAudioType type => EAudioType.SE;

	public SEData data { get; protected set; }

	public void Play(SEData _data)
	{
		data = _data;
		m_Source.clip = data.clip;
		volume = data.volume;
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

	public override void SelfUpdate()
	{
		base.SelfUpdate();

		if (isPlaying) {
			float ratio = (m_Source.time / m_Source.clip.length);
			if (ratio > 1) {
				Stop();
			}
		}
	}

}