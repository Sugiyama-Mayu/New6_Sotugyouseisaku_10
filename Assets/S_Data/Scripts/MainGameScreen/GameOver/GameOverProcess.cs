using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
// M.S �Q�[���I�[�o�[��ʂ̕\��
public class GameOverProcess : MonoBehaviour
{
    // UI��J�����Ȃǂ�mainSceneObj�X�N���v�g��1�̃I�u�W�F�N�g�ɃA�^�b�`����
    // �g�p���Ă��܂��B
    // prefab��mainSceneObj�A�^�b�`�I�u�W�F�N�g��u���Ă���̂�
    // �K�v�ȏꍇ�͊�{�̓V�[��1�g�p���Ă��������B
    [SerializeField]private MainSceneObj mainSceneObj;
    [SerializeField] private TitleSceneButton titleSceneButton;
    [SerializeField] private GameOverButton gameOverButton;
    [SerializeField] private RingSound ringSound;
    [SerializeField] private GameObject titleButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TitleUI titleUI;
    public bool callTitleFlag;  //�^�C�g�����Ăԃt���O
    public bool gameOverMode;   //�Q�[���I�[�o�[�ɂ���t���O
    private int selectButtonNum;//�I�𒆂̃{�^������
    private int oldButtonNum;   //�I�����Ă����{�^������
    private void Start()
    {
        gameOverMode = false;
        callTitleFlag = false;
        selectButtonNum = 0;
        oldButtonNum = 0;
    }
    private void Update()
    {
        if(gameOverMode == true)
        {
            Vector2 wheelRotateNum = Mouse.current.scroll.ReadValue();
            //�z�C�[���̉�]�ɂ���ă{�^����I��
            if (wheelRotateNum.y > 0.0f)
            {
                selectButtonNum--;
            }
            else if (wheelRotateNum.y < 0.0f)
            {
                selectButtonNum++;
            }
            //�I�𐔎����͈͓��ɂȂ��ꍇ�A�͈͓��ɐݒ�
            if (selectButtonNum < 0)
            {
                selectButtonNum = 0;
            }
            else if (selectButtonNum > 1)
            {
                selectButtonNum = 1;
            }
            //�I���{�^�����ς���Ă����猳�̃{�^���̐F��߂�
            if (oldButtonNum != selectButtonNum)
            {
                switch (oldButtonNum)
                {
                    case 0:
                        continueButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        break;
                    case 1:
                        titleButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        break;
                }
            }
            //�I�𒆃{�^���̐F��ύX
            switch (selectButtonNum)
            {
                case 0:
                    continueButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                    break;
                case 1:
                    titleButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                    break;
            }
            //�{�^�����菈��
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                switch (selectButtonNum)
                {
                    case 0:
                        gameOverButton.ContinueGame();
                        continueButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                        break;
                    case 1:
                        gameOverButton.ToTitle();
                        titleButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                        break;
                }
            }
            //�{�^�������̍X�V
            oldButtonNum = selectButtonNum;
        }
        //�^�C�g�����Ă�
        if (callTitleFlag == true)
        {
            titleUI.TitleControl(true);
            callTitleFlag = false;
        }
    }
    // �Q�[���I�[�o�[��ʂ̕\��
    // ��  ���F�Ȃ�
    // �߂�l�F�Ȃ�
    public void CallGameOver()
    {
        continueButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        titleButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        gameOverMode = true;
        ringSound.RingBGM(5);
        // �Q�[�����̂��̂��\��
        mainSceneObj.player.SetActive(false);
        mainSceneObj.questCanvas.SetActive(false);
        // �^�C�g���J��������\��
        mainSceneObj.switchTitleCamera.gameObject.SetActive(true);
        mainSceneObj.gameOverCanvas.gameObject.SetActive(true);
        mainSceneObj.switchTitleCamera.transform.position = mainSceneObj.player.transform.position;
        mainSceneObj.switchTitleCamera.transform.localEulerAngles = new Vector3(-25.0f, 0.0f, 0.0f);
        //gameOverCanvas.transform.position = new Vector3(gameOverCanvas.transform.position.x, -40.0f, gameOverCanvas.transform.position.z);
        //titleSceneButton.VisibleCursor(true);
    }
}
