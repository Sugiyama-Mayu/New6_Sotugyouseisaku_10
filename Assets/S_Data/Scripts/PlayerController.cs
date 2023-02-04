using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// �v���C���[�̈ړ��X�N���v�g
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField]
    private float speed = 3.0f;   // �ړ��X�s�[�h
    private float gravity = 4.21f;  // �d��

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // ���݂̃L�[�{�[�h���
        var current = Keyboard.current;

        // �L�[�{�[�h�̐ڑ��`�F�b�N
        if(current == null)
        {
            // �L�[�{�[�h���ڑ�����Ă��Ȃ�
            return;
        }

        var aKey = current.aKey;

        if (aKey.wasPressedThisFrame)
        {
            Debug.Log("A�L�[�������ꂽ�I");
        }
        /*float horizonInput = Input.GetAxis("Horizontal");
        float vertexInput = Input.GetAxis("Vertical");
        // ���͂����ړ����������߂�
        Vector3 direction = new Vector3(horizonInput, 0, vertexInput);
        // ���߂��ړ������~����
        Vector3 velocity = direction * speed;
      
        // ���[�J�����W���烏�[���h���W�֕ϊ�s
        velocity = transform.transform.TransformDirection(velocity);
        // y���W�͌Œ�
        //velocity.y = gravity;
        // �o�߂������Ԃ������Ĉړ�������
        controller.Move(velocity * Time.deltaTime);*/
    }
}
