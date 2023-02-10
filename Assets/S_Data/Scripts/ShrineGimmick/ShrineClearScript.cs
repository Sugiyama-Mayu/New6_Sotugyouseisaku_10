using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// �M�~�b�N�N���A�ۑ�����
public class ShrineClearScript : MonoBehaviour
{
    [SerializeField] private ConnectionFile connectFile;
    [SerializeField] private RingSound ringSound;
    [SerializeField] GameObject jewelObj;   //�K��΃I�u�W�F
    [SerializeField] bool initialState;     //��Ε\�������t���O
    [SerializeField] private int shrineNum; //�KID�ԍ�
    private bool initialProcess; //�K��΂̏����\������
    public bool shrineClearFlag;
    private bool getJewelFlag;
    private bool onlyProcessFlag;
    void Start()
    {
        initialProcess = false;
        shrineClearFlag = false;
        onlyProcessFlag = true;
    }
    void Update()
    {
        string array = "";
        // �e�L�X�g�t�@�C���̃f�[�^�p�X���ݒ�ς�
        // �K��΂̏����\���ݒ肪�I����Ă��Ȃ�������
        if (connectFile.doneDataPath == true && initialProcess == false)
        {
            connectFile.TranslationDataArray(connectFile.ReadFile(shrineNum, array), 4); //�K��Ύ擾�󋵂𒲂ׂ�
            // �����擾���Ă����珉���ݒ肪�\���ł��\�������Ȃ�
            if (connectFile.haveNum >= 1)
            {
                jewelObj.SetActive(false);
            }
            else
            {
                jewelObj.SetActive(initialState);
            }
            initialProcess = true;
        }
        // ��΂���ɓ��ꂽ��(�\���t���O����������)��΂�\��
        if (getJewelFlag == false && onlyProcessFlag == true)
        {
            if (shrineClearFlag == true && jewelObj.activeSelf == false)
            {
                ringSound.RingSE(18);
                connectFile.TranslationDataArray(connectFile.ReadFile(shrineNum, array), 4); //�K��Ύ擾�󋵂𒲂ׂ�
                // �����擾���Ă�����\�������Ȃ�
                if (connectFile.haveNum >= 1)
                {
                    jewelObj.SetActive(false);
                }
                else
                {
                    jewelObj.SetActive(true);
                }
                onlyProcessFlag = false;
            }
        }
    }
    // ��Ε\���t���O�̃Z�b�^�[
    public void SetGetJewelFlag(bool active)
    {
        getJewelFlag = active;
    }
    // �K�̐����̃Z�b�^�[
    public void SetShrineNum(int getShrineNum)
    {
        shrineNum = getShrineNum;
    }
}
