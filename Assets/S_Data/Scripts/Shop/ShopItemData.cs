using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// M.S
// アイテムにそれぞれアタッチして情報を保存するプログラム
public class ShopItemData : MonoBehaviour, IPointerEnterHandler
{
    // アイテムの個データ
    public ConnectionFile connectionFile;
    public ProcessShop processShop;
    public int buyItemID;
    public int buyKindID;
    public int haveItemID;
    public int haveKindID;
    [SerializeField] private int haveNum;
    [SerializeField] private int buyNum;
    [SerializeField] GameObject itemNameObj;
    [SerializeField] GameObject haveNumObj;
    private string textArray;
    private string array;
    private void Start()
    {
    }    
    // カーソルをかざすと商品情報更新
    public void OnPointerEnter(PointerEventData eventData)
    {
        processShop.SetItemImage(buyItemID);
        UpdateShop();
    }
    // ショップ(買う)の時の商品情報更新
    // 引  数：なし
    // 戻り値：なし
    public void UpdateShop()
    {
        // 売っている数の情報を更新
        textArray = connectionFile.ReadFile(buyItemID, textArray);
        connectionFile.TranslationDataArray(textArray, buyKindID);
        buyNum = connectionFile.buyNum;
        processShop.SetBuyDatabaseArray(textArray);
        processShop.buyNumObj.GetComponent<Text>().text = buyNum.ToString();

        // 持っている数の情報を更新
        textArray = connectionFile.ReadFile(haveItemID, textArray);
        connectionFile.TranslationDataArray(textArray, haveKindID);
        haveNum = connectionFile.haveNum;
        processShop.SetHaveDatabaseArray(textArray);
        processShop.haveNumObj.GetComponent<Text>().text = haveNum.ToString();

        if (processShop.buyMode == true)
        {            
            // 買値を表示
            processShop.priceNumObj.GetComponent<Text>().text = connectionFile.buyPrice.ToString();
        }
        else
        {
            // 売値を表示
            processShop.priceNumObj.GetComponent<Text>().text = connectionFile.sellPrice.ToString();
        }
        processShop.SetNowShopItem(this);
        processShop.SetArrayFlag(true);
    }
    // 持ち物リストの情報の更新
    // 引  数：なし
    // 戻り値：なし
    public void UpdateHave()
    {
        array = connectionFile.ReadFile(haveItemID, array);
        connectionFile.TranslationDataArray(array, haveKindID);
        haveNumObj.GetComponent<Text>().text = connectionFile.haveNum.ToString();
        itemNameObj.GetComponent<Text>().text = connectionFile.itemName;
    }
}
