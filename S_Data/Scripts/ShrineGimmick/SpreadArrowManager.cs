using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// ��̉��ď����̕ϐ��Ǘ�
public class SpreadArrowManager : MonoBehaviour
{
    public int doneOver;              // list�ɒǉ������I�u�W�F�N�g�̐�
    public int doneEffect;            // �����������G�t�F�N�g�̐�
    public List<GameObject> overObj = new List<GameObject>();  // list�^
    public List<ParticleSystem> effectObj = new List<ParticleSystem>();  // list�^(�G�t�F�N�g)
    public int overLapColl;                // ���Ă�����I�u�W�F�N�g(�d�Ȃ��Ă������)�̐�
    public float waitEffectTime;           // �G�t�F�N�g�̔����ҋ@����
    public float waitEffectTimeCopy;       // �G�t�F�N�g�̔����ҋ@���Ԃ̃R�s�[
    public GameObject collArrow;    // ����������
    public GameObject collObj;
    public bool CollFlag;
    public int destroyNum;
    public bool firstCollFlag;
    void Start()
    {
        destroyNum = 0;
        doneOver = 0;              // list�ɒǉ������I�u�W�F�N�g�̐�
        doneEffect = 0;            // �����������G�t�F�N�g�̐�
        waitEffectTimeCopy = waitEffectTime;
        firstCollFlag = false;
    }
}
