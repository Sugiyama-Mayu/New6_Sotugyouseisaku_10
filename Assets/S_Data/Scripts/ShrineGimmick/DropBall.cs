using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// �e�]�����Q�[���̃{�[���ʒu�Z�b�g�̃X�N���v�g
public class DropBall : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    private Vector3 ballInitialPos;
    private Vector3 ballInitialRotate;
    [SerializeField] private int intialSwitch;
    private int num;
    void Start()
    {
        // �{�[���������ʒu�ɃZ�b�g
        ballInitialPos = ball.transform.position;
        ballInitialRotate = ball.transform.localEulerAngles;
        num = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        // ���ɗ�������{�[���������ʒu�ɖ߂�
        if (other.gameObject.tag == "Ball")
        {
            if (intialSwitch == 0)
            {
                other.gameObject.transform.position = ballInitialPos;
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                num = 1;
            }
            else
            {
                if (num == 1) {
                    ball.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    num = 0;
                }
            }

        }
    }
}
