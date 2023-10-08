using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class del : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hit()
    {

        //宝物消す
        Destroy(gameObject);
    }

    //======================================================
    //ゲームオブジェクトが何かに当たった時に呼び出される
    //======================================================
    void OnTriggerEnter2D(Collider2D collider)
    {
        //coliderのゲームオブジェクトが敵かどうか
        if (collider.gameObject.CompareTag("bullet"))
        {//プレイヤー死亡処理

            //ヒット処理
            Hit();
        }
    }
}
