using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// M.S
// �A�C�e���ɂ��ꂼ��A�^�b�`���ď���ۑ�����v���O����
public class ShopItemData : MonoBehaviour, IPointerEnterHandler
{
    // �A�C�e���̌f�[�^
    public ConnectionFile connectionFile;
    public ProcessShop processShop;
    public int buyItemID;
    public int buyKindID;
    public int haveItemID;
    public int haveKindID;
    [SerializeField] private int haveNum;
    [SerializeField] private int buyNum;
    [SerializeField] GameObject itemNameObj;
    [SerializeField] GameObject haveNumObj;
    private string textArray;
    private string array;
    private void Start()
    {
    }    
    // �J�[�\�����������Ə��i���X�V
    public void OnPointerEnter(PointerEventData eventData)
    {
        processShop.SetItemImage(buyItemID);
        UpdateShop();
    }
    // �V���b�v(����)�̎��̏��i���X�V
    // ��  ���F�Ȃ�
    // �߂�l�F�Ȃ�
    public void UpdateShop()
    {
        // �����Ă��鐔�̏����X�V
        textArray = connectionFile.ReadFile(buyItemID, textArray);
        connectionFile.TranslationDataArray(textArray, buyKindID);
        buyNum = connectionFile.buyNum;
        processShop.SetBuyDatabaseArray(textArray);
        processShop.buyNumObj.GetComponent<Text>().text = buyNum.ToString();

        // �����Ă��鐔�̏����X�V
        textArray = connectionFile.ReadFile(haveItemID, textArray);
        connectionFile.TranslationDataArray(textArray, haveKindID);
        haveNum = connectionFile.haveNum;
        processShop.SetHaveDatabaseArray(textArray);
        processShop.haveNumObj.GetComponent<Text>().text = haveNum.ToString();

        if (processShop.buyMode == true)
        {            
            // ���l��\��
            processShop.priceNumObj.GetComponent<Text>().text = connectionFile.buyPrice.ToString();
        }
        else
        {
            // ���l��\��
            processShop.priceNumObj.GetComponent<Text>().text = connectionFile.sellPrice.ToString();
        }
        processShop.SetNowShopItem(this);
        processShop.SetArrayFlag(true);
    }
    // ���������X�g�̏��̍X�V
    // ��  ���F�Ȃ�
    // �߂�l�F�Ȃ�
    public void UpdateHave()
    {
        array = connectionFile.ReadFile(haveItemID, array);
        connectionFile.TranslationDataArray(array, haveKindID);
        haveNumObj.GetComponent<Text>().text = connectionFile.haveNum.ToString();
        itemNameObj.GetComponent<Text>().text = connectionFile.itemName;
    }
}
