using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �K�̃M�~�b�N
// ��̉��ď���
public class SpreadArrow : MonoBehaviour
{
    public int spreadTypeFlag;    // 0:���Ă��Ă��Ȃ� 1:���̉��� 2:�d�C�̉���
    public bool spreadDoneFlag;   // false:������ true:���čς�
    public bool spreadStartFlag;  // false:���Č��̖�������Ă��Ȃ� true:��������

    public ParticleSystem spreadFX_Fire;   // ���ẴG�t�F�N�g
    public ParticleSystem spreadFX_Elect;  // ����(�d�C)�̃G�t�F�N�g
    private bool doneAddFlag;              // list�ɒǉ������I�u�W�F�N�g���ǂ����t���O
    public GameObject startSpreadObj;      // ����������I�u�W�F�N�g
    //public static bool CollFlag = false;   // ������������ǂ����t���O
    [SerializeField] private float electricShockTime; // ���d����
    private float keepElectricShockTime;      // ���ۂ̊��d����
    public bool keepElectricFlag;             // ���d��ԃt���O
    public bool burnFlag;                     // �R���Ă��邩�ǂ����t���O
    public Arrow arrow;
    private bool initialFlag;
    private float burnTime;  // �I�u�W�F�N�g��������܂ł̎���
    [SerializeField] private GameObject destroyCollider;
    [SerializeField] RingSound ringSound;
    public SpreadArrowManager spreadArrowManager;
    void Start()
    {
        burnFlag = false;
        burnTime = 100;
        spreadDoneFlag = false;
        doneAddFlag = false;
        initialFlag = true;
        keepElectricShockTime = electricShockTime;
    }

    private void Update()
    {
        // ���d�]�C����
        if(keepElectricFlag == true)
        {
            if (keepElectricShockTime > 0)
            {
                keepElectricShockTime--;
            }
            else
            {
                keepElectricFlag = false;
                keepElectricShockTime = electricShockTime;
            }
        }
        if (spreadArrowManager.overLapColl <= spreadArrowManager.doneEffect)
        {
            if (spreadArrowManager.destroyNum < spreadArrowManager.overLapColl)
            {
                if (spreadArrowManager.overObj[spreadArrowManager.destroyNum].GetComponent<SpreadArrow>().burnFlag == true)
                {
                    if (spreadArrowManager.overObj[spreadArrowManager.destroyNum].GetComponent<SpreadArrow>().burnTime > 0)
                    {
                        spreadArrowManager.overObj[spreadArrowManager.destroyNum].GetComponent<SpreadArrow>().burnTime--;
                    }
                    else
                    {
                        if (spreadArrowManager.overObj[spreadArrowManager.destroyNum].activeSelf == true)
                        {
                            spreadArrowManager.effectObj[spreadArrowManager.destroyNum].gameObject.SetActive(false);
                            spreadArrowManager.overObj[spreadArrowManager.destroyNum].SetActive(false);
                            Destroy(spreadArrowManager.overObj[spreadArrowManager.destroyNum].GetComponent<SpreadArrow>().destroyCollider.gameObject);
                            spreadArrowManager.destroyNum++;
                        }
                        if (spreadArrowManager.destroyNum >= spreadArrowManager.overLapColl)
                        {
                            spreadArrowManager.doneEffect = 0;
                            spreadArrowManager.overObj.Clear();
                            spreadArrowManager.effectObj.Clear();
                            
                            Destroy(spreadArrowManager.collArrow);
                        }
                    }
                }
            }
        }
    }

    // ���Ă����邽�߂ɃI�u�W�F�N�g�ƐڐG���Ă�����̂̔�������
    private void OnCollisionStay(Collision collision)
    {
        //MMMM
        if (spreadArrowManager.CollFlag == true && collision.gameObject.tag != "DoneFireObj")
        {
            if (spreadArrowManager.collObj.layer == collision.gameObject.layer
               && spreadArrowManager.collArrow.tag == collision.gameObject.tag)
            {
                // �ڐG���Ă���I�u�W�F�N�g��list�^�ɒǉ����Ă���
                // �S��list�ɒǉ����Ă��Ȃ����ǂ���
                if (spreadArrowManager.overLapColl > spreadArrowManager.doneOver)
                {
                    // �ڐG���Ă���I�u�W�F�N�g�̃X�N���v�g�������Ă���
                    var collScript = collision.gameObject.GetComponent<SpreadArrow>();
                    // �^�O�����ă^�O"FireObj"���ǂ�������
                    if (spreadArrowManager.collObj.GetComponent<SpreadArrow>().initialFlag == true)
                    {
                        spreadArrowManager.overObj.Add(spreadArrowManager.collObj);
                        spreadArrowManager.collObj.GetComponent<SpreadArrow>().doneAddFlag = true;
                        spreadArrowManager.collObj.GetComponent<SpreadArrow>().initialFlag = false;
                        spreadArrowManager.doneOver++;
                    }
                    if (/*collision.gameObject.tag == "FireObj" &&*/ collScript.doneAddFlag == false)
                    {
                        spreadArrowManager.overObj.Add(collision.gameObject);
                        collScript.doneAddFlag = true;    // list�ǉ��t���O��true
                        spreadArrowManager.doneOver++;    // list�ǉ������̂�+1
                    }
                }
                else
                {
                    // ���̃G�t�F�N�g�̑҂����Ԃ����������ǂ���
                    if (spreadArrowManager.waitEffectTime <= 0)
                    {
                        // ���ă^�C�v������
                        if (spreadTypeFlag == 0)
                        {
                            return;
                        }
                        // ��������Ă��đS�ẴG�t�F�N�g���������Ă��Ȃ��ꍇ
                        else if (spreadArrowManager.CollFlag == true && spreadArrowManager.doneEffect < spreadArrowManager.overLapColl)
                        {
                            // list�^�̃I�u�W�F�N�g�̃X�N���v�g
                            var collScript = spreadArrowManager.overObj[spreadArrowManager.doneEffect].GetComponent<SpreadArrow>();
                            // ��ڐG�����I�u�W�F�N�g�̃X�N���v�g
                            startSpreadObj = spreadArrowManager.collObj;
                            var startCollScript = startSpreadObj.GetComponent<SpreadArrow>();
                            // ���čς݂łȂ��A��������Ă�����
                            if (collScript.spreadDoneFlag == false && startCollScript.spreadStartFlag == true)
                            {
                                // ���Ẵ^�C�v������
                                switch (collScript.spreadTypeFlag)
                                {
                                    case 1:  // ��
                                        if (collision.gameObject.tag == "FireObj")
                                        {
                                            // ���ď���
                                            ParticleSystem fire_1 = Instantiate(spreadFX_Fire);
                                            spreadArrowManager.effectObj.Add(fire_1);
                                            // ���Ĉʒu��������ɏグ��
                                            spreadArrowManager.effectObj[spreadArrowManager.doneEffect].transform.position = spreadArrowManager.overObj[spreadArrowManager.doneEffect].transform.position;
                                            spreadArrowManager.effectObj[spreadArrowManager.doneEffect].transform.position += new Vector3(0.0f, 1.0f, 0.0f);
                                            spreadArrowManager.effectObj[spreadArrowManager.doneEffect].Play();
                                            collScript.spreadStartFlag = true;
                                            collScript.spreadDoneFlag = true;
                                            spreadArrowManager.doneEffect++;
                                            collision.gameObject.tag = "DoneFireObj";
                                            //collScript.burnFlag = true;
                                            collScript.burnFlag = true;
                                            Destroy(fire_1.gameObject,6);
                                            ringSound.RingSE(17);
                                        }
                                        break;
                                    case 2: // �d�C
                                        if (collision.gameObject.tag == "ElectObj")
                                        {
                                            // ���ď���
                                            ParticleSystem elect_1 = Instantiate(spreadFX_Elect);
                                            // ���Ĉʒu��������ɏグ��
                                            elect_1.transform.position = spreadArrowManager.overObj[spreadArrowManager.doneEffect].transform.position;
                                            elect_1.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
                                            elect_1.Play();
                                            Destroy(elect_1.gameObject, 5.0f);
                                            collScript.spreadStartFlag = true;
                                            collScript.spreadDoneFlag = true;
                                            //spreadDoneFlag = true;
                                            spreadArrowManager.doneEffect++;
                                            collScript.keepElectricFlag = true;
                                            ringSound.RingSE(16);

                                        }
                                        break;
                                }
                            }

                        }
                        spreadArrowManager.waitEffectTime = spreadArrowManager.waitEffectTimeCopy;
                    }
                    else
                    {
                        spreadArrowManager.waitEffectTime--;
                    }
                }
               
            }

        }
        // ���ď���
        if ((spreadArrowManager.overLapColl <= spreadArrowManager.doneEffect)
            && spreadArrowManager.overObj.Count > 0)
        {
            spreadArrowManager.collObj.GetComponent<SpreadArrow>().initialFlag = true;
            spreadArrowManager.CollFlag = false;
            spreadArrowManager.doneOver = 0;
            if (spreadTypeFlag == 2)
            {
                for (int i = 0; i < spreadArrowManager.overLapColl; i++)
                {
                    spreadArrowManager.overObj[i].GetComponent<SpreadArrow>().spreadDoneFlag = false;
                    startSpreadObj.GetComponent<SpreadArrow>().spreadStartFlag = false;
                    spreadArrowManager.overObj[i].GetComponent<SpreadArrow>().doneAddFlag = false;

                }
                spreadArrowManager.doneEffect = 0;
            }
          

        }
        // �R�����g�Ɠd�C����
    }
}
