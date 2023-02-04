using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anim.Nav;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavController navController;
    [SerializeField] private WeaponDamage damage;
    [SerializeField] private ConnectionFile connectionFile;
    [SerializeField] private ConnectionQuestFile connectionQuestFile;

    [SerializeField] private GameObject matObj;
    [SerializeField] private Material[] fellowMat;
    private EnemyNavMesh enemyNav;
    private EnemyAnim enemyAnim;
    private CreateEnemy createEnemy;
    private FellowManager fellowManager;


    private GameObject homeObj;
    private Animator animator;
    [SerializeField] private GameObject ragDollObj;
 
    [Header("�h���b�v")]
    [SerializeField] private GameObject dropObj;
    [SerializeField] private string dropName;
    [SerializeField] private int dropCount;
    [SerializeField] private int[] dropProbability;

    [Header("����")]
    [SerializeField] private int allyProbability;
    [SerializeField] private GameObject bodyCol;
    [SerializeField] private GameObject AttackCol;


    [Header("�X�e�[�^�X")]
    [SerializeField] private char enemyName;
    [SerializeField] private int enemyId;
    [SerializeField] private float maxHp;
    [SerializeField] private float hp;
    [SerializeField] private float defense;
    [SerializeField] private float[] moveSpeed;
    [SerializeField] private bool deadOnes;  

    [SerializeField] private BoxCollider swordCol;


    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerObj();
        fellowManager = playerObj.GetComponentInChildren<FellowManager>();
        createEnemy = homeObj.GetComponent<CreateEnemy>();

        enemyAnim = GetComponent<EnemyAnim>();

        enemyNav = GetComponent<EnemyNavMesh>();
        enemyNav.SetTransfrom = homeObj.transform;

        animator = GetComponent<Animator>();

        navController = gameObject.GetComponent<NavController>();
        navController.GetSetSpeed = moveSpeed[0];

        connectionFile = GameObject.Find("Connection").GetComponent<ConnectionFile>();
        connectionFile.GetEnmName_DropItemName(enemyId);
        dropName = connectionFile.dropItem;
        enemyName = SettingNameData();

        connectionQuestFile = GameObject.Find("ConnectQuestFile").GetComponent<ConnectionQuestFile>();

        matObj.GetComponent<Renderer>().material = fellowMat[0];

        ragDollObj.SetActive(false);
        deadOnes = false;
        hp = maxHp;
    }

    // ���S����
    private void Dead()
    {
        Debug.Log("dead");
        navController.GetSetSpeed = 0;
        enemyNav.SetIsAiStateRunning = true;
        animator.enabled = false;
        ragDollObj.SetActive(true);
        if (!deadOnes)
        {
            if(enemyNav.GetSetFellowMode == false)createEnemy.EnemyDead(gameObject);
            deadOnes = true;
            Invoke("EnemyDestroy", 3.0f);
        }
    }

    // �G������
    private void EnemyDestroy()
    {

        int rnd = Random.Range(0, 100); // �m���v�Z
        if (enemyNav.GetSetFellowMode == true) // ���ԂȂ�
        {
            Destroy(gameObject);
        }
        else if (rnd < allyProbability && fellowManager.GetFellowNum < 100) // ���ԂɂȂ�E���Ԃ̐����ő�ɂȂ��ĂȂ����
        {
           // DropItem();
            FellowMode();
        }
        else // ����ȊO
        {
           // DropItem();
            Destroy(gameObject);
        }
    }
    
    // �����ɂ���
    private void FellowMode()
    {
        AttackCol.layer = LayerMask.NameToLayer("PlayerWeapon");
        bodyCol.layer = LayerMask.NameToLayer("Player");
        ragDollObj.SetActive(false);
        animator.enabled = true;
        enemyAnim.SetResetAttackTrigger();
        enemyNav.GetSetFellowMode = true;
        deadOnes = false;
        hp = maxHp / 2;
        enemyNav.SetIsAiStateRunning = false;
        matObj.GetComponent<Renderer>().material = fellowMat[1];

    }

    // �h���b�v����
    private void DropItem()
    {
        int count = 0;
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
        for (int i = 0; i < dropCount; i++)
        {
            int rnd = Random.Range(0, 100); // �m���v�Z
            if (rnd < dropProbability[i])
            {
                GameObject game = Instantiate(dropObj, pos, gameObject.transform.rotation);  // ��������
                count++;
                Destroy(game, 3);
            }
            // Debug.Log(dropItem[i].name + " �m���F" + rnd);
        }
        connectionFile.SetMaterialNum(true, dropName, count);
        connectionQuestFile.KnockEnemy(SettingNameData());
    }

    //
    // 301:�X�P���g�� 302:�S�u���� 303:�o���p�C�A  304:�I�[�N 305:�D�I�[�N 
    //S(�X�P���g��)�AG(�S�u����)�AB(�o���p�C�A)�AO(�I�[�N)�AH(�D�I�[�N)
    private char SettingNameData()
    {
        char c = 'n';
        switch (enemyId)
        {
            case 301:
                c = 'S';
                break;
            case 302:
                c = 'G';
                break;
            case 303:
                c = 'B';
                break;
            case 304:
                c = 'O';
                break;
            case 305:
                c = 'H';
                break;
        }
        return c;
    }

    public float GetMoveSpeed(int i)
    {
        if (i == 0)
        {
            return moveSpeed[0];
        }
        else if(i == 1)
        {
            return moveSpeed[1];
        }
        return moveSpeed[0];
    }
    public GameObject GetSetHomeObj
    {
        get
        {
            return homeObj;
        }
        set
        {
            homeObj = value;
        }
    }


    public float SetDamege
    {
        set
        {
            Debug.Log("EDamege :" + ((value / 2) - (defense / 4)));
            // �_���[�W = �i�U���� �� 2�j - �i�h��� �� 4�j
            hp -= (value / 2) - (defense / 4);
            if (hp <= 0)
            {
                hp = 0;
                Dead();
            }
        }
    }

}
