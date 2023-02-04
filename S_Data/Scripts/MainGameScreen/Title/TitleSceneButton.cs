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
    private bool visibleCursor;

    // �J�[�\���̕\���E��\������
    // ��  ���Fbool visible  true �\��  false ��\��
    // �߂�l�F�Ȃ�
    public void VisibleCursor(bool visible)
    {
        if(visible == true)  // �\��
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else                 // ��\��
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    // ---------------------------- ��ʃ{�^������---------------------------
    // �͂��߂���
    public void StartGame()
    {
        titleUI.TitleControl(false);  // �^�C�g����ʂ���߂�               
        Debug.Log("�͂��߂���");
    }
    // ��������
    public void ContinueGame()
    {
        Debug.Log("�Â�����");
    }
    // �I�v�V����
    public void OptionGame()
    {
        Debug.Log("�I�v�V����");
    }
}
