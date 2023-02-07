using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
// M.S
// データベースの読み込み・書き込み
// 基本一つのオブジェクトにのみアタッチしてください
public class ConnectionFile : MonoBehaviour
{
    string dataPath1;                // テキストファイルのパス
    public int itemNum;              // 読み込む配列の数
    public int itemKindNum;          // 読み込む配列の数
    private const int IdDigit = 3;   // IDの桁数
    private const int UpperId = 724; // IDの上限
    private const int LowerId = 101; // IDの下限

    // TranslationDataArray実行時、各変数がここに格納
    public int kindNum;
    public int id;
    public string itemName;
    public string enmName;
    public int buyNum;
    public int sellPrice;
    public int buyPrice;
    public string Explanation;
    public string Endurance;
    public int upperLuck;
    public int lowerLuck;
    public int haveNum;
    public int hp;
    public int attack;
    public int defense;
    public int stun;
    public int drop;
    public string dropItem;

    public bool doneDataPath = false;
    void Start()
    {
        // テキストデータのパス取得
        dataPath1 = Application.dataPath + "/StreamingAssets/gameData.txt";
        doneDataPath = true;
        //SetAllMaterialNum(false,1,99999,27,10,10);
        //test = SetMaterialNum(true,"爪",100);
        //testNum = GetMaterialNum("骨");
        //GetEnmName_DropItemName(305);
    }
    // データべ―ス書き込み関数
    // 引  数：int id         書き込むデータのID番号
    //         int writeArray 書き込むデータ(行ごと) 例：101,薬草,5,体力小回復\n
    // 戻り値：なし
    // 注  意：書き込むデータはデータベースの行データと文字数を一致させる
    //         最後に改行を忘れずに
    //         ,で区切る
    //         装備の説明文はとりあえず10字に設定してあります
    public void WriteFile(int id, string writeArray)
    {
        // 読み込み終わった文字の最新位置
        int lastNNum = 0;
        // 読み込み終了データの最新ID(位置)
        int nNum = 100;
        // 書き込むデータの配列要素位置
        int refeNum = 0;
        // データベーステキストファイル読み込み
        StreamReader fs = new StreamReader(dataPath1);

        string allReadArray = fs.ReadToEnd();  // ファイルからデータをすべて読み込む
        char[] charAllReadArray = allReadArray.ToCharArray(); // 読み込んだデータをcharに変換
        char[] charWriteArray = writeArray.ToCharArray(); // 書き込むデータをcharに変換
        string errorJudgeArray = ""; // エラー判定用配列

        // 書き込みデータの3桁のIDを取り出す
        for (int num = 0; num < IdDigit; num++)
        {
            errorJudgeArray = errorJudgeArray + writeArray[num];
        }

        // 入力IDのエラー判定
        if (id < LowerId || id > UpperId)
        {
            Debug.Log("指定IDが範囲外です");
            return;
        }
        else
        {
            int errorJudgeWriteId = 0;
            // 書き込みIDから取り出したIDをintにする
            errorJudgeWriteId = Convert.ToInt32(errorJudgeArray);
            if (errorJudgeWriteId < LowerId || errorJudgeWriteId > UpperId)
            {
                Debug.Log("'" + errorJudgeWriteId + "'" + "書き込み指定IDが範囲外です");
                return;
            }
        }

        for (int i = 0; i <= allReadArray.Length; i++)
        {
            // 読み込んでいるIDが対象のIDと一致する場合
            if (id <= nNum)
            {
                // 書き込むデータのデータベースでの最初の位置を計算
                int numArr = lastNNum - (writeArray.Length - 1);
                refeNum = 0;
                for (; numArr < lastNNum; numArr++)
                {
                    // 読み込んだデータベースのデータに書き込みデータを代入
                    charAllReadArray[numArr] = charWriteArray[refeNum];
                    refeNum++;
                    // データの代入が終わっている場合
                    if (numArr + 1 >= lastNNum)
                    {
                        string finalStringData = "";
                        // 代入が終わったデータをstringに変換
                        for (int numArr2 = 0; numArr2 < charAllReadArray.Length; numArr2++)
                        {
                            finalStringData = finalStringData + charAllReadArray[numArr2];
                        }
                        fs.Close();
                        // デ―タを書き込み
                        File.WriteAllText(dataPath1, finalStringData);
                        return;
                    }
                }
            }
            else if (charAllReadArray[i] == '\n')
            {
                // 現在の読み込みIDに合わせて次のIDを設定
                if (nNum == 117)
                {
                    nNum = 201;
                    lastNNum = i;
                }
                else if (nNum == 210)
                {
                    nNum = 301;
                    lastNNum = i;
                }
                else if (nNum == 305)
                {
                    nNum = 401;
                    lastNNum = i;
                }
                else if (nNum == 407)
                {
                    nNum = 501;
                    lastNNum = i;
                }
                else if (nNum == 522)
                {
                    nNum = 601;
                    lastNNum = i;
                }
                else if (nNum == 610)
                {
                    nNum = 701;
                    lastNNum = i;
                }
                else
                {
                    lastNNum = i;
                    nNum++;
                }
            }
        }
        fs.Close();
        return;
    }

    // データベース読み込み関数_1
    // 与えられたIDのデータを読み込み、配列に入れる
    // 引  数：int id        読み込むID番号
    //         int dataArray 読み込んだデータを書き込む配列
    // 戻り値：string        ID番号の読み込んだデータを返す
    public string ReadFile(int id, string dataArray)
    {
        string idNumStr = "";
        int idNum = 0;
        var fs = new StreamReader(dataPath1, System.Text.Encoding.GetEncoding("UTF-8"));
        for (int i = 0; i < 100; i++)
        {
            // 目的のIDまで行を読み込む
            dataArray = fs.ReadLine();
            for (int num = 0; num < 3; num++)
            {
                idNumStr = idNumStr + dataArray[num];
                idNum = Convert.ToInt32(idNumStr);
            }
            // 読み込んだID番号と指定したID番号が一致していたらその配列を返す
            if (idNum == id)
            {
                fs.Close();
                return dataArray;
            }
            idNumStr = "";
        }
        fs.Close();
        Debug.Log("IDにあてはまるデータがありませんでした");
        return null;
    }

    // データベース読み込み関数_2
    // 1で読み込んだデータベース配列を各変数に分ける関数
    // 引  数：string dataArray   変数に分けるデータベース配列
    //         int    kindNum     データベースがどのようなデータかの数字
    //                            (IDの頭数字 例:IDが208ならば2)
    // 戻り値：bool               終了後にtrueを返す
    public bool TranslationDataArray(string dataArray, int kindNum)
    {
        // 各データを設定する際にデータベースを調べる配列
        string translateArray = "";
        string translateluckArray = "";  // 幸運値用
        bool luckFlag = false;           // 幸運地の上限、下限格納フラグ
        bool logFlag = false;            // ログを残すフラグ
        int nextNum = 0;  // 設定する変数の数
        int loadNum = 0;  // 指定する配列要素の値
        int nextNumUpper = 0;  // 設定する各変数の数の上限
        switch (kindNum)
        {
            case 1:
                nextNumUpper = 6;
                break;
            case 2:
                nextNumUpper = 7;
                break;
            case 3:
                nextNumUpper = 3;
                break;
            case 5:
                nextNumUpper = 4;
                break;
            case 6:
                nextNumUpper = 6;
                break;
        }
        switch (kindNum)
        {
            // ショップ、所持アイテム
            case 1:
            case 5:
                for (nextNum = 0; nextNum < nextNumUpper; nextNum++)
                {
                    translateArray = "";
                    for (; loadNum < dataArray.Length; loadNum++)
                    {
                        if (dataArray[loadNum] == ',')
                        {
                            loadNum++;
                            break;
                        }
                        else
                        {
                            translateArray = translateArray + dataArray[loadNum];
                        }
                    }
                    switch (nextNum)
                    {
                        case 0:  // id
                            id = Convert.ToInt32(translateArray);
                            break;
                        case 1:  // アイテム名
                            itemName = translateArray;
                            break;
                        case 2:  // 数
                            if (kindNum == 1)
                            {
                                buyNum = Convert.ToInt32(translateArray);
                            }
                            else if (kindNum == 5)
                            {
                                haveNum = Convert.ToInt32(translateArray);
                            }
                            break;
                        case 3:  // 説明文
                            Explanation = translateArray;
                            if (nextNumUpper == 4)
                            {
                                Debug.Log("変更された値   id,itemName,haveNum,Explanation");
                            }
                            break;
                        case 4: // 買い値
                            buyPrice = Convert.ToInt32(translateArray);
                            break;
                        case 5: // 売値
                            sellPrice = Convert.ToInt32(translateArray);
                            Debug.Log("変更された値   id,itemName,buyNum,Explanation,sellPrice,buyPrice");
                            break;
                    }
                }
                break;
            // ショップ(道具)、所持道具
            case 2:
            case 6:
                for (nextNum = 0; nextNum < nextNumUpper; nextNum++)
                {
                    translateArray = "";
                    for (; loadNum < dataArray.Length; loadNum++)
                    {
                        if (dataArray[loadNum] == ',')
                        {
                            loadNum++;
                            break;
                        }
                        else
                        {
                            translateArray = translateArray + dataArray[loadNum];
                        }
                    }
                    switch (nextNum)
                    {
                        case 0: //id
                            id = Convert.ToInt32(translateArray);
                            break;
                        case 1: // アイテム名
                            itemName = translateArray;
                            break;
                        case 2: // 数
                            if (kindNum == 2)
                            {
                                buyNum = Convert.ToInt32(translateArray);
                            }
                            else if (kindNum == 6)
                            {
                                haveNum = Convert.ToInt32(translateArray);
                            }
                            break;
                        case 3: // 説明文
                            Explanation = translateArray;
                            break;
                        case 4: // 耐久度(文字)
                            Endurance = translateArray;
                            break;
                        case 5: // 幸運値
                            for (int i = 0; i < translateArray.Length; i++)
                            {
                                if (translateArray[i] == '～')
                                {
                                    lowerLuck = Convert.ToInt32(translateluckArray);
                                    translateluckArray = "";
                                    i++;
                                    luckFlag = true;
                                }
                                if (luckFlag == false)
                                {
                                    translateluckArray = translateluckArray + translateArray[i];
                                }
                                else if (luckFlag == true)
                                {
                                    translateluckArray = translateluckArray + translateArray[i];

                                }
                                if (i + 1 >= translateArray.Length)
                                {
                                    upperLuck = Convert.ToInt32(translateluckArray);
                                    luckFlag = false;
                                    translateluckArray = "";
                                    if (nextNumUpper == 6)
                                    {
                                        Debug.Log("変更された値   id,itemName,haveNum,Explanation,Endurance,lowerLuck,upperLuck");
                                    }
                                    break;
                                }
                            }
                            break;
                        case 6: // 買い値
                            buyPrice = Convert.ToInt32(translateArray);
                            Debug.Log("変更された値   id,itemName,buyNum,Explanation,Endurance,lowerLuck,upperLuck,buyPrice");
                            break;
                    }
                }
                break;
            // 敵
            case 3:
                for (nextNum = 0; nextNum < nextNumUpper; nextNum++)
                {
                    translateArray = "";
                    for (; loadNum < dataArray.Length; loadNum++)
                    {
                        if (dataArray[loadNum] == ',')
                        {
                            loadNum++;
                            break;
                        }
                        else
                        {
                            translateArray = translateArray + dataArray[loadNum];
                        }
                    }
                    switch (nextNum)
                    {
                        case 0: // id
                            id = Convert.ToInt32(translateArray);
                            break;
                        case 1: // 名前
                            enmName = translateArray;
                            break;
                        case 2: // ドロップアイテム
                            dropItem = translateArray;
                            Debug.Log("変更された値   id,enmName,dropItem");
                            break;
                    }
                }
                break;
            // 証(大切なもの)
            case 4:
                for (nextNum = 0; nextNum < 4; nextNum++)
                {
                    translateArray = "";
                    for (; loadNum < dataArray.Length; loadNum++)
                    {
                        if (dataArray[loadNum] == ',')
                        {
                            loadNum++;
                            break;
                        }
                        else
                        {
                            translateArray = translateArray + dataArray[loadNum];
                        }
                    }
                    switch (nextNum)
                    {
                        case 0:  // id
                            id = Convert.ToInt32(translateArray);
                            break;
                        case 1:  // アイテム名
                            itemName = translateArray;
                            break;
                        case 2:  // 説明文
                            Explanation = translateArray;
                            break;
                        case 3:  // 所持数
                            haveNum = Convert.ToInt32(translateArray);
                            Debug.Log("変更された値   id,itemName,Explanation,haveNum");
                            break;
                    }
                }
                break;
            // 装備
            case 7:
                for (nextNum = 0; nextNum < 5; nextNum++)
                {
                    translateArray = "";
                    for (; loadNum < dataArray.Length; loadNum++)
                    {
                        if (dataArray[loadNum] == ',')
                        {
                            loadNum++;
                            break;
                        }
                        else
                        {
                            translateArray = translateArray + dataArray[loadNum];
                        }
                    }
                    switch (nextNum)
                    {
                        case 0:  // id
                            id = Convert.ToInt32(translateArray);
                            break;
                        case 1:  // アイテム名
                            itemName = translateArray;
                            break;
                        case 2:  // 所持数
                            haveNum = Convert.ToInt32(translateArray);
                            break;
                        case 3:  // 攻撃力or防御力(装備に合わせて)
                            if (id <= 712)
                            {
                                attack = Convert.ToInt32(translateArray);
                                logFlag = true;
                            }
                            else
                            {
                                defense = Convert.ToInt32(translateArray);
                            }
                            break;
                        case 4:  // 説明文
                            Explanation = translateArray;
                            if (logFlag == true)
                            {
                                Debug.Log("変更された値   id,itemName,haveNum,attack,Explanation");

                            }
                            else
                            {
                                Debug.Log("変更された値   id,itemName,haveNum,defense,Explanation");
                            }
                            logFlag = false;
                            break;
                    }
                }
                break;
        }
        return true;
    }
    //*******
    // IDからエネミー名とドロップアイテム名の取得
    // このスクリプトのenmNameとdropItemに格納
    // 引  数：int id  ID番号(301:スケルトン 302:ゴブリン 303:バンパイア 
    //                        304:オーク     305:灰オーク)
    // 戻り値：なし
    public void GetEnmName_DropItemName(int id)
    {
        string array = "";
        TranslationDataArray(ReadFile(id, array), 3);
    }
    // 素材の数を取得する
    // 引  数：string materialName 所持数を知りたい素材の名前(骨or皮or牙or毛皮or爪)
    // 戻り値：int                 今の引数で指定した素材の所持数
    //                             (戻り値が-1の場合、materialNameが間違っている)
    public int GetMaterialNum(string materialName)
    {
        int id = -1;
        string array = "";
        int haveNum = -1;
        switch (materialName)
        {
            case "骨":
                id = 501;
                break;
            case "皮":
                id = 502;
                break;
            case "牙":
                id = 503;
                break;
            case "毛皮":
                id = 504;
                break;
            case "爪":
                id = 505;
                break;
            case "銅":
                id = 519;
                break;
            case "銀":
                id = 520;
                break;
            case "金":
                id = 521;
                break;
        }
        if (id >= 0)
        {
            TranslationDataArray(ReadFile(id, array), 5);
            haveNum = this.haveNum;
        }
        return haveNum;
    }
    // 素材の数を減らしたり足したり
    // 引  数：bool   minusOrPlusMode  true 足す  false 引く
    //         string materialName     数を変更したい素材の名前(骨or皮or牙or毛皮or爪)
    //         int    minusOrPlusNum   足す数or引く数
    // 戻り値：bool           true  素材の減らしたor増やした数の書き込み成功
    //                        false 書き込み不成功(所持数が足りなかったり、materialNameが間違っている) 
    public bool SetMaterialNum(bool minusOrPlusMode, string materialName, int minusOrPlusNum)
    {
        int id = -1;
        string array = "";
        int haveNum = -1;
        string haveNumStr = "";
        switch (materialName)
        {
            case "骨":
                id = 501;
                break;
            case "皮":
                id = 502;
                break;
            case "牙":
                id = 503;
                break;
            case "毛皮":
                id = 504;
                break;
            case "爪":
                id = 505;
                break;
            case "薬草":
                id = 506;
                break;
            case "回復薬":
                id = 507;
                break;
            case "上回復薬":
                id = 508;
                break;
            case "完全回復薬":
                id = 509;
                break;
            case "銅":
                id = 519;
                break;
            case "銀":
                id = 520;
                break;
            case "金":
                id = 521;
                break;
        }
        if (id >= 0)
        {
            TranslationDataArray(ReadFile(id, array), 5);
            haveNum = this.haveNum;
            if (minusOrPlusMode == false)
            {
                haveNum = haveNum - minusOrPlusNum;
                if (haveNum < 0) //入力値以下(0未満)の場合処理不可
                {
                    int writeNum = 0;
                    writeNum = GetMaterialNum(materialName);
                    SetMaterialNum(false, materialName, writeNum);
                    return false;
                }
                if (haveNum < 10)
                {
                    haveNumStr = "0" + haveNum.ToString();
                }
                else
                {
                    haveNumStr = haveNum.ToString();
                }
                array = id.ToString() + "," + materialName + ","
                    + haveNumStr + "," + Explanation + "\n";
                WriteFile(id, array);
                return true;
            }
            else
            {
                haveNum = haveNum + minusOrPlusNum;
                if (haveNum >= 100) //入力値以上(100以上)の場合書き込みはMAX値(99)
                {
                    int writeNum = 0;
                    writeNum = 99 - GetMaterialNum(materialName);
                    SetMaterialNum(true, materialName, writeNum);
                    return false;
                }
                if (haveNum < 10)
                {
                    haveNumStr = "0" + haveNum.ToString();
                }
                else
                {
                    haveNumStr = haveNum.ToString();
                }
                array = id.ToString() + "," + materialName + ","
                       + haveNumStr + "," + Explanation + "\n";
                WriteFile(id, array);
                return true;
            }
        }
        return false;
    }
    // 一行で全ての素材の数の変更をする
    // 引  数：bool minusOrPlusMode  true 足す false 引く
    //         int honeNum、kawaNum、kibaNum、kegawaNum、tumeNum 各素材の足す数又は引く数
    //         (変数に対応する素材について→ honeNum(骨)、kawaNum(皮)、kibaNum(牙)、kegawaNum(毛皮)、tumeNum(爪))
    // 戻り値：なし
    public void SetAllMaterialNum(bool minusOrPlusMode, int honeNum, int kawaNum, int kibaNum, int kegawaNum, int tumeNum ,int douNum, int ginNum, int kinNum)
    {
        //(骨or皮or牙or毛皮or爪)
        //501 骨
        SetMaterialNum(minusOrPlusMode, "骨", honeNum);
        //502 皮
        SetMaterialNum(minusOrPlusMode, "皮", kawaNum);
        //503 牙
        SetMaterialNum(minusOrPlusMode, "牙", kibaNum);
        //504 毛皮
        SetMaterialNum(minusOrPlusMode, "毛皮", kegawaNum);
        //505 爪
        SetMaterialNum(minusOrPlusMode, "爪", tumeNum);
        //519 銅
        SetMaterialNum(minusOrPlusMode, "銅", douNum);
        //520 銀
        SetMaterialNum(minusOrPlusMode, "銀", ginNum);
        //521 金
        SetMaterialNum(minusOrPlusMode, "金", kinNum);
    }

    // 祠のクリア数を返す
    // 引  数：なし
    // 戻り値：int   祠のクリア数
    public int GetClearShrineNum()
    {
        string array = "";
        int clearNum = 0;  //クリア祠数
        for (int i = 0; i < 7; i++)
        {
            TranslationDataArray(ReadFile(401 + i, array), 4);
            if (haveNum >= 1)
            {
                clearNum++;
            }
        }
        return clearNum;
    }
}