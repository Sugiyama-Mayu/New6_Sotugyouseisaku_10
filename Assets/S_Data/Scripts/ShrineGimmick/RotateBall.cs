using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// M.S �K�̃M�~�b�N
// ���X���ċʂ�]����
public class RotateBall : MonoBehaviour
{
    [SerializeField] PlayerInput bollInput;
    Vector2 oldCursorPos = Vector2.zero; // 1�O�̉�]�p�x�̕ۑ�
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
        bollInput.enabled = false;
        rotateBoardFlag = false;
        boardRoateChangeFlag = false;
        rotateSpeed = 100.0f;
        rotateModeAllow = false;
    }
    void Update()
    {
        if (rotateModeAllow == true)
        {
            // �e�]�����Q�[�����[�h�Ȃ��
            if (rotateBoardFlag == true)
            {
                if (boardRoateChangeFlag == false)
                {
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
        bollInput.enabled = false;
        // �I�u�W�F�N�g�̃A�N�e�B�u����
        VRObj.SetActive(true);
        PlayerInput playerAction = VRObj.GetComponentInChildren<PlayerInput>();
        playerAction.enabled = true;
        rotateBoardCamera.SetActive(false);

    }
    public void onRotate(InputAction.CallbackContext context)
    {
        cursorPos = context.ReadValue<Vector2>();
    }

    public void OnRotateBallStart(InputAction.CallbackContext context)
    {
        if (!context.performed && !rotateModeAllow) return;
        if (rotateBoardFlag == true)
        {
            RotateBallModeOff();
        }

    }
    public void StartRotateControll()
    {
        rotateBoardFlag = true;   // �e�]�����Q�[�����[�hON
                                  // �I�u�W�F�N�g�̃A�N�e�B�u����
        VRObj.SetActive(false);
        rotateBoardCamera.SetActive(true);
        Invoke("SetInput", 0.2f);
    }

    public bool GetRotateModeAllow
    {
        get { return rotateModeAllow; }
    }

    private void SetInput()
    {
        bollInput.enabled = true;

    }
}
