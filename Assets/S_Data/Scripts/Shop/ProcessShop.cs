using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
// M.S
// �V���b�v����
public class ProcessShop : MonoBehaviour
{
    [SerializeField] private ConnectionFile connectionFile;
    [SerializeField] private SaveDataFile saveDataFile;
    [SerializeField] private TitleUI titleUI;
    // �������\���I�u�W�F�N�g
    public GameObject haveNumObj;  // ������
    public GameObject buyNumObj;   // �����Ă��鐔
    public GameObject priceNumObj; // �l�i
    public GameObject budgedNumObj;// ������
    [SerializeField] private int budgedNum;  // ������
    [SerializeField] private int haveNum;    // ������
    [SerializeField] private int buyNum;     // �����Ă��鐔
    private bool arrayFlag;
    public bool buyMode;   // true ���� false ����

    private string buyTextArray;
    private string haveTextArray;
    public ShopItemData nowShopItem;
    [SerializeField] private GameObject oldImage; // ���\�����Ă���A�C�e���C���[�W
    [SerializeField] private GameObject yakusou;
    [SerializeField] private GameObject kaihukuyaku;
    [SerializeField] private GameObject joukaihukuyaku;
    [SerializeField] private GameObject kanzenkaihukuyaku;
    [SerializeField] private GameObject dokukeshisou;
    [SerializeField] private GameObject gedokuyaku;
    [SerializeField] private GameObject mahikesisou;
    [SerializeField] private GameObject ya;
    [SerializeField] private GameObject koukyunaya;
    [SerializeField] private GameObject hinoya;
    [SerializeField] private GameObject kaminarinoya;
    [SerializeField] private GameObject kemuridama;
    [SerializeField] private GameObject senkoudama;
    [SerializeField] private GameObject akanomagatama;
    [SerializeField] private GameObject aonomagatama;
    [SerializeField] private GameObject midorinomagatama;
    [SerializeField] private GameObject kinomagatama;
    [SerializeField] private GameObject turuhasi;
    [SerializeField] private GameObject akanoturuhasi;
    [SerializeField] private GameObject dounoturuhasi;
    [SerializeField] private GameObject ginnoturuhasi;
    [SerializeField] private GameObject kinnoturuhasi;
    [SerializeField] private GameObject kama;
    [SerializeField] private GameObject akanokama;
    [SerializeField] private GameObject dounokama;
    [SerializeField] private GameObject ginnokama;
    [SerializeField] private GameObject kinnokama;
    [SerializeField] private GameObject akasi1;
    [SerializeField] private GameObject akasi2;
    [SerializeField] private GameObject akasi3;
    [SerializeField] private GameObject akasi4;
    [SerializeField] private GameObject akasi5;
    [SerializeField] private GameObject akasi6;
    [SerializeField] private GameObject akasi7;
    [SerializeField] private GameObject Skawa;
    [SerializeField] private GameObject Skegawa;
    [SerializeField] private GameObject Suroko;
    [SerializeField] private GameObject Mkawa;
    [SerializeField] private GameObject Mkegawa;
    [SerializeField] private GameObject Muroko;
    [SerializeField] private GameObject Lkawa;
    [SerializeField] private GameObject Lkegawa;
    [SerializeField] private GameObject Luroko;
    [SerializeField] private GameObject ken;
    [SerializeField] private GameObject yumi;
    [SerializeField] private GameObject kabuto;
    [SerializeField] private GameObject yoroi;

    [SerializeField] private GameObject buyModeObj;
    [SerializeField] private GameObject sellModeObj;

    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private Color buyButtonNormalColor;
    [SerializeField] private Color sellButtonNormalColor;
    [SerializeField] private Color buyButtonSelectColor;
    [SerializeField] private Color sellButtonSelectColor;

    [SerializeField] private RingSound ringSound;
    private void Start()
    {
        arrayFlag = false;
        oldImage = yakusou;
        UpdateBudgedDisplay();
        buyButton.GetComponent<Image>().color = buyButtonSelectColor;
        buyMode = true;
        buyModeObj.SetActive(true);
        sellModeObj.SetActive(false);

    }
    private void Update()
    {
        UpdateBudgedDisplay();
        if (buyMode == true)
        {
            buyButton.GetComponent<Image>().color = buyButtonSelectColor;
            sellButton.GetComponent<Image>().color = sellButtonNormalColor;
        }
        else
        {
            sellButton.GetComponent<Image>().color = sellButtonSelectColor;
            buyButton.GetComponent<Image>().color = buyButtonNormalColor;
        }
    }
    // �������[�h�ɐ؂�ւ���
    public void BuyModeSwitch()
    {
        if (titleUI.titleMode == false)
        {
            ringSound.RingSE(0);
        }
        buyMode = true;
        buyModeObj.SetActive(true);
        sellModeObj.SetActive(false);
    }
    // ���郂�[�h�ɐ؂�ւ���
    public void SellModeSwitch()
    {
        if (titleUI.titleMode == false)
        {
            ringSound.RingSE(0);
        }
        buyMode = false;
        sellModeObj.SetActive(true);
        buyModeObj.SetActive(false);
    }
    // �������e�L�X�g�̍X�V
    // ��  ���F�Ȃ�
    // �߂�l�F�Ȃ�
    public void UpdateBudgedDisplay()
    {
        // �������e�L�X�g�ŕ\��
        budgedNumObj.GetComponent<Text>().text = saveDataFile.haveMoney.ToString();
    }
    // �w���{�^�����������ꍇ
    public void BuyClick()
    {
        nowShopItem.GetComponent<ShopItemData>().UpdateShop();
        int buyCommaNum = 0;  // �J���}�̐��𐔂���p�ϐ�
        int haveCommaNum = 0;
        string haveTranslateArray = ""; // �f�[�^�x�[�X�ɓ��͗p������
        string buyTranslateArray = "";
        int buyId = 0;        // ID
        int haveId = 0;
        string buyIdArray = "";  // ID�����߂�p������
        string haveIdArray = "";
        string buyNumT = "";     // �����Ă��鏤�i�̐��̕�����
        string haveNumT = "";    // �����Ă��鏤�i�̐��̕�����
        int buyNumTArray = 0;    // �z��̗v�f��
        int haveNumTArray = 0;
        // �����񂩂�ID�����𒊏o
        for (int num = 0; num < 3; num++)
        {
            buyIdArray = buyIdArray + buyTextArray[num];
            haveIdArray = haveIdArray + haveTextArray[num];
        }

        // ID��int�ɕς���
        buyId = Convert.ToInt32(buyIdArray);
        haveId = Convert.ToInt32(haveIdArray);
        nowShopItem.GetComponent<ShopItemData>().UpdateShop();
        // �A�C�e�����w���ł��鏊����������ꍇ
        if (saveDataFile.haveMoney - connectionFile.buyPrice >= 0 && connectionFile.buyNum - 1 >= 0)
        {
            haveTextArray = haveTextArray + "\n";
            buyTextArray = buyTextArray + "\n";
            // �������A�����Ă��鐔�A�����Ă��鐔���X�V
            budgedNum = saveDataFile.haveMoney - connectionFile.buyPrice;
            haveNum = connectionFile.haveNum + 1;
            buyNum = connectionFile.buyNum - 1;
            //
            // 10��菬���������瓪������0������
            if (buyNum < 10)
            {
                buyNumT += "0";
                buyNumT += buyNum.ToString();
            }
            else
            {
                buyNumT += buyNum.ToString();
            }

            if (haveNum < 10)
            {
                haveNumT += "0";
                haveNumT += haveNum.ToString();
            }
            else
            {
                haveNumT += haveNum.ToString();
            }
            // �������ޕ�����buyTranslateArray�����
            for (int i = 0; i < 100; i++)
            {
                if (buyTextArray[i] == '\n')
                {
                    break;
                }
                else if (buyTextArray[i] == ',')
                {
                    buyCommaNum++;
                    // ������ɑ���
                    buyTranslateArray = buyTranslateArray + buyTextArray[i];
                    i++;
                }
                switch (buyCommaNum)
                {
                    case 0:
                        buyTranslateArray = buyTranslateArray + buyTextArray[i];
                        break;
                    case 1:
                        buyTranslateArray = buyTranslateArray + buyTextArray[i];
                        break;
                    case 2:
                        buyTranslateArray = buyTranslateArray + buyNumT[buyNumTArray];
                        buyNumTArray++;
                        break;
                    default:
                        buyTranslateArray = buyTranslateArray + buyTextArray[i];
                        break;
                }
            }
            // �������ޕ�����buyTranslateArray�����
            for (int i = 0; i < 100; i++)
            {

                if (haveTextArray[i] == '\n')
                {
                    break;
                }
                if (haveTextArray[i] == ',')
                {
                    haveCommaNum++;
                    haveTranslateArray = haveTranslateArray + haveTextArray[i];
                    i++;
                }
                switch (haveCommaNum)
                {
                    case 0:
                        haveTranslateArray = haveTranslateArray + haveTextArray[i];
                        break;
                    case 1:
                        haveTranslateArray = haveTranslateArray + haveTextArray[i];
                        break;
                    case 2:
                        haveTranslateArray = haveTranslateArray + haveNumT[haveNumTArray];
                        haveNumTArray++;
                        break;
                    default:
                        haveTranslateArray = haveTranslateArray + haveTextArray[i];
                        break;
                }
            }

            buyTranslateArray = buyTranslateArray + "\n";
            connectionFile.WriteFile(buyId, buyTranslateArray);
            haveTranslateArray = haveTranslateArray + "\n";
            connectionFile.WriteFile(haveId, haveTranslateArray);
            ringSound.RingSE(5);

            // �������̏�������
            saveDataFile.haveMoney = budgedNum;
            saveDataFile.WriteSaveData();
            nowShopItem.GetComponent<ShopItemData>().UpdateShop();
            UpdateBudgedDisplay();
            arrayFlag = false;
            return;
        }

    }

    // ����{�^������������
    public void SellClick()
    {
        int budgedNum = 0;
        // �Q�[�����̍X�V
        nowShopItem.GetComponent<ShopItemData>().UpdateShop();
        int haveCommaNum = 0;           // �J���}�̐��𐔂���p�ϐ�
        string haveTranslateArray = ""; // �f�[�^�x�[�X�ɓ��͗p������
        int haveId = 0;                 // ID
        string haveIdArray = "";        // ID�����߂�p������
        string haveNumT = "";           // �����Ă��鏤�i�̐��̕�����
        int haveNumTArray = 0;          // �����Ă��鐔�z��̗v�f���Ɏg�p 
        haveTextArray = haveTextArray + '\n';
        // ID�ԍ��𒲂ׂ�
        for (int num = 0; num < 3; num++)
        {
            haveIdArray = haveIdArray + haveTextArray[num];
        }
        // ID�ԍ���int�ɕς���
        haveId = Convert.ToInt32(haveIdArray);
        // �A�C�e������1�ȏォ�ǂ���
        if (connectionFile.haveNum - 1 >= 0)
        {
            budgedNum = saveDataFile.haveMoney;
            budgedNum = budgedNum + connectionFile.sellPrice;
            connectionFile.haveNum = connectionFile.haveNum - 1;
            haveNum = connectionFile.haveNum;
            // 10�������ǂ����ŕ����Ĕz������
            if (haveNum < 10)
            {
                haveNumT = "0";
                haveNumT = haveNumT + haveNum.ToString();
                // 01 �̂悤�ɂ���
            }
            else
            {
                haveNumT = haveNum.ToString();
            }
            // �f�[�^�x�[�X�ɏ������ޔz��̍쐬
            for (int i = 0, n = 0; i < 100; i++, n++)
            {
                if (haveTextArray[i] == '\n')
                {
                    break;
                }
                if (haveTextArray[i] == ',')
                {
                    haveCommaNum++;
                    // ������ɑ���
                    haveTranslateArray = haveTranslateArray + haveTextArray[i];
                    //haveTranslateArray = haveTranslateArray;
                    i++;
                }
                switch (haveCommaNum)
                {
                    case 0:
                        haveTranslateArray = haveTranslateArray + haveTextArray[i];
                        break;
                    case 1:
                        haveTranslateArray = haveTranslateArray + haveTextArray[i];
                        break;
                    case 2:  // ������
                        haveTranslateArray = haveTranslateArray + haveNumT[haveNumTArray];
                        haveNumTArray++;
                        break;
                    default:
                        haveTranslateArray = haveTranslateArray + haveTextArray[i];
                        break;
                }
            }
            ringSound.RingSE(5);
            // �����Ă��鐔�̏�������
            haveTranslateArray = haveTranslateArray + "\n";
            connectionFile.WriteFile(haveId, haveTranslateArray);
            nowShopItem.GetComponent<ShopItemData>().UpdateShop();
            // �������̏�������
            saveDataFile.haveMoney = budgedNum;
            saveDataFile.WriteSaveData();
            UpdateBudgedDisplay();

        }
    }
    // �A�C�e���摜�\���p�֐�
    // ��  ��:int id  ID
    // �߂�l:�Ȃ�
    public void SetItemImage(int id)
    {
        oldImage.gameObject.SetActive(false);
        switch (id)
        {
            case 101:  // ��
            case 510:
                yakusou.gameObject.SetActive(true);
                oldImage = yakusou;
                break;
            case 102:  // �񕜖�
            case 511:
                kaihukuyaku.gameObject.SetActive(true);
                oldImage = kaihukuyaku;
                break;
            case 103:  // ��񕜖�
            case 512:
                joukaihukuyaku.gameObject.SetActive(true);
                oldImage = joukaihukuyaku;
                break;
            case 104:  // ���S�񕜖�
            case 513:
                kanzenkaihukuyaku.gameObject.SetActive(true);
                oldImage = kanzenkaihukuyaku;
                break;
            case 105:  // �ŏ���
            case 514:
                dokukeshisou.gameObject.SetActive(true);
                oldImage = dokukeshisou;
                break;
            case 106:  // ��Ŗ�
            case 515:
                gedokuyaku.gameObject.SetActive(true);
                oldImage = gedokuyaku;
                break;
            case 107:  // ��჏�����
            case 516:
                mahikesisou.gameObject.SetActive(true);
                oldImage = mahikesisou;
                break;
            case 108:  // ��
            case 517:
                ya.gameObject.SetActive(true);
                oldImage = ya;
                break;
            case 109:  // �����̖�
            case 518:
                koukyunaya.gameObject.SetActive(true);
                oldImage = koukyunaya;
                break;
            case 110:  // �΂̖�
            case 519:
                hinoya.gameObject.SetActive(true);
                oldImage = hinoya;
                break;
            case 111:  // ���̖�
            case 520:
                kaminarinoya.gameObject.SetActive(true);
                oldImage = kaminarinoya;
                break;
            case 112:  // ����
            case 521:
                kemuridama.gameObject.SetActive(true);
                oldImage = kemuridama;
                break;
            case 113:  // �M����
            case 522:
                senkoudama.gameObject.SetActive(true);
                oldImage = senkoudama;
                break;
            case 114:  // �Ԃ̌���
            case 523:
                akanomagatama.gameObject.SetActive(true);
                oldImage = akanomagatama;
                break;
            case 115:  // �̌���
            case 524:
                aonomagatama.gameObject.SetActive(true);
                oldImage = aonomagatama;
                break;
            case 116:  // �΂̌���
            case 525:
                midorinomagatama.gameObject.SetActive(true);
                oldImage = midorinomagatama;
                break;
            case 117:  // ���̌���
            case 526:
                kinomagatama.gameObject.SetActive(true);
                oldImage = kinomagatama;
                break;
            case 201:  // �c���n�V
            case 601:
                turuhasi.gameObject.SetActive(true);
                oldImage = turuhasi;
                break;
            case 202:  // �Ԃ̃c���n�V
            case 602:
                akanoturuhasi.gameObject.SetActive(true);
                oldImage = akanoturuhasi;
                break;
            case 203:  // ���̃c���n�V
            case 603:
                dounoturuhasi.gameObject.SetActive(true);
                oldImage = dounoturuhasi;
                break;
            case 204:  // ��̃c���n�V
            case 604:
                ginnoturuhasi.gameObject.SetActive(true);
                oldImage = ginnoturuhasi;
                break;
            case 205:  // ���̃c���n�V
            case 605:
                kinnoturuhasi.gameObject.SetActive(true);
                oldImage = kinnoturuhasi;
                break;
            case 206:  // �J�}
            case 606:
                kama.gameObject.SetActive(true);
                oldImage = kama;
                break;
            case 207:  // �Ԃ̃J�}
            case 607:
                akanokama.gameObject.SetActive(true);
                oldImage = akanokama;
                break;
            case 208:  // ���̃J�}
            case 608:
                dounokama.gameObject.SetActive(true);
                oldImage = dounokama;
                break;
            case 209:  // ��̃J�}
            case 609:
                ginnokama.gameObject.SetActive(true);
                oldImage = ginnokama;
                break;
            case 210:  // ���̃J�}
            case 610:
                kinnokama.gameObject.SetActive(true);
                oldImage = kinnokama;
                break;
            /*
        case 301:
            break;
        case 302:
            break;
        case 303:
            break;
            */
            case 401:  // 1�̏�
                akasi1.gameObject.SetActive(true);
                oldImage = akasi1;
                break;
            case 402:  // 2�̏�
                akasi2.gameObject.SetActive(true);
                oldImage = akasi2;
                break;
            case 403:  // 3�̏�
                akasi3.gameObject.SetActive(true);
                oldImage = akasi3;
                break;
            case 404:  // 4�̏�
                akasi4.gameObject.SetActive(true);
                oldImage = akasi4;
                break;
            case 405:  // 5�̏�
                akasi5.gameObject.SetActive(true);
                oldImage = akasi5;
                break;
            case 406:  // 6�̏�
                akasi6.gameObject.SetActive(true);
                oldImage = akasi6;
                break;
            case 407:  // 7�̏�
                akasi7.gameObject.SetActive(true);
                oldImage = akasi7;
                break;
            case 501:  // (��)��
                Skawa.gameObject.SetActive(true);
                oldImage = Skawa;
                break;
            case 502:  // (��)�є�
                Skegawa.gameObject.SetActive(true);
                oldImage = Skegawa;
                break;
            case 503:  // (��)��
                Suroko.gameObject.SetActive(true);
                oldImage = Suroko;
                break;
            case 504:  // (��)��
                Mkawa.gameObject.SetActive(true);
                oldImage = Mkawa;
                break;
            case 505:  // (��)�є�
                Mkegawa.gameObject.SetActive(true);
                oldImage = Mkegawa;
                break;
            case 506:  // (��)��
                Muroko.gameObject.SetActive(true);
                oldImage = Muroko;
                break;
            case 507:  // (��)��
                Lkawa.gameObject.SetActive(true);
                oldImage = Lkawa;
                break;
            case 508:  // (��)�є�
                Lkegawa.gameObject.SetActive(true);
                oldImage = Lkegawa;
                break;
            case 509:  // (��)��
                Luroko.gameObject.SetActive(true);
                oldImage = Luroko;
                break;
            case 701:  // ��
            case 702:
            case 703:
            case 704:
            case 705:
            case 706:
                ken.gameObject.SetActive(true);
                oldImage = ken;
                break;
            case 707:  // �|
            case 708:
            case 709:
            case 710:
            case 711:
            case 712:
                yumi.gameObject.SetActive(true);
                oldImage = yumi;
                break;
            case 713:  // ��
            case 714:
            case 715:
            case 716:
            case 717:
            case 718:
                kabuto.gameObject.SetActive(true);
                oldImage = kabuto;
                break;
            case 719:  // �Z
            case 720:
            case 721:
            case 722:
            case 723:
            case 724:
                yoroi.gameObject.SetActive(true);
                oldImage = yoroi;
                break;
        }
    }
    // �V���b�v�A�C�e���I��(�����Ă������)�f�[�^�z��̃Z�b�^�[
    public void SetBuyDatabaseArray(string array)
    {
        buyTextArray = array;
    }
    // �V���b�v�A�C�e���I��(�������Ă������)�f�[�^�z��̃Z�b�^�[
    public void SetHaveDatabaseArray(string array)
    {
        haveTextArray = array;
    }
    // �t���O�̃Z�b�^�[
    public void SetArrayFlag(bool flag)
    {
        arrayFlag = flag;
    }
    public void SetNowShopItem(ShopItemData shopItemData)
    {
        nowShopItem = shopItemData;
    }
}
