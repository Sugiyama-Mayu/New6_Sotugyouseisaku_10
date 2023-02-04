using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S �A�C�e���h���b�v������(DropItem)�̃}�l�[�W���[
// ��{��̃I�u�W�F�N�g�ɂ̂݃A�^�b�`���Ă�������
public class DropItemManager : MonoBehaviour
{
    // ��A�є�A�؃h���b�v�I�u�W�F�͔�A�N�e�B�u�Ŕz�u���Ă�������
    public GameObject smallLeatherDrop;  // ��
    public GameObject middleLeatherDrop;
    public GameObject bigLeatherDrop;
    /* ���͔�݂̂̎���
 public GameObject smallFurDrop;      // �є�
 public GameObject middleFurDrop;
 public GameObject bigFurDrop;
 public GameObject smallScaleDrop;    // ��
 public GameObject middleScaleDrop;
 public GameObject bigScaleDrop;*/
    public int countLimit = 30;   // �h���b�v��ɃI�u�W�F���폜����鎞�Ԃ̃J�E���g
    [SerializeField] private ConnectionFile connectionFile;
    private void Start()
    {
       
    }
    // �g�p���Ȃ�
    // �G���|�ꂽ���ɌĂяo���h���b�v�A�C�e������
    // ��  ���Fchar    enmSize  �A�C�e���𗎂Ƃ��G�l�~�[�̑傫��('��'or'��'or'��')
    //         Vector3 enmPos   �A�C�e���𗎂Ƃ��G�l�~�[������ʒu
    // �߂�l�F�Ȃ�
    // ���G�l�~�[���A�N�e�B�u���̓f�X�g���C����O�ɌĂяo���ĉ�����
    public void DropItemFunc(char enmSize/*, char enmKind*/, Vector3 enmPos)
    {
        int dropItemNum = 1;   // ���͔�̂�
        /* �h���b�v�A�C�e�����ȊO���o���ꍇ�g�p����
        switch (enmKind)
        {
            case '��':
                dropItemNum = 1;
                break;
            case '��':
                dropItemNum = 2;
                break;
            case '��':
                dropItemNum = 3;
                break;
        }*/
        // 1�`10�̃����_���̐��l���o��
        int rnd = Random.Range(1, 11);
        GameObject InstantObj = null;
        // �G�̑傫���ŗ��Ƃ��A�C�e���𕪂���
        switch (enmSize)
        {
            case '��':
                if (rnd >= 8)
                {
                    switch (dropItemNum)
                    {
                        case 1:  // ��
                            InstantObj = Instantiate(smallLeatherDrop, enmPos, Quaternion.identity);
                            WriteFileDropItem(501);  // �h���b�v�A�C�e���̐����X�V���ăt�@�C���ɏ�������
                            break;
                        case 2:  // �є�
                            WriteFileDropItem(502);  // �h���b�v�A�C�e���̐����X�V���ăt�@�C���ɏ�������
                            break;
                        case 3:  // ��
                            WriteFileDropItem(503);  // �h���b�v�A�C�e���̐����X�V���ăt�@�C���ɏ�������
                            break;
                    }
                    InstantObj.SetActive(true);
                    InstantObj.GetComponent<DropItem>().doneDrop = true;
                }
                break;
            case '��':
                if (rnd >= 9)
                {
                    switch (dropItemNum)
                    {
                        case 1:  // ��
                            InstantObj = Instantiate(middleLeatherDrop, enmPos, Quaternion.identity);
                            WriteFileDropItem(504);  // �h���b�v�A�C�e���̐����X�V���ăt�@�C���ɏ�������
                            break;
                        case 2:  // �є�
                            WriteFileDropItem(505);  // �h���b�v�A�C�e���̐����X�V���ăt�@�C���ɏ�������
                            break;
                        case 3:  // ��
                            WriteFileDropItem(506);  // �h���b�v�A�C�e���̐����X�V���ăt�@�C���ɏ�������
                            break;
                    }
                    InstantObj.SetActive(true);
                    InstantObj.GetComponent<DropItem>().doneDrop = true;
                }
                break;
            case '��':
                if (rnd >= 10)
                {
                    switch (dropItemNum)
                    {
                        case 1:  // ��
                            InstantObj = Instantiate(bigLeatherDrop, enmPos, Quaternion.identity);
                            WriteFileDropItem(507);  // �h���b�v�A�C�e���̐����X�V���ăt�@�C���ɏ�������
                            break;
                        case 2:  // �є�
                            WriteFileDropItem(508);  // �h���b�v�A�C�e���̐����X�V���ăt�@�C���ɏ�������
                            break;
                        case 3: // ��
                            WriteFileDropItem(509);  // �h���b�v�A�C�e���̐����X�V���ăt�@�C���ɏ�������
                            break;
                    }
                    InstantObj.SetActive(true);
                    InstantObj.GetComponent<DropItem>().doneDrop = true;
                }
                break;
        }

    }

    // �h���b�v�A�C�e���p�t�@�C���������݊֐�(�h���b�v�A�C�e�����ǉ�(+1)�������܂�)
    // ��  ���Fint id   �������ރA�C�e����ID
    // �߂�l�F�Ȃ�
    void WriteFileDropItem(int id)
    {
        string array = "";
        int kindNum = 5;
        // �h���b�v�A�C�e���f�[�^�̓ǂݍ���
        array = connectionFile.ReadFile(id, array);
        connectionFile.TranslationDataArray(array, kindNum);
        connectionFile.haveNum++;   // �h���b�v�A�C�e���̐���+1
        // �ǂݍ��񂾃f�[�^���當������쐬
        string writeArray = connectionFile.id.ToString() + ',' + connectionFile.itemName + ','
            + connectionFile.haveNum.ToString() + ',' + connectionFile.Explanation + '\n';
        // �t�@�C���ɏ�������
        connectionFile.WriteFile(id, writeArray);
    }
}
