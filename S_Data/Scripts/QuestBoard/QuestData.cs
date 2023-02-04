using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// M.S
// �e�N�G�X�g�f�[�^����
public class QuestData : MonoBehaviour
{
    [SerializeField] private int questID;
    private string questName;       // �N�G�X�g��
    private string questExplanation;// �N�G�X�g����
    [SerializeField] private Text questNameText;         // �N�G�X�g��
    [SerializeField] private Text questExplanationText;  // �N�G�X�g����
    [SerializeField] private ConnectionQuestFile connectionQuestFile;
    [SerializeField] private VillageColl villageColl;
    // �N�G�X�g�{�[�h�Ŏg�p����I�u�W�F�N�g
    [SerializeField] private GameObject confirmationBack;
    [SerializeField] private GameObject confirmationButtons;
    [SerializeField] private GameObject BoardBackButton;
    [SerializeField] private GameObject orderReceivedButton;
    [SerializeField] private GameObject buttonTexts;
    [SerializeField] private GameObject questOrderBack;
    [SerializeField] private GameObject completeImage;
    [SerializeField] private GameObject recivedStamp;
    [SerializeField] private bool villageCollFlag;

    void Start()
    {
        // �N�G�X�gID�ɍ��킹�ăN�G�X�g�f�[�^�𓖂Ă͂߂�
        switch (questID)
        {
            case 101:
                questName = "�X�P���g���̓���";
                questExplanation = "�X�P���g����5�̓������Ăق���";
                break;
            case 102:
                questName = "�S�u�����̓���";
                questExplanation = "�S�u������5�̓������Ăق���";
                break;
            case 103:
                questName = "�o���p�C�A�̓���";
                questExplanation = "�o���p�C�A�̓�����5�̓������Ăق���";
                break;
            case 104:
                questName = "�I�[�N�̓���";
                questExplanation = "�I�[�N��5�̓������Ăق���";
                break;
            case 105:
                questName = "�D�I�[�N�̓���";
                questExplanation = "�D�I�[�N��5�̓������Ăق���";
                break;
        }
        questNameText.text = questName;
    }
    // �N���b�N(�󒍏���)
    // ��  ���F�Ȃ�
    // �߂�l�F�Ȃ�
    public bool ClickOrderReceived()
    {
        // �O�̎󒍃N�G�X�g������
        if (connectionQuestFile.TranslationQuestDataArray(connectionQuestFile.ReadQuestFile(connectionQuestFile.nowOrderReceivedQuestID))) {
            connectionQuestFile.WriteQuestFile(connectionQuestFile.nowOrderReceivedQuestID, false, connectionQuestFile.resolutionFlag);
        }
        // �N�G�X�g��
        if (connectionQuestFile.TranslationQuestDataArray(connectionQuestFile.ReadQuestFile(questID))) {
            connectionQuestFile.WriteQuestFile(connectionQuestFile.questIdNum, true, connectionQuestFile.resolutionFlag);
            connectionQuestFile.AllocationOrderReceivedID(questID);
            return true;
        } 
        return false;
    }
    // �N���b�N(���e�m�F)
    public void ClickConfirmation()
    {
        // �N�G�X�g�I����ʃI�u�W�F��؂�Ԃ�
        confirmationBack.SetActive(false);
        buttonTexts.SetActive(false);
        confirmationButtons.SetActive(false);
        questExplanationText.gameObject.SetActive(true);
        BoardBackButton.SetActive(true);
        questOrderBack.SetActive(true);
        // �I�𒆃N�G�X�g�̕������o�^
        connectionQuestFile.TranslationQuestDataArray(connectionQuestFile.ReadQuestFile(questID));
        // �N�G�X�g���N���A�ς݂̏ꍇ
        if (connectionQuestFile.resolutionFlag == true)
        {
            completeImage.SetActive(true);  // �R���v���[�g�X�^���v�̕\��
        }
        // �N�G�X�g���󒍒��̏ꍇ
        else if(connectionQuestFile.orderFlag == true)
        {
            recivedStamp.SetActive(true);   // �󒍒��X�^���v�̕\��
        }
        else {
            orderReceivedButton.SetActive(true);
        }
        // �󒍒��N�G�X�g�̐�������o�^
        questExplanationText.text = questExplanation;
    }
    // �N���b�N(�N�G�X�g�I����ʂɖ߂�)
    // ��  ���F�Ȃ�
    // �߂�l�F�Ȃ�
    public void ClickBoardBack()
    {
        // �I�u�W�F�N�g�̕\���E��\������
        confirmationBack.SetActive(true);
        buttonTexts.SetActive(true);
        confirmationButtons.SetActive(true);
        questExplanationText.gameObject.SetActive(false);
        BoardBackButton.SetActive(false);
        orderReceivedButton.SetActive(false);
        questOrderBack.SetActive(false);
        completeImage.SetActive(false);
        recivedStamp.SetActive(false);
    }
}
