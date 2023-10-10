//======================================================
//
// �Q�[���}�l�[�W���[���� [GameManager.cs]
// Author : shion satone
//
//======================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//�}�b�v�f�[�^
	[SerializeField]
	private MapData m_mapData;

	//�}�b�v��
	[SerializeField]
	private string m_mapName;

	[SerializeField]
	[Header("�v���C���[�̈ʒu")] Vector3 posCenter;   //�v���C���[�̈ʒu

	[SerializeField]
	[Header("��̉~�̔��a(X, Y)")] Vector2 fRadius;  //��̉~�̔��a

	[SerializeField]
	[Header("�v���C���[����G�̉~�̋���")] float fLength;  //�v���C���[����G�̉~�̋���

	[SerializeField]
	[Header("�G����x�Ɉ�̉~�ɑ����鐔")] int nNumGenerate;   //�G�������鐔

	[SerializeField]
	[Header("�G�̃v���n�u")] GameObject m_enemyPrefab;      //�G�̃v���n�u

	[SerializeField]
	[Header("�G�X�|�[������(�b)")] private int m_DestFrame;     //�ڕW�̃t���[���J�E���g

	//���݂̃t���[���J�E���g
	private float m_frame = 0;

	//�󔠂̏��
	private TreasureData[] m_treasureData;

	int nNumTreasure = 0;         //�󕨐�
	int nNumSaveTreasure = 0;     //�󕨐��ۑ��p

	// Start is called before the first frame update
	void Start()
	{
		m_treasureData = m_mapData.m_treasures;     //�󔠂̏����

		//======================================================
		//�󔠂̐���
		//======================================================
		for (int nCnt = 0; nCnt < m_mapData.m_treasures.Length; nCnt++)
		{
			//�󔠂̃Q�[���I�u�W�F�N�g�𐶐�����
			GameObject obj = Instantiate(m_treasureData[nCnt].m_prefab, m_treasureData[nCnt].m_position, Quaternion.identity) as GameObject;

			nNumTreasure++;     //�󕨐����Z
		}

		nNumSaveTreasure = nNumTreasure;        //�����ۑ�
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F8) &&
			nNumTreasure > 0)
		{
			nNumTreasure--;     //�󕨏���
		}

		//�G�̐���
		EnemyGenerate();

	}

	//======================================================
	//�G�̐�������
	//======================================================
	private void EnemyGenerate()
	{
		if (m_DestFrame <= m_frame)
		{//��莞�Ԍo�߂�����

			//����
			float fLengthDest = fLength * nNumTreasure / nNumSaveTreasure + ((nNumSaveTreasure - nNumTreasure) * 0.5f);

			//�G�̐�
			int nNum = nNumGenerate + (nNumSaveTreasure - nNumTreasure);

			Debug.Log(fLengthDest);

			Vector3 randPos;        //�����_���Ȉʒu

			for (int nCntEnemy = 0; nCntEnemy < nNum; nCntEnemy++)
			{
				//�~�͈͓̔��̃����_���Ȉʒu���(�E)
				randPos.x = fRadius.x * Random.insideUnitCircle.x + posCenter.x + fLengthDest;
				randPos.y = fRadius.y * Random.insideUnitCircle.y + posCenter.y;
				randPos.z = posCenter.z;

				//�G�̐���
				Instantiate(m_enemyPrefab, randPos, Quaternion.identity);

				//�~�͈͓̔��̃����_���Ȉʒu���(��)
				randPos.x = fRadius.x * Random.insideUnitCircle.x + posCenter.x - fLengthDest;
				randPos.y = fRadius.y * Random.insideUnitCircle.y + posCenter.y;
				randPos.z = posCenter.z;

				//�G�̐���
				Instantiate(m_enemyPrefab, randPos, Quaternion.identity);

				//�~�͈͓̔��̃����_���Ȉʒu���(��)
				randPos.x = fRadius.x * Random.insideUnitCircle.x + posCenter.x;
				randPos.y = fRadius.y * Random.insideUnitCircle.y + posCenter.y + fLengthDest;
				randPos.z = posCenter.z;

				//�G�̐���
				Instantiate(m_enemyPrefab, randPos, Quaternion.identity);

				//�~�͈͓̔��̃����_���Ȉʒu���(��)
				randPos.x = fRadius.x * Random.insideUnitCircle.x + posCenter.x;
				randPos.y = fRadius.y * Random.insideUnitCircle.y + posCenter.y - fLengthDest;
				randPos.z = posCenter.z;

				//Debug.Log(fInsideRadius.x + fRadius.x);
				//Debug.Log("X:" + randPos.x + " Y:" + randPos.y);

				//�G�̐���
				Instantiate(m_enemyPrefab, randPos, Quaternion.identity);
			}

			m_frame = 0;        //�t���[�������Z�b�g
		}
		else
		{//���Ԍo�߂��ĂȂ�������

			m_frame += Time.deltaTime;
		}
	}

	///======================================================
	///�~����̃����_���Ȉʒu�����߂鏈��
	///======================================================
	private Vector3 CircleHorizon(float radius)
	{
		var angle = Random.Range(0, 360);       //�����_���Ȋp�x
		var rad = angle * Mathf.Deg2Rad;        //�p�x�̕ϊ�

		//�͈͓��̍��W���߂�
		var px = Mathf.Cos(rad) * radius;
		var py = Mathf.Sin(rad) * radius;

		return new Vector3(px, py, 0);      //�͈͓��̃����_���Ȓl��Ԃ�
	}

	//======================================================
	//�}�b�v�f�[�^�̃v���p�e�B
	//======================================================
	public MapData mapData
	{
		get { return this.m_mapData; }
		set { this.m_mapData = value; }
	}

	//======================================================
	//�}�b�v���̃v���p�e�B
	//======================================================
	public string mapName
	{
		get { return this.m_mapName; }
		set { this.m_mapName = value; }
	}
}
