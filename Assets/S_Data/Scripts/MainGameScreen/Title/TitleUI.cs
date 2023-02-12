using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// M.S
// �^�C�g����ʂ̃X�N���v�g
// (�^�C�g��UI�̕\���E��\�������A�^�C�g���J�����̈ړ�����)
public class TitleUI : MonoBehaviour
{
    // UI��J�����Ȃǂ�mainSceneObj�X�N���v�g��1�̃I�u�W�F�N�g�ɃA�^�b�`����
    // �g�p���Ă��܂��B
    // prefab��mainSceneObj�A�^�b�`�I�u�W�F�N�g��u���Ă���̂�
    // �K�v�ȏꍇ�͊�{�̓V�[��1�g�p���Ă��������B
    public bool titleMode = false;     // �^�C�g����ʂ�UI���g�p���鎞��true
    public float moveSpeed;            // UI�̈ړ��X�s�[�h
    bool returnMode;                   // true�Ȃ�΃J�����ړ��Ő܂�Ԃ�
    [SerializeField] private MainSceneObj mainSceneObj;
    [SerializeField] private GameObject plusReturnPos; // +�̐܂�Ԃ��ʒu
    [SerializeField] private GameObject minusReturnPos;// -�̐܂�Ԃ��ʒu
    private Vector3 titleCameraPos;
    private Vector3 titleCameraAngle;
    [SerializeField] private TitleSceneButton titleSceneButton;
    [SerializeField] private RingSound ringSound;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject continueButton;
    private int selectButtonNum;
    private int oldButtonNum;
    private bool titlePreparationFlag;
    void Start()
    {
        // ���̓X�N���v�g�Őݒ聫�ŏ��̓^�C�g������X�^�[�g
        titlePreparationFlag = false;
        moveSpeed = 1.2f;
        selectButtonNum = 0;
        oldButtonNum = 0;
        titleCameraPos = mainSceneObj.switchTitleCamera.transform.position;
        titleCameraAngle = mainSceneObj.switchTitleCamera.transform.localEulerAngles;
        if (GameObject.Find("GameManager").GetComponent<GameManager>().GetSetXRMode == false) TitleControl(true);
    }
    void Update()
    {
        // �^�C�g���������ꍇ�A�J�������ړ�������
        if (titleMode == true && titlePreparationFlag == true)
        {
            //titleSceneButton.VisibleCursor(true);
            if (mainSceneObj.switchTitleCamera.transform.position.x >= plusReturnPos.transform.position.x)
            {
                returnMode = true;
            }
            if (mainSceneObj.switchTitleCamera.transform.position.x <= minusReturnPos.transform.position.x)
            {
                returnMode = false;
            }
            if (returnMode == true)
            {
                mainSceneObj.switchTitleCamera.transform.Translate(-moveSpeed, 0.0f, 0.0f);
            }
            else if (returnMode == false)
            {
                mainSceneObj.switchTitleCamera.transform.Translate(moveSpeed, 0.0f, 0.0f);
            }

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
                        startButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        break;
                    case 1:
                        continueButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        break;
                }
            }
            //�I�𒆃{�^���̐F��ύX
            switch (selectButtonNum)
            {
                case 0:
                    startButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                    break;
                case 1:
                    continueButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                    break;
            }
            //�{�^�����菈��
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                switch (selectButtonNum)
                {
                    case 0:
                        titleSceneButton.StartGame();
                        continueButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                        break;
                    case 1:
                        titleSceneButton.ContinueGame();
                        continueButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                        break;
                }
            }
            //�{�^�������̍X�V
            oldButtonNum = selectButtonNum;
        }
    }
    // �^�C�g����ʂɐ؂�ւ�
    // ��  ���Fbool  true �^�C�g�����UI�A�J�[�\���\��  false ��\��
    // �߂�l�F�Ȃ�
    public void TitleControl(bool Control)
    {
        if (Control == true)
        {
            StartCoroutine("ControlTitle");
        }
        else if (Control == false)
        {
            SetTitleMode(false);
            mainSceneObj.player.SetActive(true);
            mainSceneObj.questCanvas.SetActive(true);
            mainSceneObj.titleCanvas.gameObject.SetActive(false);
            mainSceneObj.switchTitleCamera.gameObject.SetActive(false);
            //titleSceneButton.VisibleCursor(false);
        }
    }
    // �^�C�g�����[�h�̃Z�b�^�[
    void SetTitleMode(bool mode)
    {
        titleMode = mode;
    }
    IEnumerator ControlTitle()
    {
        SetTitleMode(true);
        titlePreparationFlag = false;
        yield return null;
        oldButtonNum = 0;
        selectButtonNum = 0;
        //�{�^���̐F��ݒ�
        startButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        continueButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        ringSound.RingBGM(0);
        mainSceneObj.player.SetActive(false);
        mainSceneObj.questCanvas.SetActive(false);
        mainSceneObj.titleCanvas.gameObject.SetActive(true);
        mainSceneObj.switchTitleCamera.gameObject.SetActive(true);
        //titleSceneButton.VisibleCursor(true);
        // �^�C�g���J�������^�C�g���ʒu�ɐݒ�
        mainSceneObj.switchTitleCamera.transform.position = titleCameraPos;
        mainSceneObj.switchTitleCamera.transform.localEulerAngles = titleCameraAngle;
        titlePreparationFlag = true;
    }
}
