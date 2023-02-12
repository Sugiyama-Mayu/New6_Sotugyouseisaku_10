using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 祠のギミック
// 矢の延焼処理
public class SpreadArrow : MonoBehaviour
{
    public int spreadTypeFlag;    // 0:延焼していない 1:炎の延焼 2:電気の延焼
    public bool spreadDoneFlag;   // false:未延焼 true:延焼済み
    public bool spreadStartFlag;  // false:延焼元の矢が当たっていない true:当たった

    public ParticleSystem spreadFX_Fire;   // 延焼のエフェクト
    public ParticleSystem spreadFX_Elect;  // 延焼(電気)のエフェクト
    private bool doneAddFlag;              // listに追加したオブジェクトかどうかフラグ
    public GameObject startSpreadObj;      // 矢が当たったオブジェクト
    //public static bool CollFlag = false;   // 矢が当たったかどうかフラグ
    [SerializeField] private float electricShockTime; // 感電時間
    private float keepElectricShockTime;      // 実際の感電時間
    public bool keepElectricFlag;             // 感電状態フラグ
    public bool burnFlag;                     // 燃えているかどうかフラグ
    public Arrow arrow;
    private bool initialFlag;
    private float burnTime;  // オブジェクトが消えるまでの時間
    [SerializeField] private GameObject destroyCollider;
    [SerializeField] RingSound ringSound;
    public SpreadArrowManager spreadArrowManager;
    void Start()
    {
        burnFlag = false;
        burnTime = 100;
        spreadDoneFlag = false;
        doneAddFlag = false;
        initialFlag = true;
        keepElectricShockTime = electricShockTime;
    }

    private void Update()
    {
        // 感電余韻時間
        if(keepElectricFlag == true)
        {
            if (keepElectricShockTime > 0)
            {
                keepElectricShockTime--;
            }
            else
            {
                keepElectricFlag = false;
                keepElectricShockTime = electricShockTime;
            }
        }
        if (spreadArrowManager.overLapColl <= spreadArrowManager.doneEffect)
        {
            if (spreadArrowManager.destroyNum < spreadArrowManager.overLapColl)
            {
                if (spreadArrowManager.overObj[spreadArrowManager.destroyNum].GetComponent<SpreadArrow>().burnFlag == true)
                {
                    if (spreadArrowManager.overObj[spreadArrowManager.destroyNum].GetComponent<SpreadArrow>().burnTime > 0)
                    {
                        spreadArrowManager.overObj[spreadArrowManager.destroyNum].GetComponent<SpreadArrow>().burnTime--;
                    }
                    else
                    {
                        if (spreadArrowManager.overObj[spreadArrowManager.destroyNum].activeSelf == true)
                        {
                            spreadArrowManager.effectObj[spreadArrowManager.destroyNum].gameObject.SetActive(false);
                            spreadArrowManager.overObj[spreadArrowManager.destroyNum].SetActive(false);
                            Destroy(spreadArrowManager.overObj[spreadArrowManager.destroyNum].GetComponent<SpreadArrow>().destroyCollider.gameObject);
                            spreadArrowManager.destroyNum++;
                        }
                        if (spreadArrowManager.destroyNum >= spreadArrowManager.overLapColl)
                        {
                            spreadArrowManager.doneEffect = 0;
                            spreadArrowManager.overObj.Clear();
                            spreadArrowManager.effectObj.Clear();
                            
                            Destroy(spreadArrowManager.collArrow);
                        }
                    }
                }
            }
        }
    }

    // 延焼させるためにオブジェクトと接触しているものの判定を取る
    private void OnCollisionStay(Collision collision)
    {
        //MMMM
        if (spreadArrowManager.CollFlag == true && collision.gameObject.tag != "DoneFireObj")
        {
            if (spreadArrowManager.collObj.layer == collision.gameObject.layer
               && spreadArrowManager.collArrow.tag == collision.gameObject.tag)
            {
                // 接触しているオブジェクトをlist型に追加していく
                // 全てlistに追加していないかどうか
                if (spreadArrowManager.overLapColl > spreadArrowManager.doneOver)
                {
                    // 接触しているオブジェクトのスクリプトを持ってくる
                    var collScript = collision.gameObject.GetComponent<SpreadArrow>();
                    // タグが延焼タグ"FireObj"かどうか見る
                    if (spreadArrowManager.collObj.GetComponent<SpreadArrow>().initialFlag == true)
                    {
                        spreadArrowManager.overObj.Add(spreadArrowManager.collObj);
                        spreadArrowManager.collObj.GetComponent<SpreadArrow>().doneAddFlag = true;
                        spreadArrowManager.collObj.GetComponent<SpreadArrow>().initialFlag = false;
                        spreadArrowManager.doneOver++;
                    }
                    if (/*collision.gameObject.tag == "FireObj" &&*/ collScript.doneAddFlag == false)
                    {
                        spreadArrowManager.overObj.Add(collision.gameObject);
                        collScript.doneAddFlag = true;    // list追加フラグをtrue
                        spreadArrowManager.doneOver++;    // list追加したので+1
                    }
                }
                else
                {
                    // 次のエフェクトの待ち時間がたったかどうか
                    if (spreadArrowManager.waitEffectTime <= 0)
                    {
                        // 延焼タイプを見る
                        if (spreadTypeFlag == 0)
                        {
                            return;
                        }
                        // 矢が当たっていて全てのエフェクトが発生していない場合
                        else if (spreadArrowManager.CollFlag == true && spreadArrowManager.doneEffect < spreadArrowManager.overLapColl)
                        {
                            // list型のオブジェクトのスクリプト
                            var collScript = spreadArrowManager.overObj[spreadArrowManager.doneEffect].GetComponent<SpreadArrow>();
                            // 矢が接触したオブジェクトのスクリプト
                            startSpreadObj = spreadArrowManager.collObj;
                            var startCollScript = startSpreadObj.GetComponent<SpreadArrow>();
                            // 延焼済みでなく、矢が当たっていたら
                            if (collScript.spreadDoneFlag == false && startCollScript.spreadStartFlag == true)
                            {
                                // 延焼のタイプを見る
                                switch (collScript.spreadTypeFlag)
                                {
                                    case 1:  // 炎
                                        if (collision.gameObject.tag == "FireObj")
                                        {
                                            // 延焼処理
                                            ParticleSystem fire_1 = Instantiate(spreadFX_Fire);
                                            spreadArrowManager.effectObj.Add(fire_1);
                                            // 延焼位置を少し上に上げる
                                            spreadArrowManager.effectObj[spreadArrowManager.doneEffect].transform.position = spreadArrowManager.overObj[spreadArrowManager.doneEffect].transform.position;
                                            spreadArrowManager.effectObj[spreadArrowManager.doneEffect].transform.position += new Vector3(0.0f, 1.0f, 0.0f);
                                            spreadArrowManager.effectObj[spreadArrowManager.doneEffect].Play();
                                            collScript.spreadStartFlag = true;
                                            collScript.spreadDoneFlag = true;
                                            spreadArrowManager.doneEffect++;
                                            collision.gameObject.tag = "DoneFireObj";
                                            //collScript.burnFlag = true;
                                            collScript.burnFlag = true;
                                            Destroy(fire_1.gameObject,6);
                                            ringSound.RingSE(17);
                                        }
                                        break;
                                    case 2: // 電気
                                        if (collision.gameObject.tag == "ElectObj")
                                        {
                                            // 延焼処理
                                            ParticleSystem elect_1 = Instantiate(spreadFX_Elect);
                                            // 延焼位置を少し上に上げる
                                            elect_1.transform.position = spreadArrowManager.overObj[spreadArrowManager.doneEffect].transform.position;
                                            elect_1.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
                                            elect_1.Play();
                                            Destroy(elect_1.gameObject, 5.0f);
                                            collScript.spreadStartFlag = true;
                                            collScript.spreadDoneFlag = true;
                                            //spreadDoneFlag = true;
                                            spreadArrowManager.doneEffect++;
                                            collScript.keepElectricFlag = true;
                                            ringSound.RingSE(16);

                                        }
                                        break;
                                }
                            }

                        }
                        spreadArrowManager.waitEffectTime = spreadArrowManager.waitEffectTimeCopy;
                    }
                    else
                    {
                        spreadArrowManager.waitEffectTime--;
                    }
                }
               
            }

        }
        // 延焼処理
        if ((spreadArrowManager.overLapColl <= spreadArrowManager.doneEffect)
            && spreadArrowManager.overObj.Count > 0)
        {
            spreadArrowManager.collObj.GetComponent<SpreadArrow>().initialFlag = true;
            spreadArrowManager.CollFlag = false;
            spreadArrowManager.doneOver = 0;
            if (spreadTypeFlag == 2)
            {
                for (int i = 0; i < spreadArrowManager.overLapColl; i++)
                {
                    spreadArrowManager.overObj[i].GetComponent<SpreadArrow>().spreadDoneFlag = false;
                    startSpreadObj.GetComponent<SpreadArrow>().spreadStartFlag = false;
                    spreadArrowManager.overObj[i].GetComponent<SpreadArrow>().doneAddFlag = false;

                }
                spreadArrowManager.doneEffect = 0;
            }
          

        }
        // コメントと電気処理
    }
}
