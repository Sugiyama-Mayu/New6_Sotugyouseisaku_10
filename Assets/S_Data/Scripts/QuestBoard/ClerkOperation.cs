using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// M.S 
// �N�G�X�g�{�[�h�ɂ���X���̏���
public class ClerkOperation : MonoBehaviour
{
    [SerializeField] private Animator clerkAnim;  // �X���A�j���[�V����
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 fronBoardPos;
    [SerializeField] private RingSound ringSound;

    private bool withinRange;  // �v���C�L�������͈͓��ɂ��邩�ǂ���
    public bool talkMode;      // �X���Ƙb�������ǂ���
    private Vector3 savePos;   // �X���ɘb���������ۂ̃v���C�L�����ʒu����

    void Start()
    {
        withinRange = false;
        talkMode = false;
    }
    void Update()
    {
        // �N�G�X�g�{�[�h�͈͓��ɂ���ꍇ
        if (withinRange == true)
        {
            // �E�N���b�N
            if (Input.GetMouseButtonDown(1))
            {
                // �b���Ă��Ȃ������ꍇ
                if (talkMode == false)
                {
                    ringSound.RingSE(7);
                    savePos = player.transform.position;
                    clerkAnim.SetBool("talk", true);
                    player.transform.position = fronBoardPos;
                    talkMode = true;
                    return;
                }
                // �b�����̏ꍇ
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
            withinRange = true; // �N�G�X�g�{�[�h�͈͓�
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // �N�G�X�g�{�[�h�͈͊O�ɏo��
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
