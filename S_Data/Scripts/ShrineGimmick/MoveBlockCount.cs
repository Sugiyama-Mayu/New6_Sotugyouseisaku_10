using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// 宝石を動かすステージの処理
public class MoveBlockCount : MonoBehaviour
{
    [SerializeField] private MoveBlockManager moveBlockManager;
    private void OnTriggerExit(Collider other)
    {
        // 床から宝石が離れた時
        // 宝石の色と床の色が一致していたら(レイヤー)
        if ((other.gameObject.tag == "MoveObj" || other.gameObject.tag == "MoveObj_2") &&
            other.gameObject.layer == this.gameObject.layer)
        {
            // レイヤーの番号によってフラグを消す
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
        // 床と宝石が触れた時
        // 宝石の色と床の色が一致していたら(レイヤー)
        if( (other.gameObject.tag == "MoveObj" || other.gameObject.tag == "MoveObj_2") &&
            other.gameObject.layer == this.gameObject.layer)
        {
            // レイヤーの番号によってフラグを立てる
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
