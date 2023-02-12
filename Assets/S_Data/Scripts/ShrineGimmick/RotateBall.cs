using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// M.S 祠のギミック
// 板を傾けて玉を転がす
public class RotateBall : MonoBehaviour
{
    [SerializeField] PlayerInput bollInput;
    Vector2 oldCursorPos = Vector2.zero; // 1つ前の回転角度の保存
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
        bollInput.enabled = false;
        rotateBoardFlag = false;
        boardRoateChangeFlag = false;
        rotateSpeed = 100.0f;
        rotateModeAllow = false;
    }
    void Update()
    {
        if (rotateModeAllow == true)
        {
            // 弾転がしゲームモードならば
            if (rotateBoardFlag == true)
            {
                if (boardRoateChangeFlag == false)
                {
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
        bollInput.enabled = false;
        // オブジェクトのアクティブ処理
        VRObj.SetActive(true);
        PlayerInput playerAction = VRObj.GetComponentInChildren<PlayerInput>();
        playerAction.enabled = true;
        rotateBoardCamera.SetActive(false);

    }
    public void onRotate(InputAction.CallbackContext context)
    {
        cursorPos = context.ReadValue<Vector2>();
    }

    public void OnRotateBallStart(InputAction.CallbackContext context)
    {
        if (!context.performed && !rotateModeAllow) return;
        if (rotateBoardFlag == true)
        {
            RotateBallModeOff();
        }

    }
    public void StartRotateControll()
    {
        rotateBoardFlag = true;   // 弾転がしゲームモードON
                                  // オブジェクトのアクティブ処理
        VRObj.SetActive(false);
        rotateBoardCamera.SetActive(true);
        Invoke("SetInput", 0.2f);
    }

    public bool GetRotateModeAllow
    {
        get { return rotateModeAllow; }
    }

    private void SetInput()
    {
        bollInput.enabled = true;

    }
}
