using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// 弾転がしゲームのボール位置セットのスクリプト
public class DropBall : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    private Vector3 ballInitialPos;
    void Start()
    {
        // ボールを初期位置にセット
        ballInitialPos = ball.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        // 床に落ちたらボールを初期位置に戻す
        if(other.gameObject.tag == "Ball")
        {
            other.gameObject.transform.position = ballInitialPos;
        }
    }
}
