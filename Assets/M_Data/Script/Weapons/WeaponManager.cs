using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public EquimentManager equimentManager;
    public Bow bow;

    private int weaopn = 0;

    public GameObject wearSword;
    public GameObject wearBow;
    public GameObject handObj;
    public GameObject pickObj;


    [SerializeField] private GameObject[] swordObj;
    [SerializeField] private GameObject[] bowObj;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider swordCol;
    [SerializeField] private Animator handAnim;

    private void Start()
    {
        handAnim = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        if(5 < rb.velocity.magnitude)
        handAnim.SetFloat("Blend", 1);
        else handAnim.SetFloat("Blend", 0);

    }

    // ����؂�ւ��i���l�ύX�j
    public void WeaponChange(bool b)
    {
        if (b == true)
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
            if (0 < weaopn)
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

    //�@����؂�ւ��i�\����\���j
    public void WeaponSwitch()
    {
        switch (weaopn)
        {
            case 0: // ����Ȃ�
                wearSword.SetActive(false);
                wearBow.SetActive(false);
                handObj.SetActive(true);
                break;
            case 1: // ��
                wearSword.SetActive(true);
                wearBow.SetActive(false);
                handObj.SetActive(false);
                break;
            case 2: // �|
                wearSword.SetActive(false);
                wearBow.SetActive(true);
                handObj.SetActive(false);
                break;
        }
        if(pickObj.activeSelf)
        pickObj.SetActive(false);
    }

    // ����ύX
    public void WeaponChange()
    {
        // �����\��
        foreach (GameObject Obj in swordObj)
        {
            Obj.SetActive(false);
        }
        foreach (GameObject Obj in bowObj)
        {
            Obj.SetActive(false);
        }

        // �e����\��
        wearSword = swordObj[playerManager.GetEquipmentNum(0)];
        swordCol = wearSword.GetComponent<BoxCollider>();
        swordCol.enabled = false;

        wearBow = bowObj[playerManager.GetEquipmentNum(1)];
        bow = wearBow.GetComponent<Bow>();

        // �_���[�W�ݒ�
        SetLevel();

        //Debug.Log("WeaponChange");
        WeaponSwitch();
    }

    // �_���[�W�ݒ�
    public void SetLevel()
    {
        // �_���[�W�i�[�z��
        int[] value = equimentManager.GetWeaponDamege(playerManager.GetEquipmentNum(0), playerManager.GetEquipmentNum(1));

        wearSword.GetComponent<WeaponDamage>().SetiingDamege = value[0];
        wearBow.GetComponent<Bow>().SetDamege = value[1];
        //   Debug.Log("Sword :" + value[0] +" "+ "Bow :" + value[1]);

    }
    ///---------------------------------------------------------------------------------------------------------------------------------------------------
    ///�A�j���[�V����
    public void SwordAttack()
    {
        // �A�j���[�V�����J�n
        handAnim.SetTrigger("Attack");
        swordCol.enabled = true;
    }

    public void StrengthAttack()
    {
        // �A�j���[�V�����J�n
        handAnim.SetTrigger("Attack");
        swordCol.enabled = true;
    }

    public void PickStart(bool b)
    {
        if (!b)
        {
            wearSword.SetActive(false);
            wearBow.SetActive(false);
            handObj.SetActive(true);
            pickObj.SetActive(false);
            handAnim.SetTrigger("NoPick");

        }
        else
        {
            wearSword.SetActive(false);
            wearBow.SetActive(false);
            handObj.SetActive(false);
            pickObj.SetActive(true);
            handAnim.SetTrigger("Pick");

        }
    }

    public void AnimEnd()
    {
        swordCol.enabled = false;

    }
    public void PickEnd()
    {
        Invoke("WeaponChange", 0.25f);
    }

}
