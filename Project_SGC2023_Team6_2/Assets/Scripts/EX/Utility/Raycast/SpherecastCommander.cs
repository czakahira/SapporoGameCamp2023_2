#if UNITY_EDITOR && INIDEF_DEV_MODE && !INIDEF_SUBMISSION
#define DEV_MODE
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;

/// <summary>
/// 球キャスト用コマンダー
/// </summary>
public class SpherecastCommander : CastCommamder
{
	// --------------------
	// Member
	// --------------------
	#region Open
	/// <summary>
	/// 登録しているキャストが実行されます
	/// </summary>
	protected List<SpherecastInfo> m_Infos;
	protected int m_InfoCount;
	/// <summary>
	/// コマンド
	/// </summary>
	protected NativeArray<SpherecastCommand> m_Commands;
	/// <summary>
	/// コマンドパラメータ：半径
	/// </summary>
	protected NativeArray<float> m_Command_Radius;
	#endregion

	// --------------------
	// Method
	// --------------------
	#region Open
	public SpherecastCommander()
	{
		m_Infos = new List<SpherecastInfo>();
		m_InfoCount = 0;
	}
	public override void SelfDestroy()
	{
		base.SelfDestroy();

		if (m_InfoCount > 0) {
			m_Infos.Clear();
			m_Infos = null;
		}
	}

	protected override void GetCollections(int _capacity)
	{
		base.GetCollections(_capacity);
		m_Commands = new NativeArray<SpherecastCommand>(m_CommamdCount, Allocator.Persistent);
		m_Command_Radius = new NativeArray<float>(m_CommamdCount, Allocator.Persistent);
	}
	protected override void ReleaseCollections()
	{
		if (m_CommamdCount <= 0) { return; }

		m_Commands.Dispose();
		m_Command_Radius.Dispose();
		base.ReleaseCollections();
	}
	/// <summary>
	/// コマンドを実行する
	/// </summary>
	/// <returns> 実行したコマンドの内１つでもヒットしていたらTRUEを返す </returns>
	public override bool Run()
	{
		//実行できないので返す
		if (m_InfoCount <= 0) { return false; }

		base.Run();

		//リクエスト数と、コレクション数が一致しなければ、コレクションを再取得する
		if (m_InfoCount != m_CommamdCount) {
			ReleaseCollections();
			GetCollections(m_InfoCount);
		}
		//コマンドを作成する
		for (int i = 0; i < m_CommamdCount; i++) {
			m_Commamd_Origins[i] = m_Infos[i].origin;
			m_Command_Radius[i] = m_Infos[i].radius;
			m_Commamd_Directions[i] = m_Infos[i].direction.normalized;
			m_Commamd_Distances[i] = m_Infos[i].distance;
			m_Commamd_LayerMasks[i] = m_Infos[i].layerMask;
		}
		PrepareSpherecastCommamd prepareSpherecast = new PrepareSpherecastCommamd()
		{
			origins = m_Commamd_Origins,
			radius = m_Command_Radius,
			directions = m_Commamd_Directions,
			distances = m_Commamd_Distances,
			layerMasks = m_Commamd_LayerMasks,
			commamds = m_Commands,
		};
		//ジョブを実行する
		var handle = prepareSpherecast.Schedule(m_CommamdCount, 0, SpherecastCommand.ScheduleBatch(m_Commands, m_CommamdResultBuffers, 1));
		//var handle = SpherecastCommand.ScheduleBatch(m_Commands, m_CommamdResultBuffers, 1, default);
		handle.Complete();
		//コマンドの実行結果を渡す
		bool result = false;
		for (int i = 0; i < m_InfoCount; i++) {
			m_Infos[i].hit = m_CommamdResultBuffers[i];
			if (m_Infos[i].isHit) {
				result = true;
			}
		}

		return result;
	}

	/// <summary>
	/// リクエストを作成する
	/// </summary>
	public SpherecastInfo CreateInfo()
	{
		SpherecastInfo newInfo = new SpherecastInfo();
		AddInfo(newInfo);
		return newInfo;
	}
	/// <summary>
	/// リクエストを追加する
	/// </summary>
	public bool AddInfo(SpherecastInfo _info)
	{
		if (m_Infos.Contains(_info) == false) {
			m_Infos.Add(_info);
			m_InfoCount++;
			return true;
		}
		return false;
	}
	/// <summary>
	/// リクエストを除外する
	/// </summary>
	public bool RemeveInfo(SpherecastInfo _info)
	{
		if (m_Infos.Contains(_info) == true) {
			m_Infos.Remove(_info);
			m_InfoCount--;
			return true;
		}
		return false;
	}
	#endregion

	[BurstCompile]
	public struct PrepareSpherecastCommamd : IJobParallelFor
	{
		public NativeArray<Vector3> origins;
		public NativeArray<float> radius;
		public NativeArray<Vector3> directions;
		public NativeArray<float> distances;
		public NativeArray<LayerMask> layerMasks;
		public NativeArray<SpherecastCommand> commamds;

		public void Execute(int _index)
		{
			commamds[_index] = new SpherecastCommand(origins[_index], radius[_index], directions[_index], distances[_index], layerMasks[_index]);
		}
	}
}
