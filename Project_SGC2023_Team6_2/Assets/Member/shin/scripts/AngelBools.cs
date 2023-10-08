using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBools : MonoBehaviour
{
    private Animator anim;  //Animatorをanimという変数で定義する

    // Start is called before the first frame update
    void Start()
    {

        //変数animに、Animatorコンポーネントを設定する
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //もし、スペースキーが押されたらなら
        if (Input.GetKey(KeyCode.Space))
        {
            //Bool型のパラメーターであるblRotをTrueにする
            anim.SetBool("AngelBool", true);
        }
    }
}