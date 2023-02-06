using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Anim.Nav;

public class EnemyNavMesh : MonoBehaviour
{
    private Enemy enemy;
    private EnemyAnim enemyAnim;
    private ThrowObject throwObject;
    private NavMeshAgent agent;
    private NavController navController;
    private CharacterController characterController;
    private FellowManager fellowManager;

    public enum EnemyAiState
    {
        WAIT,           //�s������U��~
        NOMALMOVE,      //�ʏ�ړ�
        TARGETMOVE,     //�^�[�Q�b�g�Ɉړ�
        ATTACK,         //��~���čU��
        SHOT,           //�������U��
        IDLE,           //�ҋ@
        DEAD,
    }
    public EnemyAiState aiState = EnemyAiState.WAIT;
    public EnemyAiState nextState = EnemyAiState.WAIT;

    [SerializeField] private float enemySpeed;
    [SerializeField] private float stopDistance;
    [SerializeField] private float wrpeDistance;
    [SerializeField] private float attackDistance;

    [SerializeField] private Transform enemyRote;
    [SerializeField] private bool wait;
    [SerializeField] private bool wrpeCoolTime;
    [SerializeField]private bool detectTarget; // ���E��
    private bool isAiStateRunning;


    [SerializeField] private bool fellowMode;

    [Header("�U���p�ϐ�")]
    [SerializeField] private bool longRengeAttack;
    [SerializeField] private int attackMode;
    [SerializeField] private int attackSequence;
    [SerializeField] private int countTime;

    [SerializeField] private Transform targetObj;
    [SerializeField] private List<Transform> targetList;

    private string playerName;
    private Transform playerPos;   
    private Transform enemyPos;    
   [SerializeField] private Transform homePosison; // �z�[���I�u�W�F�N�g
    private Vector3 targetPos;

    [SerializeField] private float angle;
    private int[] maxMinPos = new int[2]{0,0};

    private void Awake()
    {
        GameObject playerObj = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerObj();
        playerPos = playerObj.transform;
        playerName = playerObj.name;
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        throwObject = GetComponent<ThrowObject>();
        navController = GetComponent<NavController>();
        enemyAnim = GetComponent<EnemyAnim>();
        fellowManager = playerPos.GetComponentInChildren<FellowManager>();
        characterController = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        agent.updatePosition = true;
        wrpeCoolTime = true;
        attackSequence = 0;
        navController.GetSetSpeed = enemySpeed;
        homePosison = enemy.GetSetHomeObj.transform;
        targetPos = new Vector3(homePosison.position.x, homePosison.position.y, homePosison.position.z);
        RandmPointSet();
        if (longRengeAttack) attackMode = 0;
        else attackMode = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerContinue();
        }

        SetAi();

        switch (aiState)
        {
            case EnemyAiState.WAIT:
                Wait();
                break;
            case EnemyAiState.NOMALMOVE:
                NomalMove();
                break;
            case EnemyAiState.TARGETMOVE:
                TargetMove();
                break;
            case EnemyAiState.ATTACK:
                enemyAnim.SetAttackTrigger(attackMode);
                if (longRengeAttack) attackMode = 0;
                else attackMode = 1;
                break;
            case EnemyAiState.DEAD:
                Wait();
                break;
        }
    }

    // �s�����菈��
    void SetAi()
    {
        if (isAiStateRunning)
        {
            if (enemyRote.localRotation != Quaternion.identity)
                enemyRote.localRotation = Quaternion.identity;
            return;
        }
        InitAi(); // ������
        AiMainRoutine();

        aiState = nextState;
       // Debug.Log("State:" + nextState);
        StartCoroutine(AiTimer());
    }

    // �X�e�[�g�N�[���^�C��
    private IEnumerator AiTimer()
    {
        isAiStateRunning = true;
        yield return new WaitForSeconds(0.5f);
        isAiStateRunning = false;
    }


    // ����������
    void InitAi()
    {
        aiState = EnemyAiState.WAIT;

        if (fellowMode && wrpeCoolTime && wrpeDistance < agent.remainingDistance)
        {
            isAiStateRunning = true;
            characterController.enabled = false;
            wrpeCoolTime = false;

            float[] rand = new float[2] { Random.Range(0.5f, 4), Random.Range(0.5f, 4) }; // ���[�v�p�ϐ�

            Vector3 pos = playerPos.position + playerPos.rotation * new Vector3(-rand[0], 0.5f, -rand[1]);
            agent.Warp(pos);
            characterController.enabled = true;
            isAiStateRunning = false;
        }
    }

    // AI���C�����[�v
    void AiMainRoutine()
    {
        if (wait)
        {
            nextState = EnemyAiState.WAIT;
            wait = false;
            return;
        }
        if (detectTarget || fellowManager.GetTargetObj != null && fellowMode) // ���E�ɓ����Ă��邩
        {
            nextState = EnemyAiState.TARGETMOVE;
        }
        else
        {
            nextState = EnemyAiState.NOMALMOVE;
            attackSequence = 0;
        }
    }

    // �ҋ@����
    private void Wait()
    {
        navController.SetDestination(gameObject.transform.position);
    }

    // �ʏ�ړ�
    private void NomalMove()
    {
        if (enemyRote.localRotation != Quaternion.identity) enemyRote.localRotation = Quaternion.identity;
        if (agent.pathPending) return;

        if (!fellowMode) // �G�̍s��
        {
            if (agent.remainingDistance < 0.8f) RandmPointSet();
        }
        else // �����̍s��
        {
            Ray ray = new (gameObject.transform.position + Vector3.up, gameObject.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red, 0.001f, false);

            FollowTarget(playerPos);
            // �O�ɒ��Ԃ�����Ƃ��Ɏ~�܂�
            if (Physics.Raycast(ray, out var hit, 1f))
            {
                if (hit.collider.gameObject.layer == LayerMask.GetMask("Ignore Raycast") || hit.collider.gameObject.layer == LayerMask.GetMask("Default")) return;
                if(hit.collider.gameObject.name == "BodyCol" && enemySpeed != 0)
                {
                    navController.GetSetSpeed = 0;
                    return;
                }
            }

            //�v���C���[�̋߂��Ȃ�~�܂�
            if (agent.remainingDistance < stopDistance)
            {
                if (enemySpeed != 0)
                    navController.GetSetSpeed = 0;
            }
            else if (stopDistance <= agent.remainingDistance) // �����Ȃ�Ƌ߂Â�
            {
                if (enemySpeed != navController.GetSetSpeed)
                    navController.GetSetSpeed = enemySpeed;
            }

        }
    }


    // �ǐՈړ�
    private void TargetMove()
    {
        if(longRengeAttack == false)
        {
            MeleeAttack(ListSort());
        }
        else
        {
            switch (attackMode)
            {
                case 0:
                    if (!isAiStateRunning) return;
                    attackMode = Random.Range(11, 29);
                    attackMode /= 10;
                    Debug.Log("AttackMode:" +attackMode);
                    break;
                case 1: // �ߐ�
                    MeleeAttack(ListSort());
                    break;
                case 2: // ������
                    LongRangeAttack(ListSort());
                    break;
            }
        }

    }

    // �ߐڍU��
    private void MeleeAttack(Transform trans)
    {
        if (agent.pathPending || !fellowMode && trans == null) return;
        if (!fellowMode) enemyRote.transform.LookAt(new Vector3(trans.position.x, gameObject.transform.position.y, trans.position.z));

        switch (attackSequence)
        {
            case 0: // �߂Â�
                if (agent.remainingDistance < 4.5f)
                {
                    navController.SetDestination(gameObject.transform.position + new Vector3(Random.Range(-2.5f, 2.5f), 0, Random.Range(-2.5f, 2.5f)));
                    if (!fellowMode)
                    {
                        attackSequence = 1;
                        countTime = 0;
                    }
                    else attackSequence = 2;
                }
                else
                {
                    if (!fellowMode) FollowTarget(trans);
                    else FollowEnemy();
                }
                break;
            case 1: // ���E�ړ�
                if (agent.remainingDistance < 0.5f || fellowMode)
                {
                    if (!fellowMode) FollowTarget(trans);
                    else FollowEnemy();
                    attackSequence = 2;
                }
                break;
            case 2: // �X�ɋ߂Â�
                if (agent.remainingDistance < attackDistance)
                {
                    attackSequence = 3;
                }
                else if (200 < countTime)
                {
                    attackSequence = 0;
                    countTime = 0;
                }
                else
                {
                    if (!fellowMode) FollowTarget(trans);
                    else FollowEnemy();
                    countTime++;
                }
                break;
            case 3:
                nextState = EnemyAiState.ATTACK;
                aiState = nextState;
                attackSequence = Random.Range(0, 1);
                break;

        }

    }


    // �������U��
    private void LongRangeAttack(Transform trans)
    {
        if (agent.pathPending || !fellowMode && trans == null) return;
        if (!fellowMode) enemyRote.transform.LookAt(new Vector3(trans.position.x, gameObject.transform.position.y, trans.position.z));

        switch (attackSequence)
        {
            case 0: // �߂Â�
                if (agent.remainingDistance < 6f || 2f < agent.remainingDistance)
                {
                    navController.SetDestination(gameObject.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                    attackSequence = 1;
                    countTime = 0;
                }
                else if (agent.remainingDistance <= 2f)
                {
                    navController.SetDestination(gameObject.transform.position);
                    attackMode = 1;
                }
                else
                {
                    if (!fellowMode) FollowTarget(trans);
                    else FollowEnemy();
                }
                break;
            case 1:
                nextState = EnemyAiState.ATTACK;
                if(!fellowMode)throwObject.SetTargetObj = trans;
                else throwObject.SetTargetObj = fellowManager.GetTargetObj;

                aiState = nextState;
                attackSequence = 0;
                break;

        }
    }


    // �����_���ړ�
    public void RandmPointSet()
    {
        Vector3 pos = new Vector3(targetPos.x + Random.Range(maxMinPos[0], maxMinPos[1]),
                                  targetPos.y,
                                  targetPos.z + Random.Range(maxMinPos[0], maxMinPos[1]));
        navController.SetDestination(pos);
    }

    // �v���C���[�Ǐ]
    private void FollowTarget(Transform trans)
    {
        if(trans != null) navController.SetDestination(trans.position);
    }

    // �^�[�Q�b�g���X�g���̋߂��I�u�W�F�N�g�̌���
    private Transform ListSort()
    {
        if (fellowMode) return null;
        if (targetList.Count == 0) 
        {
            detectTarget = false;
            nextState = EnemyAiState.NOMALMOVE;
            return null;
        }
        else
        {
            if (targetList[0] == null) targetList.Remove(targetList[0]);
            Transform near = targetList[0];
            float distance = Vector3.Distance(transform.root.position, near.position);
            //�@��ԋ߂��G��T��
            foreach (Transform trans in targetList)
            {

                if (trans == null) 
                {
                    targetList.Remove(trans);
                }else if (Vector3.Distance(transform.root.position, trans.transform.position) < distance)
                {
                    near = trans;
                    distance = Vector3.Distance(transform.root.position, trans.transform.position);
                }
            }
            return near;
        }
    }

    // ���ԂɂȂ�����
    private void FollowEnemy()
    {
        enemyPos = fellowManager.GetTargetObj;
        if (enemyPos != null) navController.SetDestination(enemyPos.position);
    }

    // ���[�v�N�[���^�C��
    private IEnumerator WapeCoolTime()
    {
        yield return new WaitForSeconds(2f);
        wrpeCoolTime = true;
    }


    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
//            Debug.Log(col.name);
            if (col.gameObject.name == playerName)
            {
                targetList.Add(col.transform);
            }
            else if(col.gameObject.name == "BodyCol")
            {
                targetList.Add(col.transform.parent);
            }
        }
    }

    public void OnTriggerStay(Collider col)
    {
        // ���E�͈͓̔��̓����蔻��
        if (!fellowMode && !detectTarget)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player")) VisibilityCheck(col);
        }
    }

    //���E�̊p�x���Ɏ��܂��Ă��邩
    private void VisibilityCheck(Collider col)
    {
        Vector3 posDelta = col.transform.position - transform.position;
        float target_angle = Vector3.Angle(transform.forward, posDelta);

        if (target_angle < angle) //target_angle��angle�Ɏ��܂��Ă��邩�ǂ���
        {
            Debug.DrawRay(transform.position, posDelta, Color.red, 0.1f);
            FollowTarget(ListSort());
            detectTarget = true;
            aiState = EnemyAiState.TARGETMOVE;

        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (!fellowMode)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if(col.gameObject.name == playerName)
                {
                    targetList.Remove(col.transform);
                }
                else
                {
                    targetList.Remove(col.transform.parent);
                }
                if (targetList.Count <= 0) detectTarget = false;

            }
        }
    }

    public void SetMoveRange(int max, int min)
    {
        maxMinPos[0] = min;
        maxMinPos[1] = max;
    }

    public Transform SetTransfrom
    {
        set
        {
            homePosison = value;
        }
    }

    public bool SetIsAiStateRunning
    {
        set
        {
            isAiStateRunning = value;
        }
    }

    public bool GetSetFellowMode
    {
        get
        {
            return fellowMode;
        }
        set 
        {
            fellowManager.SetEnemyList(gameObject.transform);
            fellowMode = value;
            detectTarget = false;
            fellowManager.FellowNumCount(true);
            aiState = EnemyAiState.NOMALMOVE;
            nextState = EnemyAiState.NOMALMOVE;
            attackSequence = 0;
            targetList.Clear();
            FollowTarget(playerPos);
        }
    }

    public void PlayerContinue()
    {
        wait = true;
        detectTarget = false;
    }

}

