using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// ���X���ċʂ�]�����M�~�b�N�̃N���A����
public class RotateBallManager : MonoBehaviour
{
    [SerializeField] private ShrineClearScript shrineClearScript;
    [SerializeField] private GameObject rotateBall;  // �{�[���I�u�W�F
    // �S�[���ɐG�ꂽ�ꍇ
    private void OnCollisionEnter(Collision collision)
    {
        // �K�N���A�t���Otrue
        shrineClearScript.shrineClearFlag = true;
        rotateBall.SetActive(false);  // �{�[��������
    }
}
