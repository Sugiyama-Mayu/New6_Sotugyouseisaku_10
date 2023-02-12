using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// M.S 
// �N�G�X�g�{�[�h�ɂ���X���̏���
public class ClerkOperation : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator clerkAnim;  // �X���A�j���[�V����
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 fronBoardPos;
    [SerializeField] private RingSound ringSound;
    [SerializeField] private BoxCollider boxCollider;

    private bool withinRange;  // �v���C�L�������͈͓��ɂ��邩�ǂ���
    public bool talkMode;      // �X���Ƙb�������ǂ���
    private Vector3 savePos;   // �X���ɘb���������ۂ̃v���C�L�����ʒu����

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
            withinRange = true; // �N�G�X�g�{�[�h�͈͓�
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // �N�G�X�g�{�[�h�͈͊O�ɏo��
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
        // �N�G�X�g�{�[�h�͈͓��ɂ���ꍇ
        if (withinRange == true)
        {
            // �b���Ă��Ȃ������ꍇ
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
            // �b�����̏ꍇ
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
