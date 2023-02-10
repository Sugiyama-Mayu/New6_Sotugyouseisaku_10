using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S 
// 的に当てるギミックのクリア処理
public class TargetManager : MonoBehaviour
{
    public int hitNum;
    [SerializeField] private int clearHitNum;
    [SerializeField] private ShrineClearScript shrineClearScript;
    [SerializeField] private AllShrineManager allShrineManager;
    [SerializeField] private bool allGimmickMode;
    public ParticleSystem hitEffect;  //ヒット時のエフェクト
    void Update()
    {
        // 的数がクリア数だったら
        if(clearHitNum <= hitNum)
        {
            if(allGimmickMode == true)
            {
                allShrineManager.targetClearFlag = true;
            }
            else
            {
                shrineClearScript.shrineClearFlag = true;
            }
        }
    }
}
