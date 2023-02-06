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
    [SerializeField] private string[,] qui = new string[,] { {"骨15", "牙10" },     // 剣
                                                             {"骨45", "牙20" },
                                                             {"骨5", "牙20" },      // 弓
                                                             {"骨15", "牙40" },
                                                             {"皮20", "毛皮5" },    //
                                                             {"皮35", "毛皮10" },
                                                             {"皮10", "毛皮10" },   //
                                                             {"皮25", "毛皮15"} ,
                                                             {"銅20", "銀20" },     //
                                                             {"骨30", "金20"} 
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

    //デバッグ用
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

    // 武器強化
    public void WeaponUpgrade(int i, int id)
    {
        if (weaponLevel[i, id] < maxLevel)
        {
            // 強化
            weaponLevel[i, id]++;
            Debug.Log(i + "" + id + ":WeaponLevel" + weaponLevel[i, id]);
        }
        else { Debug.Log(i + "" + id + ":LevelMax"); }
    }

    // 防具強化
    public void ArmorUpgrade(int i, int id)
    {
        if (armorLevel[i, id] < maxLevel)
        {
            armorLevel[i, id]++;
            Debug.Log(i + "" + id + ":ArmorLevel" + armorLevel[i, id]);
        }
        else { Debug.Log(i + "" + id + ":LevelMax"); }
    }
    // つるはし強化
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


    // 強化必要アイテム取得
    //　type 装備の種類
    //　id   
    public string[] GetItem(int type, int id)
    {
        type--;
        // 選択レベル取得
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

        // 強化素材取得
        string[] str = new string[2];
        for (int i =0 ; i < 2; i++)
        {
            str[i] = qui[type,i];
        }
        return str;
    }

    //-------------------------------------------------------------------
    // データベース参照
    // インベントリアイテム取得
    public void GetItemDataBase()
    {
        haveItem[0] = connectionFile.GetMaterialNum("骨");
        haveItem[1] = connectionFile.GetMaterialNum("皮");
        haveItem[2] = connectionFile.GetMaterialNum("牙");
        haveItem[3] = connectionFile.GetMaterialNum("毛皮");
        haveItem[4] = connectionFile.GetMaterialNum("爪");
        haveItem[5] = connectionFile.GetMaterialNum("銅");
        haveItem[6] = connectionFile.GetMaterialNum("銀");
        haveItem[7] = connectionFile.GetMaterialNum("金");
    }

    // アイテムの手持ちの数を取得
    public int GetSelectItemDataBase(string str)
    {
        switch (str)
        {
            case "骨":
                return haveItem[0];
            case "皮":
                return haveItem[1];
            case "牙":
                return haveItem[2];
            case "毛皮":
                return haveItem[3];
            case "爪":
                return haveItem[4];
            case "銅":
                return haveItem[5];
            case "銀":
                return haveItem[6];
            case "金":
                return haveItem[7];

        }
        return 0;
    }

    // アイテム画像参照用
    public int GetSelectItemDataName(string str)
    {
        switch (str)
        {
            case "骨":
                return 0;
            case "皮":
                return 1;
            case "牙":
                return 2;
            case "毛皮":
                return 3;
            case "爪":
                return 4;
            case "銅":
                return 5;
            case "銀":
                return 6;
            case "金":
                return 7;

        }
        return 0;
    }

    // アイテム画像参照用
    public void SetMaterialNum(string name ,int num)
    {
        Debug.Log(name + "を" + num + "消費");
        switch (name)
        {
            case "骨":
                haveItem[0] -= num;
                return;
            case "皮":
                haveItem[1] -= num;
                return;
            case "牙":
                haveItem[2] -= num; 
                return;
            case "毛皮":
                haveItem[3] -= num;
                return;
            case "爪":
                haveItem[4] -= num;
                return;
            case "銅":
                haveItem[5] -= num;
                return;
            case "銀":
                haveItem[6] -= num;
                return;
            case "金":
                haveItem[7] -= num;
                return;

        }
    }

    //public void 

    //-----------------------------------------------------------
    // ゲッター・セッター

    // 防御力を渡す
    public void GetDefenseValue(int i, int j)
    {
        playerManager.SetDefense = armor0Value[i, armorLevel[0, i]] + armor1Value[j, armorLevel[1, j]];
    }
    // 攻撃力を渡す
    public int[] GetWeaponDamege(int sId, int bId)
    {
        int[] value = { swordValue[sId, weaponLevel[0, sId]], bowValue[bId, weaponLevel[1, bId]] };
        return value;
    }


    // 攻撃・防御の2つずつの数値を渡す（強化プレビュー用）
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
                Debug.Log("範囲外");
                break;
        }
        return value;
    }
}
