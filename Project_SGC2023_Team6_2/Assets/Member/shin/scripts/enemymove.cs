using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    [SerializeField] private Transform playerTrans; //�ǂ�������Ώۂ�Transform
    [SerializeField] private float enemySpeed;  �@ //�G�̑��x
    [SerializeField] private float limitSpeed;      //�G�̐������x
    private Rigidbody2D rb;                         //�G��Rigidbody2D
    private Transform enemyTrans;                   //�G��Transform
    private SpriteRenderer renderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyTrans = GetComponent<Transform>();
        renderer = GetComponent<SpriteRenderer>();

    }




    private void Update()
    {
        
        Vector3 vector3 = playerTrans.position - enemyTrans.position;  //�G����ǂ�������Ώۂւ̕������v�Z
        rb.AddForce(vector3.normalized * enemySpeed);                  //�����̒�����1�ɐ��K���A�C�ӂ̗͂�AddForce�ŉ�����

        float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed);�@//X�����̑��x�𐧌�
        float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);  //Y�����̑��x�𐧌�
        rb.velocity = new Vector3(speedXTemp, speedYTemp);�@�@�@�@�@�@�@�@�@�@�@//���ۂɐ��������l����

        rb.velocity = Vector2.zero;

        Debug.Log(playerTrans.position.x < enemyTrans.position.x);
        if (playerTrans.position.x < enemyTrans.position.x)
            renderer.flipX = false;
        else
            renderer.flipX = true;


    }

}

