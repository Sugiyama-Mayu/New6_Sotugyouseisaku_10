using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    [SerializeField] private int renge;

    [SerializeField] private Transform spownObj;
    [SerializeField] private GameObject[] enemyObj;
    [SerializeField] private List<GameObject> enemySponeObj;
    [SerializeField] private int maxEnemy;
    [SerializeField] private int maxType;
    [SerializeField] private int enemyNum;

    [SerializeField] private bool createMode;

    [SerializeField] private int createCount;
    [SerializeField] private int nowCount;
    [SerializeField] private bool debugMode = false;
    [SerializeField] private int debugnum =0;
    private const float spownOffset = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        if (renge == 0) Debug.LogError("rengeÇ™Åu0ÅvÇ…Ç»Ç¡ÇƒÇ¢Ç‹Ç∑ÅB");
        if (spownObj == null) spownObj = gameObject.transform;
        createMode = false;
        nowCount = createCount;
        maxType = enemyObj.Length * 10;
        Transform cube = transform.GetChild(0);
        cube.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (!createMode) return;
        if (enemyNum < maxEnemy && createCount <= nowCount)
        {
            Create();
            nowCount = 0;
        }
        else
        {
            nowCount++;
        }

    }
    public void Create()
    {
        Vector3 createPos = gameObject.transform.position;
        int rand = 0;
        if( 1 < enemyObj.Length)
        {
            rand = Random.Range(10, maxType);
            rand /= 10;
            rand--;
            //Debug.Log(rand);
        }
        else
        {
            rand = 0;
        }

        if (debugMode)
        {
            rand = debugnum;
        }

        GameObject createObj = Instantiate(enemyObj[rand], spownObj.position +new Vector3(0, spownOffset, 0), gameObject.transform.rotation);
        createObj.GetComponent<Enemy>().GetSetHomeObj = gameObject;
        createObj.GetComponent<EnemyNavMesh>().SetMoveRange(renge, -renge);
        enemySponeObj.Add(createObj);
        enemyNum++;
    }

    private void OnTriggerStay(Collider col)
    {
        if (enemyNum < maxEnemy && createCount <= nowCount)
        {
            Create();
            nowCount = 0;
        }
        else
        {
            nowCount++;
        }

    }

    public void ListClear()
    {
        foreach(GameObject obj in enemySponeObj)
        {
            Destroy(obj);
        }
        enemySponeObj.Clear();
    }

    public void EnemyDead(GameObject obj)
    {
        enemyNum--;
        enemySponeObj.Remove(obj);
    }
}
