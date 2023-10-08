#if UNITY_EDITOR && INIDEF_DEV_MODE && !INIDEF_SUBMISSION
#define DEV_MODE
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;
using Unity.Collections;

public static class CastCommamdLibrary
{

}

public class CastInfo
{
	/// <summary>
	/// 起点
	/// </summary>
	public Vector3 origin;
	/// <summary>
	/// 方向（実行時に正規化されます）
	/// </summary>
	public Vector3 direction;
	/// <summary>
	/// 距離
	/// </summary>
	public float distance;
	/// <summary>
	/// 検知するレイヤー
	/// </summary>
	public LayerMask layerMask;
	/// <summary>
	/// ヒット結果（実行後に格納されます）
	/// </summary>
	public RaycastHit hit;

	/// <summary>
	/// ヒットしたか
	/// </summary>
	public bool isHit { get { return (hit.collider != null); } }

	public CastInfo() { }
	public CastInfo(Vector3 _origin, Vector3 _direction, float _distance, LayerMask _layer)
	{
		SetParameter(_origin, _direction, _distance, _layer);
	}

	public void SetParameter(Vector3 _origin, Vector3 _direction, float _distance, LayerMask _layer)
	{
		origin = _origin;
		direction = _direction;
		distance = _distance;
		layerMask = _layer;
	}

}
/// <summary>
/// 線キャスト用データ
/// </summary>
public class RaycastInfo : CastInfo
{

	public RaycastInfo() { }
	public RaycastInfo(Vector3 _origin, Vector3 _direction, float _distance, LayerMask _layer) : base(_origin, _direction, _distance, _layer)
	{
		
	}
}
/// <summary>
/// 球キャスト用データ
/// </summary>
public class SpherecastInfo : RaycastInfo
{
	/// <summary>
	/// 半径
	/// </summary>
	public float radius;

	public SpherecastInfo() { }
	public SpherecastInfo(Vector3 _origin, float _radius, Vector3 _direction, float _distance, LayerMask _layer) : base(_origin, _direction, _distance, _layer)
	{
		SetParameter(_origin, _radius, _direction, _distance, _layer);
	}

	public void SetParameter(Vector3 _origin, float _radius, Vector3 _direction, float _distance, LayerMask _layer)
	{
		base.SetParameter(_origin, _direction, _distance, _layer);
		radius = _radius;
	}
}
