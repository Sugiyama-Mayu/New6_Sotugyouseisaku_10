using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// 祠のギミック
// アタックブロックの処理
public class AttackChangeMaterial : MonoBehaviour
{
    public bool clickFlag;      // アタックブロックとの当たり判定フラグ
    public Material changeMat;  // 変更用のマテリアル
    [SerializeField] private AttackBlockManager attackBlockManager;
    [SerializeField] private int thisObjNum;
    void Start()
    {
        clickFlag = false;
    }

    private void Update()
    {
        // アタックブロックが剣と接触していたらブロックのマテリアルを変える
        // ギミック完了フラグをたてる
        if(clickFlag == true)
        {
            this.GetComponent<Renderer>().material = changeMat;
            attackBlockManager.blockjudge[thisObjNum - 1] = 1;
        }
    }
}
