using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S ショップ店員の処理スクリプト
public class ShopClerkOperation : MonoBehaviour
{
    [SerializeField] private Animator clerkAnim;  // 店員アニメーション
    [SerializeField] private GameObject player;
    [SerializeField] private Canvas shopStage;
    [SerializeField] private RingSound ringSound;

    private bool withinRange;  // プレイキャラが範囲内にいるかどうか
    public bool talkMode;      // 店員と話し中かどうか
    [SerializeField]private Vector3 savePos;   // 店員に話しかけた際のプレイキャラ位置調整
    [SerializeField]private Vector3 saveRot;   // 店員に話しかけた際のプレイキャラ位置調整

    void Start()
    {
        withinRange = false;
        talkMode = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            withinRange = true; // クエストボード範囲内
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // クエストボード範囲外に出た
        if (other.gameObject == player)
        {
            clerkAnim.SetBool("talk", false);
            withinRange = false;
            talkMode = false;
        }
    }

    public bool StartEndShop()
    {
        // クエストボード範囲内にいる場合
        if (withinRange == true)
        {
            // 話していなかった場合
            if (talkMode == false)
            {
                ringSound.RingSE(4);
                // 移動・ローテーション
                player.transform.position = savePos;
                clerkAnim.SetBool("talk", true);
                shopStage.gameObject.SetActive(true);
                talkMode = true;
            }
            // 話し中の場合
            else if (talkMode == true)
            {
                clerkAnim.SetBool("talk", false);
                shopStage.gameObject.SetActive(false);
                talkMode = false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 GetRot()
    {
        return saveRot;
    }
}
