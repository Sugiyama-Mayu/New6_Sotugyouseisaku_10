using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSaveCol : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ConnectionFile connectionFile;
    [SerializeField] private float countTime;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        connectionFile = GameObject.Find("Connection").GetComponent<ConnectionFile>();
        countTime = 0;
    }

    private void LateUpdate()
    {
        if (countTime < 20)
        {
            countTime += Time.deltaTime;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && 20 <= countTime )
        {
            MaterialUpdete();
            countTime = 0;
        }
    }

    // 素材書き込み処理
    private void MaterialUpdete()
    {
        Debug.Log("書き込み中・・・");
        int[] materialNum = gameManager.GetSetMaterial;
        connectionFile.SetAllMaterialNum(true, materialNum[0], materialNum[1], materialNum[2], materialNum[3], materialNum[4], materialNum[5], materialNum[6], materialNum[7]);
        gameManager.GetSetMaterial = null;
    }

    private void DragItemUpdate()
    {
        int[] materialNum = gameManager.GetSetDragItem;
       // connectionFile.SetAllMaterialNum(true, materialNum[0], materialNum[1], materialNum[2], materialNum[3], materialNum[4]);
    }

}
