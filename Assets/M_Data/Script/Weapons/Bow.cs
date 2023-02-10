using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private Transform stringPos;

    [SerializeField] private GameObject arrowRoot;
    public GameObject arrowObj;
    [SerializeField] private GameObject bowString;

    [Header("��̎��")]
    [SerializeField] private GameObject[] arrowType;
    [SerializeField] public int arrowNum;

    [SerializeField] private int arrowDamege;

    // �|�������Ă��邩
    [SerializeField] private bool drawBow = false;

    // �|�̌�
    private float stringIndex;
    [SerializeField] private float minString = 0.01f;
    [SerializeField] private float maxString = 0.075f;

    [Header("��̕���")]
    [SerializeField] private int directionPower;


    [Header("XR�p")]
    [SerializeField] private Transform handTrans;
    [SerializeField] private bool xrMode = false;


    private Vector3 reSetString;

    // ������
    private float power = 7f;
    public float maxPower = 50f;

    // �v�Z���p�ϐ�
    private float nowTime = 0;
    private float maxTime = 50;


    // Start is called before the first frame update
    void Start()
    {
        arrowNum = 0;
        reSetString = stringPos.localPosition;
        CreateArrow();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.U))
        {
            drawBow = true;
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            ArrowShot();
        }
        */
    }

    private void FixedUpdate()
    {
        if (drawBow && arrowObj != null)
        {
            BowDraw();
        }
    }

    // ���ˏ���
    public void ArrowShot()
    {
        // �͂̌v�Z
        if (!xrMode)
        {
            power = nowTime / maxTime * maxPower;
        }
        else
        {
            power = Mathf.Abs(bowString.transform.localPosition.y) / maxString * maxPower;
            Debug.Log("Pos" + bowString.transform.localPosition.y / maxString);
            Debug.Log(power);
        }
        drawBow = false;
        // �e�q�֌W
        arrowObj.transform.parent = null;
        // �͂�������
        arrowObj.GetComponent<Arrow>().ArrowShot(arrowObj.transform.forward, power);
        // ���l������
        bowString.transform.localPosition = reSetString;
        arrowObj = null;
        nowTime = 0;
        power = 2f;

        // ��̐���
        CreateArrow();
    }

    // ��̐���
    public void CreateArrow()
    {
        arrowObj = Instantiate(arrowType[arrowNum], arrowRoot.transform.position, arrowRoot.transform.rotation);
        arrowObj.transform.parent = arrowRoot.transform;
        arrowObj.GetComponent<WeaponDamage>().SetiingDamege = arrowDamege;
    }

    // �������
    public void BowDraw()
    {
        Vector3 stringPos = Vector3.zero;
        stringIndex = 0;
        // �|�������A�j���[�V��������
        if (!xrMode)
        { 
            // �ʏ�
            if (nowTime < maxTime) nowTime++;
            stringIndex = minString + (maxString - minString) * ((nowTime + 1) * nowTime / 2.0f) / ((maxTime + 1) * maxTime / 2.0f);
        }
        else
        {
            // XR
            if (handTrans.localPosition.z < 0) stringIndex = Mathf.Abs(handTrans.localPosition.z);
            else stringIndex = 0;
            stringIndex /= 10;
            if (maxString < stringIndex) stringIndex = maxString;
            else if (stringIndex < minString) stringIndex = minString;
        }

        // ����
        switch (directionPower)
        {
            case 0:
                stringPos = new Vector3(bowString.transform.localPosition.x, -stringIndex, bowString.transform.localPosition.z);
                break;
            case 1:
                stringPos = new Vector3(stringIndex, bowString.transform.localPosition.y, bowString.transform.localPosition.z);
                break;
            case 2:
                stringPos = new Vector3(bowString.transform.localPosition.x, bowString.transform.localPosition.y, -stringIndex);
                break;
        }
        bowString.transform.localPosition = stringPos;
    }

    // ��̐؂�ւ�
    public void ChengeArrow()
    {
        if (drawBow || arrowObj == null) return;
        Destroy(arrowObj);

        arrowNum++;
        if(arrowType.Length - 1 < arrowNum)
        {
            arrowNum = 0;
        }
        arrowObj = Instantiate(arrowObj);
        Destroy(arrowObj);

        arrowObj = arrowType[arrowNum];
        arrowObj = null;
        CreateArrow();
    }

    public int SetDamege
    {
        set { arrowDamege = value; }
    }

    public void SwitchDrawBow(bool b)
    {
        drawBow = b;
    }

}
