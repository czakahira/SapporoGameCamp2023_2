using System;
using System.Collections.Generic;
using System.Linq;
using UnityEx;

/// <summary>
/// 登録したCoroutineUtilを更新するManager
/// </summary>
public class CoroutineUtilManager : SingletonBehaviour<CoroutineUtilManager>
{
    #region Field
    //更新するCoroutineUtilリスト
    private List<CoroutineUtil> m_CoroutineUtils;
    #endregion

    #region MonoBehaviour
    #endregion

    #region Public
    /// <summary>
    /// 更新するコルーチンの追加
    /// </summary>
    public void Add(CoroutineUtil coroutine)
    {
        //リストに被ってなければ登録
        if (!m_CoroutineUtils.Contains(coroutine)) {
            m_CoroutineUtils.Add(coroutine);
        }
    }
    /// <summary>
    /// 外部からの指定したコルーチンの終了
    /// </summary>
    public void End(CoroutineUtil coroutine)
    {
        if (Contains(coroutine)) {
            coroutine.End();
        }
    }
    public bool Contains(CoroutineUtil coroutine)
    {
        return m_CoroutineUtils.Contains(coroutine);
    }
    #endregion

}

/// <summary>
/// コルーチン
/// </summary>
public class CoroutineUtil
{
    public enum ETime
    {
        Total,
        Action,
    }

    #region Field
    private bool m_SelfUpdate;
    private bool m_IsStop;          //更新停止フラグ(終了ではない)
    private float m_DeltaTime;      //前→現フレーム更新に掛かった時間
    private float m_TotalTime;      //このコルーチンの経過時間
    private float m_ActionTime;     //渡されたActoinの経過時間
    

    private List<Action> m_PrevActions; 

    #endregion

    #region Priperty
    public bool IsStop { get { return m_IsStop; } }
    public bool IsEnd { get; private set; }
    public Action m_CurrentAction { get { return m_PrevActions != null ? m_PrevActions.Last() : null; } }
    #endregion

    #region
    #endregion

    public CoroutineUtil()
    {

    }
    /// <summary> 初期化 </summary>
    /// <param name="selfUpdate">更新をManagerに任せるか</param>
    public void Initialize(float deltaTime, bool selfUpdate = false)
    {
        m_DeltaTime = deltaTime;
        IsEnd = false;
        m_PrevActions = new List<Action>();
        //フラグによってManagerに追加する
        if (!selfUpdate) {
            CoroutineUtilManager.instance.Add(this);
        }
        m_SelfUpdate = selfUpdate;
    }

    public void Destroy()
    {
        m_DeltaTime =
         m_TotalTime =
          m_ActionTime = 0.0f;
        if (m_PrevActions != null)
        {
            m_PrevActions.Clear();
            m_PrevActions = null;
        }
    }

    public void Update()
    {
        if(IsEnd) { return; }
        m_TotalTime += m_DeltaTime;
        if (m_IsStop) { return; }
        m_ActionTime += m_DeltaTime;
        Util.SafeInvoke(m_CurrentAction);
    }

    public void ChangeNextAction(Action next)
    {
        m_PrevActions.Add(next);
        m_ActionTime = 0.0f;
    }
    public void ChangeNextAction(Action next, float timelate)
    {
        if (IsElapsed(timelate)) {
            ChangeNextAction(next);
        }
    }

    /// <summary> 終了通知 </summary>
    public void End()
    {
        ChangeNextAction((Action)null);
        Destroy();
        IsEnd = true;
    }

    public bool IsElapsed(float time, ETime type = ETime.Action)
    {
        float elapsed = type == ETime.Action ? m_ActionTime : m_TotalTime;
        return elapsed >= time;
    }
    public float GetTime(ETime type = ETime.Action)
    {
        float result = type == ETime.Action ? m_ActionTime : m_TotalTime;
        return result;
    }
    public void SetStop(bool trigger)
    {
        m_IsStop = trigger;
    }
}
