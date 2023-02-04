using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// 回路の端にあるブロックの通電を確認するスクリプト
public class ConnectElectManager : MonoBehaviour
{
    [SerializeField] private ShrineClearScript shrineClearScript;
    [SerializeField] private MoveWallScript moveWallScript;
    // 電気の判定(マテリアルを変更するオブジェクト)オブジェクト用リスト
    public List<GameObject> electJudgeObj;
    // 回路の端にあるブロックの総数
    [SerializeField] private int allElectPillarNum;
    // 通電時にする処理を分ける
    [SerializeField] private int kindNum;
    void Start()
    {
        electJudgeObj = new List<GameObject>();
    }
    void Update()
    {
        // 全ての回路の端にあるブロックを見る
        if (electJudgeObj.Count >= allElectPillarNum)
        {
            for (int i = 0; i < allElectPillarNum; i++)
            {
                if (electJudgeObj[i].GetComponent<ConectElectBlock>().GetNowConnect() == false)
                {
                    break;
                }
                // 全てのブロックが通電していたら
                if (allElectPillarNum <= i + 1)
                {
                    switch (kindNum)
                    {
                        case 1:
                            // 祠クリアフラグ
                            shrineClearScript.shrineClearFlag = true;
                            break;
                        case 2:
                            // 壁を動かすフラグ
                            moveWallScript.moveWallFlag = true;
                            break;
                    }
                }
            }
        }
    }    
}