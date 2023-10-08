using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace World.Manager.Controller
{
    /// <summary> 入力のリザルト </summary>
    public enum EInputResult
    {
        None,   //何もしていない※これは返ってこない想定

        Down,   //押す
        Up,     //離す
        Hold,   //長押し

        Tap,   //離す(ディレイ中に離す)

        //▼ディレイを考慮した長押し
        LongPress_Begin,    //開始
        LongPress_Update,   //更新
        LongPress_Exit,     //終了
    }

    [Serializable]
    public class InputButtonBase
    {
        protected float m_ElapsedTime_LongPress; //経過時間(長押し)
        protected float m_DelayToLongPress; //長押しになるまでのディレイ
        protected bool m_LongPressBegin; //長押しが開始されたフラグ

        public EInputResult result { get; protected set; }
        public float delayToLongPress
        {
            get { return m_DelayToLongPress; }
            set { m_DelayToLongPress = value; }
        }
        /// <summary> 長押しされている </summary>
        public bool isLongPress { get { return (m_ElapsedTime_LongPress >= m_DelayToLongPress); } }

        public virtual void Update(float _deltaTime) { }

        public bool IsElapsed(float _elapsed) { return (m_ElapsedTime_LongPress >= _elapsed); }
    }

    [Serializable]
    public class InputKeyButton : InputButtonBase
    {
        public KeyCode code { get; private set; }       //入力を受け付けるキーコード
        public Action<KeyCode, EInputResult> callback;  //コールバック←入力に依存する

        public InputKeyButton(KeyCode _code, float _delay)
        {
            code = _code;
            m_DelayToLongPress = _delay;
        }

        public override void Update(float _deltaTime)
        {
            //▼押す(開始)
            if (Input.GetKeyDown(code)) {
                callback?.Invoke(code, EInputResult.Down);
                m_ElapsedTime_LongPress = 0.0f;
                m_LongPressBegin = false;
            }
            //▼押す(継続)
            else if (Input.GetKey(code)) {
                m_ElapsedTime_LongPress += _deltaTime;
                callback?.Invoke(code, EInputResult.Hold);
                if (isLongPress) {
                    if (!m_LongPressBegin) {
                        callback?.Invoke(code, EInputResult.LongPress_Begin);
                        m_LongPressBegin = true;
                    }
                    callback?.Invoke(code, EInputResult.LongPress_Update);
                }
            }
            //▼離す
            else if (Input.GetKeyUp(code)) {
                callback?.Invoke(code, EInputResult.Up);
                if (!isLongPress) {
                    callback?.Invoke(code, EInputResult.Tap);
                }
                else {
                    callback?.Invoke(code, EInputResult.LongPress_Exit);
                }
            }
        }
    }
    
    /// <summary> アクシズ入力の方向 </summary>
    public enum InputAxisDirection
    {
        Up,        //上
        Down,      //下
        Right,     //右
        Left,      //左
    }
    [Serializable]
    public class InputAxisButton : InputButtonBase
    {
        /// <summary> 入力されているフラグ </summary>
        private bool m_IsPress;

        public string axisName { get; private set; }
        /// <summary> 入力を感知できる方向 </summary>
        public InputAxisDirection direction { get; private set; }
        public Action<string, InputAxisDirection, EInputResult> callback;

        public InputAxisButton(string _axisName, InputAxisDirection _direction, float _delay)
        {
            axisName = _axisName;
            direction = _direction;

            m_DelayToLongPress = _delay;
            m_IsPress = false;
        }

        public override void Update(float _deltaTime)
        {
            float axis = Input.GetAxisRaw(axisName);

            switch (direction) {
                case InputAxisDirection.Up:
                case InputAxisDirection.Right:
                    StateChange((axis > 0), _deltaTime);
                    break;

                case InputAxisDirection.Down:
                case InputAxisDirection.Left:
                    StateChange((axis < 0), _deltaTime);
                    break;
            }
        }
        private void StateChange(bool _inputResult, float _deltaTime)
        {
            //まだ入力がない
            if (!m_IsPress) {
                //初めて入力された
                if (_inputResult) {
                    callback?.Invoke(axisName, direction, EInputResult.Down);
                    m_ElapsedTime_LongPress = 0.0f;
                    m_LongPressBegin = false;
                    m_IsPress = true;
                }
            }
            //入力中
            else {
                if (_inputResult) {
                    m_ElapsedTime_LongPress += _deltaTime;
                    callback?.Invoke(axisName, direction, EInputResult.Hold);
                    if (isLongPress) {
                        if (!m_LongPressBegin) {
                            callback?.Invoke(axisName, direction, EInputResult.LongPress_Begin);
                            m_LongPressBegin = true;
                        }
                        callback?.Invoke(axisName, direction, EInputResult.LongPress_Update);
                    }
                }
                //離された
                else {
                    callback?.Invoke(axisName, direction, EInputResult.Up);
                    if (!isLongPress) {
                        callback?.Invoke(axisName, direction, EInputResult.Tap);
                    }
                    else {
                        callback?.Invoke(axisName, direction, EInputResult.LongPress_Exit);
                    }
                    m_IsPress = false;
                }
            }
        }
    }
    [Serializable]
    public class InputAxisController : InputButtonBase
    {
        public string axisName_Vertical { get; private set; }
        public string axisName_Horizontal { get; private set; }

        public float vertical { get { return Input.GetAxis(axisName_Vertical); } }
        public float horizontal { get { return Input.GetAxis(axisName_Horizontal); } }

        public Vector3 axisDirection { get { return new Vector3(horizontal, 0, vertical).normalized; } }

        public Action<string, InputAxisDirection, EInputResult> callback
        {
            set
            {
                m_Btn_Top.callback = value;
                m_Btn_Bottom.callback = value;
                m_Btn_Right.callback = value;
                m_Btn_Left.callback = value;
            }
        }

        private InputAxisButton m_Btn_Top;
        private InputAxisButton m_Btn_Bottom;
        private InputAxisButton m_Btn_Right;
        private InputAxisButton m_Btn_Left;

        public InputAxisController(string _vertical, string _horizontal, float _delay)
        {
            axisName_Vertical = _vertical;
            axisName_Horizontal = _horizontal;
            m_DelayToLongPress = _delay;

            m_Btn_Top = new InputAxisButton(axisName_Vertical, InputAxisDirection.Up, m_DelayToLongPress);
            m_Btn_Bottom = new InputAxisButton(axisName_Vertical, InputAxisDirection.Down, m_DelayToLongPress);
            m_Btn_Right = new InputAxisButton(axisName_Horizontal, InputAxisDirection.Right, m_DelayToLongPress);
            m_Btn_Left = new InputAxisButton(axisName_Horizontal, InputAxisDirection.Left, m_DelayToLongPress);
        }

        public override void Update(float _deltaTime)
        {
            m_Btn_Top.Update(_deltaTime);
            m_Btn_Bottom.Update(_deltaTime);
            m_Btn_Right.Update(_deltaTime);
            m_Btn_Left.Update(_deltaTime);
        }

    }

    public class PadControllerManager : SingletonBehaviour<PadControllerManager>
    {
       
    }
}
