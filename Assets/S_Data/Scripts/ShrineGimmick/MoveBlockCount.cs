using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// ��΂𓮂����X�e�[�W�̏���
public class MoveBlockCount : MonoBehaviour
{
    [SerializeField] private MoveBlockManager moveBlockManager;
    private void OnTriggerExit(Collider other)
    {
        // �������΂����ꂽ��
        // ��΂̐F�Ə��̐F����v���Ă�����(���C���[)
        if ((other.gameObject.tag == "MoveObj" || other.gameObject.tag == "MoveObj_2") &&
            other.gameObject.layer == this.gameObject.layer)
        {
            // ���C���[�̔ԍ��ɂ���ăt���O������
            if (this.gameObject.layer == 14)
            {
                moveBlockManager.yellowFlag = false;
            }
            else if (this.gameObject.layer == 15)
            {
                moveBlockManager.blueFlag = false;
            }
            else if (this.gameObject.layer == 16)
            {
                moveBlockManager.redFlag = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // ���ƕ�΂��G�ꂽ��
        // ��΂̐F�Ə��̐F����v���Ă�����(���C���[)
        if( (other.gameObject.tag == "MoveObj" || other.gameObject.tag == "MoveObj_2") &&
            other.gameObject.layer == this.gameObject.layer)
        {
            // ���C���[�̔ԍ��ɂ���ăt���O�𗧂Ă�
            if (this.gameObject.layer == 14)
            {
                moveBlockManager.yellowFlag = true;
            }
            else if (this.gameObject.layer == 15)
            {
                moveBlockManager.blueFlag = true;
            }
            else if (this.gameObject.layer == 16)
            {
                moveBlockManager.redFlag = true;
            }
        }
    }
}
