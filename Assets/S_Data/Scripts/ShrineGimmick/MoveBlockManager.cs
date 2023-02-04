using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S 
// ��΂𓮂����X�e�[�W�̃N���A����
public class MoveBlockManager : MonoBehaviour
{
    // �M�̏�ɒu���ׂ��u���b�N�̐�
    [SerializeField] private ShrineClearScript shrineClearScript;
    [SerializeField] private AllShrineManager allShrineManager;
    [SerializeField] private bool allGimmickMode;
    // �e�F�̃N���A�t���O
    public bool redFlag;
    public bool yellowFlag;
    public bool blueFlag;
    void Start()
    {
        redFlag = false;
        yellowFlag = false;
        blueFlag = false;
    }
    private void Update()
    {
        // �S�Ă̐F�̃N���A�t���O����������Q�[���N���A
        if (redFlag == true && yellowFlag == true && blueFlag == true)
        {
            if(allGimmickMode == true)
            {
                allShrineManager.moveCrystalClearFlag = true;
            }
            else
            {
                shrineClearScript.shrineClearFlag = true;
            }
        }
    }
}
