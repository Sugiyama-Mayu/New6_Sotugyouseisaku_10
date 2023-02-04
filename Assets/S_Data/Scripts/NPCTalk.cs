using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S NPCトーク
public class NPCTalk : MonoBehaviour
{
    [SerializeField] private GameObject NPCTalkObj;
    private bool talkMode;

    private void Start()
    {
        talkMode = false;
    }
    // 会話時の会話テキストの切り替え
    private void OnTriggerEnter(Collider other)
    {
        // 右クリック
        if (Input.GetMouseButtonDown(1))
        {
            if (talkMode == false)
            {
                NPCTalkObj.SetActive(true);
                talkMode = true;
            }
            else if(talkMode == true)
            {
                NPCTalkObj.SetActive(false);
                talkMode = false;
            }
        }
    }
}
