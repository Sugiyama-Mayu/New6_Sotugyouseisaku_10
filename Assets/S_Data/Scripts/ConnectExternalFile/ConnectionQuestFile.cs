using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

// M.S
// クエストデータベースの読み込み・書き込み
// 基本一つのオブジェクトにのみアタッチしてください
public class ConnectionQuestFile : MonoBehaviour
{
    [SerializeField] private SaveDataFile saveDataFile;
    string dataPath;        //  テキストファイルへのパス
    //TranslationQuestDataArrayを呼び出すと各変数がここに格納される
    public int questIdNum;  // クエストID
    public bool orderFlag;  // クエストの受注状況フラグ
    public bool resolutionFlag; // クエストのクリア状況フラグ
    // 受注中のクエスト情報
    public Text nowOrderReceivedQuestNameText; // クエスト名
    public int nowOrderReceivedQuestID;        // クエストID
    public List<string> findOrderdArray = new List<string>();
    private const int MAX_ID_NUM = 5; // 総クエスト数
    // 各敵の討伐数管理変数
    // S(スケルトン)、G(ゴブリン)、B(バンパイア)、O(オーク)、H(灰オーク)
    public int SEnmNum = 5;
    public int GEnmNum = 5;
    public int BEnmNum = 5;
    public int OEnmNum = 5;
    public int HEnmNum = 5;
    // 各クエストクリア時の報酬
    public int SEnmReward = 100;
    public int GEnmReward = 200;
    public int BEnmReward = 300;
    public int OEnmReward = 400;
    public int HEnmReward = 500;
    int huntConfirmQuestIdNum = 0;
    bool huntConfirmOrderFlag = false;
    bool huntConfirmResolutionFlag = false;

    void Start()
    {
        dataPath = Application.dataPath + "/StreamingAssets/questData.txt";
        FindOrderdQuestFile();  // 受注中のクエストを探す
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            KnockEnemy('O');
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            KnockEnemy('B');
        }
        // 受注中のクエストの討伐敵数が満たされたかどうか
        // 満たされていたら解決済みにする
        if (nowOrderReceivedQuestID == 101 && SEnmNum <= 0)
        {
            TranslationHuntConfirmQuestDataArray(ReadQuestFile(nowOrderReceivedQuestID));
            if (huntConfirmResolutionFlag == false)
            {
                WriteQuestFile(nowOrderReceivedQuestID, false, true); // クエストクリア済みを書き込み
                saveDataFile.haveMoney = saveDataFile.haveMoney + SEnmReward; // 所持金に報酬を足す
            }
        }
        else if (nowOrderReceivedQuestID == 102 && GEnmNum <= 0)
        {
            TranslationHuntConfirmQuestDataArray(ReadQuestFile(nowOrderReceivedQuestID));
            if (huntConfirmResolutionFlag == false)
            {
                WriteQuestFile(nowOrderReceivedQuestID, false, true); // クエストクリア済みを書き込み
                saveDataFile.haveMoney = saveDataFile.haveMoney + GEnmReward; // 所持金に報酬を足す
            }
        }
        else if (nowOrderReceivedQuestID == 103 && BEnmNum <= 0)
        {
            TranslationHuntConfirmQuestDataArray(ReadQuestFile(nowOrderReceivedQuestID));
            if (huntConfirmResolutionFlag == false)
            {
                WriteQuestFile(nowOrderReceivedQuestID, false, true); // クエストクリア済みを書き込み
                saveDataFile.haveMoney = saveDataFile.haveMoney + BEnmReward; // 所持金に報酬を足す
            }
        }
        else if (nowOrderReceivedQuestID == 104 && OEnmNum <= 0)
        {
            TranslationHuntConfirmQuestDataArray(ReadQuestFile(nowOrderReceivedQuestID));
            if (huntConfirmResolutionFlag == false)
            {
                WriteQuestFile(nowOrderReceivedQuestID, false, true); // クエストクリア済みを書き込み
                saveDataFile.haveMoney = saveDataFile.haveMoney + OEnmReward; // 所持金に報酬を足す
                saveDataFile.WriteSaveData();
            }
        }
        else if (nowOrderReceivedQuestID == 105 && HEnmNum <= 0)
        {
            TranslationHuntConfirmQuestDataArray(ReadQuestFile(nowOrderReceivedQuestID));
            if (huntConfirmResolutionFlag == false)
            {
                WriteQuestFile(nowOrderReceivedQuestID, false, true); // クエストクリア済みを書き込み
                saveDataFile.haveMoney = saveDataFile.haveMoney + HEnmReward; // 所持金に報酬を足す
            }
        }
    }
    // 受注中のクエストを探す
    // 引  数：なし
    // 戻り値：なし
    public void FindOrderdQuestFile()
    {
        string questDataArray = "";
        StreamReader fs = new StreamReader(dataPath, System.Text.Encoding.GetEncoding("UTF-8"));
        // 全てのクエストをみる
        for (int i = 1; i <= MAX_ID_NUM; i++)
        {
            questDataArray = fs.ReadLine();
            questDataArray = questDataArray + '\n';
            TranslationQuestDataArray(questDataArray);
            // 受注中フラグが立っていたら
            if (orderFlag == true)
            {
                // そのクエストIDと名前を登録
                AllocationOrderReceivedID(questIdNum);
                break;
            }
        }
        fs.Close();
        return;
    }
    // クエストデータベース読み込み関数
    // 引  数：int    id  読み込むデータのID番号
    // 戻り値：string     読み込んだデータの行の文字配列
    public string ReadQuestFile(int id)
    {
        string questDataArray = "";
        StreamReader fs = new StreamReader(dataPath, System.Text.Encoding.GetEncoding("UTF-8"));
        // idまでクエストをみる
        for (int i = 101; i <= id; i++)
        {
            questDataArray = fs.ReadLine();
        }
        questDataArray = questDataArray + '\n';
        fs.Close();
        return questDataArray;
    }
    // ReadQuestFileで読み込んだデータを変数に格納
    // 引  数：string  textArray 主にReadQuestFileで読み込んだもの
    // 戻り値：bool              不成功 false  成功 true
    // 引数で渡したデータのID、受注状況、クエスト終了状況はそれぞれ
    // questIdNum、orderFlag、resolutionFlagに格納される
    public bool TranslationQuestDataArray(string textArray)
    {
        int arrayNum = 0;
        arrayNum = Convert.ToInt32(textArray.Length);
        if (Convert.ToInt32(textArray.Length) < 8)
        {
            return false;
        }
        int commaNum = 0;
        string outVariable = "";
        for (int i = 0; i < 100; i++)
        {
            // 文字列の端まで見たら
            if (textArray[i] == '\n')
            {
                switch (Convert.ToInt32(outVariable))
                {
                    // クエストクリア状況の格納

                    case 0:
                        resolutionFlag = false;
                        break;
                    case 1:
                        resolutionFlag = true;
                        break;

                }
                outVariable = "";
                return true;
            }
            else if (textArray[i] == ',')
            {
                commaNum++;
                switch (commaNum)
                {
                    case 1:
                        // クエストIDの格納
                        questIdNum = Convert.ToInt32(outVariable);
                        outVariable = "";
                        break;
                    case 2:
                        switch (Convert.ToInt32(outVariable))
                        {
                            // 受注状況の格納
                            case 0:
                                orderFlag = false;
                                break;
                            case 1:
                                orderFlag = true;
                                break;

                        }
                        outVariable = "";
                        break;
                }
            }
            else
            {
                outVariable = outVariable + textArray[i];
            }
        }
        return false;
    }
    // ReadQuestFileで読み込んだデータを変数に格納(討伐できたか確認用)
    // 引  数：string  textArray 主にReadQuestFileで読み込んだもの
    // 戻り値：bool              不成功 false  成功 true
    // 引数で渡したデータのID、受注状況、クエスト終了状況はそれぞれ
    // huntConfirmQuestIdNum、huntConfirmOrderFlag、huntConfirmResolutionFlagに格納される
    public bool TranslationHuntConfirmQuestDataArray(string textArray)
    {
        int arrayNum = 0;
        arrayNum = Convert.ToInt32(textArray.Length);
        if (Convert.ToInt32(textArray.Length) < 8)
        {
            return false;
        }
        int commaNum = 0;
        string outVariable = "";
        for (int i = 0; i < 100; i++)
        {
            // 文字列の端まで見たら
            if (textArray[i] == '\n')
            {
                switch (Convert.ToInt32(outVariable))
                {
                    // クエストクリア状況の格納

                    case 0:
                        huntConfirmResolutionFlag = false;
                        break;
                    case 1:
                        huntConfirmResolutionFlag = true;
                        break;

                }
                outVariable = "";
                return true;
            }
            else if (textArray[i] == ',')
            {
                commaNum++;
                switch (commaNum)
                {
                    case 1:
                        // クエストIDの格納
                        huntConfirmQuestIdNum = Convert.ToInt32(outVariable);
                        outVariable = "";
                        break;
                    case 2:
                        switch (Convert.ToInt32(outVariable))
                        {
                            // 受注状況の格納
                            case 0:
                                huntConfirmOrderFlag = false;
                                break;
                            case 1:
                                huntConfirmOrderFlag = true;
                                break;

                        }
                        outVariable = "";
                        break;
                }
            }
            else
            {
                outVariable = outVariable + textArray[i];
            }
        }
        return false;
    }
    // クエストデータベースの書き込み関数
    // 引 数：int  id         書き込むデータのID番号
    //        bool order      書き込むデータの受注状態(true 受注した、false 受注していない)
    //        bool resolution 書きこむデータのクエスト終了しているかどうか(true 終了している、false していない)
    public void WriteQuestFile(int id, bool order, bool resolution)
    {
        // パス
        StreamReader fs = new StreamReader(dataPath, System.Text.Encoding.GetEncoding("UTF-8"));
        string allData = fs.ReadToEnd();  // テキストファイルを全て読み込む
        int newLineNum = 1;               // データを書き換える部分の行数
        int calcuLineNum = 0;             
        string writeArray = "";
        int orderNum, resolutionNum;
        // 受注助教、クエストクリア状況をintに変える
        if (order == true)
        {
            orderNum = 1;
        }
        else
        {
            orderNum = 0;
        }
        if (resolution == true)
        {
            resolutionNum = 1;
        }
        else
        {
            resolutionNum = 0;
        }
        // ID、受注状況などの情報を文字列にする
        string writeLine = id.ToString() + ',' + orderNum.ToString() + ',' + resolutionNum.ToString() + '\n';
        for (int i = 0; i < allData.Length; i++)
        {
            // 書き換え行かどうか
            if (newLineNum + 100 == id)
            {
                if (i < (calcuLineNum * 8) + 8)
                {
                    // データの書き換え(writeLineを代入)
                    writeArray = writeArray + writeLine[i - calcuLineNum * 8];
                }
                // 書き換えが終わったら
                if (allData[i] == '\n')
                {
                    newLineNum++;
                    calcuLineNum = newLineNum - 1;
                }
            }
            else if (allData[i] == '\n')
            {
                newLineNum++;  // 書き換え行の更新
                calcuLineNum = newLineNum - 1;
                writeArray = writeArray + allData[i];

            }
            else
            {
                writeArray = writeArray + allData[i];
            }
        }
        fs.Close();
        // テキストファイルに書き込む
        File.WriteAllText(dataPath, writeArray);
    }
    // クエストクリア状況を受注中クエストIDをもとに書き込む
    // 引  数：なし
    // 戻り値：なし
    public void WriteCompleteQuest()
    {
        WriteQuestFile(nowOrderReceivedQuestID, false, true);
    }
    // 敵を倒した時に呼んでクエストを進める
    // 引  数：char  倒した敵の頭文字アルファベット1文字
    //         S(スケルトン)、G(ゴブリン)、B(バンパイア)、O(オーク)、H(灰オーク)
    // 戻り値：なし
    public void KnockEnemy(char initialChar)
    {
        if (nowOrderReceivedQuestID == 101 && initialChar == 'S')
        {
            if (SEnmNum > 0)
            {
                SEnmNum--;
            }
        }
        else if (nowOrderReceivedQuestID == 102 && initialChar == 'G')
        {
            if (GEnmNum > 0)
            {
                GEnmNum--;
            }
        }
        else if (nowOrderReceivedQuestID == 103 && initialChar == 'B')
        {
            if (BEnmNum > 0)
            {
                BEnmNum--;
            }
        }
        else if (nowOrderReceivedQuestID == 104 && initialChar == 'O')
        {
            if (OEnmNum > 0)
            {
                OEnmNum--;
            }
        }
        else if (nowOrderReceivedQuestID == 105 && initialChar == 'H')
        {
            if (HEnmNum > 0)
            {
                HEnmNum--;
            }
        }
    }
    // 受注中のクエストID、名前の登録
    // 引  数：int ID   ID番号
    // 戻り値：なし
    public void AllocationOrderReceivedID(int ID)
    {
        // IDの登録
        nowOrderReceivedQuestID = ID;
        // IDからクエスト名の登録
        switch (ID)
        {
            case 101:
                nowOrderReceivedQuestNameText.text = "スケルトンの討伐";
                break;
            case 102:
                nowOrderReceivedQuestNameText.text = "ゴブリンの討伐";
                break;
            case 103:
                nowOrderReceivedQuestNameText.text = "バンパイアの討伐";
                break;
            case 104:
                nowOrderReceivedQuestNameText.text = "オークの討伐";
                break;
            case 105:
                nowOrderReceivedQuestNameText.text = "灰オークの討伐";
                break;
            default:
                nowOrderReceivedQuestNameText.text = "なし";
                break;
        }
    }
}