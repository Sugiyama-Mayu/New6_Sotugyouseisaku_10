using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquimentManager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerManager playerManager;
    public WeaponManager weaponManager;
    public WeaponManagerVR weaponManagerVR;

    public ConnectionFile connectionFile;
    [SerializeField] private int[] haveItem;
    [SerializeField] private string[,] qui = new string[,] { {"��15", "��10" },     // ��
                                                             {"��45", "��20" },
                                                             {"��5", "��20" },      // �|
                                                             {"��15", "��40" },
                                                             {"��20", "�є�5" },    //
                                                             {"��35", "�є�10" },
                                                             {"��10", "�є�10" },   //
                                                             {"��25", "�є�15"} ,
                                                             {"��20", "��20" },     //
                                                             {"��30", "��20"} 
                                                           };


    private int[,] swordValue = new int[,] { { 7, 13, 20 }, { 0, 0, 0 } };
    private int[,] bowValue = new int[,] { { 5, 10, 15 }, { 10, 17, 25 } };
    private int[,] armor0Value = new int[,] { { 1, 3, 5 }, { 0, 0, 0 } };
    private int[,] armor1Value = new int[,] { { 2, 4, 6 }, { 0, 0, 0 } };

    private int[,] weaponLevel = new int[,] { { 0, 0 }, { 0, 0 } };
    private int[,] armorLevel = new int[,] { { 0, 0 }, { 0, 0 } };
    private int pickLevel = 0;
    private int maxLevel = 2;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        haveItem = new int[8];

    }

    //�f�o�b�O�p
    /*
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GetItem(0, 0);
            GetItem(1, 0);
            GetItem(2, 0);
            GetItem(3, 0);
        }
    }
    */

    // ���틭��
    public void WeaponUpgrade(int i, int id)
    {
        if (weaponLevel[i, id] < maxLevel)
        {
            // ����
            weaponLevel[i, id]++;
            Debug.Log(i + "" + id + ":WeaponLevel" + weaponLevel[i, id]);
        }
        else { Debug.Log(i + "" + id + ":LevelMax"); }
    }

    // �h���
    public void ArmorUpgrade(int i, int id)
    {
        if (armorLevel[i, id] < maxLevel)
        {
            armorLevel[i, id]++;
            Debug.Log(i + "" + id + ":ArmorLevel" + armorLevel[i, id]);
        }
        else { Debug.Log(i + "" + id + ":LevelMax"); }
    }
    // ��͂�����
    public void PickUpgrade()
    {
        if (pickLevel < maxLevel)
        {
            pickLevel++;
            Debug.Log(":PickLevel" + pickLevel);
        }
        else { Debug.Log("LevelMax"); }
    }
    
    public string GetLevel(int i)
    {
        int value = 0;
        switch (i)
        {
            case 1:
                value = weaponLevel[0, 0];
                break;
            case 2:
                value = weaponLevel[1, 0];
                break;
            case 3:
                value = armorLevel[0, 0];
                break;
            case 4:
                value = armorLevel[0, 1];
                break;
            case 5:
                value = pickLevel;
                break;
        }

        return value.ToString();
    }


    // �����K�v�A�C�e���擾
    //�@type �����̎��
    //�@id   
    public string[] GetItem(int type, int id)
    {
        type--;
        // �I�����x���擾
        int level;
        switch (type)
        {
            case 0:
                level = weaponLevel[0,id];
                break;
            case 1:
                level = weaponLevel[1, id];
                break;
            case 2:
                level = armorLevel[0, id];
                break;
            case 3:
                level = armorLevel[1, id];
                break;
            case 4:
                level = pickLevel;
                break;
            default:
                level = 2;
                break;
        }

        if (2 <= level) return null;
        type =  type * 2 +level;

        // �����f�ގ擾
        string[] str = new string[2];
        for (int i =0 ; i < 2; i++)
        {
            str[i] = qui[type,i];
        }
        return str;
    }

    //-------------------------------------------------------------------
    // �f�[�^�x�[�X�Q��
    // �C���x���g���A�C�e���擾
    public void GetItemDataBase()
    {
        haveItem[0] = connectionFile.GetMaterialNum("��");
        haveItem[1] = connectionFile.GetMaterialNum("��");
        haveItem[2] = connectionFile.GetMaterialNum("��");
        haveItem[3] = connectionFile.GetMaterialNum("�є�");
        haveItem[4] = connectionFile.GetMaterialNum("��");
        haveItem[5] = connectionFile.GetMaterialNum("��");
        haveItem[6] = connectionFile.GetMaterialNum("��");
        haveItem[7] = connectionFile.GetMaterialNum("��");
    }

    // �A�C�e���̎莝���̐����擾
    public int GetSelectItemDataBase(string str)
    {
        switch (str)
        {
            case "��":
                return haveItem[0];
            case "��":
                return haveItem[1];
            case "��":
                return haveItem[2];
            case "�є�":
                return haveItem[3];
            case "��":
                return haveItem[4];
            case "��":
                return haveItem[5];
            case "��":
                return haveItem[6];
            case "��":
                return haveItem[7];

        }
        return 0;
    }

    // �A�C�e���摜�Q�Ɨp
    public int GetSelectItemDataName(string str)
    {
        switch (str)
        {
            case "��":
                return 0;
            case "��":
                return 1;
            case "��":
                return 2;
            case "�є�":
                return 3;
            case "��":
                return 4;
            case "��":
                return 5;
            case "��":
                return 6;
            case "��":
                return 7;

        }
        return 0;
    }

    // �A�C�e���摜�Q�Ɨp
    public void SetMaterialNum(string name ,int num)
    {
        Debug.Log(name + "��" + num + "����");
        switch (name)
        {
            case "��":
                haveItem[0] -= num;
                return;
            case "��":
                haveItem[1] -= num;
                return;
            case "��":
                haveItem[2] -= num; 
                return;
            case "�є�":
                haveItem[3] -= num;
                return;
            case "��":
                haveItem[4] -= num;
                return;
            case "��":
                haveItem[5] -= num;
                return;
            case "��":
                haveItem[6] -= num;
                return;
            case "��":
                haveItem[7] -= num;
                return;

        }
    }

    //public void 

    //-----------------------------------------------------------
    // �Q�b�^�[�E�Z�b�^�[

    // �h��͂�n��
    public void GetDefenseValue(int i, int j)
    {
        playerManager.SetDefense = armor0Value[i, armorLevel[0, i]] + armor1Value[j, armorLevel[1, j]];
    }
    // �U���͂�n��
    public int[] GetWeaponDamege(int sId, int bId)
    {
        int[] value = { swordValue[sId, weaponLevel[0, sId]], bowValue[bId, weaponLevel[1, bId]] };
        return value;
    }


    // �U���E�h���2���̐��l��n���i�����v���r���[�p�j
    public int[] GetState(int a, int id)
    {
        int[] value = { 0, 0 };

        switch (a)
        {
            case 1:
                if (maxLevel < weaponLevel[0, id] + 1)
                {
                    value = new int[] { swordValue[id, maxLevel], swordValue[id, maxLevel] };
                }
                else
                {
                    value = new int[] { swordValue[id, weaponLevel[0, id]], swordValue[id, weaponLevel[0, id] + 1] };
                }
                return value;
            case 2:
                if (maxLevel < weaponLevel[1, id] + 1)
                {
                    value = new int[] { bowValue[id, maxLevel], bowValue[id, maxLevel] };
                }
                else
                {
                    value = new int[] { bowValue[id, weaponLevel[1, id]], bowValue[id, weaponLevel[1, id] + 1] };
                }
                return value;
            case 3:
                if (maxLevel < armorLevel[0, id] + 1)
                {
                    value = new int[] { armor0Value[id, maxLevel], armor0Value[id, maxLevel] };
                }
                else
                {
                    value = new int[] { armor0Value[id, armorLevel[0, id]], armor0Value[id, armorLevel[0, id] + 1] };
                }
                return value;
            case 4:
                if (maxLevel < armorLevel[1, id] + 1)
                {
                    value = new int[] { armor1Value[id, maxLevel], armor1Value[id, maxLevel] };
                }
                else
                {
                    value = new int[] { armor1Value[id, armorLevel[1, id]], armor1Value[id, armorLevel[1, id] + 1] };
                }
                return value;
            default:
                Debug.Log("�͈͊O");
                break;
        }
        return value;
    }
}
