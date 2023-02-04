using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S 
// 宝石を動かすステージのクリア処理
public class MoveBlockManager : MonoBehaviour
{
    // 皿の上に置くべきブロックの数
    [SerializeField] private ShrineClearScript shrineClearScript;
    [SerializeField] private AllShrineManager allShrineManager;
    [SerializeField] private bool allGimmickMode;
    // 各色のクリアフラグ
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
        // 全ての色のクリアフラグが立ったらゲームクリア
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
