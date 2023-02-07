using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
// M.S
// �f�[�^�x�[�X�̓ǂݍ��݁E��������
// ��{��̃I�u�W�F�N�g�ɂ̂݃A�^�b�`���Ă�������
public class ConnectionFile : MonoBehaviour
{
    string dataPath1;                // �e�L�X�g�t�@�C���̃p�X
    public int itemNum;              // �ǂݍ��ޔz��̐�
    public int itemKindNum;          // �ǂݍ��ޔz��̐�
    private const int IdDigit = 3;   // ID�̌���
    private const int UpperId = 724; // ID�̏��
    private const int LowerId = 101; // ID�̉���

    // TranslationDataArray���s���A�e�ϐ��������Ɋi�[
    public int kindNum;
    public int id;
    public string itemName;
    public string enmName;
    public int buyNum;
    public int sellPrice;
    public int buyPrice;
    public string Explanation;
    public string Endurance;
    public int upperLuck;
    public int lowerLuck;
    public int haveNum;
    public int hp;
    public int attack;
    public int defense;
    public int stun;
    public int drop;
    public string dropItem;

    public bool doneDataPath = false;
    void Start()
    {
        // �e�L�X�g�f�[�^�̃p�X�擾
        dataPath1 = Application.dataPath + "/StreamingAssets/gameData.txt";
        doneDataPath = true;
        //SetAllMaterialNum(false,1,99999,27,10,10);
        //test = SetMaterialNum(true,"��",100);
        //testNum = GetMaterialNum("��");
        //GetEnmName_DropItemName(305);
    }
    // �f�[�^�ׁ\�X�������݊֐�
    // ��  ���Fint id         �������ރf�[�^��ID�ԍ�
    //         int writeArray �������ރf�[�^(�s����) ��F101,��,5,�̗͏���\n
    // �߂�l�F�Ȃ�
    // ��  �ӁF�������ރf�[�^�̓f�[�^�x�[�X�̍s�f�[�^�ƕ���������v������
    //         �Ō�ɉ��s��Y�ꂸ��
    //         ,�ŋ�؂�
    //         �����̐������͂Ƃ肠����10���ɐݒ肵�Ă���܂�
    public void WriteFile(int id, string writeArray)
    {
        // �ǂݍ��ݏI����������̍ŐV�ʒu
        int lastNNum = 0;
        // �ǂݍ��ݏI���f�[�^�̍ŐVID(�ʒu)
        int nNum = 100;
        // �������ރf�[�^�̔z��v�f�ʒu
        int refeNum = 0;
        // �f�[�^�x�[�X�e�L�X�g�t�@�C���ǂݍ���
        StreamReader fs = new StreamReader(dataPath1);

        string allReadArray = fs.ReadToEnd();  // �t�@�C������f�[�^�����ׂēǂݍ���
        char[] charAllReadArray = allReadArray.ToCharArray(); // �ǂݍ��񂾃f�[�^��char�ɕϊ�
        char[] charWriteArray = writeArray.ToCharArray(); // �������ރf�[�^��char�ɕϊ�
        string errorJudgeArray = ""; // �G���[����p�z��

        // �������݃f�[�^��3����ID�����o��
        for (int num = 0; num < IdDigit; num++)
        {
            errorJudgeArray = errorJudgeArray + writeArray[num];
        }

        // ����ID�̃G���[����
        if (id < LowerId || id > UpperId)
        {
            Debug.Log("�w��ID���͈͊O�ł�");
            return;
        }
        else
        {
            int errorJudgeWriteId = 0;
            // ��������ID������o����ID��int�ɂ���
            errorJudgeWriteId = Convert.ToInt32(errorJudgeArray);
            if (errorJudgeWriteId < LowerId || errorJudgeWriteId > UpperId)
            {
                Debug.Log("'" + errorJudgeWriteId + "'" + "�������ݎw��ID���͈͊O�ł�");
                return;
            }
        }

        for (int i = 0; i <= allReadArray.Length; i++)
        {
            // �ǂݍ���ł���ID���Ώۂ�ID�ƈ�v����ꍇ
            if (id <= nNum)
            {
                // �������ރf�[�^�̃f�[�^�x�[�X�ł̍ŏ��̈ʒu���v�Z
                int numArr = lastNNum - (writeArray.Length - 1);
                refeNum = 0;
                for (; numArr < lastNNum; numArr++)
                {
                    // �ǂݍ��񂾃f�[�^�x�[�X�̃f�[�^�ɏ������݃f�[�^����
                    charAllReadArray[numArr] = charWriteArray[refeNum];
                    refeNum++;
                    // �f�[�^�̑�����I����Ă���ꍇ
                    if (numArr + 1 >= lastNNum)
                    {
                        string finalStringData = "";
                        // ������I������f�[�^��string�ɕϊ�
                        for (int numArr2 = 0; numArr2 < charAllReadArray.Length; numArr2++)
                        {
                            finalStringData = finalStringData + charAllReadArray[numArr2];
                        }
                        fs.Close();
                        // �f�\�^����������
                        File.WriteAllText(dataPath1, finalStringData);
                        return;
                    }
                }
            }
            else if (charAllReadArray[i] == '\n')
            {
                // ���݂̓ǂݍ���ID�ɍ��킹�Ď���ID��ݒ�
                if (nNum == 117)
                {
                    nNum = 201;
                    lastNNum = i;
                }
                else if (nNum == 210)
                {
                    nNum = 301;
                    lastNNum = i;
                }
                else if (nNum == 305)
                {
                    nNum = 401;
                    lastNNum = i;
                }
                else if (nNum == 407)
                {
                    nNum = 501;
                    lastNNum = i;
                }
                else if (nNum == 522)
                {
                    nNum = 601;
                    lastNNum = i;
                }
                else if (nNum == 610)
                {
                    nNum = 701;
                    lastNNum = i;
                }
                else
                {
                    lastNNum = i;
                    nNum++;
                }
            }
        }
        fs.Close();
        return;
    }

    // �f�[�^�x�[�X�ǂݍ��݊֐�_1
    // �^����ꂽID�̃f�[�^��ǂݍ��݁A�z��ɓ����
    // ��  ���Fint id        �ǂݍ���ID�ԍ�
    //         int dataArray �ǂݍ��񂾃f�[�^���������ޔz��
    // �߂�l�Fstring        ID�ԍ��̓ǂݍ��񂾃f�[�^��Ԃ�
    public string ReadFile(int id, string dataArray)
    {
        string idNumStr = "";
        int idNum = 0;
        var fs = new StreamReader(dataPath1, System.Text.Encoding.GetEncoding("UTF-8"));
        for (int i = 0; i < 100; i++)
        {
            // �ړI��ID�܂ōs��ǂݍ���
            dataArray = fs.ReadLine();
            for (int num = 0; num < 3; num++)
            {
                idNumStr = idNumStr + dataArray[num];
                idNum = Convert.ToInt32(idNumStr);
            }
            // �ǂݍ���ID�ԍ��Ǝw�肵��ID�ԍ�����v���Ă����炻�̔z���Ԃ�
            if (idNum == id)
            {
                fs.Close();
                return dataArray;
            }
            idNumStr = "";
        }
        fs.Close();
        Debug.Log("ID�ɂ��Ă͂܂�f�[�^������܂���ł���");
        return null;
    }

    // �f�[�^�x�[�X�ǂݍ��݊֐�_2
    // 1�œǂݍ��񂾃f�[�^�x�[�X�z����e�ϐ��ɕ�����֐�
    // ��  ���Fstring dataArray   �ϐ��ɕ�����f�[�^�x�[�X�z��
    //         int    kindNum     �f�[�^�x�[�X���ǂ̂悤�ȃf�[�^���̐���
    //                            (ID�̓����� ��:ID��208�Ȃ��2)
    // �߂�l�Fbool               �I�����true��Ԃ�
    public bool TranslationDataArray(string dataArray, int kindNum)
    {
        // �e�f�[�^��ݒ肷��ۂɃf�[�^�x�[�X�𒲂ׂ�z��
        string translateArray = "";
        string translateluckArray = "";  // �K�^�l�p
        bool luckFlag = false;           // �K�^�n�̏���A�����i�[�t���O
        bool logFlag = false;            // ���O���c���t���O
        int nextNum = 0;  // �ݒ肷��ϐ��̐�
        int loadNum = 0;  // �w�肷��z��v�f�̒l
        int nextNumUpper = 0;  // �ݒ肷��e�ϐ��̐��̏��
        switch (kindNum)
        {
            case 1:
                nextNumUpper = 6;
                break;
            case 2:
                nextNumUpper = 7;
                break;
            case 3:
                nextNumUpper = 3;
                break;
            case 5:
                nextNumUpper = 4;
                break;
            case 6:
                nextNumUpper = 6;
                break;
        }
        switch (kindNum)
        {
            // �V���b�v�A�����A�C�e��
            case 1:
            case 5:
                for (nextNum = 0; nextNum < nextNumUpper; nextNum++)
                {
                    translateArray = "";
                    for (; loadNum < dataArray.Length; loadNum++)
                    {
                        if (dataArray[loadNum] == ',')
                        {
                            loadNum++;
                            break;
                        }
                        else
                        {
                            translateArray = translateArray + dataArray[loadNum];
                        }
                    }
                    switch (nextNum)
                    {
                        case 0:  // id
                            id = Convert.ToInt32(translateArray);
                            break;
                        case 1:  // �A�C�e����
                            itemName = translateArray;
                            break;
                        case 2:  // ��
                            if (kindNum == 1)
                            {
                                buyNum = Convert.ToInt32(translateArray);
                            }
                            else if (kindNum == 5)
                            {
                                haveNum = Convert.ToInt32(translateArray);
                            }
                            break;
                        case 3:  // ������
                            Explanation = translateArray;
                            if (nextNumUpper == 4)
                            {
                                Debug.Log("�ύX���ꂽ�l   id,itemName,haveNum,Explanation");
                            }
                            break;
                        case 4: // �����l
                            buyPrice = Convert.ToInt32(translateArray);
                            break;
                        case 5: // ���l
                            sellPrice = Convert.ToInt32(translateArray);
                            Debug.Log("�ύX���ꂽ�l   id,itemName,buyNum,Explanation,sellPrice,buyPrice");
                            break;
                    }
                }
                break;
            // �V���b�v(����)�A��������
            case 2:
            case 6:
                for (nextNum = 0; nextNum < nextNumUpper; nextNum++)
                {
                    translateArray = "";
                    for (; loadNum < dataArray.Length; loadNum++)
                    {
                        if (dataArray[loadNum] == ',')
                        {
                            loadNum++;
                            break;
                        }
                        else
                        {
                            translateArray = translateArray + dataArray[loadNum];
                        }
                    }
                    switch (nextNum)
                    {
                        case 0: //id
                            id = Convert.ToInt32(translateArray);
                            break;
                        case 1: // �A�C�e����
                            itemName = translateArray;
                            break;
                        case 2: // ��
                            if (kindNum == 2)
                            {
                                buyNum = Convert.ToInt32(translateArray);
                            }
                            else if (kindNum == 6)
                            {
                                haveNum = Convert.ToInt32(translateArray);
                            }
                            break;
                        case 3: // ������
                            Explanation = translateArray;
                            break;
                        case 4: // �ϋv�x(����)
                            Endurance = translateArray;
                            break;
                        case 5: // �K�^�l
                            for (int i = 0; i < translateArray.Length; i++)
                            {
                                if (translateArray[i] == '�`')
                                {
                                    lowerLuck = Convert.ToInt32(translateluckArray);
                                    translateluckArray = "";
                                    i++;
                                    luckFlag = true;
                                }
                                if (luckFlag == false)
                                {
                                    translateluckArray = translateluckArray + translateArray[i];
                                }
                                else if (luckFlag == true)
                                {
                                    translateluckArray = translateluckArray + translateArray[i];

                                }
                                if (i + 1 >= translateArray.Length)
                                {
                                    upperLuck = Convert.ToInt32(translateluckArray);
                                    luckFlag = false;
                                    translateluckArray = "";
                                    if (nextNumUpper == 6)
                                    {
                                        Debug.Log("�ύX���ꂽ�l   id,itemName,haveNum,Explanation,Endurance,lowerLuck,upperLuck");
                                    }
                                    break;
                                }
                            }
                            break;
                        case 6: // �����l
                            buyPrice = Convert.ToInt32(translateArray);
                            Debug.Log("�ύX���ꂽ�l   id,itemName,buyNum,Explanation,Endurance,lowerLuck,upperLuck,buyPrice");
                            break;
                    }
                }
                break;
            // �G
            case 3:
                for (nextNum = 0; nextNum < nextNumUpper; nextNum++)
                {
                    translateArray = "";
                    for (; loadNum < dataArray.Length; loadNum++)
                    {
                        if (dataArray[loadNum] == ',')
                        {
                            loadNum++;
                            break;
                        }
                        else
                        {
                            translateArray = translateArray + dataArray[loadNum];
                        }
                    }
                    switch (nextNum)
                    {
                        case 0: // id
                            id = Convert.ToInt32(translateArray);
                            break;
                        case 1: // ���O
                            enmName = translateArray;
                            break;
                        case 2: // �h���b�v�A�C�e��
                            dropItem = translateArray;
                            Debug.Log("�ύX���ꂽ�l   id,enmName,dropItem");
                            break;
                    }
                }
                break;
            // ��(��؂Ȃ���)
            case 4:
                for (nextNum = 0; nextNum < 4; nextNum++)
                {
                    translateArray = "";
                    for (; loadNum < dataArray.Length; loadNum++)
                    {
                        if (dataArray[loadNum] == ',')
                        {
                            loadNum++;
                            break;
                        }
                        else
                        {
                            translateArray = translateArray + dataArray[loadNum];
                        }
                    }
                    switch (nextNum)
                    {
                        case 0:  // id
                            id = Convert.ToInt32(translateArray);
                            break;
                        case 1:  // �A�C�e����
                            itemName = translateArray;
                            break;
                        case 2:  // ������
                            Explanation = translateArray;
                            break;
                        case 3:  // ������
                            haveNum = Convert.ToInt32(translateArray);
                            Debug.Log("�ύX���ꂽ�l   id,itemName,Explanation,haveNum");
                            break;
                    }
                }
                break;
            // ����
            case 7:
                for (nextNum = 0; nextNum < 5; nextNum++)
                {
                    translateArray = "";
                    for (; loadNum < dataArray.Length; loadNum++)
                    {
                        if (dataArray[loadNum] == ',')
                        {
                            loadNum++;
                            break;
                        }
                        else
                        {
                            translateArray = translateArray + dataArray[loadNum];
                        }
                    }
                    switch (nextNum)
                    {
                        case 0:  // id
                            id = Convert.ToInt32(translateArray);
                            break;
                        case 1:  // �A�C�e����
                            itemName = translateArray;
                            break;
                        case 2:  // ������
                            haveNum = Convert.ToInt32(translateArray);
                            break;
                        case 3:  // �U����or�h���(�����ɍ��킹��)
                            if (id <= 712)
                            {
                                attack = Convert.ToInt32(translateArray);
                                logFlag = true;
                            }
                            else
                            {
                                defense = Convert.ToInt32(translateArray);
                            }
                            break;
                        case 4:  // ������
                            Explanation = translateArray;
                            if (logFlag == true)
                            {
                                Debug.Log("�ύX���ꂽ�l   id,itemName,haveNum,attack,Explanation");

                            }
                            else
                            {
                                Debug.Log("�ύX���ꂽ�l   id,itemName,haveNum,defense,Explanation");
                            }
                            logFlag = false;
                            break;
                    }
                }
                break;
        }
        return true;
    }
    //*******
    // ID����G�l�~�[���ƃh���b�v�A�C�e�����̎擾
    // ���̃X�N���v�g��enmName��dropItem�Ɋi�[
    // ��  ���Fint id  ID�ԍ�(301:�X�P���g�� 302:�S�u���� 303:�o���p�C�A 
    //                        304:�I�[�N     305:�D�I�[�N)
    // �߂�l�F�Ȃ�
    public void GetEnmName_DropItemName(int id)
    {
        string array = "";
        TranslationDataArray(ReadFile(id, array), 3);
    }
    // �f�ނ̐����擾����
    // ��  ���Fstring materialName ��������m�肽���f�ނ̖��O(��or��or��or�є�or��)
    // �߂�l�Fint                 ���̈����Ŏw�肵���f�ނ̏�����
    //                             (�߂�l��-1�̏ꍇ�AmaterialName���Ԉ���Ă���)
    public int GetMaterialNum(string materialName)
    {
        int id = -1;
        string array = "";
        int haveNum = -1;
        switch (materialName)
        {
            case "��":
                id = 501;
                break;
            case "��":
                id = 502;
                break;
            case "��":
                id = 503;
                break;
            case "�є�":
                id = 504;
                break;
            case "��":
                id = 505;
                break;
            case "��":
                id = 519;
                break;
            case "��":
                id = 520;
                break;
            case "��":
                id = 521;
                break;
        }
        if (id >= 0)
        {
            TranslationDataArray(ReadFile(id, array), 5);
            haveNum = this.haveNum;
        }
        return haveNum;
    }
    // �f�ނ̐������炵���葫������
    // ��  ���Fbool   minusOrPlusMode  true ����  false ����
    //         string materialName     ����ύX�������f�ނ̖��O(��or��or��or�є�or��)
    //         int    minusOrPlusNum   ������or������
    // �߂�l�Fbool           true  �f�ނ̌��炵��or���₵�����̏������ݐ���
    //                        false �������ݕs����(������������Ȃ�������AmaterialName���Ԉ���Ă���) 
    public bool SetMaterialNum(bool minusOrPlusMode, string materialName, int minusOrPlusNum)
    {
        int id = -1;
        string array = "";
        int haveNum = -1;
        string haveNumStr = "";
        switch (materialName)
        {
            case "��":
                id = 501;
                break;
            case "��":
                id = 502;
                break;
            case "��":
                id = 503;
                break;
            case "�є�":
                id = 504;
                break;
            case "��":
                id = 505;
                break;
            case "��":
                id = 506;
                break;
            case "�񕜖�":
                id = 507;
                break;
            case "��񕜖�":
                id = 508;
                break;
            case "���S�񕜖�":
                id = 509;
                break;
            case "��":
                id = 519;
                break;
            case "��":
                id = 520;
                break;
            case "��":
                id = 521;
                break;
        }
        if (id >= 0)
        {
            TranslationDataArray(ReadFile(id, array), 5);
            haveNum = this.haveNum;
            if (minusOrPlusMode == false)
            {
                haveNum = haveNum - minusOrPlusNum;
                if (haveNum < 0) //���͒l�ȉ�(0����)�̏ꍇ�����s��
                {
                    int writeNum = 0;
                    writeNum = GetMaterialNum(materialName);
                    SetMaterialNum(false, materialName, writeNum);
                    return false;
                }
                if (haveNum < 10)
                {
                    haveNumStr = "0" + haveNum.ToString();
                }
                else
                {
                    haveNumStr = haveNum.ToString();
                }
                array = id.ToString() + "," + materialName + ","
                    + haveNumStr + "," + Explanation + "\n";
                WriteFile(id, array);
                return true;
            }
            else
            {
                haveNum = haveNum + minusOrPlusNum;
                if (haveNum >= 100) //���͒l�ȏ�(100�ȏ�)�̏ꍇ�������݂�MAX�l(99)
                {
                    int writeNum = 0;
                    writeNum = 99 - GetMaterialNum(materialName);
                    SetMaterialNum(true, materialName, writeNum);
                    return false;
                }
                if (haveNum < 10)
                {
                    haveNumStr = "0" + haveNum.ToString();
                }
                else
                {
                    haveNumStr = haveNum.ToString();
                }
                array = id.ToString() + "," + materialName + ","
                       + haveNumStr + "," + Explanation + "\n";
                WriteFile(id, array);
                return true;
            }
        }
        return false;
    }
    // ��s�őS�Ă̑f�ނ̐��̕ύX������
    // ��  ���Fbool minusOrPlusMode  true ���� false ����
    //         int honeNum�AkawaNum�AkibaNum�AkegawaNum�AtumeNum �e�f�ނ̑��������͈�����
    //         (�ϐ��ɑΉ�����f�ނɂ��ā� honeNum(��)�AkawaNum(��)�AkibaNum(��)�AkegawaNum(�є�)�AtumeNum(��))
    // �߂�l�F�Ȃ�
    public void SetAllMaterialNum(bool minusOrPlusMode, int honeNum, int kawaNum, int kibaNum, int kegawaNum, int tumeNum ,int douNum, int ginNum, int kinNum)
    {
        //(��or��or��or�є�or��)
        //501 ��
        SetMaterialNum(minusOrPlusMode, "��", honeNum);
        //502 ��
        SetMaterialNum(minusOrPlusMode, "��", kawaNum);
        //503 ��
        SetMaterialNum(minusOrPlusMode, "��", kibaNum);
        //504 �є�
        SetMaterialNum(minusOrPlusMode, "�є�", kegawaNum);
        //505 ��
        SetMaterialNum(minusOrPlusMode, "��", tumeNum);
        //519 ��
        SetMaterialNum(minusOrPlusMode, "��", douNum);
        //520 ��
        SetMaterialNum(minusOrPlusMode, "��", ginNum);
        //521 ��
        SetMaterialNum(minusOrPlusMode, "��", kinNum);
    }

    // �K�̃N���A����Ԃ�
    // ��  ���F�Ȃ�
    // �߂�l�Fint   �K�̃N���A��
    public int GetClearShrineNum()
    {
        string array = "";
        int clearNum = 0;  //�N���A�K��
        for (int i = 0; i < 7; i++)
        {
            TranslationDataArray(ReadFile(401 + i, array), 4);
            if (haveNum >= 1)
            {
                clearNum++;
            }
        }
        return clearNum;
    }
}