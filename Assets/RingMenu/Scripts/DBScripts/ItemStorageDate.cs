using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemStorageDate : MonoBehaviour
{
    // アイテムの個データ
    public ConnectionFile connectionFile;
    //    public ProcessShop processShop;
    //    public int buyItemID;
    //    public int buyKindID;
    public int haveItemID;
    public int haveKindID;
    //    [SerializeField] private int haveNum;
    //    [SerializeField] private int buyNum;
    [SerializeField] GameObject itemNameObj;
    [SerializeField] GameObject haveNumObj;
    [SerializeField] GameObject explanationObj;
    private string textArray;
    private string array;


    public ButtonColor BC;
    public SelectionItem selection;
    public ControllScripts CS;

    void LateUpdate()
    {
        /*

    switch (CS.ItemKindId)
    {
        case 1:     //  薬
            drug();
            break;
        case 2:     //  道具
            Tool();
            break;
        case 3:      //  証
            Material();
            break;
        case 4:     //  素材
            Testimony();
            break;
    }
    if (CS.RingCanvasActveSelf)
    {
        if (haveItemID != 100)
        {
            ItemName();
            ItemExplanation();
        }
        else
        {
            haveNumObj.GetComponent<Text>().text = "--";
            itemNameObj.GetComponent<Text>().text = "-----";
            explanationObj.GetComponent<Text>().text = "-----";
        }
    }

    /*
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
        if (BC.ButtonId == 0)
        {
            haveItemID = 610;
            haveKindID = 6;
        }
    }
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
        if (BC.ButtonId == 0)
        {
            haveItemID = 510;
            haveKindID = 5;
        }
    }
    if (Input.GetKeyDown(KeyCode.Alpha3))
    {
        if (BC.ButtonId == 1)
        {
            haveItemID = 401;
            haveKindID = 4;
        }
    }*/


    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        connectionFile.ReadFile(collision.gameObject.GetComponent<ShopItemData>().itemID, textArray);
        connectionFile.TranslationDataArray(textArray, collision.gameObject.GetComponent<ShopItemData>().kindID);
        haveNumText.SetActive(true);
        buyNumText.SetActive(true);
        haveNum = connectionFile.haveNum;
        buyNum = connectionFile.buyNum;
        haveNumText.GetComponent<Text>().text = haveNum.ToString();
        buyNumText.GetComponent<Text>().text = buyNum.ToString();
    }
    */

    public void OnPointerEnter(PointerEventData eventData)
    {
        //        UpdateShop();
    }

    /*
    public void UpdateShop()
    {
        textArray = connectionFile.ReadFile(buyItemID, textArray);
        connectionFile.TranslationDataArray(textArray, buyKindID);
        buyNum = connectionFile.buyNum;
        processShop.SetBuyDatabaseArray(textArray);

        textArray = connectionFile.ReadFile(haveItemID, textArray);
        connectionFile.TranslationDataArray(textArray, haveKindID);
        haveNum = connectionFile.haveNum;
        processShop.SetHaveDatabaseArray(textArray);

        processShop.haveNumObj.GetComponent<Text>().text = haveNum.ToString();
        processShop.buyNumObj.GetComponent<Text>().text = buyNum.ToString();
        processShop.priceNumObj.GetComponent<Text>().text = connectionFile.sellPrice.ToString();
        processShop.SetNowShopItem(this);
        processShop.SetArrayFlag(true);
    }
    */

    public void ListUpdate()
    {
        switch (CS.ItemKindId)
        {
            case 1:     //  薬
                drug();
                break;
            case 2:     //  道具
                Tool();
                break;
            case 3:      //  証
                Material();
                break;
            case 4:     //  素材
                Testimony();
                break;
        }

        if (haveItemID != 100)
        {
            ItemName();
            ItemExplanation();
        }
        else
        {
            haveNumObj.GetComponent<Text>().text = "--";
            itemNameObj.GetComponent<Text>().text = "-----";
            //explanationObj.GetComponent<Text>().text = "-----";
        }

    }

    public void ItemUpdate()
    {
        if (haveItemID != 100)
        {
            ItemExplanation();
        }
        else
        {
            explanationObj.GetComponent<Text>().text = "-----";
        }

    }

    public void ItemName()
    {
        array = connectionFile.ReadFile(haveItemID, array);         // 更新
        connectionFile.TranslationDataArray(array, haveKindID);     // 更新
        switch (haveKindID)
        {
            case 1:
            case 6:
                haveNumObj.GetComponent<Text>().text = connectionFile.buyNum.ToString();
                break;
            case 4:
            case 5:
                haveNumObj.GetComponent<Text>().text = connectionFile.haveNum.ToString();
                break;
        }
        itemNameObj.GetComponent<Text>().text = connectionFile.itemName;
    }

    public void OnClickButton()
    {
        array = connectionFile.ReadFile(haveItemID, array);
        connectionFile.TranslationDataArray(array, haveKindID);
        explanationObj.GetComponent<Text>().text = connectionFile.Explanation;
    }

    public void ItemExplanation()
    {
        switch (BC.ButtonId)
        {
            case 0:
                if (selection.n_PosNum == 0)
                {
                    array = connectionFile.ReadFile(haveItemID, array);
                    connectionFile.TranslationDataArray(array, haveKindID);
                    explanationObj.GetComponent<Text>().text = connectionFile.Explanation;
                }
                break;
            case 1:
                if (selection.n_PosNum == 1)
                {
                    array = connectionFile.ReadFile(haveItemID, array);
                    connectionFile.TranslationDataArray(array, haveKindID);
                    explanationObj.GetComponent<Text>().text = connectionFile.Explanation;
                }
                break;
            case 2:
                if (selection.n_PosNum == 2)
                {
                    array = connectionFile.ReadFile(haveItemID, array);
                    connectionFile.TranslationDataArray(array, haveKindID);
                    explanationObj.GetComponent<Text>().text = connectionFile.Explanation;
                }
                break;
            case 3:
                if (selection.n_PosNum == 3)
                {
                    array = connectionFile.ReadFile(haveItemID, array);
                    connectionFile.TranslationDataArray(array, haveKindID);
                    explanationObj.GetComponent<Text>().text = connectionFile.Explanation;
                }
                break;
            case 4:
                if (selection.n_PosNum == 4)
                {
                    array = connectionFile.ReadFile(haveItemID, array);
                    connectionFile.TranslationDataArray(array, haveKindID);
                    explanationObj.GetComponent<Text>().text = connectionFile.Explanation;
                }
                break;
            case 5:
                if (selection.n_PosNum == 5)
                {
                    array = connectionFile.ReadFile(haveItemID, array);
                    connectionFile.TranslationDataArray(array, haveKindID);
                    explanationObj.GetComponent<Text>().text = connectionFile.Explanation;
                }
                break;
            case 6:
                if (selection.n_PosNum == 6)
                {
                    array = connectionFile.ReadFile(haveItemID, array);
                    connectionFile.TranslationDataArray(array, haveKindID);
                    explanationObj.GetComponent<Text>().text = connectionFile.Explanation;
                }
                break;
            case 7:
                if (selection.n_PosNum == 7)
                {
                    array = connectionFile.ReadFile(haveItemID, array);
                    connectionFile.TranslationDataArray(array, haveKindID);
                    explanationObj.GetComponent<Text>().text = connectionFile.Explanation;
                }
                break;
            case 8:
                if (selection.n_PosNum == 8)
                {
                    array = connectionFile.ReadFile(haveItemID, array);
                    connectionFile.TranslationDataArray(array, haveKindID);
                    explanationObj.GetComponent<Text>().text = connectionFile.Explanation;
                }
                break;
            case 9:
                if (selection.n_PosNum == 9)
                {
                    array = connectionFile.ReadFile(haveItemID, array);
                    connectionFile.TranslationDataArray(array, haveKindID);
                    explanationObj.GetComponent<Text>().text = connectionFile.Explanation;
                }
                break;
        }

    }


    public void drug()     // 薬
    {
        if (BC.ButtonId == 0)
        {
            haveItemID = 101;
            haveKindID = 1;
        }
        if (BC.ButtonId == 1)
        {
            haveItemID = 102;
            haveKindID = 1;
        }
        if (BC.ButtonId == 2)
        {
            haveItemID = 103;
            haveKindID = 1;
        }
        if (BC.ButtonId == 3)
        {
            haveItemID = 104;
            haveKindID = 1;
        }
        if (BC.ButtonId == 4)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 5)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 6)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 7)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 8)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 9)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
    }

    public void Item()     // 道具
    {
        if (BC.ButtonId == 0)
        {
            haveItemID = 108;
            haveKindID = 1;
        }
        if (BC.ButtonId == 1)
        {
            haveItemID = 109;
            haveKindID = 1;
        }
        if (BC.ButtonId == 2)
        {
            haveItemID = 110;
            haveKindID = 1;
        }
        if (BC.ButtonId == 3)
        {
            haveItemID = 111;
            haveKindID = 1;
        }
        if (BC.ButtonId == 4)
        {
            haveItemID = 112;
            haveKindID = 1;
        }
        if (BC.ButtonId == 5)
        {
            haveItemID = 113;
            haveKindID = 1;
        }
        if (BC.ButtonId == 6)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 7)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 8)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 9)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
    }

    public void magatama()     // 勾玉
    {
        if (BC.ButtonId == 0)
        {
            haveItemID = 114;
            haveKindID = 1;
        }
        if (BC.ButtonId == 1)
        {
            haveItemID = 115;
            haveKindID = 1;
        }
        if (BC.ButtonId == 2)
        {
            haveItemID = 116;
            haveKindID = 1;
        }
        if (BC.ButtonId == 3)
        {
            haveItemID = 117;
            haveKindID = 1;
        }
        if (BC.ButtonId == 4)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 5)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 6)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 7)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 8)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 9)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
    }

    public void Tool()     // ツール
    {
        if (BC.ButtonId == 0)
        {
            haveItemID = 601;
            haveKindID = 6;
        }
        if (BC.ButtonId == 1)
        {
            haveItemID = 602;
            haveKindID = 6;
        }
        if (BC.ButtonId == 2)
        {
            haveItemID = 603;
            haveKindID = 6;
        }
        if (BC.ButtonId == 3)
        {
            haveItemID = 604;
            haveKindID = 6;
        }
        if (BC.ButtonId == 4)
        {
            haveItemID = 605;
            haveKindID = 6;
        }
        if (BC.ButtonId == 5)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 6)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 7)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 8)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 9)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
    }


    public void Testimony()     // 証
    {
        if (BC.ButtonId == 0)// && 証カウント == 1)
        {
            haveItemID = 401;
            haveKindID = 4;
        }
        if (BC.ButtonId == 1)// && 証カウント == 2)
        {
            haveItemID = 402;
            haveKindID = 4;
        }
        if (BC.ButtonId == 2)// && 証カウント == 3)
        {
            haveItemID = 403;
            haveKindID = 4;
        }
        if (BC.ButtonId == 3)// && 証カウント == 4)
        {
            haveItemID = 404;
            haveKindID = 4;
        }
        if (BC.ButtonId == 4)// && 証カウント == 5)
        {
            haveItemID = 405;
            haveKindID = 4;
        }
        if (BC.ButtonId == 5)// && 証カウント == 6)
        {
            haveItemID = 406;
            haveKindID = 4;
        }
        if (BC.ButtonId == 6)// && 証カウント == 7)
        {
            haveItemID = 407;
            haveKindID = 4;
        }
        if (BC.ButtonId == 7)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 8)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 9)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
    }


    public void Material()     // 素材
    {
        if (BC.ButtonId == 0)
        {
            haveItemID = 501;
            haveKindID = 5;
        }
        if (BC.ButtonId == 1)
        {
            haveItemID = 502;
            haveKindID = 5;
        }
        if (BC.ButtonId == 2)
        {
            haveItemID = 503;
            haveKindID = 5;
        }
        if (BC.ButtonId == 3)
        {
            haveItemID = 504;
            haveKindID = 5;
        }
        if (BC.ButtonId == 4)
        {
            haveItemID = 505;
            haveKindID = 5;
        }
        if (BC.ButtonId == 5)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 6)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 7)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 8)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 9)
        {
            haveItemID = 100;
            haveKindID = 1;
        }
    }


    

    public void Weapon()     // 武器
    {
        if (BC.ButtonId == 0)// && 武器カウント == 1)
        {
            haveItemID = 701;
            haveKindID = 7;
        }
        if (BC.ButtonId == 1)// && 武器カウント == 2)
        {
            haveItemID = 706;
            haveKindID = 7;
        }
        if (BC.ButtonId == 2)// && 武器カウント == 3)
        {
            haveItemID = 707;
            haveKindID = 7;
        }
        if (BC.ButtonId == 3)// && 武器カウント == 4)
        {
            haveItemID = 712;
            haveKindID = 7;
        }
        if (BC.ButtonId == 4)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 5)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 6)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 7)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 8)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 9)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
    }


    public void Armor()     // 防具
    {
        if (BC.ButtonId == 0)// && 防具カウント == 1)
        {
            haveItemID = 713;
            haveKindID = 7;
        }
        if (BC.ButtonId == 1)// && 防具カウント == 2)
        {
            haveItemID = 718;
            haveKindID = 7;
        }
        if (BC.ButtonId == 2)// && 防具カウント == 3)
        {
            haveItemID = 719;
            haveKindID = 7;
        }
        if (BC.ButtonId == 3)// && 防具カウント == 4)
        {
            haveItemID = 724;
            haveKindID = 7;
        }
        if (BC.ButtonId == 4)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 5)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 6)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 7)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 8)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
        if (BC.ButtonId == 9)// 空白
        {
            haveItemID = 100;
            haveKindID = 1;
        }
    }

}
