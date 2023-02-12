using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WheelShopProcess : MonoBehaviour
{
    public ProcessShop processShop;

    // 処理を呼ぶボタン(買う)
    [SerializeField] GameObject buyYakusou;
    [SerializeField] GameObject buyKaihukuyaku;
    [SerializeField] GameObject buyJoukaihukuyaku;
    [SerializeField] GameObject buyKanzenkaihukuyaku;
    [SerializeField] GameObject buyDou;
    [SerializeField] GameObject buyGin;
    [SerializeField] GameObject buyKin;
    [SerializeField] GameObject buyTuruhashi;

    // 処理を呼ぶボタン(売る)
    [SerializeField] GameObject sellYakusou;
    [SerializeField] GameObject sellKaihukuyaku;
    [SerializeField] GameObject sellJoukaihukuyaku;
    [SerializeField] GameObject sellKanzenkaihukuyaku;
    [SerializeField] GameObject sellDou;
    [SerializeField] GameObject sellGin;
    [SerializeField] GameObject sellKin;

    private bool buyOrSellswichMode;  //true 買う  sell 売る
    private int bSelectButton;
    private int sSelectButton;
    private int bOldButton;
    private int sOldButton;
    private void Start()
    {
        bSelectButton = 0;
        sSelectButton = 0;
        bOldButton = 0;
        sOldButton = 0;
        buyOrSellswichMode = true;
}
    //買うモード時の処理
    //引  数：なし
    //戻り値：なし
    private void BuyModeProcess(float wheelRotateNum)
    {
        //ホイールの回転によってボタンを選択
        if (wheelRotateNum > 0.0f)
        {
            bSelectButton--;
        }
        else if (wheelRotateNum < 0.0f)
        {
            bSelectButton++;
        }
        //選択数字が範囲内にない場合、範囲内に設定
        if (bSelectButton < 0)
        {
            bSelectButton = 0;
        }
        else if (bSelectButton > 7)
        {
            bSelectButton = 7;
        }
        //選択ボタンが変わっていたら元のボタンの色を戻す
        if (bOldButton != bSelectButton)
        {
            switch (bOldButton)
            {
                case 0:
                    buyYakusou.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 1:
                    buyKaihukuyaku.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 2:
                    buyJoukaihukuyaku.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 3:
                    buyKanzenkaihukuyaku.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 4:
                    buyDou.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 5:
                    buyGin.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 6:
                    buyKin.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 7:
                    buyTuruhashi.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
            }
        }
        //選択中ボタンの色を変更
        switch (bSelectButton)
        {
            case 0:
                processShop.SetItemImage(buyYakusou.GetComponent<ShopItemData>().buyItemID);
                buyYakusou.GetComponent<ShopItemData>().UpdateShop();
                buyYakusou.GetComponent<Image>().color = new Color(0.97f, 0.71f, 0.71f, 1.0f);
                break;
            case 1:
                processShop.SetItemImage(buyKaihukuyaku.GetComponent<ShopItemData>().buyItemID);
                buyKaihukuyaku.GetComponent<ShopItemData>().UpdateShop();
                buyKaihukuyaku.GetComponent<Image>().color = new Color(0.97f, 0.71f, 0.71f, 1.0f);
                break;
            case 2:
                processShop.SetItemImage(buyJoukaihukuyaku.GetComponent<ShopItemData>().buyItemID);
                buyJoukaihukuyaku.GetComponent<ShopItemData>().UpdateShop();
                buyJoukaihukuyaku.GetComponent<Image>().color = new Color(0.97f, 0.71f, 0.71f, 1.0f);
                break;
            case 3:
                processShop.SetItemImage(buyKanzenkaihukuyaku.GetComponent<ShopItemData>().buyItemID);
                buyKanzenkaihukuyaku.GetComponent<ShopItemData>().UpdateShop();
                buyKanzenkaihukuyaku.GetComponent<Image>().color = new Color(0.97f, 0.71f, 0.71f, 1.0f);
                break;
            case 4:
                processShop.SetItemImage(buyDou.GetComponent<ShopItemData>().buyItemID);
                buyDou.GetComponent<ShopItemData>().UpdateShop();
                buyDou.GetComponent<Image>().color = new Color(0.97f, 0.71f, 0.71f, 1.0f);
                break;
            case 5:
                processShop.SetItemImage(buyGin.GetComponent<ShopItemData>().buyItemID);
                buyGin.GetComponent<ShopItemData>().UpdateShop();
                buyGin.GetComponent<Image>().color = new Color(0.97f, 0.71f, 0.71f, 1.0f);
                break;
            case 6:
                processShop.SetItemImage(buyKin.GetComponent<ShopItemData>().buyItemID);
                buyKin.GetComponent<ShopItemData>().UpdateShop();
                buyKin.GetComponent<Image>().color = new Color(0.97f, 0.71f, 0.71f, 1.0f);
                break;
            case 7:
                processShop.SetItemImage(buyTuruhashi.GetComponent<ShopItemData>().buyItemID);
                buyTuruhashi.GetComponent<ShopItemData>().UpdateShop();
                buyTuruhashi.GetComponent<Image>().color = new Color(0.97f, 0.71f, 0.71f, 1.0f);
                break;
        }
        //ボタン数字の更新
        bOldButton = bSelectButton;
    }
    //売るモード時の処理
    //引  数：なし
    //戻り値：なし
    private void SellModeProcess(float wheelRotateNum)
    {
        //ホイールの回転によってボタンを選択
        if (wheelRotateNum > 0.0f) 
        {
            sSelectButton--;
        }
        else if (wheelRotateNum < 0.0f)
        {
            sSelectButton++;
        }
        //選択数字が範囲内にない場合、範囲内に設定
        if (sSelectButton < 0)
        {
            sSelectButton = 0;
        }
        else if (sSelectButton > 6)
        {
            sSelectButton = 6;
        }
        //選択ボタンが変わっていたら元のボタンの色を戻す
        if (sOldButton !=sSelectButton)
        {
            switch (sOldButton)
            {
                case 0:
                    sellYakusou.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 1:
                    sellKaihukuyaku.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 2:
                    sellJoukaihukuyaku.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 3:
                    sellKanzenkaihukuyaku.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 4:
                    sellDou.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 5:
                    sellGin.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
                case 6:
                    sellKin.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;
            }
        }
        //選択中ボタンの色を変更
        switch (sSelectButton)
        {
            case 0:
                processShop.SetItemImage(sellYakusou.GetComponent<ShopItemData>().buyItemID);
                sellYakusou.GetComponent<ShopItemData>().UpdateShop();
                sellYakusou.GetComponent<Image>().color = new Color(0.7f, 0.8f, 0.9f, 1.0f);
                break;
            case 1:
                processShop.SetItemImage(sellKaihukuyaku.GetComponent<ShopItemData>().buyItemID);
                sellKaihukuyaku.GetComponent<ShopItemData>().UpdateShop();
                sellKaihukuyaku.GetComponent<Image>().color = new Color(0.7f, 0.8f, 0.9f, 1.0f);
                break;
            case 2:
                processShop.SetItemImage(sellJoukaihukuyaku.GetComponent<ShopItemData>().buyItemID);
                sellJoukaihukuyaku.GetComponent<ShopItemData>().UpdateShop();
                sellJoukaihukuyaku.GetComponent<Image>().color = new Color(0.7f, 0.8f, 0.9f, 1.0f);
                break;
            case 3:
                processShop.SetItemImage(sellKanzenkaihukuyaku.GetComponent<ShopItemData>().buyItemID);
                sellKanzenkaihukuyaku.GetComponent<ShopItemData>().UpdateShop();
                sellKanzenkaihukuyaku.GetComponent<Image>().color = new Color(0.7f, 0.8f, 0.9f, 1.0f);
                break;
            case 4:
                processShop.SetItemImage(sellDou.GetComponent<ShopItemData>().buyItemID);
                sellDou.GetComponent<ShopItemData>().UpdateShop();
                sellDou.GetComponent<Image>().color = new Color(0.7f, 0.8f, 0.9f, 1.0f);
                break;
            case 5:
                processShop.SetItemImage(sellGin.GetComponent<ShopItemData>().buyItemID);
                sellGin.GetComponent<ShopItemData>().UpdateShop();
                sellGin.GetComponent<Image>().color = new Color(0.7f, 0.8f, 0.9f, 1.0f);
                break;
            case 6:
                processShop.SetItemImage(sellKin.GetComponent<ShopItemData>().buyItemID);
                sellKin.GetComponent<ShopItemData>().UpdateShop();
                sellKin.GetComponent<Image>().color = new Color(0.7f, 0.8f, 0.9f, 1.0f);
                break;
        }
        //ボタン数字の更新
        sOldButton = sSelectButton;
    }
    // 買うモード、売るモードの変更
    // 引  数：なし
    // 戻り値：なし
    private void ShopModeChange()
    {
        if(buyOrSellswichMode == false)
        {
            buyOrSellswichMode = true;
        }
        else 
        {
            buyOrSellswichMode = false;
        }
        if (buyOrSellswichMode == false)
        {
            processShop.SellModeSwitch();
        }
        else
        {
            processShop.BuyModeSwitch();
        }
    }

    // スクロール処理
    public void WheelScroll(float f)
    {
        if (buyOrSellswichMode == true)  //買う
        {
            BuyModeProcess(f);
        }
        else
        {
            SellModeProcess(f);
        }

    }

    // モード変更
    public void ShopMode()
    {
        ShopModeChange(); //売買モードセレクト
    }


    public void ShopClick()
    {
        if (buyOrSellswichMode)
        {
            //購入処理
            switch (bSelectButton)
            {
                case 0:
                    //processShop.SetItemImage(buyYakusou.GetComponent<ShopItemData>().buyItemID);
                    buyYakusou.GetComponent<ShopItemData>().UpdateShop();
                    buyYakusou.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.BuyClick();
                    break;
                case 1:
                    //processShop.SetItemImage(buyKaihukuyaku.GetComponent<ShopItemData>().buyItemID);
                    buyKaihukuyaku.GetComponent<ShopItemData>().UpdateShop();
                    buyKaihukuyaku.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.BuyClick();
                    break;
                case 2:
                    //processShop.SetItemImage(buyJoukaihukuyaku.GetComponent<ShopItemData>().buyItemID);
                    buyJoukaihukuyaku.GetComponent<ShopItemData>().UpdateShop();
                    buyJoukaihukuyaku.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.BuyClick();
                    break;
                case 3:
                    //processShop.SetItemImage(buyKanzenkaihukuyaku.GetComponent<ShopItemData>().buyItemID);
                    buyKanzenkaihukuyaku.GetComponent<ShopItemData>().UpdateShop();
                    buyKanzenkaihukuyaku.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.BuyClick();
                    break;
                case 4:
                    //processShop.SetItemImage(buyDou.GetComponent<ShopItemData>().buyItemID);
                    buyDou.GetComponent<ShopItemData>().UpdateShop();
                    buyDou.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.BuyClick();
                    break;
                case 5:
                    //processShop.SetItemImage(buyGin.GetComponent<ShopItemData>().buyItemID);
                    buyGin.GetComponent<ShopItemData>().UpdateShop();
                    buyGin.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.BuyClick();
                    break;
                case 6:
                    //processShop.SetItemImage(buyKin.GetComponent<ShopItemData>().buyItemID);
                    buyKin.GetComponent<ShopItemData>().UpdateShop();
                    buyKin.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.BuyClick();
                    break;
                case 7:
                    //processShop.SetItemImage(buyTuruhashi.GetComponent<ShopItemData>().buyItemID);
                    buyTuruhashi.GetComponent<ShopItemData>().UpdateShop();
                    buyTuruhashi.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.BuyClick();
                    break;
            }
        }
        else
        {
            //売る処理
            switch (sSelectButton)
            {
                case 0:
                    //processShop.SetItemImage(buyYakusou.GetComponent<ShopItemData>().buyItemID);
                    sellYakusou.GetComponent<ShopItemData>().UpdateShop();
                    sellYakusou.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.SellClick();
                    break;
                case 1:
                    //processShop.SetItemImage(buyKaihukuyaku.GetComponent<ShopItemData>().buyItemID);
                    sellKaihukuyaku.GetComponent<ShopItemData>().UpdateShop();
                    sellKaihukuyaku.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.SellClick();
                    break;
                case 2:
                    //processShop.SetItemImage(buyJoukaihukuyaku.GetComponent<ShopItemData>().buyItemID);
                    sellJoukaihukuyaku.GetComponent<ShopItemData>().UpdateShop();
                    sellJoukaihukuyaku.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.SellClick();
                    break;
                case 3:
                    //processShop.SetItemImage(buyKanzenkaihukuyaku.GetComponent<ShopItemData>().buyItemID);
                    sellKanzenkaihukuyaku.GetComponent<ShopItemData>().UpdateShop();
                    sellKanzenkaihukuyaku.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.SellClick();
                    break;
                case 4:
                    //processShop.SetItemImage(buyDou.GetComponent<ShopItemData>().buyItemID);
                    sellDou.GetComponent<ShopItemData>().UpdateShop();
                    sellDou.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.SellClick();
                    break;
                case 5:
                    //processShop.SetItemImage(buyGin.GetComponent<ShopItemData>().buyItemID);
                    sellGin.GetComponent<ShopItemData>().UpdateShop();
                    sellGin.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.SellClick();
                    break;
                case 6:
                    //processShop.SetItemImage(buyKin.GetComponent<ShopItemData>().buyItemID);
                    sellKin.GetComponent<ShopItemData>().UpdateShop();
                    sellKin.GetComponent<Image>().color = new Color(0.5f, 0.4f, 0.4f, 1.0f);
                    processShop.SellClick();
                    break;
            }

        }
    }
}
 