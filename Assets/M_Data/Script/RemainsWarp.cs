using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainsWarp : MonoBehaviour
{
    // �e
    [SerializeField] public Transform[] remainsPoint;
    [SerializeField] private GameObject[] remainsObj;
    [SerializeField] public Transform inRemainsPoint; 

    [SerializeField] public int inRemainsNum; // �����Ă����K��ۑ�
    [SerializeField] public bool inRemains; // �K�ɓ�������

    // �K�\��
    public void SettingRemains(int i)
    {
        inRemainsNum = i;
        inRemains = true;

       // remainsObj[i].SetActive(true);
    }

    // �K��\��
    public void ResetRemains()
    {
        /*  foreach(GameObject Obj in remainsObj)
          {
              Obj.SetActive(false);
          }*/
        Debug.Log("�t�B�[���h�Ɉړ�(" + remainsPoint[inRemainsNum].name + ")");

        inRemainsNum = 10;
        inRemains = false;

    }
}
