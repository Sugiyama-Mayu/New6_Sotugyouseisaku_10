using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S �Q�[���I�[�o�[��ʂ̃{�^������
public class GameOverButton : MonoBehaviour
{
    // UI��J�����Ȃǂ�mainSceneObj�X�N���v�g��1�̃I�u�W�F�N�g�ɃA�^�b�`����
    // �g�p���Ă��܂��B
    // prefab��mainSceneObj�A�^�b�`�I�u�W�F�N�g��u���Ă���̂�
    // �K�v�ȏꍇ�͊�{�̓V�[��1�g�p���Ă��������B
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MainSceneObj mainSceneObj;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TitleUI titleUI;
    [SerializeField] private TitleSceneButton titleSceneButton;

    //�����ăQ�[����V��
    public void ContinueGame()
    {
        //�Q�[���I�[�o�[�\���L�����o�X�ƃJ�������\��
        mainSceneObj.gameOverCanvas.gameObject.SetActive(false);
        mainSceneObj.switchTitleCamera.gameObject.SetActive(false);
        //�J�[�\�����������ăv���C���[��\�����ăQ�[�����s
        mainSceneObj.player.SetActive(true);
        titleSceneButton.VisibleCursor(false);
        StartCoroutine(gameManager.Continue());
        Debug.Log("�Â�����");
    }
    //�^�C�g����
    public void ToTitle()
    {
        mainSceneObj.gameOverCanvas.gameObject.SetActive(false);
        titleUI.TitleControl(true);
        Debug.Log("�^�C�g����");
    }
}