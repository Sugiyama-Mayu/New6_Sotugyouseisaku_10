using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// �A�^�b�N�u���b�N�̃N���A����}�l�[�W���[
public class AttackBlockManager : MonoBehaviour
{
    public int blockNum;  // ���u���b�N��
    public int[] blockjudge;  
    private bool blockClear;
    [SerializeField] private bool allGimmickMode;
    [SerializeField] private ShrineClearScript shrineClearScript;
    [SerializeField] private AllShrineManager allShrineManager;
    public ParticleSystem hitEffect;  //�q�b�g���̃G�t�F�N�g
    void Start()
    {
        blockjudge = new int[blockNum];
        blockClear = false;
    }

    void Update()
    {
        // �S�Ẵu���b�N���A�^�b�N����Ă��邩�ǂ�������
        // ����Ă����ꍇ�N���A
        for(int i = 0;i < blockNum; i++)
        {
            if(blockjudge[i] == 1)
            {
                blockClear = true;
            }
            else
            {
                blockClear = false;
                break;
            }
        }

        if(blockClear == true)
        {
            if(allGimmickMode == true)
            {
                allShrineManager.attackClearFlag = true;
            }
            else
            {
                shrineClearScript.shrineClearFlag = true;
            }
        }
    }
}
