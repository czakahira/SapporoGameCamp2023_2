using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class DirectController
{
    [SerializeField] private bool m_Loop;
    [SerializeField] private float m_Speed;

    private List<DirectElement> m_DirectList;

    private float m_DirectElapsedTime; //演出の経過時間
    private float m_DirectDuration; //演出の期間

    public int currentDirectIndex { get; private set; }
    public DirectElement currentDirect { get; private set; }

    public bool loop { get { return m_Loop; } set { m_Loop = value; } }
    public bool pause { get; set; }

    public float speed
    {
        get { return m_Speed; }
        set { m_Speed = value;
            foreach(var dir in m_DirectList) {
                dir.SetDuration(m_Speed);
            }
        }
    }

    
    public DirectController()
    {
        currentDirectIndex = 0;
        m_DirectList = new List<DirectElement>();
        m_DirectElapsedTime = 0.0f;

        pause = true;
    }
    ~DirectController()
    {

    }
    
    public void Initialize()
    {
        speed = 1;
    }
    public void Update(float delta)
    {
        if(pause) { return; }

        if(currentDirect != null) { currentDirect.OnUpdate(); }

        m_DirectElapsedTime += delta;

        if(m_DirectElapsedTime >= m_DirectDuration) {
            currentDirectIndex++;
            if (currentDirectIndex >= m_DirectList.Count) {
                if(m_Loop) {
                    currentDirectIndex = 0;
                }
                else {
                    currentDirectIndex--;currentDirect.OnExit();
                    pause = true;
                    return;
                }
            }
            NextDirect(currentDirectIndex);
        }

    }

    public bool AddDirect(DirectElement element)
    {
        if(GetDirect(element.key) != null) { return false; }

        m_DirectList.Add(element);
        return true;
    }
    public DirectElement GetDirect(int index)
    {
        DirectElement ret = null;
        if(index <= m_DirectList.Count) { ret = m_DirectList[index]; }
        return ret;
    }
    public DirectElement GetDirect(uint key)
    {
        DirectElement ret = null;
        foreach (var dir in m_DirectList) {
            if (dir.key == key) { ret = dir; }
        }
        return ret;
    }

    public void NextDirect(int index)
    {
        if(currentDirect != null) { currentDirect.OnExit(); }
        currentDirect = m_DirectList[index];
        currentDirect.OnEnter();
        m_DirectDuration = GetToNextDuration(index);
    }

    public float GetToNextDuration(int index)
    {
        float ret = 0.0f;
        for(int i = 0; i < m_DirectList.Count; i++) {
            if(i <= index) {
                ret += m_DirectList[i].duration;
            }
        }
        return ret;
    }

    public void StartDirect(int index = 0)
    {
        currentDirectIndex = index;
        NextDirect(index);
        m_DirectDuration = GetToNextDuration(index);
        m_DirectElapsedTime = 0.0f;

        if(index > 0) { m_DirectElapsedTime = GetToNextDuration(index - 1); }

        pause = false;
    }

}
[SerializeField]
public class DirectElement
{
    public enum DirectElement_State
    {
        Enter,
        Update,
        Exit,
    }

    [SerializeField]
    protected float m_DefaultDuration; //演出の期間(標準) 

    public uint key { get; private set; }
    public float duration { get; private set; }
    public event Action<DirectElement_State> callback; 

    public DirectElement(uint key, float defaultDuration)
    {
        this.key = key;
        m_DefaultDuration = defaultDuration;
    }

    public void SetDuration(float speed)
    {
        duration = m_DefaultDuration / speed;
    }

    public virtual void OnEnter() { callback?.Invoke(DirectElement_State.Enter); }
    public virtual void OnUpdate() { callback?.Invoke(DirectElement_State.Update); }
    public virtual void OnExit() { callback?.Invoke(DirectElement_State.Exit); }
}
