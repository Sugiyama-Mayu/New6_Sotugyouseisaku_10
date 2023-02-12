using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
// M.S
// ショップ処理
public class ProcessShop : MonoBehaviour
{
    [SerializeField] private ConnectionFile connectionFile;
    [SerializeField] private SaveDataFile saveDataFile;
    [SerializeField] private TitleUI titleUI;
    // 持ち物表示オブジェクト
    public GameObject haveNumObj;  // 所持数
    public GameObject buyNumObj;   // 売っている数
    public GameObject priceNumObj; // 値段
    public GameObject budgedNumObj;// 所持金
    [SerializeField] private int budgedNum;  // 所持金
    [SerializeField] private int haveNum;    // 所持数
    [SerializeField] private int buyNum;     // 売っている数
    private bool arrayFlag;
    public bool buyMode;   // true 買う false 売る

    private string buyTextArray;
    private string haveTextArray;
    public ShopItemData nowShopItem;
    [SerializeField] private GameObject oldImage; // 今表示しているアイテムイメージ
    [SerializeField] private GameObject yakusou;
    [SerializeField] private GameObject kaihukuyaku;
    [SerializeField] private GameObject joukaihukuyaku;
    [SerializeField] private GameObject kanzenkaihukuyaku;
    [SerializeField] private GameObject dokukeshisou;
    [SerializeField] private GameObject gedokuyaku;
    [SerializeField] private GameObject mahikesisou;
    [SerializeField] private GameObject ya;
    [SerializeField] private GameObject koukyunaya;
    [SerializeField] private GameObject hinoya;
    [SerializeField] private GameObject kaminarinoya;
    [SerializeField] private GameObject kemuridama;
    [SerializeField] private GameObject senkoudama;
    [SerializeField] private GameObject akanomagatama;
    [SerializeField] private GameObject aonomagatama;
    [SerializeField] private GameObject midorinomagatama;
    [SerializeField] private GameObject kinomagatama;
    [SerializeField] private GameObject turuhasi;
    [SerializeField] private GameObject akanoturuhasi;
    [SerializeField] private GameObject dounoturuhasi;
    [SerializeField] private GameObject ginnoturuhasi;
    [SerializeField] private GameObject kinnoturuhasi;
    [SerializeField] private GameObject kama;
    [SerializeField] private GameObject akanokama;
    [SerializeField] private GameObject dounokama;
    [SerializeField] private GameObject ginnokama;
    [SerializeField] private GameObject kinnokama;
    [SerializeField] private GameObject akasi1;
    [SerializeField] private GameObject akasi2;
    [SerializeField] private GameObject akasi3;
    [SerializeField] private GameObject akasi4;
    [SerializeField] private GameObject akasi5;
    [SerializeField] private GameObject akasi6;
    [SerializeField] private GameObject akasi7;
    [SerializeField] private GameObject Skawa;
    [SerializeField] private GameObject Skegawa;
    [SerializeField] private GameObject Suroko;
    [SerializeField] private GameObject Mkawa;
    [SerializeField] private GameObject Mkegawa;
    [SerializeField] private GameObject Muroko;
    [SerializeField] private GameObject Lkawa;
    [SerializeField] private GameObject Lkegawa;
    [SerializeField] private GameObject Luroko;
    [SerializeField] private GameObject ken;
    [SerializeField] private GameObject yumi;
    [SerializeField] private GameObject kabuto;
    [SerializeField] private GameObject yoroi;

    [SerializeField] private GameObject buyModeObj;
    [SerializeField] private GameObject sellModeObj;

    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private Color buyButtonNormalColor;
    [SerializeField] private Color sellButtonNormalColor;
    [SerializeField] private Color buyButtonSelectColor;
    [SerializeField] private Color sellButtonSelectColor;

    [SerializeField] private RingSound ringSound;
    private void Start()
    {
        arrayFlag = false;
        oldImage = yakusou;
        UpdateBudgedDisplay();
        buyButton.GetComponent<Image>().color = buyButtonSelectColor;
        buyMode = true;
        buyModeObj.SetActive(true);
        sellModeObj.SetActive(false);

    }
    private void Update()
    {
        UpdateBudgedDisplay();
        if (buyMode == true)
        {
            buyButton.GetComponent<Image>().color = buyButtonSelectColor;
            sellButton.GetComponent<Image>().color = sellButtonNormalColor;
        }
        else
        {
            sellButton.GetComponent<Image>().color = sellButtonSelectColor;
            buyButton.GetComponent<Image>().color = buyButtonNormalColor;
        }
    }
    // 買うモードに切り替える
    public void BuyModeSwitch()
    {
        if (titleUI.titleMode == false)
        {
            ringSound.RingSE(0);
        }
        buyMode = true;
        buyModeObj.SetActive(true);
        sellModeObj.SetActive(false);
    }
    // 売るモードに切り替える
    public void SellModeSwitch()
    {
        if (titleUI.titleMode == false)
        {
            ringSound.RingSE(0);
        }
        buyMode = false;
        sellModeObj.SetActive(true);
        buyModeObj.SetActive(false);
    }
    // 所持金テキストの更新
    // 引  数：なし
    // 戻り値：なし
    public void UpdateBudgedDisplay()
    {
        // 所持金テキストで表示
        budgedNumObj.GetComponent<Text>().text = saveDataFile.haveMoney.ToString();
    }
    // 購入ボタンを押した場合
    public void BuyClick()
    {
        nowShopItem.GetComponent<ShopItemData>().UpdateShop();
        int buyCommaNum = 0;  // カンマの数を数える用変数
        int haveCommaNum = 0;
        string haveTranslateArray = ""; // データベースに入力用文字列
        string buyTranslateArray = "";
        int buyId = 0;        // ID
        int haveId = 0;
        string buyIdArray = "";  // IDを求める用文字列
        string haveIdArray = "";
        string buyNumT = "";     // 売っている商品の数の文字列
        string haveNumT = "";    // 持っている商品の数の文字列
        int buyNumTArray = 0;    // 配列の要素数
        int haveNumTArray = 0;
        // 文字列からID部分を抽出
        for (int num = 0; num < 3; num++)
        {
            buyIdArray = buyIdArray + buyTextArray[num];
            haveIdArray = haveIdArray + haveTextArray[num];
        }

        // IDをintに変える
        buyId = Convert.ToInt32(buyIdArray);
        haveId = Convert.ToInt32(haveIdArray);
        nowShopItem.GetComponent<ShopItemData>().UpdateShop();
        // アイテムが購入できる所持金がある場合
        if (saveDataFile.haveMoney - connectionFile.buyPrice >= 0 && connectionFile.buyNum - 1 >= 0)
        {
            haveTextArray = haveTextArray + "\n";
            buyTextArray = buyTextArray + "\n";
            // 所持金、売っている数、持っている数を更新
            budgedNum = saveDataFile.haveMoney - connectionFile.buyPrice;
            haveNum = connectionFile.haveNum + 1;
            buyNum = connectionFile.buyNum - 1;
            //
            // 10より小さかったら頭文字に0を入れる
            if (buyNum < 10)
            {
                buyNumT += "0";
                buyNumT += buyNum.ToString();
            }
            else
            {
                buyNumT += buyNum.ToString();
            }

            if (haveNum < 10)
            {
                haveNumT += "0";
                haveNumT += haveNum.ToString();
            }
            else
            {
                haveNumT += haveNum.ToString();
            }
            // 書き込む文字列buyTranslateArrayを作る
            for (int i = 0; i < 100; i++)
            {
                if (buyTextArray[i] == '\n')
                {
                    break;
                }
                else if (buyTextArray[i] == ',')
                {
                    buyCommaNum++;
                    // 文字列に足す
                    buyTranslateArray = buyTranslateArray + buyTextArray[i];
                    i++;
                }
                switch (buyCommaNum)
                {
                    case 0:
                        buyTranslateArray = buyTranslateArray + buyTextArray[i];
                        break;
                    case 1:
                        buyTranslateArray = buyTranslateArray + buyTextArray[i];
                        break;
                    case 2:
                        buyTranslateArray = buyTranslateArray + buyNumT[buyNumTArray];
                        buyNumTArray++;
                        break;
                    default:
                        buyTranslateArray = buyTranslateArray + buyTextArray[i];
                        break;
                }
            }
            // 書き込む文字列buyTranslateArrayを作る
            for (int i = 0; i < 100; i++)
            {

                if (haveTextArray[i] == '\n')
                {
                    break;
                }
                if (haveTextArray[i] == ',')
                {
                    haveCommaNum++;
                    haveTranslateArray = haveTranslateArray + haveTextArray[i];
                    i++;
                }
                switch (haveCommaNum)
                {
                    case 0:
                        haveTranslateArray = haveTranslateArray + haveTextArray[i];
                        break;
                    case 1:
                        haveTranslateArray = haveTranslateArray + haveTextArray[i];
                        break;
                    case 2:
                        haveTranslateArray = haveTranslateArray + haveNumT[haveNumTArray];
                        haveNumTArray++;
                        break;
                    default:
                        haveTranslateArray = haveTranslateArray + haveTextArray[i];
                        break;
                }
            }

            buyTranslateArray = buyTranslateArray + "\n";
            connectionFile.WriteFile(buyId, buyTranslateArray);
            haveTranslateArray = haveTranslateArray + "\n";
            connectionFile.WriteFile(haveId, haveTranslateArray);
            ringSound.RingSE(5);

            // 所持金の書き込み
            saveDataFile.haveMoney = budgedNum;
            saveDataFile.WriteSaveData();
            nowShopItem.GetComponent<ShopItemData>().UpdateShop();
            UpdateBudgedDisplay();
            arrayFlag = false;
            return;
        }

    }

    // 売るボタンを押した時
    public void SellClick()
    {
        int budgedNum = 0;
        // ゲーム情報の更新
        nowShopItem.GetComponent<ShopItemData>().UpdateShop();
        int haveCommaNum = 0;           // カンマの数を数える用変数
        string haveTranslateArray = ""; // データベースに入力用文字列
        int haveId = 0;                 // ID
        string haveIdArray = "";        // IDを求める用文字列
        string haveNumT = "";           // 持っている商品の数の文字列
        int haveNumTArray = 0;          // 持っている数配列の要素数に使用 
        haveTextArray = haveTextArray + '\n';
        // ID番号を調べる
        for (int num = 0; num < 3; num++)
        {
            haveIdArray = haveIdArray + haveTextArray[num];
        }
        // ID番号をintに変える
        haveId = Convert.ToInt32(haveIdArray);
        // アイテム数が1以上かどうか
        if (connectionFile.haveNum - 1 >= 0)
        {
            budgedNum = saveDataFile.haveMoney;
            budgedNum = budgedNum + connectionFile.sellPrice;
            connectionFile.haveNum = connectionFile.haveNum - 1;
            haveNum = connectionFile.haveNum;
            // 10未満かどうかで分けて配列を作る
            if (haveNum < 10)
            {
                haveNumT = "0";
                haveNumT = haveNumT + haveNum.ToString();
                // 01 のようにする
            }
            else
            {
                haveNumT = haveNum.ToString();
            }
            // データベースに書き込む配列の作成
            for (int i = 0, n = 0; i < 100; i++, n++)
            {
                if (haveTextArray[i] == '\n')
                {
                    break;
                }
                if (haveTextArray[i] == ',')
                {
                    haveCommaNum++;
                    // 文字列に足す
                    haveTranslateArray = haveTranslateArray + haveTextArray[i];
                    //haveTranslateArray = haveTranslateArray;
                    i++;
                }
                switch (haveCommaNum)
                {
                    case 0:
                        haveTranslateArray = haveTranslateArray + haveTextArray[i];
                        break;
                    case 1:
                        haveTranslateArray = haveTranslateArray + haveTextArray[i];
                        break;
                    case 2:  // 所持数
                        haveTranslateArray = haveTranslateArray + haveNumT[haveNumTArray];
                        haveNumTArray++;
                        break;
                    default:
                        haveTranslateArray = haveTranslateArray + haveTextArray[i];
                        break;
                }
            }
            ringSound.RingSE(5);
            // 持っている数の書き込み
            haveTranslateArray = haveTranslateArray + "\n";
            connectionFile.WriteFile(haveId, haveTranslateArray);
            nowShopItem.GetComponent<ShopItemData>().UpdateShop();
            // 所持金の書き込み
            saveDataFile.haveMoney = budgedNum;
            saveDataFile.WriteSaveData();
            UpdateBudgedDisplay();

        }
    }
    // アイテム画像表示用関数
    // 引  数:int id  ID
    // 戻り値:なし
    public void SetItemImage(int id)
    {
        oldImage.gameObject.SetActive(false);
        switch (id)
        {
            case 101:  // 薬草
            case 510:
                yakusou.gameObject.SetActive(true);
                oldImage = yakusou;
                break;
            case 102:  // 回復薬
            case 511:
                kaihukuyaku.gameObject.SetActive(true);
                oldImage = kaihukuyaku;
                break;
            case 103:  // 上回復薬
            case 512:
                joukaihukuyaku.gameObject.SetActive(true);
                oldImage = joukaihukuyaku;
                break;
            case 104:  // 完全回復薬
            case 513:
                kanzenkaihukuyaku.gameObject.SetActive(true);
                oldImage = kanzenkaihukuyaku;
                break;
            case 105:  // 毒消し
            case 514:
                dokukeshisou.gameObject.SetActive(true);
                oldImage = dokukeshisou;
                break;
            case 106:  // 解毒薬
            case 515:
                gedokuyaku.gameObject.SetActive(true);
                oldImage = gedokuyaku;
                break;
            case 107:  // 麻痺消し草
            case 516:
                mahikesisou.gameObject.SetActive(true);
                oldImage = mahikesisou;
                break;
            case 108:  // 矢
            case 517:
                ya.gameObject.SetActive(true);
                oldImage = ya;
                break;
            case 109:  // 高級の矢
            case 518:
                koukyunaya.gameObject.SetActive(true);
                oldImage = koukyunaya;
                break;
            case 110:  // 火の矢
            case 519:
                hinoya.gameObject.SetActive(true);
                oldImage = hinoya;
                break;
            case 111:  // 雷の矢
            case 520:
                kaminarinoya.gameObject.SetActive(true);
                oldImage = kaminarinoya;
                break;
            case 112:  // 煙玉
            case 521:
                kemuridama.gameObject.SetActive(true);
                oldImage = kemuridama;
                break;
            case 113:  // 閃光玉
            case 522:
                senkoudama.gameObject.SetActive(true);
                oldImage = senkoudama;
                break;
            case 114:  // 赤の勾玉
            case 523:
                akanomagatama.gameObject.SetActive(true);
                oldImage = akanomagatama;
                break;
            case 115:  // 青の勾玉
            case 524:
                aonomagatama.gameObject.SetActive(true);
                oldImage = aonomagatama;
                break;
            case 116:  // 緑の勾玉
            case 525:
                midorinomagatama.gameObject.SetActive(true);
                oldImage = midorinomagatama;
                break;
            case 117:  // 黄の勾玉
            case 526:
                kinomagatama.gameObject.SetActive(true);
                oldImage = kinomagatama;
                break;
            case 201:  // ツルハシ
            case 601:
                turuhasi.gameObject.SetActive(true);
                oldImage = turuhasi;
                break;
            case 202:  // 赤のツルハシ
            case 602:
                akanoturuhasi.gameObject.SetActive(true);
                oldImage = akanoturuhasi;
                break;
            case 203:  // 銅のツルハシ
            case 603:
                dounoturuhasi.gameObject.SetActive(true);
                oldImage = dounoturuhasi;
                break;
            case 204:  // 銀のツルハシ
            case 604:
                ginnoturuhasi.gameObject.SetActive(true);
                oldImage = ginnoturuhasi;
                break;
            case 205:  // 金のツルハシ
            case 605:
                kinnoturuhasi.gameObject.SetActive(true);
                oldImage = kinnoturuhasi;
                break;
            case 206:  // カマ
            case 606:
                kama.gameObject.SetActive(true);
                oldImage = kama;
                break;
            case 207:  // 赤のカマ
            case 607:
                akanokama.gameObject.SetActive(true);
                oldImage = akanokama;
                break;
            case 208:  // 銅のカマ
            case 608:
                dounokama.gameObject.SetActive(true);
                oldImage = dounokama;
                break;
            case 209:  // 銀のカマ
            case 609:
                ginnokama.gameObject.SetActive(true);
                oldImage = ginnokama;
                break;
            case 210:  // 金のカマ
            case 610:
                kinnokama.gameObject.SetActive(true);
                oldImage = kinnokama;
                break;
            /*
        case 301:
            break;
        case 302:
            break;
        case 303:
            break;
            */
            case 401:  // 1の証
                akasi1.gameObject.SetActive(true);
                oldImage = akasi1;
                break;
            case 402:  // 2の証
                akasi2.gameObject.SetActive(true);
                oldImage = akasi2;
                break;
            case 403:  // 3の証
                akasi3.gameObject.SetActive(true);
                oldImage = akasi3;
                break;
            case 404:  // 4の証
                akasi4.gameObject.SetActive(true);
                oldImage = akasi4;
                break;
            case 405:  // 5の証
                akasi5.gameObject.SetActive(true);
                oldImage = akasi5;
                break;
            case 406:  // 6の証
                akasi6.gameObject.SetActive(true);
                oldImage = akasi6;
                break;
            case 407:  // 7の証
                akasi7.gameObject.SetActive(true);
                oldImage = akasi7;
                break;
            case 501:  // (小)皮
                Skawa.gameObject.SetActive(true);
                oldImage = Skawa;
                break;
            case 502:  // (小)毛皮
                Skegawa.gameObject.SetActive(true);
                oldImage = Skegawa;
                break;
            case 503:  // (小)鱗
                Suroko.gameObject.SetActive(true);
                oldImage = Suroko;
                break;
            case 504:  // (中)皮
                Mkawa.gameObject.SetActive(true);
                oldImage = Mkawa;
                break;
            case 505:  // (中)毛皮
                Mkegawa.gameObject.SetActive(true);
                oldImage = Mkegawa;
                break;
            case 506:  // (中)鱗
                Muroko.gameObject.SetActive(true);
                oldImage = Muroko;
                break;
            case 507:  // (大)皮
                Lkawa.gameObject.SetActive(true);
                oldImage = Lkawa;
                break;
            case 508:  // (大)毛皮
                Lkegawa.gameObject.SetActive(true);
                oldImage = Lkegawa;
                break;
            case 509:  // (大)鱗
                Luroko.gameObject.SetActive(true);
                oldImage = Luroko;
                break;
            case 701:  // 剣
            case 702:
            case 703:
            case 704:
            case 705:
            case 706:
                ken.gameObject.SetActive(true);
                oldImage = ken;
                break;
            case 707:  // 弓
            case 708:
            case 709:
            case 710:
            case 711:
            case 712:
                yumi.gameObject.SetActive(true);
                oldImage = yumi;
                break;
            case 713:  // 兜
            case 714:
            case 715:
            case 716:
            case 717:
            case 718:
                kabuto.gameObject.SetActive(true);
                oldImage = kabuto;
                break;
            case 719:  // 鎧
            case 720:
            case 721:
            case 722:
            case 723:
            case 724:
                yoroi.gameObject.SetActive(true);
                oldImage = yoroi;
                break;
        }
    }
    // ショップアイテム選択中(売っているもの)データ配列のセッター
    public void SetBuyDatabaseArray(string array)
    {
        buyTextArray = array;
    }
    // ショップアイテム選択中(所持しているもの)データ配列のセッター
    public void SetHaveDatabaseArray(string array)
    {
        haveTextArray = array;
    }
    // フラグのセッター
    public void SetArrayFlag(bool flag)
    {
        arrayFlag = flag;
    }
    public void SetNowShopItem(ShopItemData shopItemData)
    {
        nowShopItem = shopItemData;
    }
}
