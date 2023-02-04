using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// �e�]�����Q�[���̃{�[���ʒu�Z�b�g�̃X�N���v�g
public class DropBall : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    private Vector3 ballInitialPos;
    void Start()
    {
        // �{�[���������ʒu�ɃZ�b�g
        ballInitialPos = ball.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        // ���ɗ�������{�[���������ʒu�ɖ߂�
        if(other.gameObject.tag == "Ball")
        {
            other.gameObject.transform.position = ballInitialPos;
        }
    }
}
