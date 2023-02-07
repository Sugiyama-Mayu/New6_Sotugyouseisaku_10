using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

// M.S
// �N�G�X�g�f�[�^�x�[�X�̓ǂݍ��݁E��������
// ��{��̃I�u�W�F�N�g�ɂ̂݃A�^�b�`���Ă�������
public class ConnectionQuestFile : MonoBehaviour
{
    [SerializeField] private SaveDataFile saveDataFile;
    string dataPath;        //  �e�L�X�g�t�@�C���ւ̃p�X
    //TranslationQuestDataArray���Ăяo���Ɗe�ϐ��������Ɋi�[�����
    public int questIdNum;  // �N�G�X�gID
    public bool orderFlag;  // �N�G�X�g�̎󒍏󋵃t���O
    public bool resolutionFlag; // �N�G�X�g�̃N���A�󋵃t���O
    // �󒍒��̃N�G�X�g���
    public Text nowOrderReceivedQuestNameText; // �N�G�X�g��
    public int nowOrderReceivedQuestID;        // �N�G�X�gID
    public List<string> findOrderdArray = new List<string>();
    private const int MAX_ID_NUM = 5; // ���N�G�X�g��
    // �e�G�̓������Ǘ��ϐ�
    // S(�X�P���g��)�AG(�S�u����)�AB(�o���p�C�A)�AO(�I�[�N)�AH(�D�I�[�N)
    public int SEnmNum = 5;
    public int GEnmNum = 5;
    public int BEnmNum = 5;
    public int OEnmNum = 5;
    public int HEnmNum = 5;
    // �e�N�G�X�g�N���A���̕�V
    public int SEnmReward = 100;
    public int GEnmReward = 200;
    public int BEnmReward = 300;
    public int OEnmReward = 400;
    public int HEnmReward = 500;
    int huntConfirmQuestIdNum = 0;
    bool huntConfirmOrderFlag = false;
    bool huntConfirmResolutionFlag = false;

    void Start()
    {
        dataPath = Application.dataPath + "/StreamingAssets/questData.txt";
        FindOrderdQuestFile();  // �󒍒��̃N�G�X�g��T��
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            KnockEnemy('O');
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            KnockEnemy('B');
        }
        // �󒍒��̃N�G�X�g�̓����G�����������ꂽ���ǂ���
        // ��������Ă���������ς݂ɂ���
        if (nowOrderReceivedQuestID == 101 && SEnmNum <= 0)
        {
            TranslationHuntConfirmQuestDataArray(ReadQuestFile(nowOrderReceivedQuestID));
            if (huntConfirmResolutionFlag == false)
            {
                WriteQuestFile(nowOrderReceivedQuestID, false, true); // �N�G�X�g�N���A�ς݂���������
                saveDataFile.haveMoney = saveDataFile.haveMoney + SEnmReward; // �������ɕ�V�𑫂�
            }
        }
        else if (nowOrderReceivedQuestID == 102 && GEnmNum <= 0)
        {
            TranslationHuntConfirmQuestDataArray(ReadQuestFile(nowOrderReceivedQuestID));
            if (huntConfirmResolutionFlag == false)
            {
                WriteQuestFile(nowOrderReceivedQuestID, false, true); // �N�G�X�g�N���A�ς݂���������
                saveDataFile.haveMoney = saveDataFile.haveMoney + GEnmReward; // �������ɕ�V�𑫂�
            }
        }
        else if (nowOrderReceivedQuestID == 103 && BEnmNum <= 0)
        {
            TranslationHuntConfirmQuestDataArray(ReadQuestFile(nowOrderReceivedQuestID));
            if (huntConfirmResolutionFlag == false)
            {
                WriteQuestFile(nowOrderReceivedQuestID, false, true); // �N�G�X�g�N���A�ς݂���������
                saveDataFile.haveMoney = saveDataFile.haveMoney + BEnmReward; // �������ɕ�V�𑫂�
            }
        }
        else if (nowOrderReceivedQuestID == 104 && OEnmNum <= 0)
        {
            TranslationHuntConfirmQuestDataArray(ReadQuestFile(nowOrderReceivedQuestID));
            if (huntConfirmResolutionFlag == false)
            {
                WriteQuestFile(nowOrderReceivedQuestID, false, true); // �N�G�X�g�N���A�ς݂���������
                saveDataFile.haveMoney = saveDataFile.haveMoney + OEnmReward; // �������ɕ�V�𑫂�
                saveDataFile.WriteSaveData();
            }
        }
        else if (nowOrderReceivedQuestID == 105 && HEnmNum <= 0)
        {
            TranslationHuntConfirmQuestDataArray(ReadQuestFile(nowOrderReceivedQuestID));
            if (huntConfirmResolutionFlag == false)
            {
                WriteQuestFile(nowOrderReceivedQuestID, false, true); // �N�G�X�g�N���A�ς݂���������
                saveDataFile.haveMoney = saveDataFile.haveMoney + HEnmReward; // �������ɕ�V�𑫂�
            }
        }
    }
    // �󒍒��̃N�G�X�g��T��
    // ��  ���F�Ȃ�
    // �߂�l�F�Ȃ�
    public void FindOrderdQuestFile()
    {
        string questDataArray = "";
        StreamReader fs = new StreamReader(dataPath, System.Text.Encoding.GetEncoding("UTF-8"));
        // �S�ẴN�G�X�g���݂�
        for (int i = 1; i <= MAX_ID_NUM; i++)
        {
            questDataArray = fs.ReadLine();
            questDataArray = questDataArray + '\n';
            TranslationQuestDataArray(questDataArray);
            // �󒍒��t���O�������Ă�����
            if (orderFlag == true)
            {
                // ���̃N�G�X�gID�Ɩ��O��o�^
                AllocationOrderReceivedID(questIdNum);
                break;
            }
        }
        fs.Close();
        return;
    }
    // �N�G�X�g�f�[�^�x�[�X�ǂݍ��݊֐�
    // ��  ���Fint    id  �ǂݍ��ރf�[�^��ID�ԍ�
    // �߂�l�Fstring     �ǂݍ��񂾃f�[�^�̍s�̕����z��
    public string ReadQuestFile(int id)
    {
        string questDataArray = "";
        StreamReader fs = new StreamReader(dataPath, System.Text.Encoding.GetEncoding("UTF-8"));
        // id�܂ŃN�G�X�g���݂�
        for (int i = 101; i <= id; i++)
        {
            questDataArray = fs.ReadLine();
        }
        questDataArray = questDataArray + '\n';
        fs.Close();
        return questDataArray;
    }
    // ReadQuestFile�œǂݍ��񂾃f�[�^��ϐ��Ɋi�[
    // ��  ���Fstring  textArray ���ReadQuestFile�œǂݍ��񂾂���
    // �߂�l�Fbool              �s���� false  ���� true
    // �����œn�����f�[�^��ID�A�󒍏󋵁A�N�G�X�g�I���󋵂͂��ꂼ��
    // questIdNum�AorderFlag�AresolutionFlag�Ɋi�[�����
    public bool TranslationQuestDataArray(string textArray)
    {
        int arrayNum = 0;
        arrayNum = Convert.ToInt32(textArray.Length);
        if (Convert.ToInt32(textArray.Length) < 8)
        {
            return false;
        }
        int commaNum = 0;
        string outVariable = "";
        for (int i = 0; i < 100; i++)
        {
            // ������̒[�܂Ō�����
            if (textArray[i] == '\n')
            {
                switch (Convert.ToInt32(outVariable))
                {
                    // �N�G�X�g�N���A�󋵂̊i�[

                    case 0:
                        resolutionFlag = false;
                        break;
                    case 1:
                        resolutionFlag = true;
                        break;

                }
                outVariable = "";
                return true;
            }
            else if (textArray[i] == ',')
            {
                commaNum++;
                switch (commaNum)
                {
                    case 1:
                        // �N�G�X�gID�̊i�[
                        questIdNum = Convert.ToInt32(outVariable);
                        outVariable = "";
                        break;
                    case 2:
                        switch (Convert.ToInt32(outVariable))
                        {
                            // �󒍏󋵂̊i�[
                            case 0:
                                orderFlag = false;
                                break;
                            case 1:
                                orderFlag = true;
                                break;

                        }
                        outVariable = "";
                        break;
                }
            }
            else
            {
                outVariable = outVariable + textArray[i];
            }
        }
        return false;
    }
    // ReadQuestFile�œǂݍ��񂾃f�[�^��ϐ��Ɋi�[(�����ł������m�F�p)
    // ��  ���Fstring  textArray ���ReadQuestFile�œǂݍ��񂾂���
    // �߂�l�Fbool              �s���� false  ���� true
    // �����œn�����f�[�^��ID�A�󒍏󋵁A�N�G�X�g�I���󋵂͂��ꂼ��
    // huntConfirmQuestIdNum�AhuntConfirmOrderFlag�AhuntConfirmResolutionFlag�Ɋi�[�����
    public bool TranslationHuntConfirmQuestDataArray(string textArray)
    {
        int arrayNum = 0;
        arrayNum = Convert.ToInt32(textArray.Length);
        if (Convert.ToInt32(textArray.Length) < 8)
        {
            return false;
        }
        int commaNum = 0;
        string outVariable = "";
        for (int i = 0; i < 100; i++)
        {
            // ������̒[�܂Ō�����
            if (textArray[i] == '\n')
            {
                switch (Convert.ToInt32(outVariable))
                {
                    // �N�G�X�g�N���A�󋵂̊i�[

                    case 0:
                        huntConfirmResolutionFlag = false;
                        break;
                    case 1:
                        huntConfirmResolutionFlag = true;
                        break;

                }
                outVariable = "";
                return true;
            }
            else if (textArray[i] == ',')
            {
                commaNum++;
                switch (commaNum)
                {
                    case 1:
                        // �N�G�X�gID�̊i�[
                        huntConfirmQuestIdNum = Convert.ToInt32(outVariable);
                        outVariable = "";
                        break;
                    case 2:
                        switch (Convert.ToInt32(outVariable))
                        {
                            // �󒍏󋵂̊i�[
                            case 0:
                                huntConfirmOrderFlag = false;
                                break;
                            case 1:
                                huntConfirmOrderFlag = true;
                                break;

                        }
                        outVariable = "";
                        break;
                }
            }
            else
            {
                outVariable = outVariable + textArray[i];
            }
        }
        return false;
    }
    // �N�G�X�g�f�[�^�x�[�X�̏������݊֐�
    // �� ���Fint  id         �������ރf�[�^��ID�ԍ�
    //        bool order      �������ރf�[�^�̎󒍏��(true �󒍂����Afalse �󒍂��Ă��Ȃ�)
    //        bool resolution �������ރf�[�^�̃N�G�X�g�I�����Ă��邩�ǂ���(true �I�����Ă���Afalse ���Ă��Ȃ�)
    public void WriteQuestFile(int id, bool order, bool resolution)
    {
        // �p�X
        StreamReader fs = new StreamReader(dataPath, System.Text.Encoding.GetEncoding("UTF-8"));
        string allData = fs.ReadToEnd();  // �e�L�X�g�t�@�C����S�ēǂݍ���
        int newLineNum = 1;               // �f�[�^�����������镔���̍s��
        int calcuLineNum = 0;             
        string writeArray = "";
        int orderNum, resolutionNum;
        // �󒍏����A�N�G�X�g�N���A�󋵂�int�ɕς���
        if (order == true)
        {
            orderNum = 1;
        }
        else
        {
            orderNum = 0;
        }
        if (resolution == true)
        {
            resolutionNum = 1;
        }
        else
        {
            resolutionNum = 0;
        }
        // ID�A�󒍏󋵂Ȃǂ̏��𕶎���ɂ���
        string writeLine = id.ToString() + ',' + orderNum.ToString() + ',' + resolutionNum.ToString() + '\n';
        for (int i = 0; i < allData.Length; i++)
        {
            // ���������s���ǂ���
            if (newLineNum + 100 == id)
            {
                if (i < (calcuLineNum * 8) + 8)
                {
                    // �f�[�^�̏�������(writeLine����)
                    writeArray = writeArray + writeLine[i - calcuLineNum * 8];
                }
                // �����������I�������
                if (allData[i] == '\n')
                {
                    newLineNum++;
                    calcuLineNum = newLineNum - 1;
                }
            }
            else if (allData[i] == '\n')
            {
                newLineNum++;  // ���������s�̍X�V
                calcuLineNum = newLineNum - 1;
                writeArray = writeArray + allData[i];

            }
            else
            {
                writeArray = writeArray + allData[i];
            }
        }
        fs.Close();
        // �e�L�X�g�t�@�C���ɏ�������
        File.WriteAllText(dataPath, writeArray);
    }
    // �N�G�X�g�N���A�󋵂��󒍒��N�G�X�gID�����Ƃɏ�������
    // ��  ���F�Ȃ�
    // �߂�l�F�Ȃ�
    public void WriteCompleteQuest()
    {
        WriteQuestFile(nowOrderReceivedQuestID, false, true);
    }
    // �G��|�������ɌĂ�ŃN�G�X�g��i�߂�
    // ��  ���Fchar  �|�����G�̓������A���t�@�x�b�g1����
    //         S(�X�P���g��)�AG(�S�u����)�AB(�o���p�C�A)�AO(�I�[�N)�AH(�D�I�[�N)
    // �߂�l�F�Ȃ�
    public void KnockEnemy(char initialChar)
    {
        if (nowOrderReceivedQuestID == 101 && initialChar == 'S')
        {
            if (SEnmNum > 0)
            {
                SEnmNum--;
            }
        }
        else if (nowOrderReceivedQuestID == 102 && initialChar == 'G')
        {
            if (GEnmNum > 0)
            {
                GEnmNum--;
            }
        }
        else if (nowOrderReceivedQuestID == 103 && initialChar == 'B')
        {
            if (BEnmNum > 0)
            {
                BEnmNum--;
            }
        }
        else if (nowOrderReceivedQuestID == 104 && initialChar == 'O')
        {
            if (OEnmNum > 0)
            {
                OEnmNum--;
            }
        }
        else if (nowOrderReceivedQuestID == 105 && initialChar == 'H')
        {
            if (HEnmNum > 0)
            {
                HEnmNum--;
            }
        }
    }
    // �󒍒��̃N�G�X�gID�A���O�̓o�^
    // ��  ���Fint ID   ID�ԍ�
    // �߂�l�F�Ȃ�
    public void AllocationOrderReceivedID(int ID)
    {
        // ID�̓o�^
        nowOrderReceivedQuestID = ID;
        // ID����N�G�X�g���̓o�^
        switch (ID)
        {
            case 101:
                nowOrderReceivedQuestNameText.text = "�X�P���g���̓���";
                break;
            case 102:
                nowOrderReceivedQuestNameText.text = "�S�u�����̓���";
                break;
            case 103:
                nowOrderReceivedQuestNameText.text = "�o���p�C�A�̓���";
                break;
            case 104:
                nowOrderReceivedQuestNameText.text = "�I�[�N�̓���";
                break;
            case 105:
                nowOrderReceivedQuestNameText.text = "�D�I�[�N�̓���";
                break;
            default:
                nowOrderReceivedQuestNameText.text = "�Ȃ�";
                break;
        }
    }
}