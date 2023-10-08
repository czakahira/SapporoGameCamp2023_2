
using UnityEngine;
using System.Diagnostics;
using UnityEx;


public static class DebugEX
{
	private static string warning = $"<color={ColorEX.gold.ToCode()}>▲ Warning: </color>";
	private static string error   = $"<color={ColorEX.red.ToCode()}>● Error: </color>";

	private static ILogger m_Logger = DefaultLogger;
	public static ILogger Loger { get { return m_Logger; } }
	public static ILogger DefaultLogger
	{
		get { return UnityEngine.Debug.unityLogger; }
	}

	/// <summary>
	/// エディタ実行を停止します
	/// </summary>
	public static void Break()
	{
		UnityEngine.Debug.Break();
	}
	
	public static void Assert(bool condition)
	{
		if(!condition) { LogError("Assert Error"); }
	}

	public static void Log(object mes)
	{
		m_Logger.Log(LogType.Log, mes);
	}
	public static void LogFormat(string format, params object[] args)
	{
		m_Logger.LogFormat(LogType.Log,format, args);
	}
	public static void LogWarning(object mes)
	{
		m_Logger.Log(LogType.Warning, warning + mes);
	}
	public static void LogWarningFormat(string format, params object[] args)
	{
		m_Logger.Log(LogType.Warning, warning + format, args);
	}
	public static void LogError(object mes)
	{
		m_Logger.Log(LogType.Error, error + mes);
	}
	public static void LogErrorFormat(string format, params object[] args)
	{
		m_Logger.Log(LogType.Error, error + format, args);
	}
	public static void LogColored(object message, string code)
	{
		Log($"<color={code}>{message}</color>");
	}
	public static void LogColored(object mes, Color color)
	{
		LogColored(mes, color.ToCode());
	}


	/// <summary>
	/// 開始位置と終了位置の間にラインを描画します
	/// </summary>
	/// <param name="start">ラインの開始位置(ワールド座標)</param>
	/// <param name="end">ラインの終了位置(ワールド座標)</param>
	/// <param name="color">ラインの色</param>
	/// <param name="duration">ラインを表示する時間(秒単位)</param>
	/// <param name="depthTest">ラインがカメラから近いオブジェクトによって隠れた場合、ラインを隠すか</param>
	public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.0f, bool depthTest = true)
	{
		UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
	}
	
	/// <summary>
	/// 開始地点から(開始地点＋方向)までレイを描画します。
	/// </summary>
	/// <param name="start">レイを開始地点(ワールド座標)</param>
	/// <param name="direction">レイの方向</param>
	/// <param name="distance">レイの長さ</param>
	/// <param name="color">レイの色</param>
	/// <param name="duration">レイを表示する時間(秒単位)</param>
	/// <param name="depthTest">レイがカメラから近いオブジェクトによって隠れた場合、レイを隠すか</param>
	public static void DrawRay(Vector3 start, Vector3 direction, float distance, Color color, float duration = 0.0f, bool depthTest = true)
	{
		UnityEngine.Debug.DrawRay(start, direction.normalized * distance, color, duration, depthTest);
	}

}
