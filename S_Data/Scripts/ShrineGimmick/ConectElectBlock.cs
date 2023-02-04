using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// 回路の端にあるブロックの通電処理
public class ConectElectBlock : MonoBehaviour
{
    public ConnectElectManager connectElectManager;
    // 回路の端にあるブロックの数
    [SerializeField] private int allConnectBlockNum;
    public Material electroMat;  // 通電後のマテリアル
    public Material normalMat;   // 通常時のマテリアル
    static private int loopJudge;
    private bool nowConnect;
    //private int timeCount;
    void Start()
    {
        loopJudge = 0;
        nowConnect = false;
    }

    void Update()
    {
        // 全ての回路の端ブロックを一度にみる
        loopJudge++;
        if(loopJudge <= allConnectBlockNum)
        {
            // listに追加されているブロックが足りない場合
            connectElectManager.electJudgeObj.Add(this.gameObject);
        }
        else
        {
            // 全て追加された場合
            loopJudge = 0;
            connectElectManager.electJudgeObj.Clear();
        }
        //timeCount++;
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "ElectObj")
        {
            // 隣の回路ブロックが通電していたら
            if(collision.gameObject.GetComponent<SpreadArrow>().keepElectricFlag == true)
            {
                // マテリアルを変更、フラグを立てる
                this.gameObject.GetComponent<Renderer>().material = electroMat;
                nowConnect = true;
            }
        
        /* 通電後の処理（設定していない）
         * else if(connectElectManager.whileConnectTime <= timeCount)
        {
            this.gameObject.GetComponent<Renderer>().material = normalMat;
            nowConnect = false;
            timeCount = 0;
        }*/
        }
    }
    // 回路の端にあるブロックの通電フラグのゲッター
    // 引  数：なし
    // 戻り値：bool  nowConnect
    public bool GetNowConnect()
    {
        return nowConnect;
    }
}