using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
// M.S セーブデータの読み込み・書き込み
// 基本一つのオブジェクトにのみアタッチしてください
public class SaveDataFile : MonoBehaviour
{
    string dataPath;        //  テキストファイルへのパス
    // セーブデータが格納される変数
    Vector3 pcPos;    // ゲーム終了時の位置
    public int haveMoney;     // 所持金
    int monster1Num;  // モンスター1の数
    int monster2Num;  // モンスター2の数
    int monster3Num;  // モンスター3の数
    int monster4Num;  // モンスター4の数
    int monster5Num;  // モンスター5の数
    bool doneWarp1;   // ワープ行った済み1
    bool doneWarp2;   // ワープ行った済み2
    bool doneWarp3;   // ワープ行った済み3
    bool doneWarp4;   // ワープ行った済み4
    bool doneWarp5;   // ワープ行った済み5
    bool doneWarp6;   // ワープ行った済み6

    void Start()
    {
        dataPath = Application.dataPath + "/StreamingAssets/saveData.txt";
        ReadSaveData();
        //haveMoney = 9;
        WriteSaveData();
    }
    // セーブデータの読み込み(各変数への格納も行う)
    // 引  数：なし
    // 戻り値：なし
    void ReadSaveData()
    {
        StreamReader fs = new StreamReader(dataPath, System.Text.Encoding.GetEncoding("UTF-8"));
        // 行(セーブデータ全て)読み込む
        string saveReadData = fs.ReadLine();
        saveReadData = saveReadData + '\n';
        string numS = "";
        int commaNum = 0;
        // 読み込んだデータから各変数に格納
        for (int i = 0; i < 50; i++)
        {
            if (saveReadData[i] == '\n')
            {
                //ワープポイント6行った済みかどうか
                int warp6 = Convert.ToInt32(numS);
                doneWarp6 = Convert.ToBoolean(warp6);
                numS = "";
                fs.Close();
                break;
            }
            else if (saveReadData[i] == ',')
            {
                commaNum++;
                switch (commaNum)
                {
                    case 1:     //X座標
                        pcPos.x = Convert.ToInt32(numS);
                        break;
                    case 2:     //Y座標
                        pcPos.y = Convert.ToInt32(numS);
                        break;
                    case 3:     //Z座標
                        pcPos.z = Convert.ToInt32(numS);
                        break;
                    case 4:     //所持金
                        haveMoney = Convert.ToInt32(numS);
                        break;
                    case 5:     //モンスター1の数
                        monster1Num = Convert.ToInt32(numS);
                        break;
                    case 6:     //モンスター2の数
                        monster2Num = Convert.ToInt32(numS);
                        break;
                    case 7:     //モンスター3の数
                        monster3Num = Convert.ToInt32(numS);
                        break;
                    case 8:     //モンスター4の数
                        monster4Num = Convert.ToInt32(numS);
                        break;
                    case 9:     //モンスター5の数
                        monster5Num = Convert.ToInt32(numS);
                        break;
                    case 10:    //ワープポイント1行った済みかどうか
                        int warp1 = Convert.ToInt32(numS);
                        doneWarp1 = Convert.ToBoolean(warp1);
                        break;
                    case 11:    //ワープポイント2行った済みかどうか
                        int warp2 = Convert.ToInt32(numS);
                        doneWarp2 = Convert.ToBoolean(warp2);
                        break;
                    case 12:    //ワープポイント3行った済みかどうか
                        int warp3 = Convert.ToInt32(numS);
                        doneWarp3 = Convert.ToBoolean(warp3);
                        break;
                    case 13:    //ワープポイント4行った済みかどうか
                        int warp4 = Convert.ToInt32(numS);
                        doneWarp4 = Convert.ToBoolean(warp4);
                        break;
                    case 14:    //ワープポイント5行った済みかどうか
                        int warp5 = Convert.ToInt32(numS);
                        doneWarp5 = Convert.ToBoolean(warp5);
                        break;
                }
                numS = "";
            }
            else
            {
                numS = numS + saveReadData[i];
            }
        }
    }
    // セーブデータの書き込み
    // ここのスクリプトのpcPos,monster1Num～doneWarp6を書き込む
    // 引  数：なし
    // 戻り値：なし
    public void WriteSaveData()
    {
        string haveMoneyStr = "";
        // boolをintに変換
        int warp1 = Convert.ToInt32(doneWarp1);
        int warp2 = Convert.ToInt32(doneWarp2);
        int warp3 = Convert.ToInt32(doneWarp3);
        int warp4 = Convert.ToInt32(doneWarp4);
        int warp5 = Convert.ToInt32(doneWarp5);
        int warp6 = Convert.ToInt32(doneWarp6);
        // 所持金を書き込み用文字列にする
        if (haveMoney >= 10000)
        {
            haveMoneyStr = haveMoney.ToString();
        }
        else if (haveMoney >= 1000)
        {
            haveMoneyStr = "0" + haveMoney.ToString();
        }
        else if (haveMoney >= 100)
        {
            haveMoneyStr = "0" + "0" + haveMoney.ToString();
        }
        else if (haveMoney >= 10)
        {
            haveMoneyStr = "0" + "0" + "0" + haveMoney.ToString();
        }
        else
        {
            haveMoneyStr = "0" + "0" + "0" + "0" + haveMoney.ToString();
        }
        // 書き込み文字列の作成
        string allWriteText = pcPos.x.ToString() + ',' + pcPos.y.ToString() + ',' + pcPos.z.ToString() + ',' +
            haveMoneyStr + ',' + monster1Num.ToString() + ',' + monster2Num.ToString() + ',' +
            monster3Num.ToString() + ',' + monster4Num.ToString() + ',' + monster5Num.ToString() + ',' +
            warp1.ToString() + ',' + warp2.ToString() + ',' + warp3.ToString() + ',' +
            warp4.ToString() + ',' + warp5.ToString() + ',' + warp6.ToString() + '\n';
        File.WriteAllText(dataPath, allWriteText);  // データベースに書き込み
    }
}
