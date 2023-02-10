using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// M.S 
// クエストボードにいる店員の処理
public class ClerkOperation : MonoBehaviour
{
    [SerializeField] private Animator clerkAnim;  // 店員アニメーション
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 fronBoardPos;
    [SerializeField] private RingSound ringSound;

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
                    ringSound.RingSE(7);
                    savePos = player.transform.position;
                    clerkAnim.SetBool("talk", true);
                    player.transform.position = fronBoardPos;
                    talkMode = true;
                    return;
                }
                // 話し中の場合
                if (talkMode == true)
                {
                    clerkAnim.SetBool("talk", false);
                    player.GetComponentInChildren<InputPlayer>().SetHuntSelectOrder(false);
                    player.GetComponentInChildren<InputPlayer>().awayProcessFlag = true;
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
            player.GetComponentInChildren<InputPlayer>().SetHuntSelectOrder(false);
            player.GetComponentInChildren<InputPlayer>().awayProcessFlag = true;
            withinRange = false;
            talkMode = false;
        }
    }     
}
