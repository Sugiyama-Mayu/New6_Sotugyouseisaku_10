using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Anim.Nav;

public class NPCNavMeshTest : MonoBehaviour
{
    public NavController navController;

    [SerializeField] private Transform npcRoot;

    // 目的地の配列
    [SerializeField] private Transform[] points;
 //   [SerializeField] private Transform player;

    // 目的地のオブジェクト数
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
        // 次の目的地設定
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
        // 巡回地点が設定されていなければ
        if (points.Length == 0)
            return;
        // 目的地設定
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
