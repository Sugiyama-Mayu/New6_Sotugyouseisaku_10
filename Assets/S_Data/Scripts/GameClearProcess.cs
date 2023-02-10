using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// M.S ゲームクリア処理
public class GameClearProcess : MonoBehaviour
{
    [SerializeField] private ConnectionFile connectionFile;
    [SerializeField] private SaveDataFile saveDataFile;
    [SerializeField] private GameObject GameClearObj;   //ゲームクリアオブジェクト
    [SerializeField] private GameObject gameClearImage; //ゲームクリアイメージ
    private float nowAlpha;
    private void Start()
    {
        nowAlpha = 1.0f;
    }
    void Update()
    {
        //ゲームクリア処理を一度もしていなく、全ての祠をクリアしていたら
        //ゲームクリアテキストの表示
        if (saveDataFile.doneGameClear == false)
        {
            if (connectionFile.GetClearShrineNum() >= 7)
            {
                GameObject ringObj = GameObject.Find("SoundObj");
                ringObj.GetComponent<RingSound>().RingSE(2);
                saveDataFile.doneGameClear = true;
                saveDataFile.WriteSaveData();
                StartCoroutine("DrawGameClearText");
            }
        }
    }
    //ゲームクリアテキストの表示
    IEnumerator DrawGameClearText()
    {
        //画像をアクティブ
        GameClearObj.gameObject.SetActive(true);
        for (int i = 0; i < 100; i++)
        {
            //アルファを徐々に減らしてイメージに設定
            nowAlpha = nowAlpha - 0.01f;
            gameClearImage.GetComponent<Image>().color = new Color(gameClearImage.GetComponent<Image>().color.r, gameClearImage.GetComponent<Image>().color.g,
                gameClearImage.GetComponent<Image>().color.b, nowAlpha);
            //アルファが0以下ならば
            if(nowAlpha <= 0.0f)
            {
                GameClearObj.gameObject.SetActive(false); //画像を非表示
                yield break;
            }
            yield return new WaitForSeconds(0.01f); //少し待つ
        }
    }
}
