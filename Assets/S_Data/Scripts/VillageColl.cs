using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���̓����ڐG�I�u�W�F
// �K�̃N���A���̍X�V�A�N�G�X�g�{�[�h�̍X�V
public class VillageColl : MonoBehaviour
{
    public int clearShrineNum = 0;
    [SerializeField] private ConnectionFile connectFile;
    //�e�N�G�X�g���e�m�F�{�^��
    [SerializeField] private GameObject questButton1;
    [SerializeField] private GameObject questButton2;
    [SerializeField] private GameObject questButton3;
    [SerializeField] private GameObject questButton4;
    [SerializeField] private GameObject questButton5;
    //�e�N�G�X�g���X�^���v
    [SerializeField] private GameObject unQuestStamp1;
    [SerializeField] private GameObject unQuestStamp2;
    [SerializeField] private GameObject unQuestStamp3;
    [SerializeField] private GameObject unQuestStamp4;
    [SerializeField] private GameObject unQuestStamp5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            clearShrineNum = connectFile.GetClearShrineNum();
            //�N���A�K���ɂ���Ď󒍂ł���N�G�X�g�𒲐�����
            if (clearShrineNum >= 6)
            {
                questButton1.GetComponent<BoxCollider>().enabled = true;
                questButton2.GetComponent<BoxCollider>().enabled = true;
                questButton3.GetComponent<BoxCollider>().enabled = true;
                questButton4.GetComponent<BoxCollider>().enabled = true;
                questButton5.GetComponent<BoxCollider>().enabled = true;
                unQuestStamp1.SetActive(false);
                unQuestStamp2.SetActive(false);
                unQuestStamp3.SetActive(false);
                unQuestStamp4.SetActive(false);
                unQuestStamp5.SetActive(false);
            }
            else if (clearShrineNum >= 5)
            {
                questButton1.GetComponent<BoxCollider>().enabled = true;
                questButton2.GetComponent<BoxCollider>().enabled = true;
                questButton3.GetComponent<BoxCollider>().enabled = true;
                questButton4.GetComponent<BoxCollider>().enabled = true;
                questButton5.GetComponent<BoxCollider>().enabled = false;
                unQuestStamp1.SetActive(false);
                unQuestStamp2.SetActive(false);
                unQuestStamp3.SetActive(false);
                unQuestStamp4.SetActive(false);
                unQuestStamp5.SetActive(true);
            }
            else if (clearShrineNum >= 4)
            {
                questButton1.GetComponent<BoxCollider>().enabled = true;
                questButton2.GetComponent<BoxCollider>().enabled = true;
                questButton3.GetComponent<BoxCollider>().enabled = true;
                questButton4.GetComponent<BoxCollider>().enabled = false;
                questButton5.GetComponent<BoxCollider>().enabled = false;
                unQuestStamp1.SetActive(false);
                unQuestStamp2.SetActive(false);
                unQuestStamp3.SetActive(false);
                unQuestStamp4.SetActive(true);
                unQuestStamp5.SetActive(true);
            }
            else if (clearShrineNum >= 3)
            {
                questButton1.GetComponent<BoxCollider>().enabled = true;
                questButton2.GetComponent<BoxCollider>().enabled = true;
                questButton3.GetComponent<BoxCollider>().enabled = false;
                questButton4.GetComponent<BoxCollider>().enabled = false;
                questButton5.GetComponent<BoxCollider>().enabled = false;
                unQuestStamp1.SetActive(false);
                unQuestStamp2.SetActive(false);
                unQuestStamp3.SetActive(true);
                unQuestStamp4.SetActive(true);
                unQuestStamp5.SetActive(true);
            }
            else if (clearShrineNum >= 2)
            {
                questButton1.GetComponent<BoxCollider>().enabled = true;
                questButton2.GetComponent<BoxCollider>().enabled = false;
                questButton3.GetComponent<BoxCollider>().enabled = false;
                questButton4.GetComponent<BoxCollider>().enabled = false;
                questButton5.GetComponent<BoxCollider>().enabled = false;
                unQuestStamp1.SetActive(false);
                unQuestStamp2.SetActive(true);
                unQuestStamp3.SetActive(true);
                unQuestStamp4.SetActive(true);
                unQuestStamp5.SetActive(true);
            }
            else
            {
                questButton1.GetComponent<BoxCollider>().enabled = false;
                questButton2.GetComponent<BoxCollider>().enabled = false;
                questButton3.GetComponent<BoxCollider>().enabled = false;
                questButton4.GetComponent<BoxCollider>().enabled = false;
                questButton5.GetComponent<BoxCollider>().enabled = false;
                unQuestStamp1.SetActive(true);
                unQuestStamp2.SetActive(true);
                unQuestStamp3.SetActive(true);
                unQuestStamp4.SetActive(true);
                unQuestStamp5.SetActive(true);
            }
        }
    }
}
