using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    void Start()
    {
        // 今はスクリプトで設定↓最初はタイトルからスタート
        moveSpeed = 1.2f;    
        titleCameraPos = mainSceneObj.switchTitleCamera.transform.position;
        titleCameraAngle = mainSceneObj.switchTitleCamera.transform.localEulerAngles;
        TitleControl(true);
    }
    void Update()
    {
        // タイトルだった場合、カメラを移動させる
        if (titleMode == true)
        {
            titleSceneButton.VisibleCursor(true);

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
        }
    }
    // タイトル画面に切り替え
    // 引  数：bool  true タイトル画面UI、カーソル表示  false 非表示
    // 戻り値：なし
    public void TitleControl(bool Control)
    {
        if (Control == true)
        {
            SetTitleMode(true);
            mainSceneObj.player.SetActive(false);
            mainSceneObj.questCanvas.SetActive(false);
            mainSceneObj.titleCanvas.gameObject.SetActive(true);
            mainSceneObj.switchTitleCamera.gameObject.SetActive(true);
            titleSceneButton.VisibleCursor(true);
            // タイトルカメラをタイトル位置に設定
            mainSceneObj.switchTitleCamera.transform.position = titleCameraPos;
            mainSceneObj.switchTitleCamera.transform.localEulerAngles = titleCameraAngle;
        }
        else if(Control == false)
        {
            SetTitleMode(false);
            mainSceneObj.player.SetActive(true);
            mainSceneObj.questCanvas.SetActive(true);
            mainSceneObj.titleCanvas.gameObject.SetActive(false);
            mainSceneObj.switchTitleCamera.gameObject.SetActive(false);
            titleSceneButton.VisibleCursor(false);
        }
    }
    // タイトルモードのセッター
    void SetTitleMode(bool mode)
    {
        titleMode = mode;
    }
}
