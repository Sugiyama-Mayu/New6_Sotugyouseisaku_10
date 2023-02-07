using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagerVR : MonoBehaviour
{
    public PlayerManager playerManager;
    public EquimentManager equimentManager;
    public Bow bow;

    private int weaopn = 0;

    public GameObject wearSword;
    public GameObject wearBow;
    public GameObject[] handObj = new GameObject[2];

    [SerializeField] private GameObject[] swordObj;
    [SerializeField] private GameObject[] bowObj;


    // Start is called before the first frame update
    void Start()
    {
        handObj[0] = GameObject.Find("VRHand_L");
        handObj[1] = GameObject.Find("VRHand_R");
        WeaponChange();
    }


    // 武器切り替え（数値変更）
    public void WeaponChange(bool b)
    {
        if(b == true)
        {
            if (weaopn < 2)
            {
                weaopn++;
            }
            else
            {
                weaopn = 0;
            }
        }
        else
        {
            if(0 < weaopn)
            {
                weaopn--;
            }
            else
            {
                weaopn = 2;
            }
        }
        WeaponSwitch();
    }

    //　武器切り替え（表示非表示）
    public void WeaponSwitch()
    {
       // Debug.Log(weaopn);
        switch (weaopn)
        {
            case 0: // 武器なし
                wearSword.SetActive(false);
                wearBow.SetActive(false);
                handObj[0].SetActive(true);
                handObj[1].SetActive(true);
                break;
            case 1: // 剣
                wearSword.SetActive(true);
                wearBow.SetActive(false);
                handObj[0].SetActive(false);
                handObj[1].SetActive(false);
                break;
            case 2: // 弓
                wearSword.SetActive(false);
                wearBow.SetActive(true);
                handObj[0].SetActive(false);
                handObj[1].SetActive(false);
                break;
        }
    }

    // 武器変更
    public void WeaponChange()
    {
        // 武器非表示
        foreach (GameObject Obj in swordObj )
        {
            Obj.SetActive(false);
        }
        foreach (GameObject Obj in bowObj )
        {
            Obj.SetActive(false);
        }

        // 各武器表示
        wearSword = swordObj[0];
        wearBow = bowObj[0];
        bow = wearBow.GetComponent<Bow>();

        // ダメージ設定
        SetLevel();

        //Debug.Log("WeaponChange");
        WeaponSwitch();
    }

    // ダメージ設定
    public void SetLevel()
    {
        // ダメージ格納配列
        int[] value =equimentManager.GetWeaponDamege(0,0);

        wearSword.GetComponent<WeaponDamage>().SetiingDamege = value[0];
        wearBow.GetComponent<Bow>().SetDamege = value[1];
     //   Debug.Log("Sword :" + value[0] +" "+ "Bow :" + value[1]);

    }
}
