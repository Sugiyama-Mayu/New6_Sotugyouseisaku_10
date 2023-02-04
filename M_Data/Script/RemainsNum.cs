using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainsNum : MonoBehaviour
{
    [SerializeField] private RemainsWarp remainsWarp;
    // �e���[�v�|�C���g
    [SerializeField] private Transform player;
    [SerializeField] private int remainsNum;


    public void PlayerWarp()
    {
        if (!remainsWarp.inRemains)
        {
            Debug.Log("��ՂɈړ�");
            //�K�ɓ���
            remainsWarp.SettingRemains(remainsNum);

            player.position = remainsWarp.inRemainsPoint.position;
        }
        else
        {
            //�K����o��
            player.position = remainsWarp.remainsPoint[remainsWarp.inRemainsNum].position;
            remainsWarp.ResetRemains();
        }

    }
}
