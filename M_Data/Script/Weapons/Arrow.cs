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
    public bool electArrowStanSwitch;  // �d�C�̖�̃X�^���X�C�b�`

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

    // �����Ă�������Ɍ���
    void FallArrowTask()
    {
        if (!rb.isKinematic)
        {
            Vector3 ver = rb.velocity;
            Vector3 falldir = ver.normalized;
            //����������������悤�ɂ���
            rb.MoveRotation(Quaternion.LookRotation(falldir));
        }

    }

    public void ArrowShot(Vector3 forwad, float power)
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        // �͂�������
        rb.AddForce(forwad * power, ForceMode.Impulse);
        tr.enabled = true;
        Invoke("SetCol", 0.05f);
        Destroy(gameObject, 10);
    }


    // ��I�u�W�F�N�g�ɓ���������
    public void Stop(Transform target)
    {
        Vector3 ver = rb.velocity;
        transform.parent = target;

        // ��~����
        rb.useGravity = false;
        rb.isKinematic = true;
        tr.enabled = false;

        col.enabled = false;

        rb.transform.position += ver * Time.fixedDeltaTime * 2;

        Destroy(gameObject, 5f);
        // �ڐG�����I�u�W�F�N�g�̕ۑ�
        var collObjScript = target.gameObject.GetComponent<SpreadArrow>();
        // ���āE�ʓd�Ώۂ�����(�K���̏����p)
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
        // �d�C��G�ɓ���������
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
        // �~�܂��Ă��Ȃ����
        if(!rb.isKinematic) Stop(col.transform);
    }
}
