using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S ゲームオーバー画面の表示
public class GameOverProcess : MonoBehaviour
{
    // UIやカメラなどはmainSceneObjスクリプトを1つのオブジェクトにアタッチして
    // 使用しています。
    // prefabにmainSceneObjアタッチオブジェクトを置いてあるので
    // 必要な場合は基本はシーン1つ使用してください。
    [SerializeField]private MainSceneObj mainSceneObj;
    [SerializeField] private TitleSceneButton titleSceneButton;
    [SerializeField] private RingSound ringSound;
    // ゲームオーバー画面の表示
    // 引  数：なし
    // 戻り値：なし
    public void CallGameOver()
    {
        ringSound.RingBGM(5);
        // ゲーム中のものを非表示
        mainSceneObj.player.SetActive(false);
        mainSceneObj.questCanvas.SetActive(false);
        // タイトルカメラから表示
        mainSceneObj.switchTitleCamera.gameObject.SetActive(true);
        mainSceneObj.gameOverCanvas.gameObject.SetActive(true);
        mainSceneObj.switchTitleCamera.transform.position = mainSceneObj.player.transform.position;
        mainSceneObj.switchTitleCamera.transform.localEulerAngles = new Vector3(-18.0f, 0.0f, 0.0f);
        titleSceneButton.VisibleCursor(true);
    }
}
