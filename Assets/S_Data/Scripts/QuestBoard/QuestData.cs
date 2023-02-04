using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// M.S
// 各クエストデータ処理
public class QuestData : MonoBehaviour
{
    [SerializeField] private int questID;
    private string questName;       // クエスト名
    private string questExplanation;// クエスト説明
    [SerializeField] private Text questNameText;         // クエスト名
    [SerializeField] private Text questExplanationText;  // クエスト説明
    [SerializeField] private ConnectionQuestFile connectionQuestFile;
    [SerializeField] private VillageColl villageColl;
    // クエストボードで使用するオブジェクト
    [SerializeField] private GameObject confirmationBack;
    [SerializeField] private GameObject confirmationButtons;
    [SerializeField] private GameObject BoardBackButton;
    [SerializeField] private GameObject orderReceivedButton;
    [SerializeField] private GameObject buttonTexts;
    [SerializeField] private GameObject questOrderBack;
    [SerializeField] private GameObject completeImage;
    [SerializeField] private GameObject recivedStamp;
    [SerializeField] private bool villageCollFlag;

    void Start()
    {
        // クエストIDに合わせてクエストデータを当てはめる
        switch (questID)
        {
            case 101:
                questName = "スケルトンの討伐";
                questExplanation = "スケルトンを5体討伐してほしい";
                break;
            case 102:
                questName = "ゴブリンの討伐";
                questExplanation = "ゴブリンを5体討伐してほしい";
                break;
            case 103:
                questName = "バンパイアの討伐";
                questExplanation = "バンパイアの討伐を5体討伐してほしい";
                break;
            case 104:
                questName = "オークの討伐";
                questExplanation = "オークを5体討伐してほしい";
                break;
            case 105:
                questName = "灰オークの討伐";
                questExplanation = "灰オークを5体討伐してほしい";
                break;
        }
        questNameText.text = questName;
    }
    // クリック(受注処理)
    // 引  数：なし
    // 戻り値：なし
    public bool ClickOrderReceived()
    {
        // 前の受注クエストを解除
        if (connectionQuestFile.TranslationQuestDataArray(connectionQuestFile.ReadQuestFile(connectionQuestFile.nowOrderReceivedQuestID))) {
            connectionQuestFile.WriteQuestFile(connectionQuestFile.nowOrderReceivedQuestID, false, connectionQuestFile.resolutionFlag);
        }
        // クエスト受注
        if (connectionQuestFile.TranslationQuestDataArray(connectionQuestFile.ReadQuestFile(questID))) {
            connectionQuestFile.WriteQuestFile(connectionQuestFile.questIdNum, true, connectionQuestFile.resolutionFlag);
            connectionQuestFile.AllocationOrderReceivedID(questID);
            return true;
        } 
        return false;
    }
    // クリック(内容確認)
    public void ClickConfirmation()
    {
        // クエスト選択画面オブジェを切り返る
        confirmationBack.SetActive(false);
        buttonTexts.SetActive(false);
        confirmationButtons.SetActive(false);
        questExplanationText.gameObject.SetActive(true);
        BoardBackButton.SetActive(true);
        questOrderBack.SetActive(true);
        // 選択中クエストの文字列を登録
        connectionQuestFile.TranslationQuestDataArray(connectionQuestFile.ReadQuestFile(questID));
        // クエストがクリア済みの場合
        if (connectionQuestFile.resolutionFlag == true)
        {
            completeImage.SetActive(true);  // コンプリートスタンプの表示
        }
        // クエストが受注中の場合
        else if(connectionQuestFile.orderFlag == true)
        {
            recivedStamp.SetActive(true);   // 受注中スタンプの表示
        }
        else {
            orderReceivedButton.SetActive(true);
        }
        // 受注中クエストの説明文を登録
        questExplanationText.text = questExplanation;
    }
    // クリック(クエスト選択画面に戻る)
    // 引  数：なし
    // 戻り値：なし
    public void ClickBoardBack()
    {
        // オブジェクトの表示・非表示処理
        confirmationBack.SetActive(true);
        buttonTexts.SetActive(true);
        confirmationButtons.SetActive(true);
        questExplanationText.gameObject.SetActive(false);
        BoardBackButton.SetActive(false);
        orderReceivedButton.SetActive(false);
        questOrderBack.SetActive(false);
        completeImage.SetActive(false);
        recivedStamp.SetActive(false);
    }
}
