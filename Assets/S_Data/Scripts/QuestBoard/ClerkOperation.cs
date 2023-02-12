using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// M.S 
// クエストボードにいる店員の処理
public class ClerkOperation : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator clerkAnim;  // 店員アニメーション
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 fronBoardPos;
    [SerializeField] private RingSound ringSound;
    [SerializeField] private BoxCollider boxCollider;

    private bool withinRange;  // プレイキャラが範囲内にいるかどうか
    public bool talkMode;      // 店員と話し中かどうか
    private Vector3 savePos;   // 店員に話しかけた際のプレイキャラ位置調整

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            if(gameManager.GetSetXRMode == false)
            {
                player.GetComponentInChildren<InputPlayer>().SetHuntSelectOrder(false);
                player.GetComponentInChildren<InputPlayer>().awayProcessFlag = true;
            }
            else
            {
                player.GetComponentInChildren<InputPlayerVR>().SetHuntSelectOrder(false);
                player.GetComponentInChildren<InputPlayerVR>().awayProcessFlag = true;
            }
            withinRange = false;
            talkMode = false;
        }
    }     

    public void QuestBoardStartEnd()
    {
        // クエストボード範囲内にいる場合
        if (withinRange == true)
        {
            // 話していなかった場合
            if (talkMode == false)
            {
                gameManager.SetiingActionMap(4);
                ringSound.RingSE(7);
                savePos = player.transform.position;
                clerkAnim.SetBool("talk", true);
                player.transform.position = fronBoardPos;
                talkMode = true;
                boxCollider.enabled = false;
                return;
            }
            // 話し中の場合
            if (talkMode == true)
            {
                clerkAnim.SetBool("talk", false);
                if(gameManager.GetSetXRMode == false)
                {
                    player.GetComponentInChildren<InputPlayer>().SetHuntSelectOrder(false);
                    player.GetComponentInChildren<InputPlayer>().awayProcessFlag = true;
                }
                else
                {
                    player.GetComponentInChildren<InputPlayerVR>().SetHuntSelectOrder(false);
                    player.GetComponentInChildren<InputPlayerVR>().awayProcessFlag = true;
                }
                talkMode = false;
                boxCollider.enabled = true;
            }
        }

    }

}
