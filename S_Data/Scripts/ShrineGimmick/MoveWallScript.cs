using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// �d�C�Œʓd�������ɕǂ𓮂�������
public class MoveWallScript : MonoBehaviour
{
    public bool moveWallFlag;  // �ǂ𓮂����t���O
    private bool stopMoveFlag; // �ǂ��~�߂�t���O
    //private float upperLimit;
    [SerializeField] private GameObject moveWallPos; // �����ǂ��������
    void Start()
    {
        stopMoveFlag = false;
    }
    void Update()
    {
        // �ǂ𓮂����t���O��true�A�ǂ��~�߂�t���O��false�̏ꍇ
        if(moveWallFlag == true && stopMoveFlag == false)
        {
            // ���X�ɕǂ���ɓ�����
            this.transform.Translate(0.0f, 0.1f, 0.0f);
        }
        // �ǂ�����܂œ�������
        if (this.transform.position.y >= moveWallPos.transform.position.y)
        {
            // �ǂ��~�߂�
            stopMoveFlag = true;
        }
    }
}
