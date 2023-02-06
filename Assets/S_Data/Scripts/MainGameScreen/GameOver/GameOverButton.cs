using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S ゲームオーバー画面のボタン処理
public class GameOverButton : MonoBehaviour
{
    // UIやカメラなどはmainSceneObjスクリプトを1つのオブジェクトにアタッチして
    // 使用しています。
    // prefabにmainSceneObjアタッチオブジェクトを置いてあるので
    // 必要な場合は基本はシーン1つ使用してください。
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MainSceneObj mainSceneObj;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TitleUI titleUI;
    [SerializeField] private TitleSceneButton titleSceneButton;

    //続けてゲームを遊ぶ
    public void ContinueGame()
    {
        //ゲームオーバー表示キャンバスとカメラを非表示
        mainSceneObj.gameOverCanvas.gameObject.SetActive(false);
        mainSceneObj.switchTitleCamera.gameObject.SetActive(false);
        //カーソルをを消してプレイヤーを表示してゲーム続行
        mainSceneObj.player.SetActive(true);
        titleSceneButton.VisibleCursor(false);
        StartCoroutine(gameManager.Continue());
        Debug.Log("つづきから");
    }
    //タイトルへ
    public void ToTitle()
    {
        mainSceneObj.gameOverCanvas.gameObject.SetActive(false);
        titleUI.TitleControl(true);
        Debug.Log("タイトルへ");
    }
}