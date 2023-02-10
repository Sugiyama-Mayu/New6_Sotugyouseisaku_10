using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainsWarp : MonoBehaviour
{
    // 親
    [SerializeField] public Transform[] remainsPoint;
    [SerializeField] private GameObject[] remainsObj;
    [SerializeField] public Transform inRemainsPoint; 

    [SerializeField] public int inRemainsNum; // 入ってきた祠を保存
    [SerializeField] public bool inRemains; // 祠に入ったか

    // 祠表示
    public void SettingRemains(int i)
    {
        inRemainsNum = i;
        inRemains = true;

       // remainsObj[i].SetActive(true);
    }

    // 祠非表示
    public void ResetRemains()
    {
        /*  foreach(GameObject Obj in remainsObj)
          {
              Obj.SetActive(false);
          }*/
        Debug.Log("フィールドに移動(" + remainsPoint[inRemainsNum].name + ")");

        inRemainsNum = 10;
        inRemains = false;

    }
}
