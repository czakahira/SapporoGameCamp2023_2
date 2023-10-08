using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary
{

}


public enum EAudioType
{
	None = -1,

	SE,
	BGM,
}

/// <summary>
/// BGMのラベル
/// </summary>
public enum EBGM
{
	Title1, //エレクトリカル
	Title2,	//ザ怪盗

	GameMain,
}
/// <summary>
/// SEのラベル
/// </summary>
public enum ESe
{
	OpenLetter,
	
	GameClear,
	GameOver,
}