using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// ��H�̒[�ɂ���u���b�N�̒ʓd����
public class ConectElectBlock : MonoBehaviour
{
    public ConnectElectManager connectElectManager;
    // ��H�̒[�ɂ���u���b�N�̐�
    [SerializeField] private int allConnectBlockNum;
    public Material electroMat;  // �ʓd��̃}�e���A��
    public Material normalMat;   // �ʏ펞�̃}�e���A��
    static private int loopJudge;
    private bool nowConnect;
    //private int timeCount;
    void Start()
    {
        loopJudge = 0;
        nowConnect = false;
    }

    void Update()
    {
        // �S�Ẳ�H�̒[�u���b�N����x�ɂ݂�
        loopJudge++;
        if(loopJudge <= allConnectBlockNum)
        {
            // list�ɒǉ�����Ă���u���b�N������Ȃ��ꍇ
            connectElectManager.electJudgeObj.Add(this.gameObject);
        }
        else
        {
            // �S�Ēǉ����ꂽ�ꍇ
            loopJudge = 0;
            connectElectManager.electJudgeObj.Clear();
        }
        //timeCount++;
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "ElectObj")
        {
            // �ׂ̉�H�u���b�N���ʓd���Ă�����
            if(collision.gameObject.GetComponent<SpreadArrow>().keepElectricFlag == true)
            {
                // �}�e���A����ύX�A�t���O�𗧂Ă�
                this.gameObject.GetComponent<Renderer>().material = electroMat;
                nowConnect = true;
            }
        
        /* �ʓd��̏����i�ݒ肵�Ă��Ȃ��j
         * else if(connectElectManager.whileConnectTime <= timeCount)
        {
            this.gameObject.GetComponent<Renderer>().material = normalMat;
            nowConnect = false;
            timeCount = 0;
        }*/
        }
    }
    // ��H�̒[�ɂ���u���b�N�̒ʓd�t���O�̃Q�b�^�[
    // ��  ���F�Ȃ�
    // �߂�l�Fbool  nowConnect
    public bool GetNowConnect()
    {
        return nowConnect;
    }
}