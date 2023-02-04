using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainsNum : MonoBehaviour
{
    [SerializeField] private RemainsWarp remainsWarp;
    // 各ワープポイント
    [SerializeField] private Transform player;
    [SerializeField] private int remainsNum;


    public void PlayerWarp()
    {
        if (!remainsWarp.inRemains)
        {
            Debug.Log("遺跡に移動");
            //祠に入る
            remainsWarp.SettingRemains(remainsNum);

            player.position = remainsWarp.inRemainsPoint.position;
        }
        else
        {
            //祠から出る
            player.position = remainsWarp.remainsPoint[remainsWarp.inRemainsNum].position;
            remainsWarp.ResetRemains();
        }

    }
}
