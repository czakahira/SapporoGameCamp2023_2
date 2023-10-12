using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccBase : MonoBehaviour
{ 

    public ccshot m_shotPrefab; // 弾のプレハブ
    public float m_shotSpeed; // 弾の移動の速さ
    public float m_shotAngleRange; // 複数の弾を発射する時の角度
    public float m_shotTimer; // 弾の発射タイミングを管理するタイマー
    public int m_shotCount; // 弾の発射数
    public float m_shotInterval; // 弾の発射間隔（秒）


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var angle = 0; //プレイヤーの向き、だと思われる

        // 弾の発射タイミングを管理するタイマーを更新する
        m_shotTimer += Time.deltaTime;

        

        // まだ弾の発射タイミングではない場合は、ここで処理を終える
        if (m_shotTimer < m_shotInterval) return;

        // 弾の発射タイミングを管理するタイマーをリセットする
        //m_shotTimer = 0;

        // 自動で弾を発射する
        //ShootNWay(angle, m_shotAngleRange, m_shotSpeed, m_shotCount);

        //キー入力で発射する場合
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.C))
        {
            ShootNWay(angle, m_shotAngleRange, m_shotSpeed, m_shotCount);
            m_shotTimer = 0;
        }

        }

    // 弾を発射する関数
    private void ShootNWay(
        float angleBase, float angleRange, float speed, int count)
    {
        var pos = transform.localPosition; // プレイヤーの位置
        var rot = transform.localRotation; // プレイヤーの向き

        // 弾を複数発射する場合
        if (1 < count)
        {
            // 発射する回数分ループする
            for (int i = 0; i < count; ++i)
            {
                // 弾の発射角度を計算する
                var angle = angleBase +
                    angleRange * ((float)i / (count - 1) - 0.5f);

                // 発射する弾を生成する
                var shot = Instantiate(m_shotPrefab, pos, rot);

                // 弾を発射する方向と速さを設定する
                shot.Init(angle, speed);
            }
        }
        // 弾を 1 つだけ発射する場合
        else if (count == 1)
        {
            // 発射する弾を生成する
            var shot = Instantiate(m_shotPrefab, pos, rot);

            // 弾を発射する方向と速さを設定する
            shot.Init(angleBase, speed);
        }
    }
}
