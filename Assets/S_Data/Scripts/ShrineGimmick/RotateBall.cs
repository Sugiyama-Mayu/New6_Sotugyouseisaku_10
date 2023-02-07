using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// M.S 祠のギミック
// 板を傾けて玉を転がす
public class RotateBall : MonoBehaviour
{
    Vector2 oldCursorPos = (new Vector3(0.0f, 0.0f, 0.0f)); // 1つ前の回転角度の保存
    float differenceCursorPosX;  // 回転角度の差分X
    float differenceCursorPosY;  // 回転角度の差分Y
    float rotateSpeed;           // 板の回転スピード
    bool boardRoateChangeFlag;   // 板の回転方向(XとZ軸)の変更フラグ
    [SerializeField] private GameObject rotateBoard;  // 板のオブジェクト
    bool rotateBoardFlag;        // 玉転がしゲームモードに変更するフラグ
    [SerializeField] private GameObject rotateBoardCamera;   // 玉転がしゲームのカメラオブジェクト
    [SerializeField] private GameObject VRObj;               // 通常ゲーム時のカメラなどのオブジェクト
    Vector2 cursorPos;           // 回転角度
    public bool rotateModeAllow; // trueでローテートボードモードに変更できるようにする
    void Start()
    {
        rotateBoardFlag = false;
        boardRoateChangeFlag = false;
        rotateSpeed = 100.0f;
        rotateModeAllow = false;
    }
    void Update()
    {
        if (rotateModeAllow == true)
        {
            // マウス情報とキーボード情報の取得
            var current = Mouse.current;
            //var keyCurrent = Keyboard.current;
            // 仮でOキーで弾転がしゲームモード、Lキーで通常モードに変更できるようにしてある
            if (current.middleButton.wasPressedThisFrame && rotateBoardFlag == false)
            {
                rotateBoardFlag = true;   // 弾転がしゲームモードON
                                          // オブジェクトのアクティブ処理
                VRObj.SetActive(false);
                rotateBoardCamera.SetActive(true);
            }
            else if (current.middleButton.wasPressedThisFrame && rotateBoardFlag == true)
            {
                RotateBallModeOff(); // 弾転がしゲームモードOFF
            }
            // 弾転がしゲームモードならば
            if (rotateBoardFlag == true)
            {
                if (current == null)
                {
                    // マウスが接続されていないとMouse.currentがnull
                    return;
                }
                // 左クリック&ホールド中はX軸回転、離すとZ軸回転のフラグ処理
                if (current.leftButton.wasPressedThisFrame)
                {
                    boardRoateChangeFlag = true;
                }
                if (current.leftButton.wasReleasedThisFrame)
                {
                    boardRoateChangeFlag = false;
                }

                // 板の回転移動処理
                if (boardRoateChangeFlag == true)
                {
                    //differenceCursorPosY = cursorPos.y - oldCursorPos.y;
                    rotateBoard.transform.Rotate(new Vector3(cursorPos.y * rotateSpeed * Time.deltaTime, 0.0f, 0.0f));
                    oldCursorPos = cursorPos;
                }
                else if (boardRoateChangeFlag == false)
                {
                    //differenceCursorPosX = cursorPos.x - oldCursorPos.x;
                    rotateBoard.transform.Rotate(new Vector3(0.0f, 0.0f, cursorPos.x * rotateSpeed * Time.deltaTime));
                    oldCursorPos = cursorPos;
                }
            }
        }
    }
    // 玉転がしモードをオフにする
    // 引  数：なし
    // 戻り値：なし
    public void RotateBallModeOff()
    {
        rotateBoardFlag = false;  // 弾転がしゲームモードOFF
        // オブジェクトのアクティブ処理
        VRObj.SetActive(true);
        rotateBoardCamera.SetActive(false);
    }
    public void onRotate(InputAction.CallbackContext context)
    {
        cursorPos = context.ReadValue<Vector2>();
    }
}
