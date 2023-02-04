using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// 矢の延焼処理の変数管理
public class SpreadArrowManager : MonoBehaviour
{
    public int doneOver;              // listに追加したオブジェクトの数
    public int doneEffect;            // 発生させたエフェクトの数
    public List<GameObject> overObj = new List<GameObject>();  // list型
    public List<ParticleSystem> effectObj = new List<ParticleSystem>();  // list型(エフェクト)
    public int overLapColl;                // 延焼させるオブジェクト(重なっているもの)の数
    public float waitEffectTime;           // エフェクトの発生待機時間
    public float waitEffectTimeCopy;       // エフェクトの発生待機時間のコピー
    public GameObject collArrow;    // 当たった矢
    public GameObject collObj;
    public bool CollFlag;
    public int destroyNum;
    public bool firstCollFlag;
    void Start()
    {
        destroyNum = 0;
        doneOver = 0;              // listに追加したオブジェクトの数
        doneEffect = 0;            // 発生させたエフェクトの数
        waitEffectTimeCopy = waitEffectTime;
        firstCollFlag = false;
    }
}
