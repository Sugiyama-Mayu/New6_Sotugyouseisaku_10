using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// M.S
// ��ʂ�UI�{�^������
// (�{�^���̊֐��A�J�[�\���̕\���E��\������)
public class TitleSceneButton : MonoBehaviour
{
    [SerializeField] MainSceneObj mainSceneObj;
    [SerializeField] TitleUI titleUI;
    [SerializeField] GameManager gameManager;
    [SerializeField] private RingSound ringSound;
    private bool visibleCursor;
    private Vector3 initialPlayerPos;
    private Vector3 initialPlayerRotate;

    private void Start()
    {
        // �����ʒu�̕ۑ�
        initialPlayerPos = mainSceneObj.player.transform.position;
        initialPlayerRotate = mainSceneObj.player.transform.localEulerAngles;
    }
    // �J�[�\���̕\���E��\������
    // ��  ���Fbool visible  true �\��  false ��\��
    // �߂�l�F�Ȃ�
    public void VisibleCursor(bool visible)
    {
        if(visible == true)  // �\��
        {
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }
        else                 // ��\��
        {
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }
    // ---------------------------- ��ʃ{�^������---------------------------
    // �͂��߂���
    public void StartGame()
    { 
        ringSound.RingSE(0);
        titleUI.TitleControl(false);  // �^�C�g����ʂ���߂�
        ringSound.RingBGM(3);
        // �ʒu�������ʒu�ɐݒ�
        mainSceneObj.player.transform.position = initialPlayerPos;
        mainSceneObj.player.transform.localEulerAngles = initialPlayerRotate;
        gameManager.GameStart();
        Debug.Log("�͂��߂���");
    }
    // ��������
    public void ContinueGame()
    {
        titleUI.TitleControl(false);  // �^�C�g����ʂ���߂�               
        gameManager.GameContinue();
        ringSound.RingSE(0);
        ringSound.RingBGM(3);
        Debug.Log("�Â�����");
    }
    // �I�v�V����
    public void OptionGame()
    {
        Debug.Log("�I�v�V����");
    }
}
