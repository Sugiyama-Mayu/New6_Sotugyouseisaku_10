using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S 
// �I�ɓ��Ă�M�~�b�N�̃N���A����
public class TargetManager : MonoBehaviour
{
    public int hitNum;
    [SerializeField] private int clearHitNum;
    [SerializeField] private ShrineClearScript shrineClearScript;
    [SerializeField] private AllShrineManager allShrineManager;
    [SerializeField] private bool allGimmickMode;
    public ParticleSystem hitEffect;  //�q�b�g���̃G�t�F�N�g
    void Update()
    {
        // �I�����N���A����������
        if(clearHitNum <= hitNum)
        {
            if(allGimmickMode == true)
            {
                allShrineManager.targetClearFlag = true;
            }
            else
            {
                shrineClearScript.shrineClearFlag = true;
            }
        }
    }
}
