//======================================================
//
// �󔠏��� [Treasure.cs]
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
//�󕨂̏���
//======================================================
public class Treasure : MonoBehaviour
{
    Vector3 pos;      //�ʒu
    static int Num = 0;         //�󕨐�

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;       //���݂̈ʒu���
        Num += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //======================================================
    //�󕨂̐��ݒ菈��
    //======================================================
    public void SetNumTreasure(int nNum)
    {
        Num = nNum;
    }

    //======================================================
    //�󕨂̐��擾����
    //======================================================
    public int GetNumTreasure()
    {
        return Num;
    }

    //======================================================
    //�q�b�g����
    //======================================================
    public void Hit()
    {
        //Canvas�Q�[���I�u�W�F�N�g���������擾
        //GameObject obj = GameObject.Find("Canvas");

        //�A�^�b�`����Ă���UIManager�̃C���X�^���X���擾
        //UIManager manager = obj.GetComponent<UIManager>();

        //�X�R�A
        //manager.AddScore(1000);

        Num--;      //�󕨌��炷

        //�󕨏���
        Destroy(gameObject);
    }

    //======================================================
    //�Q�[���I�u�W�F�N�g�������ɓ����������ɌĂяo�����
    //======================================================
    void OnTriggerEnter2D(Collider2D collider)
    {
        //colider�̃Q�[���I�u�W�F�N�g���G���ǂ���
        if (collider.gameObject.CompareTag("Player"))
        {//�v���C���[���S����

            //�q�b�g����
            Hit();
        }
    }
}
