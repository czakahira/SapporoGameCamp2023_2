//======================================================
//
// 宝箱処理 [Treasure.cs]
// Author : shion satone
//
//======================================================
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

//======================================================
//宝物の処理
//======================================================
public class Treasure : MonoBehaviour
{
    Vector3 pos;      //位置
    static int Num = 0;         //宝物数

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;       //現在の位置代入
        Num += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //======================================================
    //宝物の数設定処理
    //======================================================
    public void SetNumTreasure(int nNum)
    {
        Num = nNum;
    }

    //======================================================
    //宝物の数取得処理
    //======================================================
    public int GetNumTreasure()
    {
        return Num;
    }

    //======================================================
    //ヒット処理
    //======================================================
    public void Hit()
    {
        //Canvasゲームオブジェクトを検索し取得
        //GameObject obj = GameObject.Find("Canvas");

        //アタッチされているUIManagerのインスタンスを取得
        //UIManager manager = obj.GetComponent<UIManager>();

        //スコア
        //manager.AddScore(1000);

        Num--;      //宝物減らす

        //宝物消す
        Destroy(gameObject);
    }

    //======================================================
    //ゲームオブジェクトが何かに当たった時に呼び出される
    //======================================================
    void OnTriggerEnter2D(Collider2D collider)
    {
        //coliderのゲームオブジェクトが敵かどうか
        if (collider.gameObject.CompareTag("Player"))
        {//プレイヤー死亡処理

            //ヒット処理
            Hit();
        }
    }
}
