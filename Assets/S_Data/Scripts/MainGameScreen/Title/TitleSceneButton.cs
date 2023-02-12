using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// M.S
// 画面のUIボタン処理
// (ボタンの関数、カーソルの表示・非表示処理)
public class TitleSceneButton : MonoBehaviour
{
    [SerializeField] MainSceneObj mainSceneObj;
    [SerializeField] TitleUI titleUI;
    [SerializeField] GameManager gameManager;
    [SerializeField] private RingSound ringSound;
    private bool visibleCursor;
    private Vector3 initialPlayerPos;
    private Vector3 initialPlayerRotate;

    private void Start()
    {
        // 初期位置の保存
        initialPlayerPos = mainSceneObj.player.transform.position;
        initialPlayerRotate = mainSceneObj.player.transform.localEulerAngles;
    }
    // カーソルの表示・非表示処理
    // 引  数：bool visible  true 表示  false 非表示
    // 戻り値：なし
    public void VisibleCursor(bool visible)
    {
        if(visible == true)  // 表示
        {
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }
        else                 // 非表示
        {
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }
    // ---------------------------- 画面ボタン処理---------------------------
    // はじめから
    public void StartGame()
    { 
        ringSound.RingSE(0);
        titleUI.TitleControl(false);  // タイトル画面をやめる
        ringSound.RingBGM(3);
        // 位置を初期位置に設定
        mainSceneObj.player.transform.position = initialPlayerPos;
        mainSceneObj.player.transform.localEulerAngles = initialPlayerRotate;
        gameManager.GameStart();
        Debug.Log("はじめから");
    }
    // 続きから
    public void ContinueGame()
    {
        titleUI.TitleControl(false);  // タイトル画面をやめる               
        gameManager.GameContinue();
        ringSound.RingSE(0);
        ringSound.RingBGM(3);
        Debug.Log("つづきから");
    }
    // オプション
    public void OptionGame()
    {
        Debug.Log("オプション");
    }
}
