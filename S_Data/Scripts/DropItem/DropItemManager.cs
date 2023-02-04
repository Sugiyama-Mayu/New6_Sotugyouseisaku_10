using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S アイテムドロップ処理と(DropItem)のマネージャー
// 基本一つのオブジェクトにのみアタッチしてください
public class DropItemManager : MonoBehaviour
{
    // 皮、毛皮、鱗ドロップオブジェは非アクティブで配置しておく推奨
    public GameObject smallLeatherDrop;  // 皮
    public GameObject middleLeatherDrop;
    public GameObject bigLeatherDrop;
    /* 今は皮のみの実装
 public GameObject smallFurDrop;      // 毛皮
 public GameObject middleFurDrop;
 public GameObject bigFurDrop;
 public GameObject smallScaleDrop;    // 鱗
 public GameObject middleScaleDrop;
 public GameObject bigScaleDrop;*/
    public int countLimit = 30;   // ドロップ後にオブジェが削除される時間のカウント
    [SerializeField] private ConnectionFile connectionFile;
    private void Start()
    {
       
    }
    // 使用しない
    // 敵が倒れた時に呼び出すドロップアイテム処理
    // 引  数：char    enmSize  アイテムを落とすエネミーの大きさ('小'or'中'or'大')
    //         Vector3 enmPos   アイテムを落とすエネミーがいる位置
    // 戻り値：なし
    // ※エネミーを非アクティブ又はデストロイする前に呼び出して下さい
    public void DropItemFunc(char enmSize/*, char enmKind*/, Vector3 enmPos)
    {
        int dropItemNum = 1;   // 今は皮のみ
        /* ドロップアイテムを皮以外も出す場合使用する
        switch (enmKind)
        {
            case '皮':
                dropItemNum = 1;
                break;
            case '毛':
                dropItemNum = 2;
                break;
            case '鱗':
                dropItemNum = 3;
                break;
        }*/
        // 1～10のランダムの数値を出す
        int rnd = Random.Range(1, 11);
        GameObject InstantObj = null;
        // 敵の大きさで落とすアイテムを分ける
        switch (enmSize)
        {
            case '小':
                if (rnd >= 8)
                {
                    switch (dropItemNum)
                    {
                        case 1:  // 皮
                            InstantObj = Instantiate(smallLeatherDrop, enmPos, Quaternion.identity);
                            WriteFileDropItem(501);  // ドロップアイテムの数を更新してファイルに書き込み
                            break;
                        case 2:  // 毛皮
                            WriteFileDropItem(502);  // ドロップアイテムの数を更新してファイルに書き込み
                            break;
                        case 3:  // 鱗
                            WriteFileDropItem(503);  // ドロップアイテムの数を更新してファイルに書き込み
                            break;
                    }
                    InstantObj.SetActive(true);
                    InstantObj.GetComponent<DropItem>().doneDrop = true;
                }
                break;
            case '中':
                if (rnd >= 9)
                {
                    switch (dropItemNum)
                    {
                        case 1:  // 皮
                            InstantObj = Instantiate(middleLeatherDrop, enmPos, Quaternion.identity);
                            WriteFileDropItem(504);  // ドロップアイテムの数を更新してファイルに書き込み
                            break;
                        case 2:  // 毛皮
                            WriteFileDropItem(505);  // ドロップアイテムの数を更新してファイルに書き込み
                            break;
                        case 3:  // 鱗
                            WriteFileDropItem(506);  // ドロップアイテムの数を更新してファイルに書き込み
                            break;
                    }
                    InstantObj.SetActive(true);
                    InstantObj.GetComponent<DropItem>().doneDrop = true;
                }
                break;
            case '大':
                if (rnd >= 10)
                {
                    switch (dropItemNum)
                    {
                        case 1:  // 皮
                            InstantObj = Instantiate(bigLeatherDrop, enmPos, Quaternion.identity);
                            WriteFileDropItem(507);  // ドロップアイテムの数を更新してファイルに書き込み
                            break;
                        case 2:  // 毛皮
                            WriteFileDropItem(508);  // ドロップアイテムの数を更新してファイルに書き込み
                            break;
                        case 3: // 鱗
                            WriteFileDropItem(509);  // ドロップアイテムの数を更新してファイルに書き込み
                            break;
                    }
                    InstantObj.SetActive(true);
                    InstantObj.GetComponent<DropItem>().doneDrop = true;
                }
                break;
        }

    }

    // ドロップアイテム用ファイル書き込み関数(ドロップアイテム数追加(+1)処理も含む)
    // 引  数：int id   書き込むアイテムのID
    // 戻り値：なし
    void WriteFileDropItem(int id)
    {
        string array = "";
        int kindNum = 5;
        // ドロップアイテムデータの読み込み
        array = connectionFile.ReadFile(id, array);
        connectionFile.TranslationDataArray(array, kindNum);
        connectionFile.haveNum++;   // ドロップアイテムの数を+1
        // 読み込んだデータから文字列を作成
        string writeArray = connectionFile.id.ToString() + ',' + connectionFile.itemName + ','
            + connectionFile.haveNum.ToString() + ',' + connectionFile.Explanation + '\n';
        // ファイルに書き込み
        connectionFile.WriteFile(id, writeArray);
    }
}
