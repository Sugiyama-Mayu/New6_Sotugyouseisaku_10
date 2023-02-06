using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    void Start()
    {
        // ���̓X�N���v�g�Őݒ聫�ŏ��̓^�C�g������X�^�[�g
        moveSpeed = 1.2f;    
        titleCameraPos = mainSceneObj.switchTitleCamera.transform.position;
        titleCameraAngle = mainSceneObj.switchTitleCamera.transform.localEulerAngles;
        TitleControl(true);
    }
    void Update()
    {
        // �^�C�g���������ꍇ�A�J�������ړ�������
        if (titleMode == true)
        {
            titleSceneButton.VisibleCursor(true);

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
        }
    }
    // �^�C�g����ʂɐ؂�ւ�
    // ��  ���Fbool  true �^�C�g�����UI�A�J�[�\���\��  false ��\��
    // �߂�l�F�Ȃ�
    public void TitleControl(bool Control)
    {
        if (Control == true)
        {
            SetTitleMode(true);
            mainSceneObj.player.SetActive(false);
            mainSceneObj.questCanvas.SetActive(false);
            mainSceneObj.titleCanvas.gameObject.SetActive(true);
            mainSceneObj.switchTitleCamera.gameObject.SetActive(true);
            titleSceneButton.VisibleCursor(true);
            // �^�C�g���J�������^�C�g���ʒu�ɐݒ�
            mainSceneObj.switchTitleCamera.transform.position = titleCameraPos;
            mainSceneObj.switchTitleCamera.transform.localEulerAngles = titleCameraAngle;
        }
        else if(Control == false)
        {
            SetTitleMode(false);
            mainSceneObj.player.SetActive(true);
            mainSceneObj.questCanvas.SetActive(true);
            mainSceneObj.titleCanvas.gameObject.SetActive(false);
            mainSceneObj.switchTitleCamera.gameObject.SetActive(false);
            titleSceneButton.VisibleCursor(false);
        }
    }
    // �^�C�g�����[�h�̃Z�b�^�[
    void SetTitleMode(bool mode)
    {
        titleMode = mode;
    }
}
