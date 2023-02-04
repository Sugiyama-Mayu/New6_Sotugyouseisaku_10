using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkNPC : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] private TextMeshProUGUI talkText;

    private int textCount;
    private GameObject npcObj;

    [SerializeField] private List<string> strageText;


    // Start is called before the first frame update
    void Start()
    {
        // ������
        textCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TextCountUp()
    {
        textCount++;
        if (strageText[textCount] != "End")
        {
            // �y�[�W����
            talkText.text = strageText[textCount];
        }
        else
        {
            // �I������
            Debug.Log("�I��");

            // �����l�ݒ�
            ValueReset();
        }
    }

    // �����l�ݒ�
    public void ValueReset()
    {
        gameManager.SetiingActionMap(0);
        gameManager.uiManager.textCanvas.SetActive(false);
        if(npcObj != null)
        {
            npcObj.GetComponent<NPCNavMesh>().TalkEnd();
        }
        npcObj = null;
    }

    public void SetText(GameObject gObj)
    {
        npcObj = gObj;
        textCount = 0;
        strageText = gameManager.dataRead.GetNpcText(gObj.GetComponent<NPCManager>().GetNpcId);
        talkText.text = strageText[textCount];
    }
}
