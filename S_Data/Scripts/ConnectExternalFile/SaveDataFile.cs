using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
// M.S �Z�[�u�f�[�^�̓ǂݍ��݁E��������
// ��{��̃I�u�W�F�N�g�ɂ̂݃A�^�b�`���Ă�������
public class SaveDataFile : MonoBehaviour
{
    string dataPath;        //  �e�L�X�g�t�@�C���ւ̃p�X
    // �Z�[�u�f�[�^���i�[�����ϐ�
    Vector3 pcPos;    // �Q�[���I�����̈ʒu
    public int haveMoney;     // ������
    int monster1Num;  // �����X�^�[1�̐�
    int monster2Num;  // �����X�^�[2�̐�
    int monster3Num;  // �����X�^�[3�̐�
    int monster4Num;  // �����X�^�[4�̐�
    int monster5Num;  // �����X�^�[5�̐�
    bool doneWarp1;   // ���[�v�s�����ς�1
    bool doneWarp2;   // ���[�v�s�����ς�2
    bool doneWarp3;   // ���[�v�s�����ς�3
    bool doneWarp4;   // ���[�v�s�����ς�4
    bool doneWarp5;   // ���[�v�s�����ς�5
    bool doneWarp6;   // ���[�v�s�����ς�6

    void Start()
    {
        dataPath = Application.dataPath + "/StreamingAssets/saveData.txt";
        ReadSaveData();
        //haveMoney = 9;
        WriteSaveData();
    }
    // �Z�[�u�f�[�^�̓ǂݍ���(�e�ϐ��ւ̊i�[���s��)
    // ��  ���F�Ȃ�
    // �߂�l�F�Ȃ�
    void ReadSaveData()
    {
        StreamReader fs = new StreamReader(dataPath, System.Text.Encoding.GetEncoding("UTF-8"));
        // �s(�Z�[�u�f�[�^�S��)�ǂݍ���
        string saveReadData = fs.ReadLine();
        saveReadData = saveReadData + '\n';
        string numS = "";
        int commaNum = 0;
        // �ǂݍ��񂾃f�[�^����e�ϐ��Ɋi�[
        for (int i = 0; i < 50; i++)
        {
            if (saveReadData[i] == '\n')
            {
                //���[�v�|�C���g6�s�����ς݂��ǂ���
                int warp6 = Convert.ToInt32(numS);
                doneWarp6 = Convert.ToBoolean(warp6);
                numS = "";
                fs.Close();
                break;
            }
            else if (saveReadData[i] == ',')
            {
                commaNum++;
                switch (commaNum)
                {
                    case 1:     //X���W
                        pcPos.x = Convert.ToInt32(numS);
                        break;
                    case 2:     //Y���W
                        pcPos.y = Convert.ToInt32(numS);
                        break;
                    case 3:     //Z���W
                        pcPos.z = Convert.ToInt32(numS);
                        break;
                    case 4:     //������
                        haveMoney = Convert.ToInt32(numS);
                        break;
                    case 5:     //�����X�^�[1�̐�
                        monster1Num = Convert.ToInt32(numS);
                        break;
                    case 6:     //�����X�^�[2�̐�
                        monster2Num = Convert.ToInt32(numS);
                        break;
                    case 7:     //�����X�^�[3�̐�
                        monster3Num = Convert.ToInt32(numS);
                        break;
                    case 8:     //�����X�^�[4�̐�
                        monster4Num = Convert.ToInt32(numS);
                        break;
                    case 9:     //�����X�^�[5�̐�
                        monster5Num = Convert.ToInt32(numS);
                        break;
                    case 10:    //���[�v�|�C���g1�s�����ς݂��ǂ���
                        int warp1 = Convert.ToInt32(numS);
                        doneWarp1 = Convert.ToBoolean(warp1);
                        break;
                    case 11:    //���[�v�|�C���g2�s�����ς݂��ǂ���
                        int warp2 = Convert.ToInt32(numS);
                        doneWarp2 = Convert.ToBoolean(warp2);
                        break;
                    case 12:    //���[�v�|�C���g3�s�����ς݂��ǂ���
                        int warp3 = Convert.ToInt32(numS);
                        doneWarp3 = Convert.ToBoolean(warp3);
                        break;
                    case 13:    //���[�v�|�C���g4�s�����ς݂��ǂ���
                        int warp4 = Convert.ToInt32(numS);
                        doneWarp4 = Convert.ToBoolean(warp4);
                        break;
                    case 14:    //���[�v�|�C���g5�s�����ς݂��ǂ���
                        int warp5 = Convert.ToInt32(numS);
                        doneWarp5 = Convert.ToBoolean(warp5);
                        break;
                }
                numS = "";
            }
            else
            {
                numS = numS + saveReadData[i];
            }
        }
    }
    // �Z�[�u�f�[�^�̏�������
    // �����̃X�N���v�g��pcPos,monster1Num�`doneWarp6����������
    // ��  ���F�Ȃ�
    // �߂�l�F�Ȃ�
    public void WriteSaveData()
    {
        string haveMoneyStr = "";
        // bool��int�ɕϊ�
        int warp1 = Convert.ToInt32(doneWarp1);
        int warp2 = Convert.ToInt32(doneWarp2);
        int warp3 = Convert.ToInt32(doneWarp3);
        int warp4 = Convert.ToInt32(doneWarp4);
        int warp5 = Convert.ToInt32(doneWarp5);
        int warp6 = Convert.ToInt32(doneWarp6);
        // ���������������ݗp������ɂ���
        if (haveMoney >= 10000)
        {
            haveMoneyStr = haveMoney.ToString();
        }
        else if (haveMoney >= 1000)
        {
            haveMoneyStr = "0" + haveMoney.ToString();
        }
        else if (haveMoney >= 100)
        {
            haveMoneyStr = "0" + "0" + haveMoney.ToString();
        }
        else if (haveMoney >= 10)
        {
            haveMoneyStr = "0" + "0" + "0" + haveMoney.ToString();
        }
        else
        {
            haveMoneyStr = "0" + "0" + "0" + "0" + haveMoney.ToString();
        }
        // �������ݕ�����̍쐬
        string allWriteText = pcPos.x.ToString() + ',' + pcPos.y.ToString() + ',' + pcPos.z.ToString() + ',' +
            haveMoneyStr + ',' + monster1Num.ToString() + ',' + monster2Num.ToString() + ',' +
            monster3Num.ToString() + ',' + monster4Num.ToString() + ',' + monster5Num.ToString() + ',' +
            warp1.ToString() + ',' + warp2.ToString() + ',' + warp3.ToString() + ',' +
            warp4.ToString() + ',' + warp5.ToString() + ',' + warp6.ToString() + '\n';
        File.WriteAllText(dataPath, allWriteText);  // �f�[�^�x�[�X�ɏ�������
    }
}
