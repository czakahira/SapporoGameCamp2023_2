using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SEのデータ
/// </summary>
[Serializable]
public struct SEData
{
	public ESe label;
	public AudioClip clip;
	public float volume;
}

[CreateAssetMenu(fileName = "SEDatas", menuName = "ScriptableObjects/SEDatas", order = 1)]
[Serializable]
public class SEDatas : ScriptableObject
{
	public SEData[] dats;
}
