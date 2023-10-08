//======================================================
//
// UI���� [UIManager.cs]
// Author : shion satone
//
//======================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_scoreText;     //�X�R�A�e�L�X�g

    int m_score = 1000;        //�X�R�A

    // Start is called before the first frame update
    void Start()
    {
        //�X�R�A��ݒ�
        m_score = 1000;

        Debug.Log(this.m_score.ToString());

        //�X�R�A�e�L�X�g�ɃX�R�A�\��
        m_scoreText.GetComponent<TextMeshProUGUI>().text = this.m_score.ToString("D4");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //=================================================================================
    //�X�R�A���Z����
    //=================================================================================
    public void AddScore(int addScore)
    {
        //�X�R�A�����Z
        m_score += addScore;

        m_scoreText.GetComponent<TextMeshProUGUI>().text = "Score:" + this.m_score.ToString("D4");
    }
}
