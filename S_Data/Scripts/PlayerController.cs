using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// プレイヤーの移動スクリプト
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField]
    private float speed = 3.0f;   // 移動スピード
    private float gravity = 4.21f;  // 重力

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // 現在のキーボード情報
        var current = Keyboard.current;

        // キーボードの接続チェック
        if(current == null)
        {
            // キーボードが接続されていない
            return;
        }

        var aKey = current.aKey;

        if (aKey.wasPressedThisFrame)
        {
            Debug.Log("Aキーが押された！");
        }
        /*float horizonInput = Input.GetAxis("Horizontal");
        float vertexInput = Input.GetAxis("Vertical");
        // 入力した移動方向を求める
        Vector3 direction = new Vector3(horizonInput, 0, vertexInput);
        // 求めた移動方向×速さ
        Vector3 velocity = direction * speed;
      
        // ローカル座標からワールド座標へ変換s
        velocity = transform.transform.TransformDirection(velocity);
        // y座標は固定
        //velocity.y = gravity;
        // 経過した時間をかけて移動させる
        controller.Move(velocity * Time.deltaTime);*/
    }
}
