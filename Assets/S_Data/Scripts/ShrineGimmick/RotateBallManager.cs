using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// 板を傾けて玉を転がすギミックのクリア処理
public class RotateBallManager : MonoBehaviour
{
    [SerializeField] private ShrineClearScript shrineClearScript;
    [SerializeField] private GameObject rotateBall;  // ボールオブジェ
    // ゴールに触れた場合
    private void OnCollisionEnter(Collision collision)
    {
        // 祠クリアフラグtrue
        shrineClearScript.shrineClearFlag = true;
        rotateBall.SetActive(false);  // ボールを消す
    }
}
