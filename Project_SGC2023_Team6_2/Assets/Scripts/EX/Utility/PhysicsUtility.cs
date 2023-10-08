using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public class PhysicsUtility
{
	/// <summary>
	/// JobによるRaycast
	/// </summary>
	/// <param name="_origin"></param>
	/// <param name="_direction"></param>
	/// <param name="_hit"></param>
	/// <param name="_distance"></param>
	/// <param name="_layerMask"></param>
	/// <param name="_maxHits"></param>
	/// <returns></returns>
	public static bool RaycastCmd(Vector3 _origin, Vector3 _direction, out RaycastHit _hit, float _distance = float.MaxValue, int _layerMask = -5, int _maxHits = 1)
	{
		int capacity = 1;
		NativeArray<RaycastCommand> cmd = new NativeArray<RaycastCommand>(capacity, Allocator.TempJob);
		NativeArray<RaycastHit> hit = new NativeArray<RaycastHit>(capacity, Allocator.TempJob);

		cmd[0] = new RaycastCommand(_origin, _direction, _distance, _layerMask, _maxHits);
		RaycastCommand.ScheduleBatch(cmd, hit, capacity, default).Complete();

		_hit = hit[0];

		cmd.Dispose();
		hit.Dispose();

		return (_hit.collider != null);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="_origin"></param>
	/// <param name="_direction"></param>
	/// <param name="_maxDistance"></param>
	/// <param name="_layerMask"></param>
	/// <param name="_capacity"></param>
	/// <returns></returns>
	public static RaycastHit[] RaycastCmdAll(Vector3 _origin, Vector3 _direction, float _maxDistance = float.MaxValue, int _layerMask = -5, int _capacity = 100)
	{
		//取得
		NativeArray<RaycastCommand> cmds = new NativeArray<RaycastCommand>(_capacity, Allocator.TempJob);
		NativeArray<RaycastHit> hits = new NativeArray<RaycastHit>(_capacity, Allocator.TempJob);

		//１つのコマンドで衝突できるオブジェクトの数
		int maxSize = 1;
		for (int i = 0; i < _capacity; i++) {
			cmds[i] = new RaycastCommand(_origin, _direction, _maxDistance, _layerMask, maxSize);
		}
		int minPerJob = (_capacity / 10);
		RaycastCommand.ScheduleBatch(cmds, hits, minPerJob, default).Complete();
		//返り値確保
		RaycastHit[] result = hits.ToArray();
		//解放
		cmds.Dispose();
		hits.Dispose();

		return result;
	}


}
