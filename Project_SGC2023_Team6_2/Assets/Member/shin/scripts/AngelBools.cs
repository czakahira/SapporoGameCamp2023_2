using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBools : MonoBehaviour
{
    private Animator anim;  //Animator��anim�Ƃ����ϐ��Œ�`����

    // Start is called before the first frame update
    void Start()
    {

        //�ϐ�anim�ɁAAnimator�R���|�[�l���g��ݒ肷��
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //�����A�X�y�[�X�L�[�������ꂽ��Ȃ�
        if (Input.GetKey(KeyCode.Space))
        {
            //Bool�^�̃p�����[�^�[�ł���blRot��True�ɂ���
            anim.SetBool("AngelBool", true);
        }
    }
}