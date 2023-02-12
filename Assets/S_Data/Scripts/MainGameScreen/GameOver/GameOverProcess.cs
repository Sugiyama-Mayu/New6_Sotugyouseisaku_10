using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
// M.S ゲームオーバー画面の表示
public class GameOverProcess : MonoBehaviour
{
    // UIやカメラなどはmainSceneObjスクリプトを1つのオブジェクトにアタッチして
    // 使用しています。
    // prefabにmainSceneObjアタッチオブジェクトを置いてあるので
    // 必要な場合は基本はシーン1つ使用してください。
    [SerializeField]private MainSceneObj mainSceneObj;
    [SerializeField] private TitleSceneButton titleSceneButton;
    [SerializeField] private GameOverButton gameOverButton;
    [SerializeField] private RingSound ringSound;
    [SerializeField] private GameObject titleButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TitleUI titleUI;
    public bool callTitleFlag;  //タイトルを呼ぶフラグ
    public bool gameOverMode;   //ゲームオーバーにするフラグ
    private int selectButtonNum;//選択中のボタン数字
    private int oldButtonNum;   //選択していたボタン数字
    private void Start()
    {
        gameOverMode = false;
        callTitleFlag = false;
        selectButtonNum = 0;
        oldButtonNum = 0;
    }
    private void Update()
    {
        if(gameOverMode == true)
        {
            Vector2 wheelRotateNum = Mouse.current.scroll.ReadValue();
            //ホイールの回転によってボタンを選択
            if (wheelRotateNum.y > 0.0f)
            {
                selectButtonNum--;
            }
            else if (wheelRotateNum.y < 0.0f)
            {
                selectButtonNum++;
            }
            //選択数字が範囲内にない場合、範囲内に設定
            if (selectButtonNum < 0)
            {
                selectButtonNum = 0;
            }
            else if (selectButtonNum > 1)
            {
                selectButtonNum = 1;
            }
            //選択ボタンが変わっていたら元のボタンの色を戻す
            if (oldButtonNum != selectButtonNum)
            {
                switch (oldButtonNum)
                {
                    case 0:
                        continueButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        break;
                    case 1:
                        titleButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        break;
                }
            }
            //選択中ボタンの色を変更
            switch (selectButtonNum)
            {
                case 0:
                    continueButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                    break;
                case 1:
                    titleButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                    break;
            }
            //ボタン決定処理
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                switch (selectButtonNum)
                {
                    case 0:
                        gameOverButton.ContinueGame();
                        continueButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                        break;
                    case 1:
                        gameOverButton.ToTitle();
                        titleButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                        break;
                }
            }
            //ボタン数字の更新
            oldButtonNum = selectButtonNum;
        }
        //タイトルを呼ぶ
        if (callTitleFlag == true)
        {
            titleUI.TitleControl(true);
            callTitleFlag = false;
        }
    }
    // ゲームオーバー画面の表示
    // 引  数：なし
    // 戻り値：なし
    public void CallGameOver()
    {
        continueButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        titleButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        gameOverMode = true;
        ringSound.RingBGM(5);
        // ゲーム中のものを非表示
        mainSceneObj.player.SetActive(false);
        mainSceneObj.questCanvas.SetActive(false);
        // タイトルカメラから表示
        mainSceneObj.switchTitleCamera.gameObject.SetActive(true);
        mainSceneObj.gameOverCanvas.gameObject.SetActive(true);
        mainSceneObj.switchTitleCamera.transform.position = mainSceneObj.player.transform.position;
        mainSceneObj.switchTitleCamera.transform.localEulerAngles = new Vector3(-25.0f, 0.0f, 0.0f);
        //gameOverCanvas.transform.position = new Vector3(gameOverCanvas.transform.position.x, -40.0f, gameOverCanvas.transform.position.z);
        //titleSceneButton.VisibleCursor(true);
    }
}
