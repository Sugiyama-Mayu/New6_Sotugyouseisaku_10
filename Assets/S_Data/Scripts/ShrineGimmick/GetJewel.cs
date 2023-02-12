using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// M.S 
// 祠の宝石ゲットスクリプト
public class GetJewel : MonoBehaviour
{
    [SerializeField] private ShrineClearScript shrineClearScript;
    [SerializeField] private ShrineWarpPoint shrineWarpPoint;
    [SerializeField] private ConnectionFile connectionFile;
    [SerializeField] private ShrineText shrineText;
    [SerializeField] private RingSound ringSound;
    [SerializeField] private int jewelNum;       //獲得宝石数
    [SerializeField] private int shirineJewelId; //祠ID番号
    private string writeStr = "";
    private void Start()
    {
        // 祠IDを他のスクリプトに渡す
        if (shirineJewelId > 0)
        {
            shrineClearScript.SetShrineNum(shirineJewelId);
            shrineWarpPoint.SetText(shirineJewelId);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // 宝石にプレイヤーが触れたら宝石をゲット
        if (other.name == "PlayerRoot" || other.name == "PlayerRootXR")
        {
            // 祠クリアフラグを立てる
            shrineClearScript.SetGetJewelFlag(true);
            // データベースに書き込む文字列の作成
            switch (shirineJewelId)
            {
                case 401:
                    writeStr = shirineJewelId.ToString() + "," + "ウルの証" + "," 
                        + "ウル遺跡を制覇した証" + "," + "1" + "\n";
                    break;
                case 402:
                    writeStr = shirineJewelId.ToString() + "," + "クヴァシルの証" + ","
                        + "クヴァシル遺跡を制覇した証" + "," + "1" + "\n";
                    break;
                case 403:
                    writeStr = shirineJewelId.ToString() + "," + "トールの証" + ","
                        + "トール遺跡を制覇した証" + "," + "1" + "\n";
                    break;
                case 404:
                    writeStr = shirineJewelId.ToString() + "," + "ゲルセミの証" + ","
                        + "ゲルセミ遺跡を制覇した証" + "," + "1" + "\n";
                    break;
                case 405:
                    writeStr = shirineJewelId.ToString() + "," + "スルトの証" + ","
                        + "スルト遺跡を制覇した証" + "," + "1" + "\n";
                    break;
                case 406:
                    writeStr = shirineJewelId.ToString() + "," + "テュールの証" + ","
                        + "テュール遺跡を制覇した証" + "," + "1" + "\n";
                    break;
                case 407:
                    writeStr = shirineJewelId.ToString() + "," + "ロキの証" + ","
                        + "ロキ遺跡を制覇した証" + "," + "1" + "\n";
                    break;
            }
            shrineText.shrineClearText.gameObject.SetActive(true); //祠クリアテキストの表示
            shrineWarpPoint.SetGetJewelFlag(true);              //宝石取得フラグを立てる
            connectionFile.WriteFile(shirineJewelId, writeStr); //テキストファイルに書き込む
            Destroy(this.gameObject); // 宝石を消す
            ringSound.RingSE(19);
            jewelNum++;
        }
    } 
}
