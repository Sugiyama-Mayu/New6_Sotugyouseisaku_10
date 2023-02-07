using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// M.S �K�̃M�~�b�N
// ���X���ċʂ�]����
public class RotateBall : MonoBehaviour
{
    Vector2 oldCursorPos = (new Vector3(0.0f, 0.0f, 0.0f)); // 1�O�̉�]�p�x�̕ۑ�
    float differenceCursorPosX;  // ��]�p�x�̍���X
    float differenceCursorPosY;  // ��]�p�x�̍���Y
    float rotateSpeed;           // �̉�]�X�s�[�h
    bool boardRoateChangeFlag;   // �̉�]����(X��Z��)�̕ύX�t���O
    [SerializeField] private GameObject rotateBoard;  // �̃I�u�W�F�N�g
    bool rotateBoardFlag;        // �ʓ]�����Q�[�����[�h�ɕύX����t���O
    [SerializeField] private GameObject rotateBoardCamera;   // �ʓ]�����Q�[���̃J�����I�u�W�F�N�g
    [SerializeField] private GameObject VRObj;               // �ʏ�Q�[�����̃J�����Ȃǂ̃I�u�W�F�N�g
    Vector2 cursorPos;           // ��]�p�x
    public bool rotateModeAllow; // true�Ń��[�e�[�g�{�[�h���[�h�ɕύX�ł���悤�ɂ���
    void Start()
    {
        rotateBoardFlag = false;
        boardRoateChangeFlag = false;
        rotateSpeed = 100.0f;
        rotateModeAllow = false;
    }
    void Update()
    {
        if (rotateModeAllow == true)
        {
            // �}�E�X���ƃL�[�{�[�h���̎擾
            var current = Mouse.current;
            //var keyCurrent = Keyboard.current;
            // ����O�L�[�Œe�]�����Q�[�����[�h�AL�L�[�Œʏ탂�[�h�ɕύX�ł���悤�ɂ��Ă���
            if (current.middleButton.wasPressedThisFrame && rotateBoardFlag == false)
            {
                rotateBoardFlag = true;   // �e�]�����Q�[�����[�hON
                                          // �I�u�W�F�N�g�̃A�N�e�B�u����
                VRObj.SetActive(false);
                rotateBoardCamera.SetActive(true);
            }
            else if (current.middleButton.wasPressedThisFrame && rotateBoardFlag == true)
            {
                RotateBallModeOff(); // �e�]�����Q�[�����[�hOFF
            }
            // �e�]�����Q�[�����[�h�Ȃ��
            if (rotateBoardFlag == true)
            {
                if (current == null)
                {
                    // �}�E�X���ڑ�����Ă��Ȃ���Mouse.current��null
                    return;
                }
                // ���N���b�N&�z�[���h����X����]�A������Z����]�̃t���O����
                if (current.leftButton.wasPressedThisFrame)
                {
                    boardRoateChangeFlag = true;
                }
                if (current.leftButton.wasReleasedThisFrame)
                {
                    boardRoateChangeFlag = false;
                }

                // �̉�]�ړ�����
                if (boardRoateChangeFlag == true)
                {
                    //differenceCursorPosY = cursorPos.y - oldCursorPos.y;
                    rotateBoard.transform.Rotate(new Vector3(cursorPos.y * rotateSpeed * Time.deltaTime, 0.0f, 0.0f));
                    oldCursorPos = cursorPos;
                }
                else if (boardRoateChangeFlag == false)
                {
                    //differenceCursorPosX = cursorPos.x - oldCursorPos.x;
                    rotateBoard.transform.Rotate(new Vector3(0.0f, 0.0f, cursorPos.x * rotateSpeed * Time.deltaTime));
                    oldCursorPos = cursorPos;
                }
            }
        }
    }
    // �ʓ]�������[�h���I�t�ɂ���
    // ��  ���F�Ȃ�
    // �߂�l�F�Ȃ�
    public void RotateBallModeOff()
    {
        rotateBoardFlag = false;  // �e�]�����Q�[�����[�hOFF
        // �I�u�W�F�N�g�̃A�N�e�B�u����
        VRObj.SetActive(true);
        rotateBoardCamera.SetActive(false);
    }
    public void onRotate(InputAction.CallbackContext context)
    {
        cursorPos = context.ReadValue<Vector2>();
    }
}
