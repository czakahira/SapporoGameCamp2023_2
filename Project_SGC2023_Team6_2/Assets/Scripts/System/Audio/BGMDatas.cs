using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// BGMのデータ
/// </summary>
[Serializable]
public struct BGMData
{
	public EBGM label;
	public AudioClip clip;
	public float volume;
}

[CreateAssetMenu(fileName = "BGMDatas", menuName = "ScriptableObjects/BGMDatas", order = 1)]
[Serializable]
public class BGMDatas : ScriptableObject
{
	public BGMData[] dats;
}
