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
    private bool visibleCursor;

    // カーソルの表示・非表示処理
    // 引  数：bool visible  true 表示  false 非表示
    // 戻り値：なし
    public void VisibleCursor(bool visible)
    {
        if(visible == true)  // 表示
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else                 // 非表示
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    // ---------------------------- 画面ボタン処理---------------------------
    // はじめから
    public void StartGame()
    {
        titleUI.TitleControl(false);  // タイトル画面をやめる               
        Debug.Log("はじめから");
    }
    // 続きから
    public void ContinueGame()
    {
        Debug.Log("つづきから");
    }
    // オプション
    public void OptionGame()
    {
        Debug.Log("オプション");
    }
}
