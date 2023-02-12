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
    [SerializeField] private TitleSceneButton titleSceneButton;
    [SerializeField] private RingSound ringSound;
    [SerializeField] private GameOverProcess gameOverProcess;
    [SerializeField] private TitleUI titleUI;
    //�����ăQ�[����V��
    public void ContinueGame()
    {
        ringSound.RingBGM(1);
        ringSound.RingSE(0);
        //�Q�[���I�[�o�[�\���L�����o�X�ƃJ�������\��
        mainSceneObj.gameOverCanvas.gameObject.SetActive(false);
        mainSceneObj.switchTitleCamera.gameObject.SetActive(false);
        //�J�[�\�����������ăv���C���[��\�����ăQ�[�����s
        mainSceneObj.player.SetActive(true);
        //titleSceneButton.VisibleCursor(false);
        StartCoroutine(gameManager.Continue());
        gameOverProcess.gameOverMode = false;
        Debug.Log("�Â�����");
    }
    //�^�C�g����
    public void ToTitle()
    {
        ringSound.RingSE(0);
        mainSceneObj.gameOverCanvas.gameObject.SetActive(false);
        titleUI.TitleControl(true);
        gameOverProcess.gameOverMode = false;
        Debug.Log("�^�C�g����");
    }
}