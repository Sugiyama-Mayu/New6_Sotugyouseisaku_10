using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FellowManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private SphereCollider sphereCol;
    [SerializeField] private Transform targetObj;
    [SerializeField] private List<Transform> targetList;
    [SerializeField] private bool isFellow;
    [SerializeField] private int fellowNum;
   
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sphereCol = GetComponent<SphereCollider>();
        sphereCol.enabled = false;

        targetObj = null;
        targetList.Clear();
    }

    private void LateUpdate()
    {
        if (isFellow)
        {
            if(1 < targetList.Count)
            {
                targetObj = ListSort();
            }
            else if(targetList.Count == 1)
            {
                targetObj = targetList[0];
            }
            else
            {
                targetObj = null;
            }
        }
    }


    private Transform ListSort()
    {
        if(targetList[0]== null)
        {
            targetList.Remove(targetList[0]);
        }
        Transform near = targetList[0];
        float distance = Vector3.Distance(transform.root.position, near.position);

        //　一番近いアイテムを探す
        foreach (Transform tarns in targetList)
        {
            if (tarns == null) targetList.Remove(tarns);
            if (Vector3.Distance(transform.root.position, tarns.transform.position) < distance)
            {
                near = tarns;
                distance = Vector3.Distance(transform.root.position, tarns.transform.position);
            }
        }
        return near;
    }
    /*
    // コライダーに当たったら
    private void OnTriggerEnter(Collider col)
    {


        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            targetList.Add(col.transform.parent);
        }

    }
    */
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (targetList.Contains(col.transform.parent)) return;
            else targetList.Add(col.transform.parent);
        }
    }

    // コライダーから抜けたら
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            targetList.Remove(col.transform.parent);

    }
    /// <summary>
    /// 仲間の数  true:++, false:--
    /// </summary> 
    /// <param name="b"></param>
    public void FellowNumCount(bool b)
    {
        // 数値の加算・減少
        if (b) fellowNum++;
        else fellowNum--;
        Debug.Log("仲間の数" + fellowNum);

        // 仲間がいるか
        if (0 < fellowNum) 
        {
            if(sphereCol.enabled == false)
            {
                isFellow = true;
                sphereCol.enabled = true;
            }
        }
        else
        {
            isFellow = false;
            sphereCol.enabled = false;

            targetObj = null;
            targetList.Clear();
        }
    }

    public void SetEnemyList(Transform transform)
    {
        targetObj = null;
        targetList.Remove(transform);
    }

    public Transform GetTargetObj
    {
        get { return targetObj; }
    }

    public int GetFellowNum
    {
        get { return fellowNum; }
    }

}
