using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// ギミッククリア保存処理
public class ShrineClearScript : MonoBehaviour
{
    [SerializeField] private ConnectionFile connectFile;
    [SerializeField] private RingSound ringSound;
    [SerializeField] GameObject jewelObj;   //祠宝石オブジェ
    [SerializeField] bool initialState;     //宝石表示初期フラグ
    [SerializeField] private int shrineNum; //祠ID番号
    private bool initialProcess; //祠宝石の初期表示処理
    public bool shrineClearFlag;
    private bool getJewelFlag;
    private bool onlyProcessFlag;
    void Start()
    {
        initialProcess = false;
        shrineClearFlag = false;
        onlyProcessFlag = true;
    }
    void Update()
    {
        string array = "";
        // テキストファイルのデータパスが設定済で
        // 祠宝石の初期表示設定が終わっていなかったら
        if (connectFile.doneDataPath == true && initialProcess == false)
        {
            connectFile.TranslationDataArray(connectFile.ReadFile(shrineNum, array), 4); //祠宝石取得状況を調べる
            // もう取得していたら初期設定が表示でも表示させない
            if (connectFile.haveNum >= 1)
            {
                jewelObj.SetActive(false);
            }
            else
            {
                jewelObj.SetActive(initialState);
            }
            initialProcess = true;
        }
        // 宝石を手に入れたら(表示フラグが立ったら)宝石を表示
        if (getJewelFlag == false && onlyProcessFlag == true)
        {
            if (shrineClearFlag == true && jewelObj.activeSelf == false)
            {
                ringSound.RingSE(18);
                connectFile.TranslationDataArray(connectFile.ReadFile(shrineNum, array), 4); //祠宝石取得状況を調べる
                // もう取得していたら表示させない
                if (connectFile.haveNum >= 1)
                {
                    jewelObj.SetActive(false);
                }
                else
                {
                    jewelObj.SetActive(true);
                }
                onlyProcessFlag = false;
            }
        }
    }
    // 宝石表示フラグのセッター
    public void SetGetJewelFlag(bool active)
    {
        getJewelFlag = active;
    }
    // 祠の数字のセッター
    public void SetShrineNum(int getShrineNum)
    {
        shrineNum = getShrineNum;
    }
}
