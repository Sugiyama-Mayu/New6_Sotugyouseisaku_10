using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S �Q�[���I�[�o�[��ʂ̕\��
public class GameOverProcess : MonoBehaviour
{
    // UI��J�����Ȃǂ�mainSceneObj�X�N���v�g��1�̃I�u�W�F�N�g�ɃA�^�b�`����
    // �g�p���Ă��܂��B
    // prefab��mainSceneObj�A�^�b�`�I�u�W�F�N�g��u���Ă���̂�
    // �K�v�ȏꍇ�͊�{�̓V�[��1�g�p���Ă��������B
    [SerializeField]private MainSceneObj mainSceneObj;
    [SerializeField] private TitleSceneButton titleSceneButton;
    [SerializeField] private RingSound ringSound;
    // �Q�[���I�[�o�[��ʂ̕\��
    // ��  ���F�Ȃ�
    // �߂�l�F�Ȃ�
    public void CallGameOver()
    {
        ringSound.RingBGM(5);
        // �Q�[�����̂��̂��\��
        mainSceneObj.player.SetActive(false);
        mainSceneObj.questCanvas.SetActive(false);
        // �^�C�g���J��������\��
        mainSceneObj.switchTitleCamera.gameObject.SetActive(true);
        mainSceneObj.gameOverCanvas.gameObject.SetActive(true);
        mainSceneObj.switchTitleCamera.transform.position = mainSceneObj.player.transform.position;
        mainSceneObj.switchTitleCamera.transform.localEulerAngles = new Vector3(-18.0f, 0.0f, 0.0f);
        titleSceneButton.VisibleCursor(true);
    }
}
