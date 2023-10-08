#if UNITY_EDITOR && INIDEF_DEV_MODE && !INIDEF_SUBMISSION
#define DEV_MODE
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;
using Unity.Collections;

public class CastCommamder
{	
	protected NativeArray<Vector3> m_Commamd_Origins;
	protected NativeArray<Vector3> m_Commamd_Directions;
	protected NativeArray<float> m_Commamd_Distances;
	protected NativeArray<LayerMask> m_Commamd_LayerMasks;
	/// <summary>
	/// ヒット結果コレクション
	/// </summary>
	protected NativeArray<RaycastHit> m_CommamdResultBuffers;
	/// <summary>
	/// コレクションの所持数
	/// </summary>
	protected int m_CommamdCount = 0;

	public CastCommamder()
	{
		
	}

	public virtual void SelfAwake()
	{
		
	}
	public virtual void SelfDestroy()
	{
		ReleaseCollections();
	}

	/// <summary>
	/// コマンドを実行する
	/// </summary>
	public virtual bool Run()
	{
		return false;
	}

	/// <summary>
	/// コレクションを取得する
	/// </summary>
	protected virtual void GetCollections(int _capacity)
	{
		m_CommamdCount = _capacity;
		m_Commamd_Origins = new NativeArray<Vector3>(m_CommamdCount, Allocator.Persistent);
		m_Commamd_Directions = new NativeArray<Vector3>(m_CommamdCount, Allocator.Persistent);
		m_Commamd_Distances = new NativeArray<float>(m_CommamdCount, Allocator.Persistent);
		m_Commamd_LayerMasks = new NativeArray<LayerMask>(m_CommamdCount, Allocator.Persistent);
		m_CommamdResultBuffers = new NativeArray<RaycastHit>(m_CommamdCount, Allocator.Persistent);
	}
	/// <summary>
	/// コレクションを解放する
	/// </summary>
	protected virtual void ReleaseCollections()
	{
		if (m_CommamdCount <= 0) { return; }

		m_CommamdResultBuffers.Dispose();
		m_Commamd_Origins.Dispose();
		m_Commamd_Directions.Dispose();
		m_Commamd_Distances.Dispose();
		m_Commamd_LayerMasks.Dispose();
		m_CommamdCount = 0;
	}

}
