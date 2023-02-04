using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// 電気で通電した時に壁を動かす処理
public class MoveWallScript : MonoBehaviour
{
    public bool moveWallFlag;  // 壁を動かすフラグ
    private bool stopMoveFlag; // 壁を止めるフラグ
    //private float upperLimit;
    [SerializeField] private GameObject moveWallPos; // 動く壁が動く上限
    void Start()
    {
        stopMoveFlag = false;
    }
    void Update()
    {
        // 壁を動かすフラグがtrue、壁を止めるフラグがfalseの場合
        if(moveWallFlag == true && stopMoveFlag == false)
        {
            // 徐々に壁を上に動かす
            this.transform.Translate(0.0f, 0.1f, 0.0f);
        }
        // 壁が上限まで動いたら
        if (this.transform.position.y >= moveWallPos.transform.position.y)
        {
            // 壁を止める
            stopMoveFlag = true;
        }
    }
}
