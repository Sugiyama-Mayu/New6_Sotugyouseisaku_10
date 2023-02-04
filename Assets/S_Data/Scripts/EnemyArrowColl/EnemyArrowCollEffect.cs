using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S エネミーと矢が当たった時の処理(スタン、燃える)
// このスクリプトの整理から開始
public class EnemyArrowCollEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem FireEnemyCollFX;  //炎矢が当たった時のエフェクト
    [SerializeField] private ParticleSystem ElectEnemyCollFX; //電気矢が当たった時のエフェクト
    [SerializeField] private float stanTime;     //電気弓に当たった時の停止時間(今はスクリプトで設定)
    [SerializeField] private Animator enemyAnim; //エネミーのアニメーター
    [SerializeField] private GameObject enemyObj; // エネミーオブジェ
    private float stanCount;  //スタン時間のカウント
    private bool electColl;   //電気弓衝突フラグ
    private float restartAnimTime; //再スタート用の時間
    private string animStateName;  //止まったアニメーションの名前
    private Vector3 savePos;       //位置保存
    private Vector3 saveRotate;    //回転角度の保存
    private GameObject collArrow;  //当たった矢 

    private void Start()
    {
        stanTime = 100.0f;
        electColl = false;
    }
    private void Update()
    {
        // 電気矢の接触判定を見る
        if (electColl == true)
        {
            // スタン時間を足す、対象エネミーを停止(位置、角度の保存)
            stanCount++;
            enemyObj.transform.position = savePos;
            enemyObj.transform.localEulerAngles = saveRotate;
        }
        // スタン時間が経っていたら
        if (stanTime <= stanCount && electColl == true)
        {
            stanCount = 0; //スタン時間初期化
            enemyAnim.enabled = true; //アニメーションを有効
            enemyObj.GetComponent<EnemyNavMesh>().enabled = true; //移動スクリプト有効
            electColl = false; 
            Destroy(collArrow); //矢を消す
            //enemyAnim.Play(animStateName, 0, restartAnimTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 矢が当たったかどうか
        if (collision.gameObject.layer == 7)
        {
            if (collision.gameObject.tag == "FireObj") //火の矢衝突エフェクト
            {
                collArrow = collision.gameObject; //接触した矢の保存
                //エフェクトの再生
                ParticleSystem cloneFireFX = Instantiate(FireEnemyCollFX);
                cloneFireFX.transform.position = collision.transform.position;
                cloneFireFX.Play();
                Destroy(cloneFireFX.gameObject, 5.0f);
            }
            else if (collision.gameObject.tag == "ElectObj") //電気の矢衝突エフェクト
            {
                if (collision.gameObject.GetComponent<Arrow>().electArrowStanSwitch == true)
                {
                    collArrow = collision.gameObject; //接触した矢の保存
                    enemyObj.GetComponent<EnemyNavMesh>().enabled = false; //移動スクリプトを無効
                    enemyAnim.enabled = false; //アニメーションを無効
                    // 停止位置の保存
                    savePos = enemyObj.gameObject.transform.position;
                    saveRotate = enemyObj.gameObject.transform.localEulerAngles;
                    //電気矢接触フラグを立てる
                    collision.gameObject.GetComponent<Arrow>().electArrowStanSwitch = false;
                    electColl = true; 
                    //エフェクトの再生
                    ParticleSystem cloneElectFX = Instantiate(ElectEnemyCollFX);
                    cloneElectFX.transform.position = collision.transform.position;
                    cloneElectFX.Play();
                    Destroy(cloneElectFX.gameObject, 5.0f);
                    //restartAnimTime = enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    /*if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree"))
                    {
                        animStateName = "Blend Tree";
                    }
                    else if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("UD_infantry_07_attack_A"))
                    {
                        animStateName = "UD_infantry_07_attack_A";
                    }
                    else if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                    {
                        animStateName = "attack";
                    }
                    else if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("attack0"))
                    {
                        animStateName = "attack0";
                    }
                    else if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
                    {
                        animStateName = "attack1";
                    }*/
                }
            }
        }
    }
}
