using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    [SerializeField] private CreateEnemy[] createEnemy;
    [SerializeField] private Transform playerObj;

    [SerializeField] private float count;


    // Start is called before the first frame update
    void Start()
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerObj = manager.GetPlayerObj().transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*
        if(20 < count)
        {

        }
        else
        {
            count += Time.deltaTime;
        }
        */
    }

   // private void DestroyEnemy()


    public void AllDestroyEnemy()
    {
        for(int i=0; i< createEnemy.Length; i++)
        {
            createEnemy[i].ListClear();
        }

    }
}
