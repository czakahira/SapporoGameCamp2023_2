using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class UIUtil
{
    /// <summary>
    /// 現在のポインターデータ
    /// </summary>
    public static PointerEventData currenyEventData
    {
        get { return new PointerEventData(EventSystem.current); }
    }

}


/// <summary>
/// UIのサイズ可変クラス
/// <para>
/// ・UIのサイズに親子関係は影響しない為、このクラスを使って疑似親子関係を作る
/// </para>
/// </summary>
[Serializable]
public class UISizeFitter 
{
    [SerializeField]
    private RectTransform m_RectTrs; //リサイズするRect
    [SerializeField]
    private Vector2 m_StandardPos; //標準位置
    [SerializeField]
    private Vector2 m_StandardSize; //標準サイズ

    public UISizeFitter Parent { get; private set; } //紐づけた親
    public bool HasParent { get { return Parent != null; } } //紐づく親がいるフラグ 

    //コンストラクタ
    public UISizeFitter() { }
    public UISizeFitter(RectTransform rect)
    {
        m_RectTrs = rect;
        m_StandardPos = rect.localPosition;
        m_StandardSize = rect.rect.size;
    }
    //デストラクタ
    ~UISizeFitter()
    {
        m_RectTrs = null;
        if(HasParent) { Parent = null; }
    }

    /// <summary>
    /// 各プロパティを設定する
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    public void SetProperty_SelfRect() 
    {
        m_StandardPos = m_RectTrs.localPosition;
        m_StandardSize = m_RectTrs.rect.size;
    }
    public void SetProperty_Rect(RectTransform rect)
    {
        m_RectTrs = rect;
    }
    public void SetProperty_Pos(Vector2 pos)
    {
        m_StandardPos = pos;
    }
    public void SetProperty_Size(Vector2 size)
    {
        m_StandardSize = size;
    }

    /// <summary> 紐づける親を設定します </summary>
    public void SetParent(RectTransform rect)
    {
        if(HasParent) { Parent = null; }
        Parent = new UISizeFitter(rect);
    }
    public void SetParent(RectTransform rect, Vector2 size)
    {
        if (HasParent) { Parent = null; }
        Parent = new UISizeFitter(rect);
        Parent.SetProperty_Size(size);
    }
    public void SetParent(RectTransform rect, Vector2 size, Vector2 pos)
    {
        if (HasParent) { Parent = null; }
        Parent = new UISizeFitter(rect);
        Parent.SetProperty_Size(size);
        Parent.SetProperty_Pos(pos);
    }


    /// <summary> スケーリング </summary>
    public void Resize(Vector2 rate)
    {
        m_RectTrs.sizeDelta = m_StandardSize * rate;
        m_RectTrs.localPosition = m_StandardPos * rate;
    }
    /// <summary> スケーリング(親有) </summary>
    public void ResizeforParent()
    {
        //親がいなかったら返す
        if(!HasParent) { DebugEX.LogError("親が設定されていません"); return; }

        Resize(Parent.GetRate());
    }

    /// <summary> 自身の現在のサイズと標準サイズから割合を取得 </summary>
    public Vector2 GetRate()
    {
        return m_RectTrs.sizeDelta / m_StandardSize;
    }
}
