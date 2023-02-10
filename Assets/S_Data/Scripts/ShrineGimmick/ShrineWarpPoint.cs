using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// M.S 祠のワープ処理
public class ShrineWarpPoint : MonoBehaviour
{
    [SerializeField] GameObject warpPos;
    [SerializeField] GameObject player;
    [SerializeField] ShrineText shrineText;
    [SerializeField] RotateBall rotateBall;
    //[SerializeField] private GetJewel getJewel;
    [SerializeField] int warpMode;
    [SerializeField] bool rotateShrineFlag;
    private GameObject shrineTextObj; //祠入口テキスト
    private int shrineNum;   //祠ID番号
    private bool inShrine;   //祠入ったフラグ
    private float nowAlpha;  //祠テキスト透明度
    private float nowAlphaC; //祠クリアテキスト透明度
    private bool getJewelFlag = false;
    [SerializeField]private GameObject ball;

    //初期処理
    //引  数：int shrineID  祠ID番号
    //戻り値：なし
    public void SetText(int shrineID)
    {
        nowAlpha = 1.0f;
        nowAlphaC = 1.0f;
        shrineNum = shrineID;
        inShrine = false;
        switch (shrineNum)
        {
            case 401:
                shrineTextObj = shrineText.shrineText1;
                break;
            case 402:
                shrineTextObj = shrineText.shrineText2;
                break;
            case 403:
                shrineTextObj = shrineText.shrineText3;
                break;
            case 404:
                shrineTextObj = shrineText.shrineText4;
                break;
            case 405:
                shrineTextObj = shrineText.shrineText5;
                break;
            case 406:
                shrineTextObj = shrineText.shrineText6;
                break;
            case 407:
                shrineTextObj = shrineText.shrineText7;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject soundObj = GameObject.Find("SoundObj");
        if (other.gameObject.tag == "Player")
        {
            player.transform.position = new Vector3(warpPos.transform.position.x, warpPos.transform.position.y, warpPos.transform.position.z);
            var scr = GameObject.Find("MainSceneObj").GetComponent<MainSceneObj>();
            if(warpMode == 0) //入口ワープ
            {
                scr.menuOffMode = true;
                inShrine = true;
                shrineTextObj.SetActive(true);
                soundObj.GetComponent<RingSound>().RingBGM(2);
                if (rotateShrineFlag == true)
                {
                    rotateBall.rotateModeAllow = true; //玉回転モードを許可にする
                    ball.SetActive(true);
                }
            }
            else //出口ワープ
            {
                scr.menuOffMode = false;
                soundObj.GetComponent<RingSound>().RingBGM(1);
                if (rotateShrineFlag == true)
                {
                    rotateBall.rotateModeAllow = false; //玉回転モードを非許可にする
                    rotateBall.RotateBallModeOff();     //玉回転モードを戻す
                    ball.SetActive(false);
                }
            }

        }
    }
    private void Update()
    {
        //テキスト表示コルーチンを呼び出す
        if (inShrine == true) {
            nowAlpha = 1.0f; //透明度の設定
            StartCoroutine("InvisibleText"); //祠入口テキストの表示
        }
        if (getJewelFlag == true)
        {
            nowAlphaC = 1.0f; //透明度の設定
            StartCoroutine("InvisibleClearText"); //祠クリアテキストの表示
        }
    }
    //祠入口テキストのコルーチン
    IEnumerator InvisibleText()
    {
        //徐々に透明度を下げて表示
        for (int i = 0; i < 50; i++)
        {
            inShrine = false;
            nowAlpha = nowAlpha - 0.02f;
            //下げた透明度でテキストを表示
            shrineTextObj.GetComponent<Image>().color
                = new Color(shrineTextObj.GetComponent<Image>().color.r, shrineTextObj.GetComponent<Image>().color.g, shrineTextObj.GetComponent<Image>().color.b,
                nowAlpha);
            //透明ならば
            if (nowAlpha <= 0.0f)
            {
                shrineTextObj.GetComponent<Image>().color = new Color(shrineTextObj.GetComponent<Image>().color.r, shrineTextObj.GetComponent<Image>().color.g, shrineTextObj.GetComponent<Image>().color.b,
                   0.0f);
                shrineTextObj.gameObject.SetActive(false); //テキストを非表示
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    //祠クリアテキストのコルーチン
    IEnumerator InvisibleClearText()
    {
        //徐々に透明度を下げて表示
        for (int i = 0; i < 50; i++)
        {
            getJewelFlag = false;
            nowAlphaC = nowAlphaC - 0.02f;
            //下げた透明度でテキストを表示
            shrineText.shrineClearText.GetComponent<Image>().color
                = new Color(shrineText.shrineClearText.GetComponent<Image>().color.r, shrineText.shrineClearText.GetComponent<Image>().color.g, shrineText.shrineClearText.GetComponent<Image>().color.b,
                nowAlphaC);
            //透明ならば
            if (nowAlphaC <= 0.0f)
            {
                shrineText.shrineClearText.GetComponent<Image>().color = new Color(shrineText.shrineClearText.GetComponent<Image>().color.r, shrineText.shrineClearText.GetComponent<Image>().color.g, shrineText.shrineClearText.GetComponent<Image>().color.b,
                    0.0f);
                shrineText.shrineClearText.gameObject.SetActive(false); //テキストを非表示
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    // 祠の数字のセッター
    public void SetShrineNum(int setShirineNum)
    {
        shrineNum = setShirineNum;
    }
    // 宝石ゲットフラグのセッター
    public void SetGetJewelFlag(bool getJewel)
    {
        getJewelFlag = getJewel;
    }
}
