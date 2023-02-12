using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// M.S
// タイトル画面のスクリプト
// (タイトルUIの表示・非表示処理、タイトルカメラの移動処理)
public class TitleUI : MonoBehaviour
{
    // UIやカメラなどはmainSceneObjスクリプトを1つのオブジェクトにアタッチして
    // 使用しています。
    // prefabにmainSceneObjアタッチオブジェクトを置いてあるので
    // 必要な場合は基本はシーン1つ使用してください。
    public bool titleMode = false;     // タイトル画面でUIを使用する時にtrue
    public float moveSpeed;            // UIの移動スピード
    bool returnMode;                   // trueならばカメラ移動で折り返す
    [SerializeField] private MainSceneObj mainSceneObj;
    [SerializeField] private GameObject plusReturnPos; // +の折り返す位置
    [SerializeField] private GameObject minusReturnPos;// -の折り返す位置
    private Vector3 titleCameraPos;
    private Vector3 titleCameraAngle;
    [SerializeField] private TitleSceneButton titleSceneButton;
    [SerializeField] private RingSound ringSound;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject continueButton;
    private int selectButtonNum;
    private int oldButtonNum;
    private bool titlePreparationFlag;
    void Start()
    {
        // 今はスクリプトで設定↓最初はタイトルからスタート
        titlePreparationFlag = false;
        moveSpeed = 1.2f;
        selectButtonNum = 0;
        oldButtonNum = 0;
        titleCameraPos = mainSceneObj.switchTitleCamera.transform.position;
        titleCameraAngle = mainSceneObj.switchTitleCamera.transform.localEulerAngles;
        if (GameObject.Find("GameManager").GetComponent<GameManager>().GetSetXRMode == false) TitleControl(true);
    }
    void Update()
    {
        // タイトルだった場合、カメラを移動させる
        if (titleMode == true && titlePreparationFlag == true)
        {
            //titleSceneButton.VisibleCursor(true);
            if (mainSceneObj.switchTitleCamera.transform.position.x >= plusReturnPos.transform.position.x)
            {
                returnMode = true;
            }
            if (mainSceneObj.switchTitleCamera.transform.position.x <= minusReturnPos.transform.position.x)
            {
                returnMode = false;
            }
            if (returnMode == true)
            {
                mainSceneObj.switchTitleCamera.transform.Translate(-moveSpeed, 0.0f, 0.0f);
            }
            else if (returnMode == false)
            {
                mainSceneObj.switchTitleCamera.transform.Translate(moveSpeed, 0.0f, 0.0f);
            }

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
                        startButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        break;
                    case 1:
                        continueButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        break;
                }
            }
            //選択中ボタンの色を変更
            switch (selectButtonNum)
            {
                case 0:
                    startButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                    break;
                case 1:
                    continueButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                    break;
            }
            //ボタン決定処理
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                switch (selectButtonNum)
                {
                    case 0:
                        titleSceneButton.StartGame();
                        continueButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                        break;
                    case 1:
                        titleSceneButton.ContinueGame();
                        continueButton.GetComponent<Image>().color = new Color(0.5f, 0.55f, 0.86f, 1);
                        break;
                }
            }
            //ボタン数字の更新
            oldButtonNum = selectButtonNum;
        }
    }
    // タイトル画面に切り替え
    // 引  数：bool  true タイトル画面UI、カーソル表示  false 非表示
    // 戻り値：なし
    public void TitleControl(bool Control)
    {
        if (Control == true)
        {
            StartCoroutine("ControlTitle");
        }
        else if (Control == false)
        {
            SetTitleMode(false);
            mainSceneObj.player.SetActive(true);
            mainSceneObj.questCanvas.SetActive(true);
            mainSceneObj.titleCanvas.gameObject.SetActive(false);
            mainSceneObj.switchTitleCamera.gameObject.SetActive(false);
            //titleSceneButton.VisibleCursor(false);
        }
    }
    // タイトルモードのセッター
    void SetTitleMode(bool mode)
    {
        titleMode = mode;
    }
    IEnumerator ControlTitle()
    {
        SetTitleMode(true);
        titlePreparationFlag = false;
        yield return null;
        oldButtonNum = 0;
        selectButtonNum = 0;
        //ボタンの色を設定
        startButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        continueButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        ringSound.RingBGM(0);
        mainSceneObj.player.SetActive(false);
        mainSceneObj.questCanvas.SetActive(false);
        mainSceneObj.titleCanvas.gameObject.SetActive(true);
        mainSceneObj.switchTitleCamera.gameObject.SetActive(true);
        //titleSceneButton.VisibleCursor(true);
        // タイトルカメラをタイトル位置に設定
        mainSceneObj.switchTitleCamera.transform.position = titleCameraPos;
        mainSceneObj.switchTitleCamera.transform.localEulerAngles = titleCameraAngle;
        titlePreparationFlag = true;
    }
}
