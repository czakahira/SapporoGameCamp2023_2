//======================================================
//
// UI処理 [UIManager.cs]
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
    private GameObject m_scoreText;     //スコアテキスト

    int m_score = 1000;        //スコア

    // Start is called before the first frame update
    void Start()
    {
        //スコアを設定
        m_score = 1000;

        Debug.Log(this.m_score.ToString());

        //スコアテキストにスコア表示
        m_scoreText.GetComponent<TextMeshProUGUI>().text = this.m_score.ToString("D4");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //=================================================================================
    //スコア加算処理
    //=================================================================================
    public void AddScore(int addScore)
    {
        //スコアを加算
        m_score += addScore;

        m_scoreText.GetComponent<TextMeshProUGUI>().text = "Score:" + this.m_score.ToString("D4");
    }
}
