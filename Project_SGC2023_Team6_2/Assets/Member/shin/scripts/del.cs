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

        //�󕨏���
        Destroy(gameObject);
    }

    //======================================================
    //�Q�[���I�u�W�F�N�g�������ɓ����������ɌĂяo�����
    //======================================================
    void OnTriggerEnter2D(Collider2D collider)
    {
        //colider�̃Q�[���I�u�W�F�N�g���G���ǂ���
        if (collider.gameObject.CompareTag("bullet"))
        {//�v���C���[���S����

            //�q�b�g����
            Hit();
        }
    }
}
