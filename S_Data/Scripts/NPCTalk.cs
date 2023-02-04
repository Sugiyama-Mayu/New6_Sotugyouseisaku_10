using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S NPC�g�[�N
public class NPCTalk : MonoBehaviour
{
    [SerializeField] private GameObject NPCTalkObj;
    private bool talkMode;

    private void Start()
    {
        talkMode = false;
    }
    // ��b���̉�b�e�L�X�g�̐؂�ւ�
    private void OnTriggerEnter(Collider other)
    {
        // �E�N���b�N
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
