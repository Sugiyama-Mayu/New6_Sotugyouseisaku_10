using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// �K�̃M�~�b�N
// �A�^�b�N�u���b�N�̏���
public class AttackChangeMaterial : MonoBehaviour
{
    public bool clickFlag;      // �A�^�b�N�u���b�N�Ƃ̓����蔻��t���O
    public Material changeMat;  // �ύX�p�̃}�e���A��
    [SerializeField] private AttackBlockManager attackBlockManager;
    [SerializeField] private int thisObjNum;
    void Start()
    {
        clickFlag = false;
    }

    private void Update()
    {
        // �A�^�b�N�u���b�N�����ƐڐG���Ă�����u���b�N�̃}�e���A����ς���
        // �M�~�b�N�����t���O�����Ă�
        if(clickFlag == true)
        {
            this.GetComponent<Renderer>().material = changeMat;
            attackBlockManager.blockjudge[thisObjNum - 1] = 1;
        }
    }
}
