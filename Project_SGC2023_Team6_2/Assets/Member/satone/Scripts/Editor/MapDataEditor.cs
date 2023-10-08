//======================================================
//
// �}�b�v�f�[�^�̏�񏈗� [MapDataEditor.cs]
// Author : shion satone
//
//======================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(GameManager))]     //GameManager�N���X�̃G�f�B�^�g�������悤�w��

//======================================================
//�}�b�v�̃Z�[�u���[�h����
//======================================================
public class MapDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameManager manager = target as GameManager;    //as�̓L���X�g

        if(GUILayout.Button("Save"))
        {//�Z�[�u�{�^��

            //�v���n�u�́A�v���n�u����JSON�ɏo�͂���

            //�󔠂̃t�@�C�������
            foreach (TreasureData data in manager.mapData.m_treasures)
            {
                data.m_name = data.m_prefab.name;
            }

            //json�t�@�C���̏o��
            string dataPath = Application.dataPath + "/Member/satone/Resources/" + manager.mapName + ".json";

            var json = JsonUtility.ToJson(manager.mapData);
            StreamWriter writer = new StreamWriter(dataPath, false);        //json�f�[�^�ɕϊ�
            writer.WriteLine(json);     //JSON�f�[�^��������
            writer.Flush();             //�o�b�t�@���N���A����
            writer.Close();             //�t�@�C�����N���[�Y����

        }
        else if (GUILayout.Button("Load"))
        {//���[�h�{�^��

            //JSON�t�@�C���̓ǂݍ���
            string jsonString = Resources.Load<TextAsset>(manager.mapName).ToString();

            manager.mapData = JsonUtility.FromJson<MapData>(jsonString);

            //Resource����v���n�u��ǂݍ���
            foreach (TreasureData data in manager.mapData.m_treasures)
            {
                data.m_prefab = (GameObject)Resources.Load(data.m_name);
            }
        }

        DrawDefaultInspector();

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
