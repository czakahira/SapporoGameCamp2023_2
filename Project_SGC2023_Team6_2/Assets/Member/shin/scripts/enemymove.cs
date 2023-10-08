using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    [SerializeField] private Transform playerTrans; //追いかける対象のTransform
    [SerializeField] private float enemySpeed;  　 //敵の速度
    [SerializeField] private float limitSpeed;      //敵の制限速度
    private Rigidbody2D rb;                         //敵のRigidbody2D
    private Transform enemyTrans;                   //敵のTransform
    private SpriteRenderer renderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyTrans = GetComponent<Transform>();
        renderer = GetComponent<SpriteRenderer>();

    }




    private void Update()
    {
        
        Vector3 vector3 = playerTrans.position - enemyTrans.position;  //敵から追いかける対象への方向を計算
        rb.AddForce(vector3.normalized * enemySpeed);                  //方向の長さを1に正規化、任意の力をAddForceで加える

        float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed);　//X方向の速度を制限
        float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);  //Y方向の速度を制限
        rb.velocity = new Vector3(speedXTemp, speedYTemp);　　　　　　　　　　　//実際に制限した値を代入

        rb.velocity = Vector2.zero;

        Debug.Log(playerTrans.position.x < enemyTrans.position.x);
        if (playerTrans.position.x < enemyTrans.position.x)
            renderer.flipX = false;
        else
            renderer.flipX = true;


    }

}

