using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Compass : MonoBehaviour
{

    // �v���C���[���
    [SerializeField] private Transform playerDirection;     // ���W
    private Vector3 playerForwrd;                           // ����

    // ���W
    [SerializeField] private GameObject[] warpPos;          // ���[�v�|�C���g
    [SerializeField] private GameObject targetWarpPoint;    // �^�[�Q�b�g���[�v�|�C���g

    // UI
    private GameObject[] directionText = new GameObject[4];  // ���p
    public GameObject warpPointText;                        // ���[�v�|�C���g

    [SerializeField] private float compassRange; // �\���p�x
    [SerializeField] private float textOffset;

    // Start is called before the first frame update
    void Start()
    {
        targetWarpPoint = warpPos[0];
        directionText[0] = GameObject.Find("North");
        directionText[1] = GameObject.Find("East");
        directionText[2] = GameObject.Find("South");
        directionText[3] = GameObject.Find("West");
        warpPointText = GameObject.Find("Warp");
    }

    // Update is called once per frame
    void Update()
    {
        playerForwrd = playerDirection.forward;
        playerForwrd.y = 0;

        PlayerDirection(playerForwrd);                  // ���ʏ���
        if (targetWarpPoint != null)
        {
            WarpPoint(playerDirection.transform.position);  // ���[�v�|�C���g�A�C�R������
        }

    }

    // ���ʏ���
    private void PlayerDirection(Vector3 playerForw)
    {
        Vector3[] posDelta = {  playerForw + Vector3.forward - playerForw , // �k
                                playerForw + Vector3.right - playerForw ,   // ��
                                playerForw + Vector3.back - playerForw ,    // ��
                                playerForw + Vector3.left - playerForw , }; // ��

        for (int i =0; i < 4; i++)
        {
            float angle = Vector3.Angle(playerForw, posDelta[i]);       // �p�x�v�Z
            Vector3 axis = Vector3.Cross(playerForw, posDelta[i]);      // ���l����������

            // UI���W�ݒ�
            if (axis.y <= 0) directionText[i].transform.localPosition = new Vector3(angle * -7 + textOffset, 0, 0);
            else�@directionText[i].transform.localPosition = new Vector3(angle * 7 + textOffset, 0, 0);

            // �\����\��
            if (angle <= compassRange) directionText[i].SetActive(true);
            else directionText[i].SetActive(false);
        }
    }

    // ���[�v�|�C���g�A�C�R������ 
    private void WarpPoint(Vector3 playerPos)
    {
        Vector3 posDelta = targetWarpPoint.transform.position - playerPos;

        // ��ԋ߂����[�v�|�C���g��T��
        foreach (GameObject obj in warpPos)
        {
            Vector3 posDelta1 = obj.transform.position - playerPos; // 2�̋����𑪂�
            float targetDistance = Vector3.SqrMagnitude(posDelta);
            float distance = Vector3.SqrMagnitude(posDelta1);
//            Debug.Log(targetDistance + " : " + distance);

            if (distance < targetDistance)                          // �����̔�r�E����
            {
                targetWarpPoint = obj;
                posDelta = obj.transform.position - playerPos;
            }
        }

        float angle = Vector3.Angle(playerForwrd, posDelta);        // �p�x�v�Z
        Vector3 axis = Vector3.Cross(playerForwrd, posDelta);       // ���l����������

        if (angle <= compassRange) warpPointText.SetActive(true);   // �\����\��
        else warpPointText.SetActive(false);

        // UI���W�ݒ�
        if (axis.y <= 0) warpPointText.transform.localPosition = new Vector3(angle * -7 + textOffset, 0, 0);
        else warpPointText.transform.localPosition = new Vector3(angle * 7 + textOffset, 0, 0);
    }

}
