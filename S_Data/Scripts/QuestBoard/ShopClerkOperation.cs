using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S �V���b�v�X���̏����X�N���v�g
public class ShopClerkOperation : MonoBehaviour
{
    [SerializeField] private Animator clerkAnim;  // �X���A�j���[�V����
    [SerializeField] private GameObject player;
    [SerializeField] private Canvas shopStage;
    [SerializeField] private Camera shopCamera;

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
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    shopCamera.gameObject.SetActive(true);
                    clerkAnim.SetBool("talk", true);
                    shopStage.gameObject.SetActive(true);
                    player.SetActive(false);
                    talkMode = true;
                }
                // �b�����̏ꍇ
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
            withinRange = true; // �N�G�X�g�{�[�h�͈͓�
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // �N�G�X�g�{�[�h�͈͊O�ɏo��
        if (other.gameObject == player)
        {
            clerkAnim.SetBool("talk", false);
            withinRange = false;
            talkMode = false;
        }
    }
}
