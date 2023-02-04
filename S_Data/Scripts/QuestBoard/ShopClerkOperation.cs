using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S ショップ店員の処理スクリプト
public class ShopClerkOperation : MonoBehaviour
{
    [SerializeField] private Animator clerkAnim;  // 店員アニメーション
    [SerializeField] private GameObject player;
    [SerializeField] private Canvas shopStage;
    [SerializeField] private Camera shopCamera;

    private bool withinRange;  // プレイキャラが範囲内にいるかどうか
    public bool talkMode;      // 店員と話し中かどうか
    private Vector3 savePos;   // 店員に話しかけた際のプレイキャラ位置調整

    void Start()
    {
        withinRange = false;
        talkMode = false;
    }
    void Update()
    {
        // クエストボード範囲内にいる場合
        if (withinRange == true)
        {
            // 右クリック
            if (Input.GetMouseButtonDown(1))
            {
                // 話していなかった場合
                if (talkMode == false)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    shopCamera.gameObject.SetActive(true);
                    clerkAnim.SetBool("talk", true);
                    shopStage.gameObject.SetActive(true);
                    player.SetActive(false);
                    talkMode = true;
                }
                // 話し中の場合
                else if (talkMode == true)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    shopCamera.gameObject.SetActive(false);
                    clerkAnim.SetBool("talk", false);
                    shopStage.gameObject.SetActive(false);
                    player.SetActive(true);
                    talkMode = false;
                }
            }
        }
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
}
