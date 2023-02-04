using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// ��H�̒[�ɂ���u���b�N�̒ʓd���m�F����X�N���v�g
public class ConnectElectManager : MonoBehaviour
{
    [SerializeField] private ShrineClearScript shrineClearScript;
    [SerializeField] private MoveWallScript moveWallScript;
    // �d�C�̔���(�}�e���A����ύX����I�u�W�F�N�g)�I�u�W�F�N�g�p���X�g
    public List<GameObject> electJudgeObj;
    // ��H�̒[�ɂ���u���b�N�̑���
    [SerializeField] private int allElectPillarNum;
    // �ʓd���ɂ��鏈���𕪂���
    [SerializeField] private int kindNum;
    void Start()
    {
        electJudgeObj = new List<GameObject>();
    }
    void Update()
    {
        // �S�Ẳ�H�̒[�ɂ���u���b�N������
        if (electJudgeObj.Count >= allElectPillarNum)
        {
            for (int i = 0; i < allElectPillarNum; i++)
            {
                if (electJudgeObj[i].GetComponent<ConectElectBlock>().GetNowConnect() == false)
                {
                    break;
                }
                // �S�Ẵu���b�N���ʓd���Ă�����
                if (allElectPillarNum <= i + 1)
                {
                    switch (kindNum)
                    {
                        case 1:
                            // �K�N���A�t���O
                            shrineClearScript.shrineClearFlag = true;
                            break;
                        case 2:
                            // �ǂ𓮂����t���O
                            moveWallScript.moveWallFlag = true;
                            break;
                    }
                }
            }
        }
    }    
}