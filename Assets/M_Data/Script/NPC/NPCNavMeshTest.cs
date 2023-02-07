using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Anim.Nav;

public class NPCNavMeshTest : MonoBehaviour
{
    public NavController navController;

    [SerializeField] private Transform npcRoot;

    // �ړI�n�̔z��
    [SerializeField] private Transform[] points;
 //   [SerializeField] private Transform player;

    // �ړI�n�̃I�u�W�F�N�g��
    [SerializeField] private int destPoint = 0;

    public bool talk;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
     //   GameObject playerObj = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerObj();
        //player = playerObj.transform;

      //  navController = gameObject.GetComponent<NavController>();
      //  agent = GetComponent<NavMeshAgent>();
        // ���̖ړI�n�ݒ�
        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.8f)
        {
            GotoNextPoint();
/*
            if (!talk)
            {
                GotoNextPoint();
            }
            else
            {
                var aim = player.position - npcRoot.position;
                aim.y = 0;
                var lookRotation = Quaternion.LookRotation(aim, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.2f);
                return;
            }*/
        }
        //if (npcRoot.localRotation != Quaternion.identity) npcRoot.localRotation = Quaternion.identity;
        agent.nextPosition = transform.position;
    }

    public void GotoNextPoint()
    {
        // ����n�_���ݒ肳��Ă��Ȃ����
        if (points.Length == 0)
            return;
        // �ړI�n�ݒ�
        if (navController != null)
        {
            navController.SetDestination(points[destPoint].position);
        }
        else
        {
            agent.destination = points[destPoint].position;
        }

        destPoint = (destPoint + 1) % points.Length;
    }

    /*
        public void TalkStart()
        {
            agent.destination = player.position + player.rotation * new Vector3(0,0,0.8f);
            talk = true;
        }

        public void TalkEnd()
        {
            if (points.Length != 0) agent.destination = points[destPoint].position;
            talk = false;
        }

        public bool GetTalk
        {
            get { return talk; }
        }
    */
}
