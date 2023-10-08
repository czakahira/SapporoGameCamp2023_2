using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UnityEx
{
    public static class Controller
    {
        //接続されたコントローラーの名前
        private static string[] ConnectControllerNames { get { return Input.GetJoystickNames(); } }
        //接続されたコントローラーの数
        private static int ConnectControllerLen { get { return ConnectControllerNames.Length; } }
    }

    public static class EX_Controller
    {
        /// <summary>
        /// デバッグ用
        /// </summary>
        public static ButtonCheckClass debug = new ButtonCheckClass(KeyCode.Return);
        public static int Debug { get { return debug.KeyCheck(); } }

        /// <summary>
        /// 1P
        /// </summary>
        public static float LS1_H { get { return Input.GetAxis("Horizontal"); } }
        public static float LS1_V { get { return Input.GetAxis("Vertical"); } }
        public static float RS1_H { get { return Input.GetAxis("Horizontal2"); } }
        public static float RS1_V { get { return Input.GetAxis("Vertical2"); } }

        private static StickCheckClass class_LS1_H = new StickCheckClass();
        public static bool LS1_H_DASH { get { return class_LS1_H.DashCheck(LS1_H); } }

        private static KeyCode code_p1_jump =    KeyCode.Joystick1Button1;
        private static KeyCode code_p1_dodge =   KeyCode.Joystick1Button2;
        private static KeyCode code_p1_attack1 = KeyCode.Joystick1Button0;
        private static KeyCode code_p1_attack2 = KeyCode.Joystick1Button3;
        private static KeyCode code_p1_defense = KeyCode.Joystick1Button4;

        private static ButtonCheckClass class_p1_attack1 = new ButtonCheckClass(code_p1_attack1);

        private static ButtonCheckClass class_p1_defense = new ButtonCheckClass(code_p1_defense);

        public static bool KEY_P1_JUMP { get { return Input.GetKeyDown(code_p1_jump); } }

        public static int KEY_P1_DEFENSE { get { return class_p1_defense.KeyCheck(); } }
    }


    /// <summary>
    /// ボタン処理
    /// </summary>
    public enum ButtonStatus
    {
        Nothing = 0, //無押
        Push = 1,    //一度押し
        Hold = 2,    //長押し
        Leave = 3,   //長押し→離された
    }
    public class ButtonCheckClass
    {
        private KeyCode Key;             //判定するキー
        private bool FirstPress = false; //初めて押されたチェック
        private bool ReadyHold = false;  //"長押し"チェック
        private float PushTime;          //押された経過時間
        private float DistinctionTime;   //"一度押し"か"長押し"か区別する経過時間
             
        public float ElapsedTime { get { return PushTime; } }

        public ButtonCheckClass(KeyCode key, float dist = 0.2f)
        {
            Key = key;
            FirstPress = false;
            ReadyHold = false;
            DistinctionTime = dist;
        }
        public int KeyCheck()
        {
            //押されている処理
            if (Input.GetKey(Key))
            {
                FirstPress = true; //"押された"チェック有効
                PushTime += Time.deltaTime;
                //規定時間押されたら"長押し"
                if (PushTime >= DistinctionTime)
                {
                    ReadyHold = true; //"長押し"チェック有効
                    return (int)ButtonStatus.Hold;
                }
            }
            //離された処理
            if (FirstPress)
            {
                float cnt = PushTime;
                if (!Input.GetKey(Key))
                {
                    PushTime = 0;
                    if (cnt > 0)
                    {
                        //規定以下なら"一度押し"判定
                        if (cnt <= DistinctionTime)
                        {
                            FirstPress = ReadyHold = false;
                            return (int)ButtonStatus.Push;
                        }
                        //既に"長押し"なら"離された"判定
                        else if (ReadyHold)
                        {
                            FirstPress = ReadyHold = false;
                            return (int)ButtonStatus.Leave;
                        }
                    }
                }
            }
            //押されなかった
            return (int)ButtonStatus.Nothing;
        }
    }
    public static class EX_Button
    {
        public static bool IsPush(this int button)
        {
            return button == (int)ButtonStatus.Push;
        }
        public static bool IsHold(this int button)
        {
            return button == (int)ButtonStatus.Hold;
        }
        public static bool IsLeave(this int button)
        {
            return button == (int)ButtonStatus.Leave;
        }
    }


    public enum StickStatus
    {
        Nothing = 0,
        FirstTouch = 1,
        FirstLeave = 2,
        DoubleTouch = 3,
    }
    public class StickCheckClass
    {
        private int status = 0;
        private float BeforeAngle; //以前のスティックの向き
        private float AfterAngle; //以後のスティックの向き
        private float LeaveTime = 0.05f; //離されている時間
        private float ReturnTime = 0.2f;

        public bool DashCheck(float stick)
        {
            Mathf.Clamp(LeaveTime, 0.05f, 3);
            switch (status)
            {
                case (int)StickStatus.Nothing:
                    if (stick != 0)
                    {
                        BeforeAngle = stick > 0 ? 1 :
                                      stick < 0 ? -1 : 0;
                        status = (int)StickStatus.FirstTouch;
                    }
                    break;
                case (int)StickStatus.FirstTouch:
                    Debug.Log("倒された");
                    LeaveTime = 0;
                    if (stick > -0.1f && stick < 0.1f && BeforeAngle != 0)
                        status = (int)StickStatus.FirstLeave;
                    break;
                case (int)StickStatus.FirstLeave:
                    Debug.Log("離された");
                    LeaveTime += Time.deltaTime;
                    if (LeaveTime <= ReturnTime && LeaveTime > 0)
                    {
                        if (stick != 0)
                        {
                            AfterAngle = stick > 0 ? 1 :
                                         stick < 0 ? -1 : 0;
                            status = (int)StickStatus.DoubleTouch;
                        }
                    }
                    else if (LeaveTime > ReturnTime)
                    {
                        status = (int)StickStatus.Nothing;
                    }
                    break;
                case (int)StickStatus.DoubleTouch:
                    Debug.Log("二度押し");
                    if (stick != 0 && BeforeAngle == AfterAngle) return true;
                    else status = (int)StickStatus.Nothing;
                    break;
            }
            return false;
        }
    }


}
