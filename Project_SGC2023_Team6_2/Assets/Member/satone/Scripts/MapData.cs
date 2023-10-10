//======================================================
//
// �}�b�v�f�[�^�̏��ۑ��p [MapData.cs]
// Author : shion satone
//
//======================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//======================================================
//�}�b�v�̏��܂Ƃ�
//======================================================
[System.Serializable]
public class MapData
{
	//�������z��
	public TreasureData[] m_treasures;       //�󔠂̏��
}

//======================================================
//�󔠂̏��
//======================================================
[System.Serializable]
public class TreasureData
{
	//�v���n�u�̖��O�ۑ��p
	[HideInInspector] public string m_name;

	//��������v���n�u
	public GameObject m_prefab;

	//�o���t���[����
	public int m_frame;

	//�o���ʒu
	public Vector3 m_position;
}