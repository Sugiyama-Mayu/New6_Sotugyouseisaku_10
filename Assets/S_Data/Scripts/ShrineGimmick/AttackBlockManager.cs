using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// アタックブロックのクリア判定マネージャー
public class AttackBlockManager : MonoBehaviour
{
    public int blockNum;  // 総ブロック数
    public int[] blockjudge;  
    private bool blockClear;
    [SerializeField] private bool allGimmickMode;
    [SerializeField] private ShrineClearScript shrineClearScript;
    [SerializeField] private AllShrineManager allShrineManager;
    public ParticleSystem hitEffect;  //ヒット時のエフェクト
    void Start()
    {
        blockjudge = new int[blockNum];
        blockClear = false;
    }

    void Update()
    {
        // 全てのブロックがアタックされているかどうか見る
        // されていた場合クリア
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
