using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySet
{
    public KeyCode Key { get; private set; }
    public KeyCode Pad { get; private set; }

    public KeySet(KeyCode key, KeyCode pad)
    {
        Key = key;
        Pad = pad;
    }
}

public class ControllerBase
{
    public enum EButtonStatus
    {
        Nothing,
        Push,
        Hold,
        Leave,
    }
    public class ButtonStatus
    {
        private KeySet m_CheckKey;
        private bool m_FirstPush;
        private bool m_IsHold;
        private float m_PushTimer;
        private float m_DistinctionTime;
        private float m_DeltaTime;
        
        #region Constructor
        public ButtonStatus(KeySet set, float distinction = 0.2f)
        {
            m_CheckKey = set;
            m_DistinctionTime = distinction;
            m_FirstPush =
                m_IsHold = false;
            m_PushTimer = 0.0f;
        }
        #endregion

        #region Public
        public EButtonStatus CheckKeyState()
        {
            bool input = Input.GetKey(m_CheckKey.Key) || Input.GetKey(m_CheckKey.Pad);
            
            //押された
            if (input)
            {
                m_FirstPush = true; //初回フラグ立てる
                m_PushTimer += m_DeltaTime; //時間計測
                //一定時間押されたら"長押し"
                if(m_PushTimer > m_DistinctionTime) {
                    m_IsHold = true;
                    return EButtonStatus.Hold;
                }
            }
            //離された
            else
            {
                float time = m_PushTimer; //経過時間取得
                m_PushTimer = 0.0f; //経過時間リセット
                //"長押し"なら"離された"
                if (m_IsHold) {
                    m_FirstPush = m_IsHold = false;
                    return EButtonStatus.Leave;
                }
                //"一度押し"
                else {
                    m_FirstPush = m_IsHold = false;
                    return EButtonStatus.Push;
                }
            }
            return EButtonStatus.Nothing;
        }
        #endregion

    }

    private readonly float m_DefultDistinctionTime = 0.2f;

    public ButtonStatus BS_A;
    public ButtonStatus BS_B;
    public ButtonStatus BS_X;
    public ButtonStatus BS_Y;

    public ControllerBase(int num)
    {
        
    }
}

public class ControllerManager : SingletonBehaviour<ControllerManager>
{
    
}
