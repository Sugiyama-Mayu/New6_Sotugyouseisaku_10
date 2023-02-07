using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anim.Nav;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private NavController navController;
    [SerializeField] private WeaponDamage damage;
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
 
    [Header("ドロップ")]
    [SerializeField] private GameObject dropObj;
    [SerializeField] private string dropName;
    [SerializeField] private int dropCount;
    [SerializeField] private int[] dropProbability;

    [Header("味方")]
    [SerializeField] private int allyProbability;
    [SerializeField] private GameObject bodyCol;
    [SerializeField] private GameObject AttackCol;


    [Header("ステータス")]
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject playerObj = gameManager.GetComponent<GameManager>().GetPlayerObj();
        fellowManager = playerObj.GetComponentInChildren<FellowManager>();
        createEnemy = homeObj.GetComponent<CreateEnemy>();

        enemyAnim = GetComponent<EnemyAnim>();

        enemyNav = GetComponent<EnemyNavMesh>();
        enemyNav.SetTransfrom = homeObj.transform;

        animator = GetComponent<Animator>();

        navController = gameObject.GetComponent<NavController>();
        navController.GetSetSpeed = moveSpeed[0];

        enemyName = SettingNameData();

        connectionQuestFile = GameObject.Find("ConnectQuestFile").GetComponent<ConnectionQuestFile>();

        matObj.GetComponent<Renderer>().material = fellowMat[0];

        ragDollObj.SetActive(false);
        deadOnes = false;
        hp = maxHp;
    }

    // 死亡処理
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

    // 敵を消す
    private void EnemyDestroy()
    {

        int rnd = Random.Range(0, 100); // 確率計算
        if (enemyNav.GetSetFellowMode == true) // 仲間なら
        {
            Destroy(gameObject);
        }
        else if (rnd < allyProbability && fellowManager.GetFellowNum < 100) // 仲間になる・仲間の数が最大になってなければ
        {
            DropItem();
            FellowMode();
        }
        else // それ以外
        {
            DropItem();
            Destroy(gameObject);
        }
    }
    
    // 味方にする
    private void FellowMode()
    {
        Destroy(GetComponent<SphereCollider>());
        AttackCol.layer = LayerMask.NameToLayer("PlayerWeapon");
        bodyCol.layer = LayerMask.NameToLayer("Player");
        ragDollObj.SetActive(false);
        animator.enabled = true;
        enemyAnim.SetResetAttackTrigger();
        enemyNav.GetSetFellowMode = true;
        deadOnes = false;
       // hp = maxHp / 2;
        hp = maxHp * 20;
        enemyNav.SetIsAiStateRunning = false;
        matObj.GetComponent<Renderer>().material = fellowMat[1];
        matObj.layer = LayerMask.NameToLayer("Outline");
    }

    // ドロップ処理
    private void DropItem()
    {
        int count = 0;

        Vector3 pos = new Vector3(gameObject.transform.position.x + Random.Range(-0.8f, 0.5f), gameObject.transform.position.y + 0.5f, gameObject.transform.position.z + Random.Range(-0.8f, 0.5f));
        for (int i = 0; i < dropProbability.Length; i++)
        {
            int rnd = Random.Range(0, 100); // 確率計算
            if (rnd < dropProbability[i])
            {
                GameObject game = Instantiate(dropObj, pos, gameObject.transform.rotation);  // 生成処理
                count++;
                Destroy(game, 5);
            }
            // Debug.Log(dropItem[i].name + " 確率：" + rnd);
        }
        gameManager.SetMaterial(dropName, count);
        connectionQuestFile.KnockEnemy(SettingNameData());
    }

    //
    // 301:スケルトン 302:ゴブリン 303:バンパイア  304:オーク 305:灰オーク 
    //S(スケルトン)、G(ゴブリン)、B(バンパイア)、O(オーク)、H(灰オーク)
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
            // ダメージ = （攻撃力 ÷ 2） - （防御力 ÷ 4）
            hp -= (value / 2) - (defense / 4);
            if (hp <= 0)
            {
                hp = 0;
                Dead();
            }
        }
    }

}
