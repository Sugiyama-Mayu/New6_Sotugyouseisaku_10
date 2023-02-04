using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;
    private TrailRenderer tr;

    public ParticleSystem fire;
    public GameObject arrowCollObj;
    public bool electArrowStanSwitch;  // 電気の矢のスタンスイッチ

    // public static GameObject collObj;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        tr = GetComponent<TrailRenderer>();
        rb.isKinematic = true;
        rb.useGravity = false;
        tr.enabled = false;
        col.enabled = false;
        electArrowStanSwitch = false;
    }

    void Update()
    {
        FallArrowTask();
    }

    // 落ちている方向に向く
    void FallArrowTask()
    {
        if (!rb.isKinematic)
        {
            Vector3 ver = rb.velocity;
            Vector3 falldir = ver.normalized;
            //落ちる方向を向くようにする
            rb.MoveRotation(Quaternion.LookRotation(falldir));
        }

    }

    public void ArrowShot(Vector3 forwad, float power)
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        // 力を加える
        rb.AddForce(forwad * power, ForceMode.Impulse);
        tr.enabled = true;
        Invoke("SetCol", 0.05f);
        Destroy(gameObject, 10);
    }


    // 矢がオブジェクトに当たった時
    public void Stop(Transform target)
    {
        Vector3 ver = rb.velocity;
        transform.parent = target;

        // 停止処理
        rb.useGravity = false;
        rb.isKinematic = true;
        tr.enabled = false;

        col.enabled = false;

        rb.transform.position += ver * Time.fixedDeltaTime * 2;

        Destroy(gameObject, 5f);
        // 接触したオブジェクトの保存
        var collObjScript = target.gameObject.GetComponent<SpreadArrow>();
        // 延焼・通電対象か見る(祠内の処理用)
        if (target.gameObject.tag == this.gameObject.tag &&
           (target.gameObject.tag == "FireObj" && collObjScript.spreadArrowManager.CollFlag == false) || target.gameObject.tag == "ElectObj")
        {
            collObjScript.spreadStartFlag = true;
            collObjScript.spreadArrowManager.collArrow = this.gameObject;
            if (target.gameObject.tag == "FireObj")
            {
                collObjScript.spreadArrowManager.collArrow = Instantiate(this.gameObject);
            }
            collObjScript.spreadArrowManager.CollFlag = true;
            collObjScript.startSpreadObj = target.gameObject;
            collObjScript.spreadArrowManager.collObj = target.gameObject;
        }
        // 電気矢が敵に当たった時
        else if (this.gameObject.tag == "ElectObj" && target.gameObject.tag == "Enemy")
        {
            electArrowStanSwitch = true;
        }
    }
   

    private void SetCol()
    {
        col.enabled = true;
    }

    private void OnCollisionEnter(Collision col)
    {
        // 止まっていなければ
        if(!rb.isKinematic) Stop(col.transform);
    }
}
